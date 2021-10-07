using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlaneController : MonoBehaviour
{
    public GameObject action;
    public GameObject inspectorP;
    public Toggle tg1;
    public Toggle tg2;
    public Toggle tg3;
    public GameObject sv;
    private ScrollRect scrollRect;
    public GameObject WH;

    public Text level;
    public Text userName;
   UserInfoServer userInfoServer=new UserInfoServer();

    void Start ()
    {
        
        scrollRect = sv.GetComponent<ScrollRect>();
        MainPlaneView MPV = gameObject.GetComponent<MainPlaneView>();
        MPV.ShowMyValue();
        level.text = "<b>LV."+ userInfoServer.GetUserInfo(ServerByUser.Instance().UserID).Level + "</b>";
        userName.text = "<b>" + userInfoServer.GetUserInfo(ServerByUser.Instance().UserID).UserName + "</b>";
      
    }


    #region 功能
    //活动滚动栏那里的三个活动界面切换
    //public void Toggle1()
    //{
    //    MainPlaneView MPV = gameObject.GetComponent<MainPlaneView>();
    //    MPV.ActionNewShow1();
    //}
    //public void Toggle2()
    //{
    //    MainPlaneView MPV = gameObject.GetComponent<MainPlaneView>();
    //    MPV.ActionNewShow2();
    //}
    //public void Toggle3()
    //{
    //    MainPlaneView MPV = gameObject.GetComponent<MainPlaneView>();
    //    MPV.ActionNewShow3();
    //}
    //关闭公告栏
    public void CloseAnn()
    {
        MainPlaneView MPV = gameObject.GetComponent<MainPlaneView>();
        MPV.AnnClose();
    }
    //打开公告栏
    public void OpenAnn()
    {
        MainPlaneView MPV = gameObject.GetComponent<MainPlaneView>();
        MPV.AnnOpen();
    }
    //打开仓库
    public void OpenWare()
    {
        WareHouse_View WHV = WH.GetComponent<WareHouse_View>();
        WHV.OPenWareHouse();
    }
    //打开商店
    public void OpenStore()
    {
        MainPlaneView MPV = gameObject.GetComponent<MainPlaneView>();
        MPV.StoreOpen();
        MPV.ShowMyValueInStore();
        ShopView sv = gameObject.GetComponent<ShopView>();
        sv.ShowItemsList();
    }
    public void CloseStore()
    {
        ShopView sv = gameObject.GetComponent<ShopView>();
        sv.CleanAllItems();
        MainPlaneView MPV = gameObject.GetComponent<MainPlaneView>();
        MPV.StoreClose();
        MPV.ShowMyValue();
    }
    //打开任务展示板   眼睛那个按钮
    public void OpenInspector()
    {
        inspectorP.SetActive(true);
    }
    public void CloseInspector()
    {
        inspectorP.SetActive(false);
    }
    //出击面板开关
    public void OpenAction()
    {
        action.SetActive(true);
    }
    public void CloseAction()
    {
        action.SetActive(false);
    }
    //打开船坞
    public void OpenDock()
    {
        MainPlaneView MPV = gameObject.GetComponent<MainPlaneView>();
        MPV.ShowDockPlane();
    }
    public void CloseDock()
    {
        MainPlaneView MPV = gameObject.GetComponent<MainPlaneView>();
        MPV.CloseDockPlane();
    }
    #endregion
}
