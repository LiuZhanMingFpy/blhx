using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class NewWelcome_PanelController : MonoBehaviour {
    /// <summary>
    /// 欢迎界面的按钮
    /// </summary>
    public Button Welcome_panbtn;
    /// <summary>
    /// 打开选择服务器面板的按钮
    /// </summary>
    public Button Selet_Server_btn;
    /// <summary>
    /// 单例
    /// </summary>
    public static NewWelcome_PanelController _instance;
    /// <summary>
    /// 选择服务器面板
    /// </summary>
    [Header("选择服务器面板")]
    public GameObject Selet_Server_Panel;//选择服务器面板
    /// <summary>
    /// PressToStart 会闪的图
    /// </summary>
    [Header("会闪烁的图")]
    public GameObject PressToStart;//会闪烁的图
    /// <summary>
    /// 位于欢迎面板的按钮背景
    /// </summary>
    [Header(" 位于欢迎面板的按钮背景")]
    public GameObject Selet_setver_btn_bg;//整一个选择服务器按钮的背景
    
    void Awake()
    {
        _instance = this;//做成一个单例 方便调用
        
    }
    void Start()
    {
       
        Selet_Server_btn.onClick.AddListener(Selet_Server_PanelOnClick);
        ChangeServer();


    }

    void Update()
    {
        //图闪烁
        if (UIFadeTest.Instance.canvasGroup.alpha < 0.3f)
        {
            UIFadeTest.Instance.UIShow();
        }
        if (UIFadeTest.Instance.canvasGroup.alpha > 0.86f)
        {
            UIFadeTest.Instance.UIHide();
        }
    }
    /// <summary>
    /// 改变账号 （头顶的那个）
    /// </summary>
    public void ChangeServer()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/Server.json", Encoding.UTF8);

        ServerHasBeenLogin instance = JsonUtility.FromJson<ServerHasBeenLogin>(json);
        string ServerName = instance.ServerName;

        transform.Find("Select server/Button/Text").GetComponent<Text>().text =
            "<color=white>" + ServerName + "            " + "</color>" + "<color=orange><size=13>点击更换</size></color>  ";
    }
    /// <summary>
    /// 打开服务器面板
    /// </summary>
    void Selet_Server_PanelOnClick()
    {
        Selet_Server_Panel.SetActive(true);
       // Hide(false);
       // Welcome_panbtn.interactable = false;//被点击的时候禁用主界面的按钮
        //Selet_Server_btn.onClick.AddListener();
        ServerList._instance.ServerListUpdate();

    }
    /// <summary>
    /// 隐藏位于欢迎面板的按钮背景
    /// </summary>
    /// <param name="isEnable">显示为true，隐藏为false</param>
    //public void Hide(bool isEnable)
    //{
    //    Selet_setver_btn_bg.gameObject.SetActive(isEnable);
    //}
}
