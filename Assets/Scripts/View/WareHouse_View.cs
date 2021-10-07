using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class WareHouse_View : MonoBehaviour {
    public GameObject cellPrefabs;
    public GameObject cellParent;
    public Text wareHouseCapacity;

    public GameObject HeroEquipmentPlane;
    public GameObject HeroEquipmentPlaneItemsParent;
    public Image CurrentEquip;

    public GameObject fly;
    public GameObject mainGun;
    public GameObject Auxiliary1;

    public Text part;
    void Start () {
		
	}
    #region 功能
    //控制仓库开关
    public void CloseWareHouse()
    {
        this.gameObject.SetActive(false);
    }
    public void OPenWareHouse()
    {
        this.gameObject.SetActive(true);
    }
    //展示仓库装备类
    public void ShowWareHouseEquit()
    {
        BackPackServer backPackServer=new BackPackServer();
        HaveEquipmet[] haveEquipmets = backPackServer.GetHaveEquipmet(ServerByUser.Instance().UserID);
        for (int i = 0; i < haveEquipmets.Length; i++)
        {
            GameObject obj = GameObject.Instantiate(cellPrefabs, cellParent.transform);
            obj.name = haveEquipmets[i].Nickname;
            obj.GetComponent<Image>().sprite = Resources.Load<SpriteAtlas>("UI/Warehouse/Quality").GetSprite(haveEquipmets[i].EquipmentQuality.ToString());
            obj.transform.Find("Item").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("UI/Items/Equite/" + haveEquipmets[i].Nickname);
            obj.transform.Find("ItemCount").GetComponent<Text>().text = "";
            obj.transform.Find("ItemName").GetComponent<Text>().text = haveEquipmets[i].EquipmentName;
            obj.GetComponent<WareHouseEquitDetail>().Atk = haveEquipmets[i].Atk;
            obj.GetComponent<WareHouseEquitDetail>().AtkCD = haveEquipmets[i].AtkCD;
            obj.GetComponent<WareHouseEquitDetail>().EquipmentName = haveEquipmets[i].EquipmentName;
            obj.GetComponent<WareHouseEquitDetail>().EquipmentCategory = haveEquipmets[i].EquipmentCategory;
            obj.GetComponent<WareHouseEquitDetail>().Nickname = haveEquipmets[i].Nickname;
            obj.GetComponent<WareHouseEquitDetail>().EquipmentID = haveEquipmets[i].EquipmentID;
        }

        BackPack bp=backPackServer.GetBackPackSize(ServerByUser.Instance().UserID);
        wareHouseCapacity.text = bp.QuantityNum + "/" + bp.QuantityUpLimit;

    }
    //展示仓库材料类
    public void ShowWareHouseMaterial()
    {   
        BackPackServer backPackServer = new BackPackServer();
        Goods[] goodses = backPackServer.GetBackPackSpack(ServerByUser.Instance().UserID);
        for (int i = 0; i < goodses.Length; i++)
        {
            GameObject obj = GameObject.Instantiate(cellPrefabs, cellParent.transform);
            obj.name = goodses[i].Nickname;
            obj.GetComponent<Image>().sprite = Resources.Load<SpriteAtlas>("UI/Warehouse/Quality").GetSprite(goodses[i].GoodsQuality.ToString());
            obj.transform.Find("Item").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("UI/Items/Equite/" + goodses[i].Nickname);
            obj.transform.Find("ItemCount").GetComponent<Text>().text = goodses[i].Num.ToString();
            obj.transform.Find("ItemName").GetComponent<Text>().text = goodses[i].GoodsName;
        }
        BackPack bp = backPackServer.GetBackPackSize(ServerByUser.Instance().UserID);
        wareHouseCapacity.text = bp.MaterialScienceNum + "/" + bp.MaterialScienceUpLimit;

    }


    public void ShowWareHouseEquitToggle(bool ison)//仓库页面的装备toggle回调函数
    {
        if (ison)
        {
            
             for (int i = 0; i < cellParent.transform.childCount; i++)
             {
                 Destroy(cellParent.transform.GetChild(i).gameObject);
             }
             ShowWareHouseEquit();
        }
    }
    public void ShowWareHouseMaterialToggle(bool ison)//仓库页面的材料toggle回调函数
    {
        if (ison)
        {

            for (int i = 0; i < cellParent.transform.childCount; i++)
            {
                Destroy(cellParent.transform.GetChild(i).gameObject);
            }
            ShowWareHouseMaterial();
        }
    }


    public void WareHouseClean()
    {
        for (int i = 0; i < cellParent.transform.childCount; i++)
        {
            Destroy(cellParent.transform.GetChild(i).gameObject);
        }
    }

    public void WareHouseEquitshow()
    {
        ShowWareHouseEquit();
    }

    #endregion

    //角色选装备页调用的方法
    public void ShowFlyEquit()
    {
        CurrentEquip.sprite = fly.transform.Find("Image").GetComponent<Image>().sprite;
        HeroEquipmentPlane.SetActive(true);
        BackPackServer backPackServer = new BackPackServer();
        HaveEquipmet[] haveEquipmets = backPackServer.GetHaveEquipmet(ServerByUser.Instance().UserID);
        part.text = "AirdefenseGun";
        for (int i = 0; i < haveEquipmets.Length; i++)
        {
            if (haveEquipmets[i].EquipmentCategory==4)
            {
                GameObject obj = GameObject.Instantiate(cellPrefabs, HeroEquipmentPlaneItemsParent.transform);
                obj.name = haveEquipmets[i].Nickname;
                obj.GetComponent<Image>().sprite = Resources.Load<SpriteAtlas>("UI/Warehouse/Quality").GetSprite(haveEquipmets[i].EquipmentQuality.ToString());
                obj.transform.Find("Item").GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("UI/Items/Equite/" + haveEquipmets[i].Nickname);
                obj.transform.Find("ItemCount").GetComponent<Text>().text = "";
                obj.transform.Find("ItemName").GetComponent<Text>().text = haveEquipmets[i].EquipmentName;
                obj.GetComponent<WareHouseEquitDetail>().Atk = haveEquipmets[i].Atk;
                obj.GetComponent<WareHouseEquitDetail>().AtkCD = haveEquipmets[i].AtkCD;
                obj.GetComponent<WareHouseEquitDetail>().EquipmentName = haveEquipmets[i].EquipmentName;
                obj.GetComponent<WareHouseEquitDetail>().EquipmentCategory = haveEquipmets[i].EquipmentCategory;
                obj.GetComponent<WareHouseEquitDetail>().Nickname = haveEquipmets[i].Nickname;
                obj.GetComponent<WareHouseEquitDetail>().EquipmentID = haveEquipmets[i].EquipmentID;
                obj.GetComponent<Toggle>().group = HeroEquipmentPlaneItemsParent.GetComponent<ToggleGroup>();
            }
        }
    }
    public void ShowMainGunEquit()
    {
        CurrentEquip.sprite = mainGun.transform.Find("Image").GetComponent<Image>().sprite;
        HeroEquipmentPlane.SetActive(true);
        BackPackServer backPackServer = new BackPackServer();
        HaveEquipmet[] haveEquipmets = backPackServer.GetHaveEquipmet(ServerByUser.Instance().UserID);
        part.text = "MainGun";
        for (int i = 0; i < haveEquipmets.Length; i++)
        {
            if (haveEquipmets[i].EquipmentCategory == 0)
            {
                GameObject obj = GameObject.Instantiate(cellPrefabs, HeroEquipmentPlaneItemsParent.transform);
                obj.name = haveEquipmets[i].Nickname;
                obj.GetComponent<Image>().sprite = Resources.Load<SpriteAtlas>("UI/Warehouse/Quality").GetSprite(haveEquipmets[i].EquipmentQuality.ToString());
                obj.transform.Find("Item").GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("UI/Items/Equite/" + haveEquipmets[i].Nickname);
                obj.transform.Find("ItemCount").GetComponent<Text>().text = "";
                obj.transform.Find("ItemName").GetComponent<Text>().text = haveEquipmets[i].EquipmentName;
                obj.GetComponent<WareHouseEquitDetail>().Atk = haveEquipmets[i].Atk;
                obj.GetComponent<WareHouseEquitDetail>().AtkCD = haveEquipmets[i].AtkCD;
                obj.GetComponent<WareHouseEquitDetail>().EquipmentName = haveEquipmets[i].EquipmentName;
                obj.GetComponent<WareHouseEquitDetail>().EquipmentCategory = haveEquipmets[i].EquipmentCategory;
                obj.GetComponent<WareHouseEquitDetail>().Nickname = haveEquipmets[i].Nickname;
                obj.GetComponent<WareHouseEquitDetail>().EquipmentID = haveEquipmets[i].EquipmentID;
                obj.GetComponent<Toggle>().group = HeroEquipmentPlaneItemsParent.GetComponent<ToggleGroup>();
            }
        }
    }
    public void ShowAuxiliary1Equit()
    {
        CurrentEquip.sprite = Auxiliary1.transform.Find("Image").GetComponent<Image>().sprite;
        HeroEquipmentPlane.SetActive(true);
        BackPackServer backPackServer = new BackPackServer();
        HaveEquipmet[] haveEquipmets = backPackServer.GetHaveEquipmet(ServerByUser.Instance().UserID);
        part.text = "Auxiliary1";
        for (int i = 0; i < haveEquipmets.Length; i++)
        {
            if (haveEquipmets[i].EquipmentCategory == 2)
            {
                GameObject obj = GameObject.Instantiate(cellPrefabs, HeroEquipmentPlaneItemsParent.transform);
                obj.name = haveEquipmets[i].Nickname;
                obj.GetComponent<Image>().sprite = Resources.Load<SpriteAtlas>("UI/Warehouse/Quality").GetSprite(haveEquipmets[i].EquipmentQuality.ToString());
                obj.transform.Find("Item").GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("UI/Items/Equite/" + haveEquipmets[i].Nickname);
                obj.transform.Find("ItemCount").GetComponent<Text>().text = "";
                obj.transform.Find("ItemName").GetComponent<Text>().text = haveEquipmets[i].EquipmentName;
                obj.GetComponent<WareHouseEquitDetail>().Atk = haveEquipmets[i].Atk;
                obj.GetComponent<WareHouseEquitDetail>().AtkCD = haveEquipmets[i].AtkCD;
                obj.GetComponent<WareHouseEquitDetail>().EquipmentName = haveEquipmets[i].EquipmentName;
                obj.GetComponent<WareHouseEquitDetail>().EquipmentCategory = haveEquipmets[i].EquipmentCategory;
                obj.GetComponent<WareHouseEquitDetail>().Nickname = haveEquipmets[i].Nickname;
                obj.GetComponent<WareHouseEquitDetail>().EquipmentID = haveEquipmets[i].EquipmentID;
                obj.GetComponent<Toggle>().group = HeroEquipmentPlaneItemsParent.GetComponent<ToggleGroup>();
            }
        }
    }

    public void CloseEquipSelectPlane()
    {
      
        for (int i = 0; i < HeroEquipmentPlaneItemsParent.transform.childCount; i++)
        {
         Destroy(HeroEquipmentPlaneItemsParent.transform.GetChild(i).gameObject);   
        }
        gameObject.GetComponent<Dock_View>().ShowHaveEquite();
        HeroEquipmentPlane.SetActive(false);
    }

    

}
