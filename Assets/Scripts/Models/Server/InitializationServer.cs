using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
public class InitializationServer
{

    public void Initalization(string userid, string serverid, string secretary)
    {
       
        UserInfoServer userInfoServer = new UserInfoServer();
        Role role = new Role();
        RoleServer roleServer = new RoleServer();
        UserInfo userInfo = userInfoServer.GetUserInfo(userid);
        string id = roleServer.GetNewID();//生成随机标识码
        bool backpackOK = InitalizationBackPack(userInfo.UserID, userInfo.BackPackID);
        bool dockOK = InitalizationDock(userid, userInfo.DockID);
        bool lineupOK = InitalizationLineup(userid, userInfo.LineupID,id);       
        role = roleServer.GetInitialRole(secretary);//实体舰娘值                      
        SetDock(userInfo.DockID, id);//写入船坞
        roleServer.GetRole(role, id);//获取赋值
    }
    /// <summary>
    /// 初始化用户信息/在传入名字之后
    /// </summary>
    /// <param name="userid">传入用户ID</param>
    /// <param name="username">传入用户名</param>
    /// <param name="serverid">传入服务器ID</param>
    /// <returns></returns>
    public bool InitalizationUserInfo(string userid, string username, string serverid,string secretary)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        // DataSet ds = new DataSet();
        string sql = "INSERT INTO  UserInfo(UserID,UserName,Military,Resources,Gold,Diamonds,BackPackID,DockID,LineupID,Secretary,Experience,[Level],ServerID) VALUES('" + userid + "',' " + username + "',001,500,500,0,NEWID(),NEWID(),NEWID(),'"+secretary+ "',0,1,'" + serverid + "')";
        
        return setSQLServer.Insert(sql);
    }
    /// <summary>
    /// 在初始化用户之后初始化背包，把生成的用户ID和背包ID传入
    /// </summary>
    /// <param name="userid">传入用户ID</param>
    /// <param name="backpackid">传入背包ID</param>
    /// <returns></returns>
    public bool InitalizationBackPack(string userid, string backpackid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        //DataSet ds = new DataSet();
        string sql = "INSERT INTO  BackPack(UserID, BackPackID, QuantityNum,QuantityUpLimit,MaterialScienceNum,MaterialScienceUpLimit) VALUES('" + userid + "','" + backpackid + "',0,100,0,100)";
        return setSQLServer.Insert(sql);
    }
    /// <summary>
    /// 往背包里增加东西时候所用的方法
    /// </summary>
    /// <param name="backpackid">背包ID</param>
    /// <param name="goods">物品ID</param>
    /// <param name="num">物品数量</param>
    /// <param name="category">物品种类</param>
    /// <returns></returns>
    public bool AddBackPackSpack(string backpackid, string goods, int num, int category)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        //DataSet ds = new DataSet();
        string sql = "INSERT INTO  BackPackSpack(BackPackID, GoodsID,Num,Category) VALUES('" + backpackid + "','" + goods + "'," + num + "," + category + ")";
        return setSQLServer.Insert(sql);
    }
    /// <summary>
    /// 增加装备的方法
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public bool InitalizationHaveEquipmet(string goodsid, string userid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        ShopServer shopServer = new ShopServer();
        Equipmet equipmet = shopServer.SelectEquipmet(goodsid);
        string sql = "IINSERT INTO HaveEquipmet(EquipmentID, EquipmentName, EquipmentQuality, EquipmentCategory,Description,Atk,AtkCD,AdditionalAbility,AdditionalValue,Price,Nickname,Level,UserID) VALUES(NEWID(),' " + equipmet.EquipmentName + " '," + equipmet.EquipmentQuality + "," + equipmet.EquipmentCategory + ",'" + equipmet.Description + "'," + equipmet.Atk + "," + equipmet.AtkCD + "," + equipmet.AdditionalAbility + "," + equipmet.AdditionalValue + "," + equipmet.Price + " ,'" + equipmet.Nickname + "',1,'" + userid + "')";
        return setSQLServer.Insert(sql);
    }

    /// <summary>
    /// 初始化船坞
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public bool InitalizationDock(string userid,string id)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql = "INSERT INTO  Dock(DockID, UserID,DockNum,DockUpLimit) VALUES('"+ id + "','" + userid + "',0,100)";
        return setSQLServer.Insert(sql);
    }
    /// <summary>
    /// 初始化阵容
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public bool InitalizationLineup(string userid,string lineupID, string secretary)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql = "INSERT INTO  Lineup(UserID, LineupID,ForwardFront,ForwardCenter,ForwardGuard,BehindFront,BehindCenter,BehindGuard) VALUES('" + userid + "','"+ lineupID + "',null,'"+secretary+ "',null,null,null,null)";
        return setSQLServer.Insert(sql);
    }
    /// <summary>
    /// 写入船坞
    /// </summary>
    /// <param name="dockid"></param>
    /// <param name="roleid"></param>
    public void SetDock(string dockid, string roleid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        string sql = "INSERT INTO  DockSpack (DockID,RoleID) VALUES('" + dockid + "','" + roleid + "')";
        setSQLServer.Insert(sql);
    }
    /// <summary>
    /// 调用初始起名字方法
    /// </summary>
    /// <param name="username"></param>
    /// <param name="userid"></param>
    /// <returns></returns>
    //public bool SetUserName(string username,string userid) {
    //    SetSQLServer setSQLServer = new SetSQLServer();
    //    DataSet ds = new DataSet();
    //    string sql = " UPDATE UserInfo SET UserName = '" + username + "' WHERE UserID=' "+ userid + " ' ";
    //    return setSQLServer.Insert(sql);
    //}

}
