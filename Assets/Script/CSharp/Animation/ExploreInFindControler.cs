using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ExploreInFindControler : MonoBehaviour
{
    NavMeshAgent mAgent;
    GameObject hero, monster, camera;
    UnityArmatureComponent uac;
    
    [Header("寻路完成后调用的方法")]
    public UnityAction FindWayDone;


    //英雄攻击距离，即寻路停止时与目标的距离
    public int heroAtkDistance = 2;
    Vector3 targetPos;
    /// <summary>
    /// 传入英雄数组
    /// </summary>
    public GameObject[] heroTroop = new GameObject[4];
    UnityArmatureComponent[] herosUAC = new UnityArmatureComponent[4];
    NavMeshAgent[] heroAgent = new NavMeshAgent[4];
    private void Awake()
    {

        monster = GetComponent<UISubObject>().go[0];
        camera = GetComponent<UISubObject>().go[2];


        for (int i = 0; i < heroTroop.Length; i++)
        {
            herosUAC[i] = heroTroop[i].GetComponent<UnityArmatureComponent>();
        }
        for (int i = 0; i < heroTroop.Length; i++)
        {
            heroAgent[i] = heroTroop[i].GetComponent<NavMeshAgent>();
        }

    }

    private void Update()
    {
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
            if (Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("ExploreMap")))
            {
                Debug.Log("鼠标触发ExploreMap");
                targetPos = hitInfo.point;
                StartCoroutine(FindWayMany(herosUAC, hitInfo.point));
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
                
                targetPos = hitInfo.transform.position;
                StartCoroutine(FindWayMany(herosUAC, hitInfo.transform.position));
            }
        }
        //切换动画idle
        StartCoroutine(FindWayAnimate(herosUAC, targetPos));
    }

    //相机跟随
    private void LateUpdate()
    {


        for (int i = 0; i < heroTroop.Length; i++)
        {
            Quaternion quaternion = Quaternion.LookRotation((heroTroop[i].transform.position - camera.transform.GetChild(0).transform.position));
            camera.transform.GetChild(0).transform.rotation = Quaternion.Lerp(camera.transform.GetChild(0).transform.rotation, quaternion, 0.5f);

            camera.transform.GetChild(0).transform.position = new Vector3(heroTroop[i].transform.position.x + 20, camera.transform.GetChild(0).transform.position.y, camera.transform.GetChild(0).transform.position.z);
        }

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
                if (i==heroTroop.Length-1)
                {
                    //调用寻路完成后的方法
                    StartCoroutine(GoFindWayDone(FindWayDone));
                }

            }
        }

    }
    //寻路完成后调用方法
    IEnumerator GoFindWayDone(UnityAction action){
        yield return new WaitForSeconds(0.5f);
        if (FindWayDone!=null)
        {
            FindWayDone();
        }

    }
}
