using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
public class SetSQLServer
{
    /// <summary>
    /// 增加修改
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public bool Insert(string sql)
    {
       
        try
        {
            SqlDataAdapter sda = null;
            Confige confige = new Confige();
            SqlConnection con = confige.ConfigeServer();
            con.Open();
            sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "Account");//执行的表
            con.Close();
            return true;
        }
        catch (Exception)
        {

            return false;
        }


    }

    /// <summary>
    /// c查询
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public DataSet Select(string sql)
    {
        try
        {
            SqlDataAdapter sda = null;
            Confige confige = new Confige();
            SqlConnection con = confige.ConfigeServer();
            con.Open();
            sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "Account");//执行的表
            con.Close();
            return ds;
        }
        catch (Exception)
        {

            throw;
        }


    }
}
