using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;

public class UserInfoServer
{



    public UserInfo GetUserInfo(string userID)
    {
        
        string sql = "select * from UserInfo where 1=1";//需要执行的sql语句
        string appendSQL = "AND UserID='" + userID + "'";
        sql = sql + appendSQL;
        SetSQLServer setSQLServer = new SetSQLServer();
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);
        UserInfo userInfo = new UserInfo();        
        if (ds.Tables[0].Rows.Count == 1)
        {
            userInfo.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
            userInfo.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
            userInfo.Military = ds.Tables[0].Rows[0]["Military"].ToString();
            userInfo.Resources = int.Parse(ds.Tables[0].Rows[0]["Resources"].ToString());
            userInfo.Gold = int.Parse(ds.Tables[0].Rows[0]["Gold"].ToString());
            userInfo.Diamonds = int.Parse(ds.Tables[0].Rows[0]["Diamonds"].ToString());
            userInfo.BackPackID = ds.Tables[0].Rows[0]["BackPackID"].ToString();
            userInfo.DockID = ds.Tables[0].Rows[0]["DockID"].ToString();
            userInfo.LineupID = ds.Tables[0].Rows[0]["LineupID"].ToString();
            userInfo.Secretary = ds.Tables[0].Rows[0]["Secretary"].ToString();
            userInfo.Level= int.Parse(ds.Tables[0].Rows[0]["Level"].ToString());
            userInfo.Experience = int.Parse(ds.Tables[0].Rows[0]["Experience"].ToString());           
            return userInfo;

        }
        else
        {

            return null;
        }






    }
}
