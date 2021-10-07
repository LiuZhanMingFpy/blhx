using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public static Login login; 
 

    /// <summary>
    /// serverList
    /// </summary>
    public GameObject serverList;

    /// <summary>
    /// 获取服务器数据
    /// </summary>
    private TheServerInfo theServerInfo;

   // [HideInInspector] public string Total;//用户的总账号

    /// <summary>
    /// userInfo是用户信息的实体类
    /// </summary>
    private UserInfo userInfo;
    [HideInInspector]
    public string userId;
    /// <summary>
    /// 用户数据
    /// </summary>
    private UserInfoServer userInfoServer;

    //private LoginServer loginServer;
   

    /// <summary>
    /// 返回按钮
    /// </summary>
    public Button ResClose;
    public Button Welcome_Panel;

    [HideInInspector] public string ResRepirePassword;
    [HideInInspector] public string ResPassword;
    [HideInInspector] public string user; //这个给json
    [HideInInspector] public string password; //这个给json

    /// <summary>
    /// bg
    /// </summary>
    public GameObject bg; //login_panel里的bg

    public GameObject Login_panel;//登录总界面
    public GameObject loginPanel;//子界面——登录
    public GameObject registerPanel;//子界面——注册

    public Button Login_panelbtn;

    /// <summary>
    /// 用户名输入框
    /// </summary>
    [Header("用户名输入框")] public InputField UserName; //用户名输入框

    /// <summary>
    /// 密码输入框
    /// </summary>
    [Header("密码输入框")] public InputField PasswordInputField; //密码输入框

    /// <summary>
    /// 注册按钮
    /// </summary>
    [Header("注册按钮")] public Button Resbtn; //注册按钮

    /// <summary>
    /// 注册面板
    /// </summary>
    //[Header("注册面板")] public GameObject ResPanel;

    /// <summary>
    /// 注册按钮
    /// </summary>
    [Header("登录按钮")] public Button Login_btn; //登录按钮

    /// <summary>
    /// 提示UI 
    /// </summary>
    [Header("提示面板")] public GameObject Tip_Panel;

    /// <summary>
    /// 注册用户名
    /// </summary>
    public InputField ResUserInputField;

    /// <summary>
    /// 输入密码
    /// </summary>
    public InputField ResPasswordInputField;

    /// <summary>
    /// 第二次输入
    /// </summary>
    public InputField ResRepirePassworInputField; //第二次输入

    public InputField EmailInputField;
    public Button ResSure; //确认注册

    /// <summary>
    /// 注册的密码是否有中文
    /// </summary>
    private bool IsChinese = false; //注册里密码的中文

    /// <summary>
    ///正则表达式
    /// </summary>
    private bool RespireIsmatch = false;

    /// <summary>
    /// 重复输入密码
    /// </summary>
    private bool RespirePassword; //重复输入密码

    /// <summary>
    /// 登录界面的正则表达式 重新输入是否有中文
    /// </summary>
    private bool isMatch;

    private bool PasswordisEmpty = true;
    private bool UserNameisEmpty = true;
    private bool EmailFotmat;
    public Text Tip_text;
    public GameObject ChangeAount;
    public Button LoginClose;
    [HideInInspector] public string Logininuser; //我前面已经弄过安全校验了 所以可以调用sava方法写入json 如果数据库id不为空
    [HideInInspector] public string Logininpassword;
  
    string cheak = @"[\u4E00-\u9FA5]";
    //ServerByUser serverByUser = new ServerByUser();
    #region 一堆赋值

    void Start()
    {
        login = this;

        #region 注册点击事件
        Welcome_Panel.onClick.AddListener(Login_in); //点击面板登录
        Login_btn.onClick.AddListener(OnLoginClick);//登录界面的 登录按钮
        Resbtn.onClick.AddListener(ShowRes);//登录界面的 注册按钮
        LoginClose.onClick.AddListener(OnLoginCloseClick);//登录界面的 关闭按钮
        ResClose.onClick.AddListener(OnResCloseClick);//注册界面 关闭按钮
        ResSure.onClick.AddListener(OnResSureClick);//注册界面的 确定按钮
        #endregion
        //提示不能为空
        //Tip_text = Tip_Panel.transform.Find("Text").GetComponent<Text>();

        userInfoServer = new UserInfoServer();
        userInfo = new UserInfo();

       
    }

    #endregion

    #region 按钮点击事件的回调函数

    /// <summary>
    /// 点击面板登录
    /// </summary>
    public  void Login_in()
    {
        //让ChangeAount界面显示
        ChangeAount.gameObject.SetActive(true);

        //用户的总账号 
        if (UserDataManager.Instance.TheTotalID == null)
        {
            //Login_panel.transform.SetAsLastSibling();
            Login_panel.gameObject.SetActive(true);
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "id不存在";
            Invoke("isMatchEnable", 4);
            Invoke("isMatchEnable", 4);
            return;
        }

        string Total = UserDataManager.Instance.TheTotalID;
        string json = File.ReadAllText(Application.streamingAssetsPath + "/Server.json", Encoding.UTF8);
          //获取上次登录的id
        ServerHasBeenLogin instance = JsonUtility.FromJson<ServerHasBeenLogin>(json);
        //Debug.Log( instance.ServerId);
        //得到userid
        userId= TheServerInfo.Instance.GetUserID(Total, instance.ServerId);
        //Debug.Log(Total+"," + instance.ServerId);
        ServerByUser.Instance().UserID = userId;
        
        if (ServerByUser.Instance().UserID == string.Empty)
        {
            SceneManager.LoadScene(1);
            return;
        }
        //保存userid
       
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// 登录界面的 登录按钮
    /// </summary>
    void OnLoginClick()
    {
        #region 校验
        if (UserNameisEmpty == true) //如果用户名为空
        {
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "用户名不能为空";
            Invoke("isMatchEnable", 4);
            return;
        }

        if (isMatch == true) //有中文
        {
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "好好检查一下哦，密码怎么能有中文呢";
            float time = Time.deltaTime;
            Invoke("isMatchEnable", 4);
            return;
        }

        if (PasswordisEmpty == true) //密码为空
        {
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "密码不能为空";
            Invoke("isMatchEnable", 4);
            return;
        } 
        #endregion
        password = PasswordInputField.text;
        user = UserName.text;
        UserDataManager.Instance.TheTotalID = UserDataManager.Instance.LoginVerification(user, password);

        if (UserDataManager.Instance.TheTotalID == null)
        {
            //Login_panel.transform.SetAsLastSibling();
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "id不存在";
            Invoke("isMatchEnable", 4);
            return;

        }
        else
        {
            string userName = "<color=yellow>" + user + "</color>" + "用户正在登录中";
            string Login_Info = ChangeAount.transform.Find("Text").GetComponent<Text>().text = userName;

            Invoke("hideChangeAount", 4);
            Account account = new Account();
            account.AccountNumber = user;
            account.PassWord = password;
            SavaData.Instance.Save(false,account);//存档
            Login_panel.SetActive(false);
        }

        //OnLoginCloseClick();
    }

    /// <summary>
    /// 登录界面的 注册按钮
    /// </summary>
    void ShowRes()
    {
        registerPanel.gameObject.SetActive(true);
        InitRegister();
        //OnLoginCloseClick();
    }

    /// <summary>
    /// 注册界面 关闭按钮
    /// </summary>
    void OnResCloseClick()
    {
        registerPanel.gameObject.SetActive(false);
        bg.gameObject.SetActive(true);
    }

    /// <summary>
    /// /注册界面的 确定按钮
    /// </summary>
    void OnResSureClick()
    {
        //密码 、重复输入密码
        ResPassword = ResPasswordInputField.text;
        ResRepirePassword = ResRepirePassworInputField.text;

        //校验密码如果不符合正则表达式
        if (RespireIsmatch == true || IsChinese == true)
        {
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "密码不能有中文";
            Invoke("isMatchEnable", 4);
            return;
        }

        //校验两次密码是否相同
        if (ResRepirePassword != ResPassword)
        {
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "两次输入的密码不相同";
            Invoke("isMatchEnable", 4);
            return;
        }

        //校验邮箱格式不正确
        if (EmailFotmat == true)
        {
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "邮箱格式不正确";
            Invoke("isMatchEnable", 4);
            return;
        }

        //任意一项没有填写完整
        if (ResUserInputField.text == "" || ResPasswordInputField.text == "" || ResRepirePassworInputField.text == "" || EmailInputField.text == "")
        {
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "请填完整";
            Invoke("isMatchEnable", 4);
            return;
        }

        //通用的提示框
      
        Account user = new Account();
        user.AccountNumber = ResUserInputField.text;//用户名保存起来
        user.PassWord = ResPasswordInputField.text;//密码保存起来
        SavaData.Instance.Save(true, user);
        if (SavaData.Instance.Isok)
        {
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "注册成功！";
        }
        else
        {
            Tip_Panel.gameObject.SetActive(true);
            Tip_text.text = "注册失败！";
            return;
        }
        //通过所有校验



        Invoke("SaveUserData", 2);
    }

    /// <summary>
    /// 保存注册界面的用户数据
    /// </summary>
    void SaveUserData()
    {

        UserName.text =     ResUserInputField.text;
        PasswordInputField.text =  ResPasswordInputField.text;
        ChangePanel(registerPanel,loginPanel);

        //Login_panel.transform.SetAsFirstSibling();//调整子物体顺序
    }

    /// <summary>
    /// 注册界面关闭按钮的回调函数
    /// </summary>
    void OnLoginCloseClick()
    {
        gameObject.SetActive(false);//调整顺序被遮挡相当于隐藏
    }
    #endregion


    /// <summary>
    /// 初始化注册面板，每次打开的时候调用
    /// </summary>
    public void InitRegister()
    {
        ResUserInputField.text = "";
        ResPasswordInputField.text = "";
        ResRepirePassworInputField.text = "";
        EmailInputField.text = "";
    }

    #region 正则表达式注册校验
    /// <summary>
    /// 注册的密码框里是否有中文
    /// </summary>
    void ResInputPasswordResPassword()
    {

        for (int i = 0; i < ResPasswordInputField.text.Length; i++)
        {
            if (Regex.IsMatch(ResPasswordInputField.text[i].ToString(), cheak)) //判断密码是否有中文 有 bool为true
            {

                IsChinese = true;
                return;
            }

            IsChinese = false;
        }
    }

    void ResInputPasswordAgain()
    {
        for (int i = 0; i < ResRepirePassworInputField.text.Length; i++)
        {
            if (Regex.IsMatch(ResRepirePassworInputField.text[i].ToString(), cheak)) //判断密码是否有中文 有 bool为true
            {
                RespireIsmatch = true;
                return;
            }

            RespireIsmatch = false;
        }

    }

    void UsernameInputFlieisEmpty()
    {
        if (UserName.text.Length <= 0) //如果username.text为空 return
        {
            UserNameisEmpty = true;
            return;
        }
        else //不为空 不return
        {
            UserNameisEmpty = false;
        }
    }

    void PasswordInputFlieisEmpty()
    {

        if (PasswordInputField.text == String.Empty) //密码为空
        {
            PasswordisEmpty = true;
            return;
        }
        else if (PasswordInputField.text != String.Empty) //密码不为空
        {
            PasswordisEmpty = false;
        }

    }

    void AgeaginPassword()
    {
        for (int i = 0; i < PasswordInputField.text.Length; i++)
        {
            if (Regex.IsMatch(PasswordInputField.text[i].ToString(), cheak)) //判断密码是否有中文 有 bool为true
            {
                isMatch = true;

                return;
            }

            isMatch = false;
        }
    }

    #endregion
    // Update is called once per frame
    void Update()
    {

        AgeaginPassword();
        PasswordInputFlieisEmpty();
        ResInputPasswordAgain();
        UsernameInputFlieisEmpty();
        ResInputPasswordResPassword();
        string Email = @"^[A - Za - z\d] + ([-_.][A - Za - z\d] +)*@([A - Za - z\d]+[-.])+[A - Za - z\d]{2,4}$";
        for (int i = 0; i < EmailInputField.text.Length; i++)
        {
            if (Regex.IsMatch(EmailInputField.text[i].ToString(), Email)) //判断密码是否有中文 有 bool为true
            {
                EmailFotmat = true;

                return;
            }

            EmailFotmat = false;
        }

    }
    //Tip_Panel开关
    public void isMatchEnable()
    {
        Tip_Panel.gameObject.SetActive(false);
    }
    //登录账号提示开关
    public void hideChangeAount()
    {
        ChangeAount.gameObject.SetActive(false);
    }


 /// <summary>
 /// 切换场景的方法
 /// </summary>
   

    #region 暂时用不到
    //void HideTipText()
    //{
    //    Welcome_Panel.transform.Find("Text").GetComponent<Text>().gameObject.SetActive(false);
    //}
    #endregion
/// <summary>
/// 从一个界面切换到另一个界面
/// </summary>
/// <param name="from"></param>
/// <param name="to"></param>
    public void ChangePanel(GameObject from,GameObject to)
    {
        to.SetActive(true);
        from.SetActive(false);
    }
}




