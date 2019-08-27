using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using DG.Tweening;

public class ExploreInFindControler : MonoBehaviour
{
    [Header("怪物出生点")]
    /// <summary>
    /// 怪物出生点
    /// </summary>
    public GameObject[] MonsterSpawnPos = new GameObject[18];
    [Header("怪物")]
    /// <summary>
    /// 怪物
    /// </summary>
    public GameObject[] mapMonster = new GameObject[8];
    [Header("地图编号，依靠此数值决定生成怪物的数值")]
    /// <summary>
    /// 地图编号，依靠此数值决定生成怪物的数值
    /// </summary>
    public int MapNumber = 0;
    NavMeshAgent mAgent;
    GameObject hero, monster, camera, relaCamera;
    UnityArmatureComponent uac;
    /// <summary>
    /// 是否在战斗状态
    /// </summary>
    public bool isATK = false;

    [Header("选定的怪物")]
    /// <summary>
    /// 选定的怪物
    /// </summary>
    public GameObject CurrentMonster;
    [Header("寻路完成后调用的方法")]
    public UnityAction FindWayDone;
    [Header("战斗中禁止寻路警告")]
    public UnityAction WarringATKFindWay;
    /// <summary>
    /// 遇一级怪（小怪）委托
    /// </summary>
    public UnityAction LevelOneMonster;
    /// <summary>
    /// 遇二级怪（精英怪）委托
    /// </summary>
    public UnityAction LevelTwoMonster;
    /// <summary>
    /// 遇三级怪委托
    /// </summary>
    public UnityAction LevelThreeMonster;
    /// <summary>
    /// 遇四级怪（boss怪）委托
    /// </summary>
    public UnityAction LevelFourMonster;


    [Header("英雄攻击距离，即寻路停止时与目标的距离")]
    /// <summary>
    /// 英雄攻击距离，即寻路停止时与目标的距离
    /// </summary>
    public int heroAtkDistance = 2;
    Vector3 targetPos;
    /// <summary>
    /// 传入英雄数组
    /// </summary>
    public GameObject[] heroTroop = new GameObject[4];
    UnityArmatureComponent[] herosUAC = new UnityArmatureComponent[4];
    NavMeshAgent[] heroAgent = new NavMeshAgent[4];
    bool isIdle = true;
    //是否键盘控制相机移动
    bool isKey;
    //键盘控制相机移动类型
    int cameraMoveType;
    //相机是否跟随英雄
    bool cameraTrack = true;

    /// <summary>
    /// 复制出的怪物，int为地图编号，list为怪物
    /// </summary>
    Dictionary<int, List<GameObject>> monsterDic = new Dictionary<int, List<GameObject>>();
    /// <summary>
    /// 怪物池
    /// </summary>
    public GameObject monsterPool;
    private void Awake()
    {
        
        monster = GetComponent<UISubObject>().go[0];
        camera = GetComponent<UISubObject>().go[2];
        relaCamera = GetComponent<UISubObject>().go[2].transform.GetChild(0).gameObject;

        for (int i = 0; i < heroTroop.Length; i++)
        {
            herosUAC[i] = heroTroop[i].GetComponent<UnityArmatureComponent>();
            heroAgent[i] = heroTroop[i].GetComponent<NavMeshAgent>();
        }

        //根据地图编号分配怪物出生点

        SpawnMonster(MapNumber);

        Quaternion quaternion = Quaternion.LookRotation((heroTroop[0].transform.position - camera.transform.GetChild(0).transform.position));
        camera.transform.GetChild(0).transform.rotation = Quaternion.Lerp(camera.transform.GetChild(0).transform.rotation, quaternion, 0.5f);

    }

    private void Update()
    {

        //英雄看向相机
        for (int i = 0; i < heroTroop.Length; i++)
        {
            herosUAC[i].transform.LookAt(camera.transform.GetChild(0).transform.position);
        }


        //点击地面后的寻路
        if (Input.GetMouseButton(0))
        {
            // Debug.Log("鼠标触发");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + Vector3.forward * 100);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("ExploreMap")) && isATK == false)
            {
                cameraTrack = true;
                targetPos = hitInfo.point;
                isIdle = false;
                for (int i = 0; i < heroTroop.Length; i++)
                {
                    // if(targetPos.x>heroTroop[i].transform.position.x){
                    //     heroTroop[i].transform.DORotate(new Vector3(0,180,0),0f);
                    // }
                    // if(targetPos.x<heroTroop[i].transform.position.x){
                    //     heroTroop[i].transform.DORotate(new Vector3(0,0,0),0f);
                    // }
                    heroAgent[i].SetDestination(targetPos);
                    heroAgent[i].stoppingDistance = heroAtkDistance;
                    herosUAC[i].animation.Play("run");

                }

            }
            //战斗中点击地面寻路
            else if (Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("ExploreMap")) && isATK == true)
            {
                //调用禁止寻路警告方法
                if (WarringATKFindWay == null) return;
                WarringATKFindWay();
            }
        }
        //点击怪物后的寻路
        if (Input.GetMouseButton(0))
        {
            // Debug.Log("鼠标触发");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + Vector3.forward * 100);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("Monster")))
            {
                CurrentMonster = hitInfo.transform.gameObject;
                cameraTrack = true;
                targetPos = hitInfo.transform.position;
                isIdle = false;
                for (int i = 0; i < heroTroop.Length; i++)
                {

                    heroAgent[i].SetDestination(targetPos);
                    heroAgent[i].stoppingDistance = heroAtkDistance;
                    herosUAC[i].animation.Play("run");

                }

            }
        }

        //判断英雄是否应该停止寻路
        for (int i = 0; i < heroTroop.Length; i++)
        {

            if (Vector3.Distance(heroTroop[i].transform.position, targetPos) <= heroAtkDistance)
            {
                // Debug.Log("播放idle动画");
               // isIdle = true;
                herosUAC[i].animation.Play("idle");
                //调用遇怪方法
                if (CurrentMonster != null)
                {
                    if (FindWayDone != null)
                    {
                        FindWayDone();
                    }
                    if (CurrentMonster.name == "01" || CurrentMonster.name == "02")
                    {
                        if (LevelOneMonster == null) return;
                        LevelOneMonster();
                    }
                    else if (CurrentMonster.name == "03" || CurrentMonster.name == "04")
                    {
                        if (LevelTwoMonster == null) return;
                        LevelTwoMonster();
                    }
                    else if (CurrentMonster.name == "05" || CurrentMonster.name == "06")
                    {
                        if (LevelThreeMonster == null) return;
                        LevelThreeMonster();
                    }
                    else if (CurrentMonster.name == "07" || CurrentMonster.name == "08")
                    {
                        if (LevelFourMonster == null) return;
                        LevelFourMonster();
                    }


                }

            }

        }


        //怪物看向摄像机
        if (monsterDic.ContainsKey(MapNumber))
        {
            for (int i = 0; i < 18; i++)
            {

                monsterDic[MapNumber][i].transform.LookAt(relaCamera.transform.position);
            }
        }




    }
    
    //相机跟随
    private void FixedUpdate() {
        if ((cameraTrack && heroTroop[0] != null))
        {

            // Quaternion quaternion = Quaternion.LookRotation((heroTroop[0].transform.position - camera.transform.GetChild(0).transform.position));
            // relaCamera.transform.rotation = Quaternion.Lerp(camera.transform.GetChild(0).transform.rotation, quaternion, 0.5f);
            // camera.transform.GetChild(0).transform.position = new Vector3(heroTroop[0].transform.position.x + (relaCamera.transform.position.x - heroTroop[0].transform.position.x),heroTroop[0].transform.position.y + (relaCamera.transform.position.y - heroTroop[0].transform.position.y), camera.transform.GetChild(0).transform.position.z);
            camera.transform.localPosition =heroTroop[0].transform.position;
        }

    }


    //相机跟随
    private void LateUpdate()
    {
        if (!cameraTrack && Input.GetKeyDown(KeyCode.Space))
        {
            cameraTrack = true;
        }

        



        CameraController();
        //键盘控制相机移动
        if (isKey)
        {
            switch (cameraMoveType)
            {
                case 1:

                    CameraMove( Input.GetAxis("Vertical") * 0.1f,0,0);
                    break;
                case 2:

                    CameraMove( 0, 0,-Input.GetAxis("Horizontal") * 0.1f);
                    break;

            }
        }



    }
    private void OnDisable()
    {
        //怪物失活

        for (int j = 0; j < 18; j++)
        {
            if (!monsterDic.ContainsKey(MapNumber)||monsterDic[MapNumber][j].activeSelf==false) return;
            monsterDic[MapNumber][j].SetActive(false);
        }


    }

    //WASD控制相机移动
    void CameraController()
    {

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            isKey = true;
            cameraTrack = false;
            cameraMoveType = 1;
        }

        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            isKey = true;
            cameraTrack = false;
            cameraMoveType = 2;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isKey = false;
        }

    }
    void CameraMove(float x, float y, float z)
    {
        camera.transform.Translate(new Vector3(x, y, z), Space.Self);
    }

    /// <summary>
    /// 怪物生成
    /// </summary>
    void SpawnMonster(int MapNumber)
    {
        if (MapNumber != 0)
        {
            switch (MapNumber)
            {
                case 1:
                    if (monsterDic.ContainsKey(1))
                    {
                        for (int i = 0; i < 18; i++)
                        {
                            monsterDic[4][i].SetActive(true);

                        }
                        return;
                    }
                    List<GameObject> list1 = new List<GameObject>();
                    for (int i = 0; i < 7; i++)
                    {
                        GameObject go = Instantiate(mapMonster[0], MonsterSpawnPos[i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        GameObject go1 = Instantiate(mapMonster[1], MonsterSpawnPos[7 + i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));

                        go.name = "01";
                        go1.name = "02";
                        go.SetActive(true);
                        go1.SetActive(true);
                        list1.Add(go);
                        list1.Add(go1);

                    }
                    for (int i = 14; i < 18; i++)
                    {
                        GameObject go = Instantiate(mapMonster[2], MonsterSpawnPos[i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        go.name = "03";
                        go.SetActive(true);
                        list1.Add(go);
                    }
                    //设怪物池为父物体
                    for (int i = 14; i < 18; i++){
                        list1[i].transform.SetParent(monsterPool.transform);
                    }
                    //加入字典
                    monsterDic.Add(1, list1);
                    break;

                case 2:
                    if (monsterDic.ContainsKey(2))
                    {
                        for (int i = 0; i < 18; i++)
                        {
                            monsterDic[4][i].SetActive(true);

                        }
                        return;
                    }
                    List<GameObject> list2 = new List<GameObject>();
                    for (int i = 0; i < 7; i++)
                    {
                        GameObject go = Instantiate(mapMonster[2], MonsterSpawnPos[i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        GameObject go1 = Instantiate(mapMonster[3], MonsterSpawnPos[7 + i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        go.name = "03";
                        go1.name = "04";
                        go.SetActive(true);
                        go1.SetActive(true);
                        list2.Add(go);
                        list2.Add(go1);
                    }
                    for (int i = 14; i < 18; i++)
                    {
                        GameObject go = Instantiate(mapMonster[4], MonsterSpawnPos[i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        go.name = "05";
                        go.SetActive(true);
                        list2.Add(go);
                    }
                    //设怪物池为父物体
                    for (int i = 14; i < 18; i++){
                        list2[i].transform.SetParent(monsterPool.transform);
                    }
                    monsterDic.Add(1, list2);
                    break;
                case 3:
                    if (monsterDic.ContainsKey(3))
                    {
                        for (int i = 0; i < 18; i++)
                        {
                            monsterDic[4][i].SetActive(true);

                        }
                        return;
                    }
                    List<GameObject> list3 = new List<GameObject>();
                    for (int i = 0; i < 7; i++)
                    {
                        GameObject go = Instantiate(mapMonster[4], MonsterSpawnPos[i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        GameObject go1 = Instantiate(mapMonster[5], MonsterSpawnPos[7 + i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        go.name = "05";
                        go1.name = "06";
                        go.SetActive(true);
                        go1.SetActive(true);
                        list3.Add(go);
                        list3.Add(go1);

                    }
                    for (int i = 14; i < 18; i++)
                    {
                        GameObject go = Instantiate(mapMonster[6], MonsterSpawnPos[i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        go.name = "07";
                        go.SetActive(true);
                        list3.Add(go);
                    }
                    //设怪物池为父物体
                    for (int i = 14; i < 18; i++){
                        list3[i].transform.SetParent(monsterPool.transform);
                    }
                    monsterDic.Add(3, list3);
                    break;
                case 4:
                    if (monsterDic.ContainsKey(4))
                    {
                        for (int i = 0; i < 18; i++)
                        {
                            monsterDic[4][i].SetActive(true);

                        }
                        return;
                    }

                    List<GameObject> list4 = new List<GameObject>();
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject go = Instantiate(mapMonster[4], MonsterSpawnPos[i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        GameObject go1 = Instantiate(mapMonster[5], MonsterSpawnPos[5 + i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        go.SetActive(true);
                        go1.SetActive(true);


                        go.name = "05";
                        go1.name = "06";
                        list4.Add(go);
                        list4.Add(go1);

                    }

                    for (int i = 10; i < 18; i++)
                    {
                        GameObject go = Instantiate(mapMonster[7], MonsterSpawnPos[i].transform.position, Quaternion.LookRotation(relaCamera.transform.position - transform.position));
                        go.name = "08";
                        go.SetActive(true);
                        list4.Add(go);
                    }
                    //设怪物池为父物体
                    for (int i = 14; i < 18; i++){
                        list4[i].transform.SetParent(monsterPool.transform);
                    }
                    monsterDic.Add(4, list4);
                    break;
            }
        }
    }




}
