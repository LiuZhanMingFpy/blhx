using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DockHeroDetail : MonoBehaviour
{
    private int total;
    private GameObject PropertyDetailPlane;
    private GameObject star;
    private SpriteAtlas HeroType;
    private GameObject HeroDetailPlane;

    private GameObject MainCamera;
    //private HaveEquipmet fly;
    //private HaveEquipmet mainGun;
    //private HaveEquipmet Auxiliary1;
    void Start()
    {
        //获取必要对象
        PropertyDetailPlane = GameObject.Find("Canvas").transform.Find("HeroDetailPropertyPlane").gameObject;
        star = Resources.Load<GameObject>("Prefabs/UI/Star");
        HeroType = Resources.Load<SpriteAtlas>("UI/Dock/HeroTypeWordImage");
        HeroDetailPlane = GameObject.Find("Canvas").transform.Find("HeroDetailPlane").gameObject;
        MainCamera=GameObject.Find("Main Camera");
        Dock_View dv = MainCamera.GetComponent<Dock_View>();
        gameObject.GetComponent<Button>().onClick.AddListener(ShowHeroDetail);//动态加载回调函数
        gameObject.GetComponent<Button>().onClick.AddListener(dv.ShowHaveEquite);
    }

    /// <summary>
    /// 角色ID
    /// </summary>
    public string RoleID;

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName;

    /// <summary>
    /// 别称
    /// </summary>
    public string Nickname;

    /// <summary>
    /// 耐久
    /// </summary>
    public int Durable;

    /// <summary>
    /// 炮击
    /// </summary>
    public int TheShelling;

    /// <summary>
    /// 防空
    /// </summary>
    public int AirDefense;

    /// <summary>
    /// 反潜
    /// </summary>
    public int AntiSubmarine;

    /// <summary>
    /// 装甲类型
    /// </summary>
    public int Armor;

    /// <summary>
    /// 雷击
    /// </summary>
    public int TheTorpedo;

    /// <summary>
    /// 航空
    /// </summary>
    public int Aviation;

    /// <summary>
    /// 装填
    /// </summary>
    public int Loading;

    /// <summary>
    /// 机动
    /// </summary>
    public int Motor;

    /// <summary>
    /// 消耗
    /// </summary>
    public int Consumption;

    /// <summary>
    /// 阵营
    /// </summary>
    public int Camp;

    /// <summary>
    /// 位置
    /// </summary>
    public int Position;

    /// <summary>
    /// 售价
    /// </summary>
    public int Price;

    /// <summary>
    /// 品质
    /// </summary>

    public int Quality;

    /// <summary>
    /// 技能栏ID
    /// </summary>
    public string SkillbarID;

    /// <summary>
    /// 星级
    /// </summary>
    public int Stars;

    /// <summary>
    /// 等级
    /// </summary>
    public int Level;
    public void ShowHeroDetail()
    {
        HeroDetailPlane.transform.Find("RoleId").GetComponent<Text>().text = RoleID;
        HeroDetailPlane.SetActive(true);//点击船坞英雄显示详情面板
        GameObject.Find("Main Camera").GetComponent<MainPlaneView>().ShowMyValueInHeroDetail();//加刷新 石油金币砖石方法
        GameObject.Find("Main Camera").GetComponent<MainPlaneView>().ShowMyValueInPropertyDetail();
        HeroDetailPlane.transform.Find("MainLook").GetComponent<Image>().sprite =
            Resources.Load<Sprite>("UI/Dock/" + Nickname + "b");
        HeroDetailPlane.transform.Find("HeroName_bg/Name").GetComponent<Text>().text = RoleName;
        switch (Position)
        {
            case 1:
                HeroDetailPlane.transform.Find("HeroName_bg/HeroType_bg/HeroType").GetComponent<Image>().sprite =
                    HeroType.GetSprite("shiptype_1");
                break;
            case 2:
                HeroDetailPlane.transform.Find("HeroName_bg/HeroType_bg/HeroType").GetComponent<Image>().sprite =
                    HeroType.GetSprite("shiptype_2");
                break;
            case 3:
                HeroDetailPlane.transform.Find("HeroName_bg/HeroType_bg/HeroType").GetComponent<Image>().sprite =
                    HeroType.GetSprite("shiptype_3");
                break;

        }

        GameObject father = HeroDetailPlane.transform.Find("HeroName_bg/GameObject").gameObject;
        HorizontalLayoutGroup childSpace = father.GetComponent<HorizontalLayoutGroup>();
        for (int i = 0; i < Stars; i++)
        {
            GameObject obj = GameObject.Instantiate(star, father.transform);
        }
        if (Stars==2)
        {
            childSpace.spacing = -120;
        }
        else if (Stars == 3)
        {
            childSpace.spacing = -100;
        }
        else if (Stars == 4)
        {
            childSpace.spacing = -80;
        }
        else if (Stars == 5)
        {
            childSpace.spacing = -60;
        }
        else
        {
            childSpace.spacing = -40;
        }
        //属性栏各项数字装填    
        PropertyDetailPlane.transform.Find("info/Attributes/naijiu").GetComponent<Text>().text = Durable.ToString();
        PropertyDetailPlane.transform.Find("info/Attributes/zhuangjia").GetComponent<Text>().text = Armor.ToString();
        PropertyDetailPlane.transform.Find("info/Attributes/zhuangtian").GetComponent<Text>().text = Loading.ToString();
        PropertyDetailPlane.transform.Find("info/Attributes/paoji").GetComponent<Text>().text = TheShelling.ToString();
        PropertyDetailPlane.transform.Find("info/Attributes/leiji").GetComponent<Text>().text = TheTorpedo.ToString();
        PropertyDetailPlane.transform.Find("info/Attributes/jidong").GetComponent<Text>().text = Motor.ToString();
        PropertyDetailPlane.transform.Find("info/Attributes/fangkong").GetComponent<Text>().text = AirDefense.ToString();
        PropertyDetailPlane.transform.Find("info/Attributes/hangkong").GetComponent<Text>().text = Aviation.ToString();
        PropertyDetailPlane.transform.Find("info/Attributes/xiaohao").GetComponent<Text>().text = Consumption.ToString();
        PropertyDetailPlane.transform.Find("info/Attributes/fanqian").GetComponent<Text>().text = AntiSubmarine.ToString();
        PropertyDetailPlane.transform.Find("info/Attributes/level").GetComponent<Text>().text = Level.ToString();
        total = (Durable + Armor + Loading + TheShelling + TheTorpedo + Motor + AirDefense + Aviation + Consumption +
                 AntiSubmarine) * 76+Level*13;
        PropertyDetailPlane.transform.Find("info/Attributes/total").GetComponent<Text>().text = total.ToString();
        PropertyDetailPlane.transform.Find("backGround/Set_painting").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Dock/" + Nickname + "b");

        //渲染装备格子里面已装备的装备.若空则显示空的图


    }







}
