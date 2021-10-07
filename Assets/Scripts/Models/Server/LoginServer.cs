using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;

public class LoginServer
{

    // Use this for initialization


    //public string LoginVerification(string username,string password)
    //{
    //    try
    //    {
    //        SqlDataAdapter sda = null;
    //        Confige confige = new Confige();
    //        SqlConnection con = confige.ConfigeServer();
    //        con.Open();
    //        string sql = "select * from Account where 1=1";//需要执行的sql语句
    //        sda = new SqlDataAdapter(sql, con);
    //        DataSet ds = new DataSet();
    //        sda.Fill(ds, "Account");//执行的表
    //                                //print(ds.Tables[0].Rows[0][1]);

    //        //object name = ds.Tables[0].Rows[0]["AccountNumber"];//遍历一行

    //        //遍历所有
    //        //foreach (DataRow mDr in ds.Tables[0].Rows)
    //        //{
    //        //    foreach (DataColumn mDc in ds.Tables[0].Columns)
    //        //    {
    //        //        Debug.Log(mDr[mDc].ToString());
    //        //    }
    //        //}

    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            if (ds.Tables[0].Rows[i]["AccountNumber"].ToString() == username)
    //            {
    //                for ( i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    if (ds.Tables[0].Rows[i]["PassWord"].ToString() == password)
    //                    {
    //                        con.Close();
    //                        return ds.Tables[0].Rows[i]["UserID"].ToString();
                           
    //                    }
    //                }
    //            }
    //        }

    //        con.Close();
    //        return null;
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }

        
    //}

}
