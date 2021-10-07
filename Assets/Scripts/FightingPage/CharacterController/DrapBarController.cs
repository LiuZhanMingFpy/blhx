using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*

    林哲

    遥杆控制脚本

    12月7日

*/

public class DrapBarController : MonoBehaviour, IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    public RectTransform Bar;
    public RectTransform bg;
    
    private float max=90;

    public void OnDrag(PointerEventData eventData)
    {

        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(bg as RectTransform, eventData.position, eventData.pressEventCamera, out pos);

        if (pos.magnitude> max)
        {
            pos = pos.normalized * max;
        }

        //Debug.Log("2");

        Bar.localPosition = new Vector3(pos.x, pos.y, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        //Vector2 pos = Vector2.zero;

        //RectTransformUtility.ScreenPointToLocalPointInRectangle(Bar as RectTransform,eventData.position,eventData.pressEventCamera,out pos);

        //Bar.transform.localPosition = new Vector3(pos.x,pos.y,0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Bar.localPosition = Vector2.zero;
    }
   
}
