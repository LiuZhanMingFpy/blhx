using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    public GameObject cellPrefabs;
    public GameObject cellParent;
  
    public void ShowItemsList()   //背包项目显示
    {
        ShopServer shopServer=new ShopServer();
        Shop[] itemsList = shopServer.GetShopInfo();  //取sql数据
        for (int i = 0; i < itemsList.Length; i++)   //遍历显示
        {
            GameObject obj = GameObject.Instantiate(cellPrefabs, cellParent.transform);
            obj.name = itemsList[i].Nickname;
            obj.GetComponent<Toggle>().group = cellParent.GetComponent<ToggleGroup>();
            obj.GetComponent<Image>().sprite = Resources.Load<SpriteAtlas>("UI/Warehouse/Quality").GetSprite(itemsList[i].Quality.ToString());
            obj.transform.Find("Item").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("UI/Items/Equite/" + itemsList[i].Nickname);
            obj.transform.Find("ItemCount").GetComponent<Text>().text = itemsList[i].Num.ToString();
            obj.transform.Find("ItemName").GetComponent<Text>().text = itemsList[i].Name;
            //实例化物体脚本上获取表中信息存储起来 以便之后的调用
            obj.GetComponent<ItemDetailInfo>().BuyFashion = itemsList[i].BuyFashion;
            obj.GetComponent<ItemDetailInfo>().Enable = itemsList[i].Enable;
            obj.GetComponent<ItemDetailInfo>().Goodes = itemsList[i].Goodes;
            obj.GetComponent<ItemDetailInfo>().Nickname = itemsList[i].Nickname;
            obj.GetComponent<ItemDetailInfo>().Num = itemsList[i].Num;
            obj.GetComponent<ItemDetailInfo>().Price = itemsList[i].Price;
            obj.GetComponent<ItemDetailInfo>().Quality = itemsList[i].Quality;
            obj.GetComponent<ItemDetailInfo>().Species = itemsList[i].Species;
            obj.GetComponent<ItemDetailInfo>().Name = itemsList[i].Name; 
        }
    }


    public void RefreshItemsList()//刷新商店界面
    {
        for (int i = 0; i < cellParent.transform.childCount; i++)
        {
            Destroy(cellParent.transform.GetChild(i).gameObject);
        }
        ShowItemsList();
    }

    public void CleanAllItems()//关闭商店清空内容  放置重复加载子物体
    {
        for (int i = 0; i < cellParent.transform.childCount; i++)
        {
            Destroy(cellParent.transform.GetChild(i).gameObject);
        }
    }
}
