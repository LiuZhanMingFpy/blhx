using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Text;
using System.Data;

public class UserDataManager
{
    #region 和json对应的结构
    public string AccountNumber;
    public string PassWord;
    public string TheTotalID;
    #endregion

    #region 单例 
    private UserDataManager() { }
    private static UserDataManager instance;
    public static UserDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                
                string json = File.ReadAllText(Application.streamingAssetsPath + "/JsonPerson.json", Encoding.UTF8);//加载json的数据
   
                instance = JsonUtility.FromJson<UserDataManager>(json);//它的实例保存的就是Json的数据
                
                instance.Set();

            }
            return instance;
        }
    }
    #endregion

    public void Set()
    {
        TheTotalID = LoginVerification(UserDataManager.Instance.AccountNumber, UserDataManager.Instance.PassWord);

    }
    /// <summary>
    /// 通过用户名和密码找到总账号
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public string LoginVerification(string username, string password)
    {
    
        string sql = "select * from Account where 1=1";//需要执行的sql语句
        SetSQLServer setSQLServer = new SetSQLServer();
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);
        //print(ds.Tables[0].Rows[0][1]);
        //object name = ds.Tables[0].Rows[0]["AccountNumber"];//遍历一行

        //遍历所有
        //foreach (DataRow mDr in ds.Tables[0].Rows)
        //{
        //    foreach (DataColumn mDc in ds.Tables[0].Columns)
        //    {
        //        Debug.Log(mDr[mDc].ToString());
        //    }
        //}

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["AccountNumber"].ToString() == username)
            {
                for (int j = i; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[j]["PassWord"].ToString() == password)
                    {

                        return ds.Tables[0].Rows[j]["TheTotalID"].ToString();

                    }
                }
            }
        }
        return null;
    }


}

public class ChangeAccountView : MonoBehaviour
{
    //当前面板的控件
    public Text userName;
    public Button changeUser;

    //获取其它面板
    public GameObject LoginPanel;

    void Awake()
    {
        transform.SetAsLastSibling();
        Invoke("hideChangeAount", 4);

        #region 注册点击事件

        changeUser.onClick.AddListener(OnChangeAccountClick); //登录界面的 切换用户按钮

        #endregion

        LoginPanel = transform.root.Find("Login_panel").gameObject;
        if (UserDataManager.Instance.AccountNumber == null)
        {
            Debug.Log("JsonPerson是空的");
            LoginPanel.transform.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
            string str = "<color=yellow>" + UserDataManager.Instance.AccountNumber + "</color>" + "用户正在登录中";
            userName.text = str;
        }


    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    public void hideChangeAount()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 登录界面的 切换用户按钮
    /// </summary>
    public void OnChangeAccountClick()
    {
        LoginPanel.SetActive(true);
        this.gameObject.SetActive(false);//??????????????????
    }

}
