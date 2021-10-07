using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
public class RegisterServer 
{

    public bool Register(string username, string password)
    {
        SetSQLServer setSQLServer = new SetSQLServer();
        DataSet ds = new DataSet();
        string sql = "select * from Account where AccountNumber='" + username + "'";       
        ds=setSQLServer.Select(sql);      
        if (ds.Tables[0].Rows.Count==0)
        {
            sql = "INSERT INTO  Account(AccountNumber, PassWord, TheTotalID) VALUES('" + username + "', '" + password + "', newId())";
           
             setSQLServer.Insert(sql);
            return true;
        }
        return false;


    }

}
