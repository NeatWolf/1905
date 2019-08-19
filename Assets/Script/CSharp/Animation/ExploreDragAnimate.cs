using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class ExploreDragAnimate : MonoBehaviour, IDragHandler, IBeginDragHandler,
IEndDragHandler, IPointerClickHandler, IPointerExitHandler, IPointerUpHandler
{
    [HideInInspector]
    public Vector3 offset;
    Vector3 worldPos;
    Camera mainCam;
    public ExploreAnimate ea;
    Button[] sceneBtns;
    [Header("滑动惯行")]
    public float inertia;
    


    private void Awake()
    {

        mainCam = GameObject.Find("Explore/Main Camera").GetComponent<Camera>();
        sceneBtns = GameObject.Find("Explore/CityPlane/Canvas").GetComponent<UISubObject>().buttons;


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
    public void OnPointerClick(PointerEventData eventData)
    {
        //方法一

        // Debug.Log("eventData.pressPosition"+eventData.pressPosition);
        // //btnCamoffset--第一个button与场景中的mainCam的Z轴差值
        // float btnCamoffset = GameObject.Find("Scene/Explore/CityPlane/Canvas").GetComponent<UISubObject>().buttons[0].transform.position.z - mainCam.transform.position.z;
        // Debug.Log("button" + btnCamoffset);
        // if(btnCamoffset>9.3f&&eventData.pressPosition.y<1100&&eventData.pressPosition.y>578){
        //     sceneBtns[0].transform.DOScale(2,0.5f);
        // }
        // else if((btnCamoffset>9.3f &&eventData.pressPosition.y>1100&&eventData.pressPosition.y<595)||(btnCamoffset>-1.74f&& btnCamoffset<8.8f&&eventData.pressPosition.y>500)){
        //     sceneBtns[1].transform.DOScale(2,0.5f);
        // }

        // if (btnCamoffset>9.3f&&eventData.pressPosition.y>1100||eventData.pressPosition.y<578)
        // {
        //     sceneBtns[0].transform.DOScale(1,0.5f);
        // }

        //方法二



    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
