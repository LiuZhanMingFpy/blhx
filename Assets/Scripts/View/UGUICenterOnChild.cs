using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// UGUI ScrollRect 滑动元素居中
/// </summary>
public class UGUICenterOnChild : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public float scrollSpeed = 8f;
    public Transform uUIGrid;
    private ScrollRect scrollRect;
    private float[] pageArray;
    private float targetPagePosition = 0f;
    private bool isDrag = false;
    private int pageCount;
    private int currentPage = 0;
    //public RectTransform parent;
    public Toggle tg1;
    public Toggle tg2;
    public Toggle tg3;
    void Awake()
    {
        //ItemClick itemClick = transform.parent.GetComponent<ItemClick>();
        scrollRect = GetComponent<ScrollRect>();
        //scrollRect.onValueChanged.AddListener( itemClick.OnItemClick(parent.GetChild(currentPage) as RectTransform));
        //scrollRect.onValueChanged.AddListener(
        //    (p)=> { itemClick.OnItemClick((parent.GetChild(currentPage)) as RectTransform); }//学名闭包  用匿名委托来包装一层
        //    );

        InitPageArray();     
    }

    void OnEnable()
    {
        InitPageArray();
    }
    void InitPageArray()
    {
        pageCount = uUIGrid.childCount;
        pageArray = new float[pageCount];
        for (int i = 0; i < pageCount; i++)
        {
            //0    0
            //1    1/4
            //2    2/4
            //3    3/4
            //4    1
            pageArray[i] = (1f / (pageCount - 1)) * i;
        }
    }


    void Update()
    {
        if (currentPage==0)
        {
            tg1.isOn = true;
        }
        else if(currentPage == 1)
        {
            tg2.isOn = true;
        }
        else
        {
            tg3.isOn = true;
        }

        if (!isDrag)
        {     
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetPagePosition, scrollSpeed * Time.deltaTime);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        float posX = scrollRect.horizontalNormalizedPosition;
        int index = 0;
        float offset = Math.Abs(pageArray[index] - posX);//下标为0的偏移量
        for (int i = 1; i < pageArray.Length; i++)//排序，找出当前坐标 和 哪页距离最近，移动过去
        {
            float _offset = Math.Abs(pageArray[i] - posX);
            if (_offset < offset)
            {
                index = i;
                offset = _offset;
            }
        }
        targetPagePosition = pageArray[index];
        currentPage = index;
    }
    //向左移动一个元素
    public void ToLeft()
    {
        if (currentPage > 0)
        {
            currentPage = currentPage - 1;
            targetPagePosition = pageArray[currentPage];
        }
    }
    //向右移动一个元素
    public void ToRight()
    {
        if (currentPage < pageCount - 1)
        {
            currentPage = currentPage + 1;
            targetPagePosition = pageArray[currentPage];
        }
    }

    public int GetCurrentPageIndex()//获取当前页
    {
        return currentPage;
    }
    public void SetCurrentPageIndex(int index,bool isAnim = true)
    {
        currentPage = index;
        targetPagePosition = pageArray[currentPage];
        if (isAnim == false)
        {
            scrollRect.horizontalNormalizedPosition = targetPagePosition;
        }
    }
 
    public int GetTotalPages()//获取总页数
    {
        return pageCount;
    }

    public void TG1isON(bool ison)
    {
        if (ison)
        {
           currentPage = 0;
            targetPagePosition = 0;
        }
    }
    public void TG2isON(bool ison)
    {
        if (ison)
        {
           currentPage = 1;
            targetPagePosition = 0.5f;
        }
    }
    public void TG3isON(bool ison)
    {
        if (ison)
        {
            currentPage = 2;
            targetPagePosition = 1;
        }
    }


}


