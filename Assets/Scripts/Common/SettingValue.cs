using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingValue : MonoBehaviour
{
  
  
    public  GameObject setPlane;
	void Start () {
		DontDestroyOnLoad(gameObject);
    
	}

    #region 功能
    //滑动条设置音量   因为音量是全局的所以把他放在这里
    public void Voice(float a)
    {
        this.gameObject.GetComponent<AudioSource>().volume = a;
    }
    //控制setting页面开关
    public void SaveTheSetting()
    {
        setPlane.SetActive(false);
    }
    public void OpenTheSetting()
    {
        setPlane.SetActive(true);
    } 
    #endregion

    public void GetValue()
    {
        UserInfoServer userInfoServer = new UserInfoServer();
        UserInfo userInfo = userInfoServer.GetUserInfo(ServerByUser.Instance().UserID);
  
    }


   





}
