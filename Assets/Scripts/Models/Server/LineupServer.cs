using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;

public class LineupServer  {

    /// <summary>
    /// 获取阵容
    /// </summary>
    /// <param name="lineupid"></param>
    /// <returns></returns>
    public Lineup GetLineup(string lineupid) {      
        SetSQLServer setSQLServer = new SetSQLServer();
        DataSet ds = new DataSet();
        Lineup lineup = new Lineup();
        string sql ="select * from Lineup where LineupID ='"+lineupid +"'";
    
        ds = setSQLServer.Select(sql);      
        if (ds.Tables[0].Rows.Count == 1)
        {
            lineup.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
            lineup.LineupID = ds.Tables[0].Rows[0]["LineupID"].ToString();
            lineup.ForwardFront = ds.Tables[0].Rows[0]["ForwardFront"].ToString();
            lineup.ForwardCenter = ds.Tables[0].Rows[0]["ForwardCenter"].ToString();
            lineup.ForwardGuard = ds.Tables[0].Rows[0]["ForwardGuard"].ToString();
            lineup.BehindFront = ds.Tables[0].Rows[0]["BehindFront"].ToString();
            lineup.BehindCenter = ds.Tables[0].Rows[0]["BehindCenter"].ToString();
            lineup.BehindGuard = ds.Tables[0].Rows[0]["BehindGuard"].ToString();
        }
       
        return lineup;
    }
    public void SaveLineup(string lineup,string post1,string post2,string post3,string post4, string post5, string post6) {
        SetSQLServer setSQLServer = new SetSQLServer();
        if (post1 !=null&&post1!="character") {
            post1 = "'" + post1 + "'";
        }
        else
        {
            post1 = "null";
        }

        if (post2 != null && post2 != "character")
        {
            post2 = "'" + post2 + "'";
        }
        else
        {
            post2 = "null";
        }

        if (post3 != null && post3 != "character")
        {
            post3 = "'" + post3 + "'";
        }
        else
        {
            post3 = "null";
        }

        if (post4 != null && post4 != "character")
        {
            post4 = "'" + post4 + "'";
        }
        else
        {
            post4 = "null";
        }

        if (post5 != null && post5 != "character")
        {
            post5 = "'" + post5 + "'";
        }
        else
        {
            post5 = "null";
        }

        if (post6 != null && post6 != "character")
        {
            post6 = "'" + post6 + "'";
        }
        else
        {
            post6 = "null";
        }

        string sql = " UPDATE Lineup SET ForwardFront="+ post4 + ",ForwardCenter="+ post5 + ",ForwardGuard="+ post6 + ",BehindFront="+post1+",BehindCenter="+post2+",BehindGuard ="+ post3+ " WHERE LineupID = '" +lineup+ "';";
        Debug.Log(sql);
        setSQLServer.Insert(sql);
    }
}
