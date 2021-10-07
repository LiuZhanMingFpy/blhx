using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
public class ShopServer
{

    /// <summary>
    /// 调用商店方法
    /// </summary>
    /// <param name="userid">传入用户ID</param>
    /// <param name="num">可以设置的购买值</param>
    /// <param name="diamonds">需要消耗的钻石</param>
    /// <param name="what">购买什么</param>
    /// <returns></returns>
    public bool Buy(string userid, int num, int diamonds, string what)
    {
        UserInfoServer userInfoServer = new UserInfoServer();
        UserInfo userInfo = userInfoServer.GetUserInfo(userid);
        int before = 0;
        int afterDiamonds = 0;
        string sql = "";
        switch (what)
        {
            case "Resources":
                before = userInfo.Resources + num;
                afterDiamonds = userInfo.Diamonds - diamonds;
                sql = " UPDATE UserInfo SET Resources = " + before + ",Diamonds =" + afterDiamonds + "WHERE UserID = '" + userid + "'"; //需要执行的sql语句
                break;
            case "Gold":
                before = userInfo.Gold + num;
                afterDiamonds = userInfo.Diamonds - diamonds;
                sql = " UPDATE UserInfo SET Gold = " + before + ",Diamonds =" + afterDiamonds + "WHERE UserID = '" + userid + "'"; //需要执行的sql语句
                break;
            case "Diamonds":
                before = userInfo.Diamonds + num;
                sql = " UPDATE UserInfo SET Diamonds = " + before + ",Diamonds =" + afterDiamonds + "WHERE UserID = '" + userid + "'"; //需要执行的sql语句
                break;
        }

        //string sql = " UPDATE UserInfo SET Resources = " + afternum + "WHERE UserID = '" + userid + "'"; //需要执行的sql语句
        SetSQLServer setSQLServer = new SetSQLServer();
        return setSQLServer.Insert(sql);

    }

    /// <summary>
    /// 加载商店
    /// </summary>
    /// <returns></returns>
    public Shop[] GetShopInfo()
    {
        string sql = "select * from Shop where Enable=1";//需要执行的sql语句
        SetSQLServer setSQLServer = new SetSQLServer();
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);
        Shop[] shopinfo = new Shop[ds.Tables[0].Rows.Count];
        if (ds.Tables[0].Rows.Count != 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                shopinfo[i]=new Shop();
                shopinfo[i].Goodes = ds.Tables[0].Rows[i]["Goodes"].ToString();
                shopinfo[i].Price = int.Parse(ds.Tables[0].Rows[i]["Price"].ToString());
                shopinfo[i].Quality = int.Parse(ds.Tables[0].Rows[i]["Quality"].ToString());
                shopinfo[i].Num = int.Parse(ds.Tables[0].Rows[i]["Num"].ToString());
                shopinfo[i].BuyFashion = int.Parse(ds.Tables[0].Rows[i]["BuyFashion"].ToString());
                shopinfo[i].Species = int.Parse(ds.Tables[0].Rows[i]["Species"].ToString());
                shopinfo[i].Enable = int.Parse(ds.Tables[0].Rows[i]["Enable"].ToString());
                shopinfo[i].Nickname = ds.Tables[0].Rows[i]["Nickname"].ToString();
                shopinfo[i].Name = ds.Tables[0].Rows[i]["Name"].ToString();
            }
        }
        return shopinfo;
    }

    public bool BuyNotResources(string goodsid, int num, int price, int buyfashion, int species,string userid,string backpackid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        //查询是否有商品
        bool addorupdata = SelectGoods(backpackid, goodsid);
       
        string sql = "";
        string currency="";
        switch (buyfashion)
        {
            case 0:
                currency = "Diamonds";
                break;
            case 1:
                currency = "Gold";
                break;
            case 2:
                currency = "Resources";
                break;
            default:
                break;
        }

        if (species == 1)
        {
            Debug.Log("消耗品");
            //买消耗品
            //扣除语句
            string paysql = " UPDATE UserInfo SET " +currency+ "= "+currency+"-" + price + " WHERE UserID = '" + userid + "';";
            string harvestsql = "";
            //收入语句
            //如果有
            if (addorupdata)
            {
                 harvestsql = " INSERT INTO BackPackSpack(BackPackID, GoodsID, Num, Category) VALUES('"+backpackid+"', '"+goodsid+"', "+num+ ", 0);"
                + " UPDATE  BackPack SET MaterialScienceNum=MaterialScienceNum+1 WHERE UserID='"+ userid+ "' AND BackPackID ='" + backpackid + "' "
                + " SET @tran_error = @tran_error + @@ERROR;";
            }
            else
            {
                 harvestsql = " UPDATE BackPackSpack SET Num = Num + "+ num + " WHERE GoodsID = '"+goodsid+"' and BackPackID='" +backpackid+"';";
            }
            sql = TogetherSQL(paysql,harvestsql);
            
        }
        else if (species == 2)
        {
            Debug.Log("材料");
            //买材料
            //买消耗品
            //扣除语句
            string paysql = "UPDATE UserInfo SET " + currency + "= " + currency + "-" + price + " WHERE UserID = '" + userid + "';";
            string harvestsql = "";
            //收入语句
            //如果有
            if (addorupdata)
            {
                harvestsql = " INSERT INTO BackPackSpack(BackPackID, GoodsID, Num, Category) VALUES('" + backpackid + "', '" + goodsid + "', " + num + ", 1);"
               + " UPDATE  BackPack SET MaterialScienceNum=MaterialScienceNum+1 WHERE UserID='" + userid + "' AND BackPackID ='" + backpackid + "'"
               + " SET @tran_error = @tran_error + @@ERROR;";
            }
            else
            {
                harvestsql = " UPDATE BackPackSpack SET Num = Num + " + num + " WHERE GoodsID = '" + goodsid + "' and BackPackID='" + backpackid + "';";
            }
            sql = TogetherSQL(paysql, harvestsql);
        }
        else if (species == 3)
        {
            //Debug.Log("装备");
            //先查询装备然后把查询到的详细信息给移植到背包中
            Equipmet equipmet = SelectEquipmet(goodsid);
            //买装备           
            string paysql = " UPDATE UserInfo SET " + currency + "= " + currency + "-" + price + " WHERE UserID = '" + userid + "';";

            string harvestsql = " INSERT INTO HaveEquipmet(EquipmentID, EquipmentName, EquipmentQuality, EquipmentCategory,Description,Atk,AtkCD,AdditionalAbility,AdditionalValue,Price,Nickname,Level,BackPackID) VALUES(NEWID(),' " + equipmet.EquipmentName+ " ',"+ equipmet.EquipmentQuality + ","+ equipmet.EquipmentCategory + ",'"+equipmet.Description+ "',"+ equipmet.Atk+","+ equipmet.AtkCD+","+ equipmet.AdditionalAbility + ","+ equipmet.AdditionalValue + ","+ equipmet.Price+ " ,'"+ equipmet.Nickname+ "',1,'"+ backpackid + "');"
            + " UPDATE  BackPack SET QuantityNum=QuantityNum+1 WHERE UserID='" + userid + "' AND BackPackID ='" + backpackid + "' "
            + " SET @tran_error = @tran_error + @@ERROR;";
            sql = TogetherSQL(paysql, harvestsql);
        }
        //Debug.Log(sql);

        return setSQLServer.Insert(sql);
    }
    /// <summary>
    /// 拼接sql
    /// </summary>
    /// <param name="paysql">支付货币sql</param>
    /// <param name="harvest">获得物品SQL</param>
    /// <returns></returns>
    public string TogetherSQL(string paysql, string harvest) {

       string sql = " BEGIN TRAN"
               + " DECLARE @tran_error int;"
               + " SET @tran_error = 0;"
               + " BEGIN TRY "
               + paysql
               //+ "UPDATE UserInfo SET Diamonds = Diamonds - 500 WHERE UserID = 'system';"
               + " SET @tran_error = @tran_error + @@ERROR;"
               //+ "INSERT INTO BackPackSpack(BackPackID, GoodsID, Num, Category) VALUES('001', 'a001', 1, 0);"
               + harvest
               + " SET @tran_error = @tran_error + @@ERROR;"
               + " END TRY"
               + " BEGIN CATCH"
               + " PRINT '出现异常，错误编号：' + convert(varchar, error_number()) + ',错误消息：' + error_message()"
               + " SET @tran_error = @tran_error + 1"
               + " END CATCH"
               + " IF(@tran_error > 0)"
               + " BEGIN"
               + " ROLLBACK TRAN;"
               + " PRINT '失败';"
               + " END"
               + " ELSE"
               + " BEGIN"
               + " COMMIT TRAN;"
               + " PRINT '成功!';"
               + " END ";
        return sql;
    }

    /// <summary>
    /// 查询是否有这个物品;
    /// </summary>
    /// <param name="backpackid"></param>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public bool SelectGoods(string backpackid,string goodsid) {
        string sql = "select * from BackPackSpack where BackPackID='"+ backpackid + "' AND GoodsID='"+goodsid+ "'";//需要执行的sql语句
        SetSQLServer setSQLServer = new SetSQLServer();
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);
     
        if (ds.Tables[0].Rows.Count==0)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 查询某件物品
    /// </summary>
    /// <param name="goodsid">物品ID</param>
    /// <returns></returns>
    public Equipmet SelectEquipmet(string goodsid)
    {
        string sql = "select * from Equipment where EquipmentID='"+ goodsid + " ' ";
        SetSQLServer setSQLServer = new SetSQLServer();
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);
        Equipmet equipmet = new Equipmet();
        if (ds.Tables[0].Rows.Count==1)
        {
            equipmet.EquipmentName= ds.Tables[0].Rows[0]["EquipmentName"].ToString();
            equipmet.EquipmentQuality = int.Parse(ds.Tables[0].Rows[0]["EquipmentQuality"].ToString());
            equipmet.EquipmentCategory = int.Parse(ds.Tables[0].Rows[0]["EquipmentCategory"].ToString());
            equipmet.Description = ds.Tables[0].Rows[0]["Description"].ToString();
            equipmet.Atk = int.Parse(ds.Tables[0].Rows[0]["Atk"].ToString());
            equipmet.AtkCD = int.Parse(ds.Tables[0].Rows[0]["AtkCD"].ToString());
            equipmet.AdditionalAbility = int.Parse(ds.Tables[0].Rows[0]["AdditionalAbility"].ToString());
            equipmet.AdditionalValue = int.Parse(ds.Tables[0].Rows[0]["AdditionalValue"].ToString());
            equipmet.Price = int.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
            equipmet.Nickname = ds.Tables[0].Rows[0]["Nickname"].ToString();           
        }

        return equipmet;
    }
}
