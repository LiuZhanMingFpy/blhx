using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// 关于背包的server
/// </summary>
public class BackPackServer
{
    /// <summary>
    /// 获取背包数量
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public BackPack GetBackPackSize(string userid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        BackPack backPack = new BackPack();
        string sql = "select * from BackPack where UserID ='" + userid + "'";
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);
        if (ds.Tables[0].Rows.Count == 1)
        {
            backPack.MaterialScienceNum = int.Parse(ds.Tables[0].Rows[0]["MaterialScienceNum"].ToString());
            backPack.MaterialScienceUpLimit = int.Parse(ds.Tables[0].Rows[0]["MaterialScienceUpLimit"].ToString());
            backPack.QuantityNum = int.Parse(ds.Tables[0].Rows[0]["QuantityNum"].ToString());
            backPack.QuantityUpLimit = int.Parse(ds.Tables[0].Rows[0]["QuantityUpLimit"].ToString());
        }
        return backPack;
    }
    /// <summary>
    /// 传入Userid获取背包物品信息
    /// </summary>
    /// <param name="userid">UserId</param>
    public Goods[] GetBackPackSpack(string userid)
    {

        SetSQLServer setSQLServer = new SetSQLServer();
        string sql =
                     " select Goods.GoodsID,GoodsName,GoodsQuality,GoodsCategory,Nickname,Price,Effect,Value,BackPackSpack.Num"
                     + " from BackPack,BackPackSpack,Goods"
                     + " where BackPack.BackPackID = BackPackSpack.BackPackID and BackPackSpack.GoodsID = Goods.GoodsID"
                     + " and BackPack.UserID = '" + userid + "'";
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);
        Goods[] goods = new Goods[ds.Tables[0].Rows.Count];
        if (ds.Tables[0].Rows.Count != 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                goods[i] = new Goods();
                goods[i].GoodsID = ds.Tables[0].Rows[i]["GoodsID"].ToString();
                goods[i].GoodsName = ds.Tables[0].Rows[i]["GoodsName"].ToString();
                goods[i].GoodsQuality = int.Parse(ds.Tables[0].Rows[i]["GoodsQuality"].ToString());
                goods[i].GoodsCategory = int.Parse(ds.Tables[0].Rows[i]["GoodsCategory"].ToString());
                //goods[i].Description = ds.Tables[0].Rows[i]["Description"].ToString();
                goods[i].Nickname = ds.Tables[0].Rows[i]["Nickname"].ToString();
                goods[i].Price = int.Parse(ds.Tables[0].Rows[i]["Price"].ToString());
                goods[i].Effect = int.Parse(ds.Tables[0].Rows[i]["Effect"].ToString());
                goods[i].Value = int.Parse(ds.Tables[0].Rows[i]["Value"].ToString());
                goods[i].Num = int.Parse(ds.Tables[0].Rows[i]["Num"].ToString());
            }
        }
        return goods;
    }
    /// <summary>
    /// 读取以有的装备
    /// </summary>
    /// <param name="userid">传入USerID</param>
    /// <returns></returns>
    public HaveEquipmet[] GetHaveEquipmet(string userid)
    {
      
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql =
                    "select HaveEquipmet.*"
                    + " from BackPack,HaveEquipmet"
                    + " where BackPack.BackPackID = HaveEquipmet.BackPackID"
                    + " and BackPack.UserID = '" + userid + "';";
        DataSet ds = new DataSet();

        ds = setSQLServer.Select(sql);      
        HaveEquipmet[] haveEquipmet = new HaveEquipmet[ds.Tables[0].Rows.Count];
        if (ds.Tables[0].Rows.Count!=0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                haveEquipmet[i] = new HaveEquipmet();
                haveEquipmet[i].EquipmentID = ds.Tables[0].Rows[i]["EquipmentID"].ToString();
                haveEquipmet[i].EquipmentName = ds.Tables[0].Rows[i]["EquipmentName"].ToString();
                haveEquipmet[i].EquipmentQuality = int.Parse(ds.Tables[0].Rows[i]["EquipmentQuality"].ToString());
                haveEquipmet[i].EquipmentCategory = int.Parse(ds.Tables[0].Rows[i]["EquipmentCategory"].ToString());
                haveEquipmet[i].Description = ds.Tables[0].Rows[i]["Description"].ToString();
                haveEquipmet[i].Atk = int.Parse(ds.Tables[0].Rows[i]["Atk"].ToString());
                haveEquipmet[i].AtkCD = int.Parse(ds.Tables[0].Rows[i]["AtkCD"].ToString());
                haveEquipmet[i].AdditionalAbility = int.Parse(ds.Tables[0].Rows[i]["AdditionalAbility"].ToString());
                haveEquipmet[i].AdditionalValue = int.Parse(ds.Tables[0].Rows[i]["AdditionalValue"].ToString());
                haveEquipmet[i].Price = int.Parse(ds.Tables[0].Rows[i]["Price"].ToString());
                haveEquipmet[i].Nickname = ds.Tables[0].Rows[i]["Nickname"].ToString();
                haveEquipmet[i].Level = int.Parse(ds.Tables[0].Rows[i]["Level"].ToString());
                haveEquipmet[i].BackPackID = ds.Tables[0].Rows[i]["BackPackID"].ToString();
            }
        }
        return haveEquipmet;
    }

    public void SellSomSing(string id,int num,int species) {

    }
}
