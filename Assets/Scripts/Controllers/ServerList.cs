using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Permissions;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerList : MonoBehaviour
{
    #region 单例
    private static ServerList instance;

    public static ServerList _instance
    {
        get
        {
            if (instance == null) 
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<ServerList>();
  
            }

            return instance;
        }

    }
    #endregion

    public Button HaveAcount;
    private ServerHasBeenLogin serverHasBeenLogin = new ServerHasBeenLogin();
    /// <summary>
    /// 登录服务器的id
    /// </summary>
    [HideInInspector] public string serverId;
    public bool newCount;//判断是不是新用户
    public GameObject HaveAcountLoginServer;
    public GameObject Selet_Server_panel;
    public Transform Father;
    public GameObject Login_panel;
    private TheServerInfo theServerInfo = TheServerInfo.Instance;
    /// <summary>
    /// userId
    /// </summary>
    [HideInInspector]
    public string userid;
    
    public static bool isSelect = false;
    private ServerByUser[] serverByUser; //已经登录过的服务器
    private Server[] server;//所有服务器
    //ServerByUser ser = new ServerByUser();

    void Awake()
    {
        instance = this;
        string json = File.ReadAllText(Application.streamingAssetsPath + "/Server.json", Encoding.UTF8);

        ServerHasBeenLogin serverHasBeenLogin = JsonUtility.FromJson<ServerHasBeenLogin>(json);
        string ServerName = serverHasBeenLogin.ServerName;    
       
        HaveAcountLoginServer.transform.Find("ServerId").GetComponent<Text>().text = ServerName;
        serverByUser = theServerInfo.GetServerByUser(UserDataManager.Instance.TheTotalID);
        
        server = theServerInfo.GetServerInfo();
        userid = ServerByUser.Instance().UserID;
        HaveAcount.onClick.AddListener(FastLogin);
    }
    //服务器列表数据更新显示
    public void ServerListUpdate()
    {
     

        ////如果total id == null 不给打开
        //if (UserDataManager.Instance.TheTotalID == String.Empty)
        //{
        //    Login.login.Login_panel.transform.gameObject.SetActive(true);
        //    Login.login.Login_panel.transform.SetAsLastSibling();
        //    return;
        //}
    

        for (int i = 0; i < server.Length; i++)
        {
          
            GameObject go = Instantiate(HaveAcountLoginServer, Vector3.zero, Quaternion.identity, Father) as GameObject;
            //go.transform.Find("Text").GetComponent<Text>().text = server[i].ServerID;
             ServerItem item = go.GetComponent<ServerItem>();
            item.name.text = server[i].ServerName;
            item.ServerID.text = server[i].ServerID;


           
            if (serverByUser == null)
            {
                continue;
            }

            ServerByUser user = null;
            for (int j = 0; j < serverByUser.Length; j++)
            {
               

                if (server[i].ServerID == serverByUser[j].ServerID)
                {
                    
                    user = serverByUser[j];
                    break;
                }

            }

            if (user!=null)
            {

                item.UserId.text = user.UserID;
                
                go.name = "haveAcount";

            }
            else
            {
                item.UserId.text = "";
            }
            item.Acount.gameObject.SetActive(user!=null);
        }
    
    }

    //某一个服务器被点击的回调函数
    public void OnServerselect(GameObject serverGo)
    {
         isSelect = true;
         string ServerName = serverGo.transform.Find("ServerId").GetComponent<Text>().text;//找到被选中的文本 服务器名字
         Image State = serverGo.transform.Find("State").GetComponent<Image>();//改变图片
         this.gameObject.transform.Find("HaveAcount").transform.Find("Text").GetComponent<Text>().text = ServerName;//改变文本 保证下次打开 第一个还是这个

       
        if (serverGo.transform.Find("emage").GetComponent<Image>().transform.gameObject.activeSelf == false)
        {
            newCount = true;
        }
        else
        {
         newCount = false;
        }

       
        this.gameObject.SetActive(false);
        //NewWelcome_PanelController._instance.Hide(true);
        // NewWelcome_PanelController._instance.Welcome_panbtn.interactable = true;//启用主界面的按钮

        this.userid = serverGo.transform.Find("userId").GetComponent<Text>().text;
       ServerByUser.Instance().UserID= this.userid;
       

        serverId = serverGo.transform.Find("Text").GetComponent<Text>().text;
       
        serverHasBeenLogin.ServerId = serverId;
        serverHasBeenLogin.ServerName = serverGo.transform.Find("ServerId").GetComponent<Text>().text;

        
  

        SavaData.Instance.Save(serverHasBeenLogin);
            for (int i = 0; i < Father.childCount; i++)
            {
                Destroy(Father.GetChild(i).gameObject);
            }

        ChangeScene();


    }

    void FastLogin()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/Server.json", Encoding.UTF8);

        ServerHasBeenLogin instance = JsonUtility.FromJson<ServerHasBeenLogin>(json);
      string  userId= TheServerInfo.Instance.GetUserID(UserDataManager.Instance.TheTotalID, instance.ServerId);
        ServerByUser.Instance().UserID = userId;
        //Debug.Log(ser.UserID);
        newCount = false;
        ChangeScene();  
    }
    //切换场景
    public void ChangeScene()
    {
        if (newCount)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}

