using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ItemClick : MonoBehaviour {

    private RectTransform SelectedObj;//选中的那个物体

    private RectTransform bg;//选中bg

    private RectTransform three;//left right text_name

    private int initSelectIdx = 3;//初始化选中的index

    public float bigger = 190;//大格子长度

    private float littler = 100f;//小格子长度

    private Text text ;//icon名字

    private Image left;//左三角

    private Image right;//右三角

    UGUICenterOnChild instance;

    /// <summary>
    /// 五个面板的相关数据
    /// </summary>
    private ScrollRect scroll;
    void Awake()
    {
        instance = transform.Find("ActionsNews").GetComponent<UGUICenterOnChild>();
        littler = (640 - bigger) / 4;
        bg = transform.Find("bg").GetComponent<RectTransform>();//给bg赋值
        three = transform.Find("three").GetComponent<RectTransform>();//给three赋值
        text = three.Find("Text").GetComponent<Text>();//获取bg下的文字
        left = three.Find("left").GetComponent<Image>();//获取bg下的三角
        right = three.Find("right").GetComponent<Image>();
        bg.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bigger);
        for (int i = 1; i <= 5; i++)//循环遍历5个item
        {
            RectTransform item = transform.Find("Image/item" + i).GetComponent<RectTransform>();//获取item
            if (i == initSelectIdx)//如果当前的item和初始化的匹配
            {
                SelectedObj = item;//这个物体就成为了选中的那个物体
                SelectedObj.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bigger);//根据当前的锚点设置宽度为213
                bg.SetParent(SelectedObj.transform, true);//设置bg的父物体
                bg.anchoredPosition = Vector3.zero;//设置bg到正确的位置
                three.SetParent(SelectedObj.transform, true);//设置three的父物体
                three.anchoredPosition = Vector3.zero;//设置three到正确的位置
                int index = int.Parse(SelectedObj.name.Substring(4, 1));//当前选中物体的index
                instance.SetCurrentPageIndex(index - 1, false);
            }
            else
            {
                item.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, littler);//根据当前的锚点设置宽度
            }
            item.GetComponent<Button>().onClick.AddListener(() => { OnItemClick(item); });//注册事件

        }
    }


    public  void OnItemClick(RectTransform clickItem)
    {
        if (SelectedObj != clickItem)//如果当前点击的物体不是选中的那个物体，才执行以下操作
        {
            int idx = int.Parse(clickItem.name.Substring(4, 1));

            //滚动到对应屏幕
            instance.SetCurrentPageIndex(idx-1);
            //复原SelectedObj
            SelectedObj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, littler);
            RectTransform selectImageOld = SelectedObj.transform.Find("Image").GetComponent<RectTransform>();
            selectImageOld.DOAnchorPos(new Vector3(0f, 5f, 0f), 0.3f);
            selectImageOld.DOScale(1f, 0.3f);
            //新设置clickItem
            clickItem.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bigger);
            SelectedObj = clickItem;

            bg.SetParent(SelectedObj.transform, true);//设置bg的父物体
            three.SetParent(SelectedObj.transform, true);//设置three的父物体

            //播放动画前不显示
            text.DOFade(0f, 0.2f);
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);

            //three播放移动动画
            three.DOAnchorPos(Vector3.zero, 0.2f).OnComplete(() => {
                text.text = GetName(SelectedObj.name);//文字显示
                text.DOFade(1f, 0.2f);
                if (!SelectedObj.name.Equals("item1"))
                {
                    left.gameObject.SetActive(true);//left right三角显示
                }
                if (!SelectedObj.name.Equals("item5"))
                {
                    right.gameObject.SetActive(true);
                }
            });
            //bg.DOAnchorPos(Vector3.zero, 0.5f);//播放移动动画
            bg.DOLocalMove(Vector3.zero, 0.5f);

            RectTransform selectImageNew = SelectedObj.transform.Find("Image").GetComponent<RectTransform>();//icon图标移动放大
            //selectImageNew.DOAnchorPos(new Vector3(0f, 30f, 0f), 0.4f);
            selectImageNew.DOLocalMove(new Vector3(0f, 30f, 0f), 0.4f);
            selectImageNew.DOScale(1.2f,0.4f);
        }
    }
    public string  GetName(string ItemName)
    {
        string name = "";
        switch (ItemName)
        {
            case "item1":
                name = "商店";
                break;
            case "item2":
                name = "卡牌";
                break;
            case "item3":
                name = "对战";
                break;
            case "item4":
                name = "社交";
                break;
            case "item5":
                name = "活动";
                break;
            default:
                Debug.Log("没返回名字");
                break;
        }
        return name;
    }
}
