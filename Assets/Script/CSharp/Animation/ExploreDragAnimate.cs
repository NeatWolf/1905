using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class ExploreDragAnimate : MonoBehaviour, IDragHandler, IBeginDragHandler,
IEndDragHandler
{
    [HideInInspector]
    public Vector3 offset;
    Vector3 worldPos;
    Camera mainCam;
    public ExploreAnimate ea;
    Button[] sceneBtns;
    [Header("滑动惯行")]
    public float inertia;
    GameObject btnMask;
    


    private void Awake()
    {
        ea=GameObject.Find("UI/ExploreUI/Canvas").GetComponent<ExploreAnimate>();
        mainCam = GameObject.Find("Explore/Main Camera").GetComponent<Camera>();
        sceneBtns = GameObject.Find("Explore/CityPlane/Canvas").GetComponent<UISubObject>().buttons;
        btnMask=GameObject.Find("UI/ExploreUI/DrapCanvas/BtnMask");


    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        ea.isDarp = true;
        

    }
   

    //场景正在向前运动或向后运动，1：向前，-1：向后
    int forwardORback;
   
    /// <summary>
    /// 屏幕滑动
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {

        //     if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position
        //  , eventData.pressEventCamera, out worldPos))
        //     {
        //         worldPos=new Vector3(worldPos.x,worldPos.y-1000,worldPos.z);
        //         offset = transform.position - worldPos;

        //         Debug.Log("offset"+offset);
        //         mainCam.transform.Translate(0,0,-(offset.y-1000)*0.01f,Space.World);
        //     }
        
        
        
        
        float k = offset.y;
        offset = eventData.pressEventCamera.ScreenToWorldPoint(eventData.position - transform.GetComponent<RectTransform>().offsetMin);


        if (k > offset.y && mainCam.transform.localPosition.z < 10f)
        {
            forwardORback = 1;
            mainCam.transform.Translate(0, 0, offset.y * 0.001f, Space.World);


        }
        else if (k < offset.y && mainCam.transform.localPosition.z > -25f)
        {
            forwardORback = -1;
            mainCam.transform.Translate(0, 0, -offset.y * 0.001f, Space.World);
        }

    
   
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {   
        
        //激活btnMask
        StartCoroutine(btnMaskTrigger());

        //惯行滑动  第一个外部if中包含滑动过程中触壁的反馈机制
        if (mainCam.transform.localPosition.z > -21 && mainCam.transform.localPosition.z < -2)
        {
            
            if (forwardORback == 1)
            {
               
                mainCam.transform.DOLocalMoveZ(mainCam.transform.localPosition.z + inertia, 1).SetEase(Ease.OutQuad).OnComplete(() =>
               {
                   
                   if (mainCam.transform.localPosition.z < -21)
                   {
                       Tweener t = mainCam.transform.DOLocalMoveZ(-21, 1);
                       t.OnComplete(() =>
                     {
                         ea.isDarp = false;

                     });

                   }
                   else if (mainCam.transform.localPosition.z > -2)
                   {
                       Tweener t = mainCam.transform.DOLocalMoveZ(-2, 1);
                       t.OnComplete(() =>
                     {
                         ea.isDarp = false;
                         if (mainCam.transform.localPosition.z < -21)
                         {
                             Tweener t1 = mainCam.transform.DOLocalMoveZ(-21, 1);
                             t.OnComplete(() =>
                           {
                               ea.isDarp = false;

                           });

                         }
                         else if (mainCam.transform.localPosition.z > -2)
                         {
                             Tweener t1 = mainCam.transform.DOLocalMoveZ(-2, 1);
                             t.OnComplete(() =>
                           {
                               ea.isDarp = false;

                           });

                         }
                         else
                         {
                             ea.isDarp = false;
                         }

                     });

                   }
                   else
                   {
                       ea.isDarp = false;
                   }

               });
                
            }
            else if (forwardORback == -1)
            {
                mainCam.transform.DOLocalMoveZ(mainCam.transform.localPosition.z - inertia, 1).SetEase(Ease.OutQuad).OnComplete(() =>
              {
                  

                  
                  if (mainCam.transform.localPosition.z < -21)
                  {
                      Tweener t = mainCam.transform.DOLocalMoveZ(-21, 1);
                      t.OnComplete(() =>
                    {
                        ea.isDarp = false;

                    });

                  }
                  else if (mainCam.transform.localPosition.z > -2)
                  {
                      Tweener t = mainCam.transform.DOLocalMoveZ(-2, 1);
                      t.OnComplete(() =>
                    {
                        ea.isDarp = false;
                        if (mainCam.transform.localPosition.z < -21)
                        {
                            Tweener t1 = mainCam.transform.DOLocalMoveZ(-21, 1);
                            t.OnComplete(() =>
                          {
                              ea.isDarp = false;

                          });

                        }
                        else if (mainCam.transform.localPosition.z > -2)
                        {
                            Tweener t1 = mainCam.transform.DOLocalMoveZ(-2, 1);
                            t.OnComplete(() =>
                          {
                              ea.isDarp = false;

                          });

                        }
                        else
                        {
                            ea.isDarp = false;
                        }

                    });

                  }
                  else
                  {
                      ea.isDarp = false;
                  }

              });
            
            }
        
        }
                //分割
        else if (mainCam.transform.localPosition.z < -21)
        {
            Tweener t = mainCam.transform.DOLocalMoveZ(-21, 1);
            t.OnComplete(() =>
            {
                ea.isDarp = false;

            });

        }
        else if (mainCam.transform.localPosition.z > -2)
        {
            Tweener t = mainCam.transform.DOLocalMoveZ(-2, 1);
            t.OnComplete(() =>
            {
                ea.isDarp = false;

            });

        }
        else
        {
            ea.isDarp = false;
        }


    }
    Vector3 mouseOffset;
    Tweener bt0;
   

    IEnumerator btnMaskTrigger(){
        btnMask.SetActive(true);  
        yield return new WaitForSeconds(1);
        btnMask.SetActive(false);
        
    }
}
