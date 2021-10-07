using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Dock_View : MonoBehaviour
{
    public GameObject warningPlane;
    
    public GameObject heroDetailPlane;
    public GameObject propertyDetail;
    public GameObject heroDetailPlaneStarsFather;

    public GameObject star;
    public GameObject shipPrefab;
    public GameObject dockPlane;
    public Text dockCapacity;
    public GameObject father;

    public GameObject fly;
    public GameObject mainGun;
    public GameObject Auxiliary1;

    public Text flyAtk;
    public Text flyFrequency;
    public Text mainGunAtk;
    public Text mainGunFrequency;
    public Text Auxiliary1Atk;
    public Text Auxiliary1Frequency;
    public Text flyName;
    public Text mainGunName;
    public Text Auxiliary1Name;

    public Text RoleId;
    public Text Part;
    public Text EquipmentID;
    public GameObject Attributes;

    RoleEquipmetServer RES=new RoleEquipmetServer();
    private string currentEquipID;
    RoleServer Rs=new RoleServer();

    public void ShowDock()//主页面调用的激活船坞的回调函数
    {
        dockPlane.SetActive(true);
    }

    public void CleanList() //关闭dock的时候清空子物体
    {
        for (int i = 0; i < father.transform.childCount; i++)
        {
            Destroy(father.transform.GetChild(i).gameObject);
        }
    }

    public void RefreshShipList()//有更新操作的时候刷新面板里面的英雄
    {
        CleanList();
        ShowList();
    }

    public void ShowList()//显示所有拥有的英雄(船)  且给他们挂上DockHeroDetail脚本存储他们的信息   以便激活详情面板调用数据
    {  
        DockServer ds = new DockServer();
        Dock dock = ds.GetDackInfo(ServerByUser.Instance().UserID);

        dockCapacity.text = "船坞容量" + dock.DockNum + "/" + dock.DockUpLimit;
        HaveRole[] hr = ds.GetRoleInfo(dock.DockID);

        for (int i = 0; i < hr.Length; i++)
        {
            GameObject obj = GameObject.Instantiate(shipPrefab, father.transform);
            Transform starFather = obj.transform.Find("HeroFrame/GameObject");
            HorizontalLayoutGroup starGroup = starFather.GetComponent<HorizontalLayoutGroup>();
            obj.name = hr[i].RoleName;
            obj.transform.Find("HeroFrame/HeroName").GetComponent<Text>().text = hr[i].RoleName;
            
            switch (hr[i].Stars)
            {
                case 2:
                    obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Dock/star_level_card_2");
                    for (int j = 0; j < hr[i].Stars; j++)
                    {
                        GameObject realstar = GameObject.Instantiate(star, starFather);
                    }

                    starGroup.spacing = -120;
                    break;
                case 3:
                    obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Dock/star_level_card_3");
                    for (int j = 0; j < hr[i].Stars; j++)
                    {
                        GameObject realstar = GameObject.Instantiate(star, starFather);
                    }

                    starGroup.spacing = -100;
                    break;
                case 4:
                    obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Dock/star_level_card_4");
                    for (int j = 0; j < hr[i].Stars; j++)
                    {
                        GameObject realstar = GameObject.Instantiate(star, starFather);
                    }

                    starGroup.spacing = -80;
                    break;
                case 5:
                    obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Dock/star_level_card_5");
                    for (int j = 0; j < hr[i].Stars; j++)
                    {
                        GameObject realstar = GameObject.Instantiate(star, starFather);
                    }

                    starGroup.spacing = -60;
                    break;
            }

            obj.transform.Find("Hero").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Dock/" + hr[i].Nickname);
            obj.transform.Find("HeroFrame/Text").GetComponent<Text>().text = "LV:" + hr[i].Level;
         
            switch (hr[i].Position)
            {
                    
                case 1:
                    obj.transform.Find("HeroFrame/HeroType").GetComponent<Image>().sprite =
                        Resources.Load<SpriteAtlas>("UI/Dock/HeroType").GetSprite("lowshoottype");
                    break;
                case 2:
                    obj.transform.Find("HeroFrame/HeroType").GetComponent<Image>().sprite =
                        Resources.Load<SpriteAtlas>("UI/Dock/HeroType").GetSprite("flytype");
                    break;
                case 3:
                    obj.transform.Find("HeroFrame/HeroType").GetComponent<Image>().sprite =
                        Resources.Load<SpriteAtlas>("UI/Dock/HeroType").GetSprite("shoottype");
                    break;
            }

            obj.GetComponent<DockHeroDetail>().AirDefense = hr[i].AirDefense;
            obj.GetComponent<DockHeroDetail>().AntiSubmarine = hr[i].AntiSubmarine;
            obj.GetComponent<DockHeroDetail>().Armor = hr[i].Armor;
            obj.GetComponent<DockHeroDetail>().Aviation = hr[i].Aviation;
            obj.GetComponent<DockHeroDetail>().Camp = hr[i].Camp;
            obj.GetComponent<DockHeroDetail>().Consumption = hr[i].Consumption;
            obj.GetComponent<DockHeroDetail>().Durable = hr[i].Durable;
            obj.GetComponent<DockHeroDetail>().Level = hr[i].Level;
            obj.GetComponent<DockHeroDetail>().Loading = hr[i].Loading;
            obj.GetComponent<DockHeroDetail>().Motor = hr[i].Motor;
            obj.GetComponent<DockHeroDetail>().Nickname = hr[i].Nickname;
            obj.GetComponent<DockHeroDetail>().Position = hr[i].Position;
            obj.GetComponent<DockHeroDetail>().Price = hr[i].Price;
            obj.GetComponent<DockHeroDetail>().SkillbarID = hr[i].SkillbarID;
            obj.GetComponent<DockHeroDetail>().Stars = hr[i].Stars;
            obj.GetComponent<DockHeroDetail>().TheShelling = hr[i].TheShelling;
            obj.GetComponent<DockHeroDetail>().TheTorpedo = hr[i].TheTorpedo;
            obj.GetComponent<DockHeroDetail>().RoleName = hr[i].RoleName;
            obj.GetComponent<DockHeroDetail>().RoleID = hr[i].RoleID;
        }
        
    }

  
    public void CloseHeroDetail()  //详情面板关闭回调函数
    {
        heroDetailPlane.SetActive(false);
        for (int i = 0; i < heroDetailPlaneStarsFather.transform.childCount; i++)
        {
            Destroy(heroDetailPlaneStarsFather.transform.GetChild(i).gameObject);
        }
    }
    //英雄相信信息  装备和属性栏替换
    public void SwitchToEquipment(bool ison)
    {
        if (ison)
        {
            heroDetailPlane.SetActive(true);
            propertyDetail.SetActive(false);
        }
    }
    public void SwitchToProperty(bool ison)
    {
        if (ison)
        {
            heroDetailPlane.SetActive(false);
            propertyDetail.SetActive(true);
        }

    }

    //点击某个英雄   已装备的物品发生改变
    public void ShowHaveEquite()
    {
        
        RoleEquipmet RE=new RoleEquipmet();
       // RoleEquipmetServer RES=new RoleEquipmetServer();
        RE = RES.GetRoleEquipmet(RoleId.text);
        
        if (RE.AirdefenseGun!=null)
        {   HaveEquipmet HE=new HaveEquipmet();
            HE = RES.GetEquipmetInfoByID(RE.AirdefenseGun);
            fly.transform.Find("Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("UI/Items/Equite/" + HE.Nickname);
           // Debug.Log(HE.Nickname);
            flyAtk.text = HE.Atk.ToString();
            flyFrequency.text = HE.AtkCD + "/秒";
            flyName.text = HE.EquipmentName;
        }
        if (RE.MainGun != null)
        {
            HaveEquipmet HE1 = new HaveEquipmet();
            HE1 = RES.GetEquipmetInfoByID(RE.MainGun);
            mainGun.transform.Find("Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("UI/Items/Equite/" + HE1.Nickname);
            mainGunAtk.text = HE1.Atk.ToString();
            mainGunFrequency.text = HE1.AtkCD + "/秒";
            mainGunName.text = HE1.EquipmentName;
        }
        if (RE.Auxiliary1 != null)
        {
            HaveEquipmet HE2 = new HaveEquipmet();
            HE2 = RES.GetEquipmetInfoByID(RE.Auxiliary1);
            Auxiliary1.transform.Find("Image").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("UI/Items/Equite/" + HE2.Nickname);
            Auxiliary1Atk.text = HE2.Atk.ToString();
            Auxiliary1Frequency.text = HE2.AtkCD + "/秒";
            Auxiliary1Name.text = HE2.EquipmentName;
        }
    }

    public void EquiteMySelect()
    {
        
        if (RES.GetOldRoleId(Part.text, EquipmentID.text)=="")
        {
            RES.ChangeEquipmet(RoleId.text, Part.text, EquipmentID.text);
            RefreshHeroProperty();
            
        }
        else
        {
            warningPlane.SetActive(true);
        }

    }

    public void ConfirmMySelect()
    {
        RES.ChangeEquipmet(RoleId.text, Part.text, EquipmentID.text);
        RefreshHeroProperty();
        warningPlane.SetActive(false);
    }

    public void CancelMySelect()
    {
        warningPlane.SetActive(false);
    }

    public void UnloadMyCurrentEquipment()
    {
        RoleEquipmet RE = new RoleEquipmet();
        
        RE = RES.GetRoleEquipmet(RoleId.text);

        switch (Part.text)
        {
            case "AirdefenseGun":
                currentEquipID =RE.AirdefenseGun;
                break;
            case "MainGun":
                currentEquipID = RE.MainGun;
                break;
            case "Auxiliary1":
                currentEquipID = RE.Auxiliary1;
                break;
        }

      
        RES.UnloadEquipmet(RoleId.text, Part.text, currentEquipID);
        RefreshHeroProperty();
    }


    public void RefreshHeroProperty()
    {
        int total;
        HaveRole hr =  Rs.GetRoleInfo(RoleId.text);
        total = (hr.Durable + hr.Durable + hr.Loading + hr.TheShelling + hr.TheTorpedo + hr.Motor + hr.AirDefense +
                 hr.Aviation + hr.Consumption + hr.AntiSubmarine) * 76 + hr.Level * 13;

        Attributes.transform.Find("naijiu").GetComponent<Text>().text = hr.Durable.ToString();
        Attributes.transform.Find("zhuangjia").GetComponent<Text>().text = hr.Durable.ToString();
        Attributes.transform.Find("zhuangtian").GetComponent<Text>().text = hr.Loading.ToString();
        Attributes.transform.Find("paoji").GetComponent<Text>().text = hr.TheShelling.ToString();
        Attributes.transform.Find("leiji").GetComponent<Text>().text = hr.TheTorpedo.ToString();
        Attributes.transform.Find("jidong").GetComponent<Text>().text = hr.Motor.ToString();
        Attributes.transform.Find("fangkong").GetComponent<Text>().text = hr.AirDefense.ToString();
        Attributes.transform.Find("hangkong").GetComponent<Text>().text = hr.Aviation.ToString();
        Attributes.transform.Find("xiaohao").GetComponent<Text>().text = hr.Consumption.ToString();
        Attributes.transform.Find("level").GetComponent<Text>().text = hr.Level.ToString();
        Attributes.transform.Find("fanqian").GetComponent<Text>().text = hr.AntiSubmarine.ToString();
        Attributes.transform.Find("total").GetComponent<Text>().text = total.ToString();
          
    }
}
