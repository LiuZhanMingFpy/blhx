using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements.StyleEnums;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selet : MonoBehaviour
{
   Sprite[] images= new Sprite[9];
    public Text TipText;
    public GameObject lafei;
    public GameObject z23;
    public GameObject biaoqiang;
    public GameObject The_initial_role;
    public InputField Username;
    private GameObject[] arr  = new GameObject[3];
    public GameObject Name;
    private Vector3 record;
    public Image Skill1;
    public Image Skill2;
    public Image Skill3;
    public Text attack;
    public Text paoji;
    public Text leiji;
    public Text fangkong;
    public Text hangkong;
    public Text jidong;
    private GameObject go;
    public Button Surebtn;
    public Button CanelBtn;
    public Button Decidebtn;
    public GameObject skillImagebackground;
    InitializationServer initializationServer = new InitializationServer();
    //ServerByUser serverByUser = new ServerByUser();
    private string userid;

    private ServerHasBeenLogin instance;
    // Use this for initialization
    void Start ()
    {//人物数组
        arr[0] = lafei;
        arr[1] = z23;
        arr[2] = biaoqiang;
        //Surebtn.onClick.AddListener(Sure);
        CanelBtn.onClick.AddListener(Canel);
        Decidebtn.onClick.AddListener(InputUserName);
      //技能数组
        images[0] =Resources.Load("UI/Login_UI/biaoqiang1", typeof(Sprite)) as Sprite;
        images[1] =Resources.Load("UI/Login_UI/biaoqiang2", typeof(Sprite)) as Sprite;
        images[2] =Resources.Load("UI/Login_UI/biaoqiang3", typeof(Sprite)) as Sprite;
        images[3] = Resources.Load("UI/Login_UI/lafei1", typeof(Sprite)) as Sprite;
        images[4] = Resources.Load("UI/Login_UI/lafei2", typeof(Sprite)) as Sprite;
        images[5] = Resources.Load("UI/Login_UI/lafei3", typeof(Sprite)) as Sprite;
        images[6] = Resources.Load("UI/Login_UI/z231", typeof(Sprite)) as Sprite;
        images[7] = Resources.Load("UI/Login_UI/z232", typeof(Sprite)) as Sprite;
        images[8] = Resources.Load("UI/Login_UI/z232", typeof(Sprite)) as Sprite;
        string json = File.ReadAllText(Application.streamingAssetsPath + "/Server.json", Encoding.UTF8);//加载json的数据

        instance = JsonUtility.FromJson<ServerHasBeenLogin>(json);//它的实例保存的就是Json的数据
    }

    public void Anim(GameObject go)
  {
      //记录位置
      record = go.transform.position;
      this.go = go;
      //动画
        go.transform.DOLocalMove(new Vector3(-364, 0, 0), 0.5f);
      //处理不同的点击事件
      for (int i = 0; i < arr.Length; i++)
      {
          if (go.name == arr[i].name)
          {
              if (go.name == "lafei")
              {
                  Skill1.overrideSprite = images[3];
                  Skill2.overrideSprite = images[4];
                  Skill3.overrideSprite = images[5];
                  attack.text = "攻击力:100";
                  paoji.text = "炮击：70";
                  leiji.text = "雷击：70";
                  fangkong.text = " 防空：70";
                  hangkong.text = " 航空：70";
                  jidong.text = "机动：70";
                    go.GetComponent<Button>().enabled = false;
                }
              else if(go.name == "z23")
              {
                  Skill1.overrideSprite = images[6];
                  Skill2.overrideSprite = images[7];
                  Skill3.overrideSprite = images[8];
                  attack.text = "攻击力:20";
                  paoji.text = "炮击：40";
                  leiji.text = "雷击：60";
                  fangkong.text = " 防空：10";
                  hangkong.text = " 航空：100";
                  jidong.text = "机动：90";
                    go.GetComponent<Button>().enabled  = false;
                }
              else if(go.name == "biaoqiang")
              {
                  Skill1.overrideSprite = images[0];
                  Skill2.overrideSprite = images[1];
                  Skill3.overrideSprite = images[2];
                  attack.text = "攻击力:60";
                  paoji.text = "炮击：90";
                  leiji.text = "雷击：10";
                  fangkong.text = " 防空：100";
                  hangkong.text = "航空：100";
                  jidong.text = "机动：70";
                    go.GetComponent<Button>().enabled  = false;
                }
            
             continue;

          }
          arr[i].SetActive(false);
      }
      //人物属性面板的动画
      The_initial_role.transform.DOLocalMove(new Vector3(502.4f, 0, 0), 0.5f);

  }
    //取消
    public void Canel()
    {
        lafei.transform.gameObject.SetActive(true);
        z23.transform.gameObject.SetActive(true);
        biaoqiang.transform.gameObject.SetActive(true);
      
        go.transform.DOMove(record, 1f);
        StartCoroutine(SetButton());
        The_initial_role.transform.localPosition = new Vector3(1920,0,0);
    }   
    //确认
    public void Sure()
    {
        Name.transform.gameObject.SetActive(true);
    }
    //输入用户名
    public void InputUserName()
    {
        TheServerInfo.Instance.SetServerByUser(UserDataManager.Instance.TheTotalID, instance.ServerId);
        userid = TheServerInfo.Instance.GetUserID(UserDataManager.Instance.TheTotalID, instance.ServerId);
        
        ServerByUser.Instance().UserID = userid;
       
    
        if (Username.text == String.Empty)
        {
            TipText.gameObject.SetActive(true);
            FlyTo(TipText);
            TipText.transform.localPosition = new Vector3(0, 46, 0);
            return;
        }
        else
        {
            string username = Username.text;
            initializationServer.InitalizationUserInfo(userid, username, instance.ServerId, go.transform.Find("Text").GetComponent<Text>().text);
            initializationServer.Initalization(userid, instance.ServerId, go.transform.Find("Text").GetComponent<Text>().text);


           
           
            Name.transform.Find("Button").GetComponent<Button>().onClick.RemoveListener(InputUserName);
            Name.transform.Find("Button").GetComponent<Button>().StartCoroutine(ChangeSence());
        }
       
      
    }
    //切换场景
    IEnumerator ChangeSence()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync("Main");
    }
    //在动画结束前不允许启用角色选择按钮
    IEnumerator SetButton()
    {
      yield return  new  WaitForSeconds(0.6f);
        go.GetComponent<Button>().enabled = true;
    }
    //特效
    public static void FlyTo(Graphic graphic)
    {
        
        RectTransform rt = graphic.rectTransform;
        Color c = graphic.color;
        c.a = 0;
        graphic.color = c;
        Sequence mySequence = DOTween.Sequence();
        Tweener move1 = rt.DOMoveY(rt.position.y + 50, 0.5f);
        Tweener move2 = rt.DOMoveY(rt.position.y + 100, 0.5f);
        Tweener alpha1 = graphic.DOColor(new Color(c.r, c.g, c.b, 1), 0.5f);
        Tweener alpha2 = graphic.DOColor(new Color(c.r, c.g, c.b, 0), 0.5f);
        mySequence.Append(move1);
        mySequence.Join(alpha1);
        mySequence.AppendInterval(1);
        mySequence.Append(move2);
        mySequence.Join(alpha2);
       
    }

}




