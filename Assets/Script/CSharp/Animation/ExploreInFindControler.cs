using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ExploreInFindControler : MonoBehaviour
{
    NavMeshAgent mAgent;
    GameObject hero, monster, camera, relaCamera;
    UnityArmatureComponent uac;
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


    //英雄攻击距离，即寻路停止时与目标的距离
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
            Debug.Log("鼠标触发");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + Vector3.forward * 100);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("ExploreMap")) && isATK == false)
            {
                cameraTrack = true;
                targetPos = hitInfo.point;
                isIdle = false;
                for (int i = 0; i < heroTroop.Length; i++)
                {

                    heroAgent[i].SetDestination(targetPos);
                    heroAgent[i].stoppingDistance = heroAtkDistance;

                    herosUAC[i].animation.Play("run");

                }

            }
            //战斗中点击地面寻路
            else if (Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("ExploreMap")) && isATK == true)
            {
                //调用禁止寻路警告方法
                if(WarringATKFindWay==null) return;
                WarringATKFindWay();
            }
        }
        //点击怪物后的寻路
        if (Input.GetMouseButton(0))
        {
            Debug.Log("鼠标触发");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + Vector3.forward * 100);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("Monster")))
            {
                CurrentMonster=hitInfo.transform.gameObject;
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
                isIdle = true;

                herosUAC[i].animation.Play("idle");
                //调用遇怪方法
                if(CurrentMonster!=null){
                    FindWayDone();
                }

            }
            
        }
        

        




    }
    IEnumerator heroMove()
    {
        yield return null;

        Debug.Log("run动画");

        for (int i = 0; i < heroTroop.Length; i++)
        {
            herosUAC[i].animation.Play("run");
        }

        for (int i = 0; i < heroTroop.Length; i++)
        {

            herosUAC[i].animation.FadeIn("idle", 0.5f);
        }


    }

    //相机跟随
    private void LateUpdate()
    {
        if (!cameraTrack && Input.GetKeyDown(KeyCode.Space))
        {
            cameraTrack = true;
        }

        if ((cameraTrack && heroTroop[0] != null))
        {

            Quaternion quaternion = Quaternion.LookRotation((heroTroop[0].transform.position - camera.transform.GetChild(0).transform.position));
            camera.transform.GetChild(0).transform.rotation = Quaternion.Lerp(camera.transform.GetChild(0).transform.rotation, quaternion, 0.5f);
            camera.transform.GetChild(0).transform.position = new Vector3(heroTroop[0].transform.position.x + (relaCamera.transform.position.x - heroTroop[0].transform.position.x), camera.transform.GetChild(0).transform.position.y, camera.transform.GetChild(0).transform.position.z);

        }



        CameraController();
        //键盘控制相机移动
        if (isKey)
        {
            switch (cameraMoveType)
            {
                case 1:
                    relaCamera.transform.Translate(0, Input.GetAxis("Vertical") * 0.1f, 0);
                    break;
                case 2:
                    relaCamera.transform.Translate(Input.GetAxis("Horizontal") * 0.1f, 0, 0);
                    break;

            }
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
        relaCamera.transform.Translate(new Vector3(x, y, z), Space.Self);
    }

    /// <summary>
    /// 多人寻路
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    IEnumerator FindWayMany(UnityArmatureComponent[] herosUAC, Vector3 targetPos)
    {

        for (int i = 0; i < herosUAC.Length; i++)
        {
            herosUAC[i].animation.Play("run");
            heroAgent[i].SetDestination(targetPos);
            heroAgent[i].stoppingDistance = heroAtkDistance;

        }
        yield return null;

    }

    //切换动画idle
    IEnumerator FindWayAnimate(UnityArmatureComponent[] herosUAC, Vector3 targetPos)
    {

        yield return null;

        for (int i = 0; i < herosUAC.Length; i++)
        {
            if (Vector3.Distance(heroTroop[i].transform.position, targetPos) <= heroAtkDistance)
            {
                //切换动画idle
                for (int j = 0; j < herosUAC.Length; j++)
                {
                    herosUAC[i].animation.FadeIn("idle", 0.5f);
                }
                if (i == heroTroop.Length - 1 && FindWayDone != null)
                {

                    //调用寻路完成后的方法
                    StartCoroutine(GoFindWayDone(FindWayDone));

                }

            }
        }

    }
    //寻路完成后调用方法
    IEnumerator GoFindWayDone(UnityAction action)
    {
        yield return new WaitForSeconds(0.5f);
        if (FindWayDone != null)
        {
            FindWayDone();
        }

    }
    //////////////////////////////////////////////////////
    /// <summary>
    /// 寻路
    /// </summary>
    /// <param name="targetPos"></param>
    void FindWay(Vector3 targetPos)
    {
        for (int i = 0; i < herosUAC.Length; i++)
        {


            heroAgent[i].SetDestination(targetPos);
            heroAgent[i].stoppingDistance = heroAtkDistance;

        }
    }

}
