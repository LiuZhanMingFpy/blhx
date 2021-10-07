using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
public class RoleEquipmetServer {

    /// <summary>
    /// 角色装备栏点击装备的时候读取拥有的装备
    /// </summary>
    /// <param name="equipmentcategory"></param>
    /// <param name="backpackid"></param>
    public HaveEquipmet[] GetHaveEquipmet(int equipmentcategory,string backpackid) {     
        SetSQLServer setSQLServer = new SetSQLServer();       
        string sql = "select * from HaveEquipmet where EquipmentCategory="+equipmentcategory + " and BackPackID='"+backpackid+"'";
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

    public RoleEquipmet GetRoleEquipmet(string roleid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql = "select * from RoleEquipmet where RoleID='"+roleid+"'";
        DataSet ds = new DataSet();
        RoleEquipmet roleEquipmet = new RoleEquipmet();
        ds = setSQLServer.Select(sql);
        if (ds.Tables[0].Rows.Count == 1)
        {
            roleEquipmet.MainGun = ds.Tables[0].Rows[0]["MainGun"].ToString();
            roleEquipmet.ViceGun = ds.Tables[0].Rows[0]["ViceGun"].ToString();
            roleEquipmet.AirdefenseGun = ds.Tables[0].Rows[0]["AirdefenseGun"].ToString();
            roleEquipmet.Auxiliary1 = ds.Tables[0].Rows[0]["Auxiliary1"].ToString();
            roleEquipmet.Auxiliary2 = ds.Tables[0].Rows[0]["Auxiliary2"].ToString();
        }
        return roleEquipmet;
    }
    /// <summary>
    /// 传入获得的装备ID 获取装备的详细信息
    /// </summary>
    /// <param name="equipmetid"></param>
    public HaveEquipmet GetEquipmetInfoByID(string equipmetid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql = "select * from HaveEquipmet where EquipmentID='"+equipmetid+"'";
        DataSet ds = new DataSet();
        HaveEquipmet haveEquipmet = new HaveEquipmet();
        ds = setSQLServer.Select(sql);
        if (ds.Tables[0].Rows.Count == 1)
        {
            haveEquipmet.EquipmentID = ds.Tables[0].Rows[0]["EquipmentID"].ToString();
            haveEquipmet.EquipmentName = ds.Tables[0].Rows[0]["EquipmentName"].ToString();
            haveEquipmet.EquipmentQuality = int.Parse(ds.Tables[0].Rows[0]["EquipmentQuality"].ToString());
            haveEquipmet.EquipmentCategory = int.Parse(ds.Tables[0].Rows[0]["EquipmentCategory"].ToString());
            haveEquipmet.Description = ds.Tables[0].Rows[0]["Description"].ToString();
            haveEquipmet.Atk = int.Parse(ds.Tables[0].Rows[0]["Atk"].ToString());
            haveEquipmet.AtkCD = int.Parse(ds.Tables[0].Rows[0]["AtkCD"].ToString());
            haveEquipmet.AdditionalAbility = int.Parse(ds.Tables[0].Rows[0]["AdditionalAbility"].ToString());
            haveEquipmet.AdditionalValue = int.Parse(ds.Tables[0].Rows[0]["AdditionalValue"].ToString());
            haveEquipmet.Price = int.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
            haveEquipmet.Nickname = ds.Tables[0].Rows[0]["Nickname"].ToString();
            haveEquipmet.Level = int.Parse(ds.Tables[0].Rows[0]["Level"].ToString());
            haveEquipmet.BackPackID = ds.Tables[0].Rows[0]["BackPackID"].ToString();
        }
        return haveEquipmet;
    }

    public bool SelectEquipmet(string roleid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql = "select * from RoleEquipmet where RoleID='"+ roleid + "'";
        DataSet ds = new DataSet();
        ds= setSQLServer.Select(sql);
        if (ds.Tables[0].Rows.Count==0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 装上装备
    /// </summary>
    /// <param name="roleid">角色ID</param>
    /// <param name="where">装备位置</param>
    public bool ChangeEquipmet(string roleid,string where,string equipmentid)
    {
        HaveEquipmet haveEquipmet = new HaveEquipmet();
        SetSQLServer setSQLServer = new SetSQLServer();
        string oldrole = GetOldRoleId(where, equipmentid);
        bool ishave= SelectEquipmet(roleid);
        string paysql = " UPDATE RoleEquipmet SET " + where + " = null WHERE " + where + " = '" + equipmentid + "'"
                        + " SET @tran_error = @tran_error + @@ERROR;";
        if (ishave)
        {
             paysql +="INSERT INTO RoleEquipmet(RoleID,"+ where + ") VALUES('"+ roleid + "','"+equipmentid+"');";
        }
        else
        {
             paysql +=" UPDATE RoleEquipmet SET " + where + " ='" + equipmentid + "'  WHERE RoleID = '" + roleid + "'";
        }                            
        int atk = GetEquipmetInfoByID(equipmentid).Atk;
        string Add = " ";
        switch (where)
        {
            case "MainGun":
                Add = "AddMainGun";
                break;
            case "AirdefenseGun":
                Add = "AddViceGun";
                break;
            case "Auxiliary1":
                Add = "AddAuxiliary";
                break;
        }
        string harvest = " UPDATE HaveRole SET " + Add + " = 0 WHERE RoleID = '" + oldrole + "'"
                         + " SET @tran_error = @tran_error + @@ERROR;"
                         + " UPDATE HaveRole SET " + Add + " =" + atk + "  WHERE RoleID = '" + roleid + "';";
        string sql = TogetherSQL(paysql, harvest);

        return setSQLServer.Insert(sql);
    }
    /// <summary>
    /// 卸下装备
    /// </summary>
    /// <param name="roleid"></param>
    /// <param name="where"></param>
    public bool UnloadEquipmet(string roleid, string where,string equipmentid)
    {
        HaveEquipmet haveEquipmet = new HaveEquipmet();
        SetSQLServer setSQLServer = new SetSQLServer();
        string paysql = "UPDATE RoleEquipmet SET " + where + " =null  WHERE RoleID = '" + roleid + "'";
        int atk = GetEquipmetInfoByID(equipmentid).Atk;
        string Add = "";
        switch (where)
        {
            case "MainGun":
                Add = "AddMainGun";
                break;
            case "AirdefenseGun":
                Add = "AddViceGun";
                break;
            case "Auxiliary1":
                Add = "AddAuxiliary";
                break;
        }
        string harvest = "UPDATE HaveRole SET  "+Add+" =0 WHERE RoleID = '" + roleid + "';";
        string sql = TogetherSQL(paysql, harvest);
        return setSQLServer.Insert(sql);
    }

    /// <summary>
    /// 拼接事务
    /// </summary>
    /// <param name="paysql"></param>
    /// <param name="harvest"></param>
    /// <returns></returns>
    public string TogetherSQL(string paysql, string harvest)
    {

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
    /// 获取原来装备的角色ID
    /// </summary>
    /// <param name="where"></param>
    /// <param name="equipmentid"></param>
    /// <returns></returns>
    public string GetOldRoleId(string where, string equipmentid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql = "select RoleID  from RoleEquipmet where " + where + "= '" + equipmentid + "'";
        DataSet ds = new DataSet();
        string oldrole = "";
        ds = setSQLServer.Select(sql);
        if (ds.Tables[0].Rows.Count==1)
        {
             oldrole = ds.Tables[0].Rows[0]["RoleID"].ToString();
        }

       
        return oldrole;
    }
}
