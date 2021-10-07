using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WareHouseEquitDetail : MonoBehaviour
{
    

    public string EquipmentID;

    /// <summary>
    /// 装备名称
    /// </summary>
    public string EquipmentName;

    /// <summary>
    /// 装备品质
    /// </summary>
    public int EquipmentQuality;

    /// <summary>
    /// 装备类别
    /// </summary>
    public int EquipmentCategory;

    /// <summary>
    /// 备注说明
    /// </summary>
    public string Description;

    /// <summary>
    /// 攻击力
    /// </summary>
    public int Atk;

    /// <summary>
    /// 攻击间隔
    /// </summary>
    public int AtkCD;

    /// <summary>
    /// 附加能力
    /// </summary>
    public int AdditionalAbility;

    /// <summary>
    /// 附加数值
    /// </summary>
    public int AdditionalValue;

    /// <summary>
    /// 贩卖价格
    /// </summary>
    public int Price;

    /// <summary>
    /// 别称
    /// </summary>
    public string Nickname;

    /// <summary>
    /// 等级
    /// </summary>
    public int Level;

    /// <summary>
    /// 用户ID
    /// </summary>
    public string BackPackID;

    void Start()
    {
         gameObject.GetComponent<Toggle>().onValueChanged.AddListener(BeSelect);
    }

    public void BeSelect(bool ison)
    {
        if (ison)
        {
            GameObject currentEquite=GameObject.Find("Canvas").transform.Find("HeroEquipmentPlane/ChangeEquip_bg/CurrentEquip_bg/CurrentEquip").gameObject;
            currentEquite.GetComponent<Image>().sprite = gameObject.transform.Find("Item").GetComponent<Image>().sprite;
            GameObject equipmentID=GameObject.Find("Canvas").transform.Find("HeroDetailPlane/EquipmentId").gameObject;
            equipmentID.GetComponent<Text>().text = EquipmentID;
        }
    }


}
