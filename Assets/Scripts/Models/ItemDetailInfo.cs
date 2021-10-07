using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ItemDetailInfo : MonoBehaviour
{   //对应商店表中每个物品的各项信息
    public string Goodes;
    public int Price;
    public int Quality;
    public int Num;
    public int BuyFashion;
    public int Species;
    public int Enable;
    public string Nickname;
    public string Name;

    public Image BFimage;
    public Sprite Diamond;
    public Sprite Oil;
    public Sprite Gold;
    public Text BFcount;

    private ShopServer sr=new ShopServer();
    private Button buy;

    UserInfo ui = new UserInfo();
     UserInfoServer userInfoServer = new UserInfoServer();
    void Start()
    {
        //当此物体被实例化出来的时候立刻获取这几个资源以便用于事件监听使用
        BFimage = GameObject.Find("/Canvas/StorePlane/CostType").GetComponent<Image>();
        BFcount = GameObject.Find("/Canvas/StorePlane/CostType/Count").GetComponent<Text>();
        Diamond = Resources.Load<SpriteAtlas>("UI/Main/OilGoldDiamond").GetSprite("MainPlane_diamond");
        Oil = Resources.Load<SpriteAtlas>("UI/Main/OilGoldDiamond").GetSprite("MainPlane_oil");
        Gold = Resources.Load<SpriteAtlas>("UI/Main/OilGoldDiamond").GetSprite("MainPlane_money");
        gameObject.GetComponent<Toggle>().onValueChanged.AddListener(ChangeBuyFashionShow);
    }
    //toggle选中时调用的方法
    public void ChangeBuyFashionShow(bool ison)
    {
        if (ison)
        {
           
            switch (BuyFashion)
            {
                case 0:
                    BFimage.sprite = Diamond;
                    break;
                case 1:
                    BFimage.sprite = Gold;
                    break;
                case 2:
                    BFimage.sprite = Oil;
                    break;
            }

            BFcount.text = Price.ToString();
        }
        //被选中的商品会更改购买按钮的回调函数
        buy = GameObject.Find("Canvas/StorePlane/Buy_btn").GetComponent<Button>();
        buy.onClick.RemoveAllListeners();//每次清空确保不会重复购买
        buy.onClick.AddListener(BuyYesOrNo);//出发购买方法
 

    }

    //封装购买方法
    public void BuyYesOrNo()
    {
        ui = userInfoServer.GetUserInfo(ServerByUser.Instance().UserID);
        sr.BuyNotResources(Goodes, Num, Price, BuyFashion, Species, ui.UserID, ui.BackPackID);//"system"是Userid 到时候登录页获取后传进来
        MainPlaneView MPV = GameObject.Find("Main Camera").GetComponent<MainPlaneView>();
        MPV.ShowMyValueInStore();//点击后出发刷新页面 金币 石油 砖石数量

    }


}
