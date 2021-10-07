using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPlaneView : MonoBehaviour
{
    public Text oilCountPropertyDetail;
    public Text moneyCountPropertyDetail;
    public Text diamondCountPropertyDetail;
    public Text oilCountHeroDetail;
    public Text moneyCountHeroDetail;
    public Text diamondCountHeroDetail;
    public Text oilCount;
    public Text moneyCount;
    public Text diamondCount;
    public Text oilCountStore;
    public Text moneyCountStore;
    public Text diamondCountStore;
    public GameObject store;
    public GameObject ann;
    public GameObject content;
    public GameObject dock;

    public GameObject setValue;
	void Start ()
	{
    
       

	}
    
    #region 功能
    //3个toggle对应的3个活动图片
    public void ActionNewShow1()
    {
        RectTransform contenRectTransform = content.GetComponent<RectTransform>();
        contenRectTransform.localPosition = new Vector3(0, 6.4f, 0);
    }
    public void ActionNewShow2()
    {
        RectTransform contenRectTransform = content.GetComponent<RectTransform>();
        contenRectTransform.localPosition = new Vector3(-679.14f, 6.4f, 0);
    }
    public void ActionNewShow3()
    {
        RectTransform contenRectTransform = content.GetComponent<RectTransform>();
        contenRectTransform.localPosition = new Vector3(-1363.4f, 6.4f, 0);
    }
    //公告栏显示关闭
    public void AnnClose()
    {
        ann.SetActive(false);
    }
    //公告栏开启
    public void AnnOpen()
    {
        ann.SetActive(true);
    }
    //商店开关
    public void StoreOpen()
    {
        store.SetActive(true);
    }
    public void StoreClose()
    {
        store.SetActive(false);
    }

    //用于刷新砖石 石油 金币的显示
    public void ShowMyValue()
    {
        UserInfoServer userInfoServer = new UserInfoServer();
        UserInfo userInfo = userInfoServer.GetUserInfo(ServerByUser.Instance().UserID);
        oilCount.text = "<b>" + userInfo.Resources + "</b>";
        moneyCount.text= "<b>" + userInfo.Gold + "</b>";
        diamondCount.text= "<b>" + userInfo.Diamonds + "</b>";
    }
    public void ShowMyValueInStore()
    {
        UserInfoServer userInfoServer = new UserInfoServer();
        UserInfo userInfo = userInfoServer.GetUserInfo(ServerByUser.Instance().UserID);
        
        oilCountStore.text = "<b>" + userInfo.Resources + "</b>";
        moneyCountStore.text = "<b>" + userInfo.Gold + "</b>";
        diamondCountStore.text = "<b>" + userInfo.Diamonds + "</b>";
    }
    public void ShowMyValueInHeroDetail()
    {
        UserInfoServer userInfoServer = new UserInfoServer();
        UserInfo userInfo = userInfoServer.GetUserInfo(ServerByUser.Instance().UserID);
        oilCountHeroDetail.text = "<b>" + userInfo.Resources + "</b>";
        moneyCountHeroDetail.text = "<b>" + userInfo.Gold + "</b>";
        diamondCountHeroDetail.text = "<b>" + userInfo.Diamonds + "</b>";
    }
    public void ShowMyValueInPropertyDetail()
    {
        UserInfoServer userInfoServer = new UserInfoServer();
        UserInfo userInfo = userInfoServer.GetUserInfo(ServerByUser.Instance().UserID);
        oilCountPropertyDetail.text = "<b>" + userInfo.Resources + "</b>";
        moneyCountPropertyDetail.text = "<b>" + userInfo.Gold + "</b>";
        diamondCountPropertyDetail.text = "<b>" + userInfo.Diamonds + "</b>";
    }

    public void ShowDockPlane()
    {
        dock.SetActive(true);
    }
    public void CloseDockPlane()
    {
        dock.SetActive(false);
    }

    #endregion
}
