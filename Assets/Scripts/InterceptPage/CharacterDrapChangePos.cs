using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterDrapChangePos : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Transform father;  //看该物体的爸爸
    private bool isSelect = false;
    
    void Start()
    {
        father = transform.parent;
    }
        
    [HideInInspector]
    public Vector3 selectPos;

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelect = true;

        selectPos = transform.position;

        SetPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isSelect==false)
        {
            return;
        }

        SetPosition(eventData);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSelect = false;

       // selectPos = Vector3.zero;
        transform.localPosition = Vector3.zero;

    }

    private void SetPosition(PointerEventData eventData)
    {
        Vector2 v = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform,eventData.position,eventData.pressEventCamera,out v);

        transform.localPosition = v;


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "character")
        {
            Transform temp = col.transform.parent;
            col.transform.SetParent(father);
            col.transform.localPosition = Vector3.zero;
            father = temp;
        }
    }

}
