using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// 拥有的角色操作
/// </summary>
public class RoleServer  {

    /// <summary>
    /// 角色详情
    /// </summary>
    /// <param name="roleid">角色ID</param>
    public HaveRole GetRoleInfo(string roleid) {
       
        SetSQLServer setSQLServer = new SetSQLServer();
        HaveRole haveRole = new HaveRole();
        string sql = "select * from HaveRole where RoleID ='" + roleid + "'";
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);
        
        if (ds.Tables[0].Rows.Count == 1)
        {
            haveRole.RoleID = ds.Tables[0].Rows[0]["RoleID"].ToString();
            haveRole.RoleName = ds.Tables[0].Rows[0]["RoleName"].ToString();
            haveRole.Nickname = ds.Tables[0].Rows[0]["Nickname"].ToString();
            haveRole.Durable = int.Parse(ds.Tables[0].Rows[0]["Durable"].ToString());
            haveRole.TheShelling = int.Parse(ds.Tables[0].Rows[0]["TheShelling"].ToString())+ int.Parse(ds.Tables[0].Rows[0]["AddMainGun"].ToString());
            haveRole.AirDefense = int.Parse(ds.Tables[0].Rows[0]["AirDefense"].ToString());
            haveRole.AntiSubmarine = int.Parse(ds.Tables[0].Rows[0]["AntiSubmarine"].ToString());
            haveRole.Armor = int.Parse(ds.Tables[0].Rows[0]["Armor"].ToString());
            haveRole.TheTorpedo = int.Parse(ds.Tables[0].Rows[0]["TheTorpedo"].ToString()) +int.Parse(ds.Tables[0].Rows[0]["AddAuxiliary"].ToString());
            haveRole.Aviation = int.Parse(ds.Tables[0].Rows[0]["Aviation"].ToString())+ int.Parse(ds.Tables[0].Rows[0]["AddViceGun"].ToString());
            haveRole.Loading = int.Parse(ds.Tables[0].Rows[0]["Loading"].ToString());
            haveRole.Motor = int.Parse(ds.Tables[0].Rows[0]["Motor"].ToString());
            haveRole.Consumption = int.Parse(ds.Tables[0].Rows[0]["Consumption"].ToString());
            haveRole.Camp = int.Parse(ds.Tables[0].Rows[0]["Camp"].ToString());
            haveRole.Price = int.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
            haveRole.SkillbarID = ds.Tables[0].Rows[0]["SkillbarID"].ToString();
            haveRole.Stars = int.Parse(ds.Tables[0].Rows[0]["Stars"].ToString());
            haveRole.Level = int.Parse(ds.Tables[0].Rows[0]["Level"].ToString());
            haveRole.Quality = int.Parse(ds.Tables[0].Rows[0]["Quality"].ToString());

            
        }
        
        return haveRole;
      }
    /// <summary>
    /// 角色升级
    /// </summary>
    /// <param name="roleid">角色ID</param>
    public bool UpLevel(string roleid,int quality) {   
        SetSQLServer setSQLServer = new SetSQLServer();
        int how = 0;
        switch (quality)
        {
            case 0:
                how = 2;
                break;
            case 1:
                how = 5;
                break;
            case 2:
                how = 10;
                break;
            case 3:
                how = 15;
                break;          
        }
        // string sql = " UPDATE HaveRole SET Level = Level + 1 WHERE RoleID = '" + roleid + "'"; 
        string sql = "UPDATE HaveRole SET Level = Level + 1 ,Durable=Durable+"+how+",TheShelling=TheShelling+"+how+ ",AirDefense=AirDefense+"+how+ ",AntiSubmarine=AntiSubmarine+"+how+",Armor=Armor+"+how+",TheTorpedo=TheTorpedo+"+how+",Aviation=Aviation+"+how+",Loading=Loading+"+how+",Motor=Motor+"+how+""
      +" WHERE RoleID = '"+ roleid + "'";
        return setSQLServer.Insert(sql);
    }
    /// <summary>
    /// 角色升星
    /// </summary>
    /// <param name="roleid">角色ID</param>
    public void UpStars(string roleid) {
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql = "";
    }

    /// <summary>
    /// 卖出角色
    /// </summary>
    /// <param name="roleid">角色ID</param>
    public void SellRole(string roleid)
    {

    }
    /// <summary>
    /// 查看技能
    /// </summary>
    /// <param name="roleid">角色ID</param>
    public void SkillInfo(string roleid)
    {

    }
    /// <summary>
    /// 升级技能
    /// </summary>
    /// <param name="roleid">升级技能</param>
    public void UpSkillLevel(string skillid)
    {

    }
    /// <summary>
    /// 获取初始船数据
    /// </summary>
    public Role GetInitialRole(string roleid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        Role role = new Role();
        DataSet ds = new DataSet();
        string sql = "select * from Role where RoleID = '" + roleid + "'";
        ds = setSQLServer.Select(sql);
        if (ds.Tables[0].Rows.Count == 1)
        {
            role.RoleName = ds.Tables[0].Rows[0]["RoleName"].ToString();
            role.Nickname = ds.Tables[0].Rows[0]["Nickname"].ToString();
            role.SkillbarID = ds.Tables[0].Rows[0]["Nickname"].ToString();
            role.Durable = int.Parse(ds.Tables[0].Rows[0]["Durable"].ToString());
            role.TheShelling = int.Parse(ds.Tables[0].Rows[0]["TheShelling"].ToString());
            role.AirDefense = int.Parse(ds.Tables[0].Rows[0]["AirDefense"].ToString());
            role.AntiSubmarine = int.Parse(ds.Tables[0].Rows[0]["AntiSubmarine"].ToString());
            role.Armor = int.Parse(ds.Tables[0].Rows[0]["Armor"].ToString());
            role.TheTorpedo = int.Parse(ds.Tables[0].Rows[0]["TheTorpedo"].ToString());
            role.Aviation = int.Parse(ds.Tables[0].Rows[0]["Aviation"].ToString());
            role.Loading = int.Parse(ds.Tables[0].Rows[0]["Loading"].ToString());
            role.Motor = int.Parse(ds.Tables[0].Rows[0]["Motor"].ToString());
            role.Consumption = int.Parse(ds.Tables[0].Rows[0]["Consumption"].ToString());
            role.Camp = int.Parse(ds.Tables[0].Rows[0]["Camp"].ToString());
            role.Position = int.Parse(ds.Tables[0].Rows[0]["Position"].ToString());
            role.Quality = int.Parse(ds.Tables[0].Rows[0]["Quality"].ToString());
        }
       
        return role;
    }
    /// <summary>
    /// 把初始船赋值已拥有的船
    /// </summary>
    /// <param name="roleid"></param>
    public void GetRole(Role role, string id)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql = "INSERT INTO  HaveRole(RoleID, RoleName,Nickname,Durable,TheShelling,AirDefense,AntiSubmarine,Armor,TheTorpedo,Aviation,Loading,Motor,Consumption,Camp,Position,Price,SkillbarID,Stars,Level,Quality,AddMainGun,AddViceGun,AddAuxiliary) VALUES('" + id + "','" + role.RoleName + "','" + role.Nickname + "'," + role.Durable + "," + role.TheShelling + "," + role.AirDefense + ", " + role.AntiSubmarine + ", " + role.Armor + ", " + role.TheTorpedo + ", " + role.Aviation + ", " + role.Loading + ", " + role.Motor + "," + role.Consumption + "," + role.Camp + ", " + role.Position + ",500,'" + role.SkillbarID + "',2,1," + role.Quality + ",0,0,0)";
        setSQLServer.Insert(sql);
    }
    /// <summary>
    /// 生成一个随机数
    /// </summary>
    /// <returns></returns>
    public string GetNewID()
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql = "select NEWID() AS ID;";
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);
        string ID = ds.Tables[0].Rows[0]["ID"].ToString();
        return ID;
    }
}
