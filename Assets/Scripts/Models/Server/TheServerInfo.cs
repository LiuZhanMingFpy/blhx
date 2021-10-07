using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// 获取服务器数据
/// </summary>
public class TheServerInfo  {
    #region 单例
    private TheServerInfo()
    {

    }

    private static TheServerInfo instance;

    public static TheServerInfo Instance
    {
        get {
            if (instance == null)
            {
                instance = new TheServerInfo();
            }

            return instance;
        }

    }

    #endregion

    /// <summary>
    /// 读取服务器列表
    /// </summary>
    /// <returns></returns>
    public Server[] GetServerInfo() {
        SetSQLServer setSQLServer = new SetSQLServer();       
        DataSet ds = new DataSet();        
        string sql = "select * from Server;";
        ds=setSQLServer.Select(sql);
       
        Server[] server = new Server[ds.Tables[0].Rows.Count];
       
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            server[i] = new Server();
            server[i].ServerID = ds.Tables[0].Rows[i]["ServerID"].ToString();
            server[i].ServerName = ds.Tables[0].Rows[i]["ServerName"].ToString();
           
        }

        return server;
    }
    /// <summary>
    /// 查看在某个服务器上有没有创建账号
    /// </summary>
    /// <param name="thetotalID"></param>
    public ServerByUser[] GetServerByUser(string thetotalID) {
        SetSQLServer setSQLServer = new SetSQLServer();
        DataSet ds = new DataSet();
        string sql = "select * from ServerByUser where TheTotalID ='" + thetotalID + "'";
        ds = setSQLServer.Select(sql);
        ServerByUser[] serverByUser = new ServerByUser[ds.Tables[0].Rows.Count];
        if (ds.Tables[0].Rows.Count!=0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                serverByUser[i] = new ServerByUser();
                serverByUser[i].ServerID = ds.Tables[0].Rows[i]["ServerID"].ToString();
                serverByUser[i].UserID = ds.Tables[0].Rows[i]["UserID"].ToString();

            }
            return serverByUser;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 如果没有账号，根据登陆的账号ID写入新的服务器和服务器对应的玩家ID
    /// </summary>
    /// <param name="thetotalid">总ID</param>
    /// <param name="userid">玩家ID</param>
    /// <param name="serverid">服务器ID</param>
    /// <returns></returns>
    public bool SetServerByUser(string thetotalid,string serverid) {
        SetSQLServer setSQLServer = new SetSQLServer();
        DataSet ds = new DataSet();
        string sql = "INSERT INTO  ServerByUser(ServerID, UserID, TheTotalID) VALUES('" + serverid + "',newId() ,  '"+ thetotalid + "')";

        return setSQLServer.Insert(sql);       
    }
    public string GetUserID(string thetotalid, string serverid)
    {
        string userid = "";
        SetSQLServer setSQLServer = new SetSQLServer();
        DataSet ds = new DataSet();
        string sql = "select * from ServerByUser where ServerID = '" + serverid + "' and TheTotalID='" + thetotalid + "' ";
        ds = setSQLServer.Select(sql);
        if (ds.Tables[0].Rows.Count == 1)
        {
            userid = ds.Tables[0].Rows[0]["UserID"].ToString();
        }


       
        return userid;
    }
}
