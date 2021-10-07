using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
public class DockServer {

    /// <summary>
    /// 获取船坞信息
    /// </summary>
    /// <param name="userid">传入的用户ID</param>
    /// <returns></returns>
    public Dock GetDackInfo(string userid)
    {
        
        SetSQLServer setSQLServer = new SetSQLServer();
        Dock dock = new Dock();
        string sql = "select * from Dock where UserID ='" + userid + "'";
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);

        if (ds.Tables[0].Rows.Count == 1)
        {
           
            dock.DockID = ds.Tables[0].Rows[0]["DockID"].ToString();
            dock.DockNum = int.Parse(ds.Tables[0].Rows[0]["DockNum"].ToString());
            dock.DockUpLimit = int.Parse(ds.Tables[0].Rows[0]["DockUpLimit"].ToString());
           
        }
      
        return dock;
    }
    /// <summary>
    /// 获取船坞容纳的角色
    /// </summary>
    public HaveRole[] GetRoleInfo(string dockid)
    {
        SetSQLServer setSQLServer = new SetSQLServer();      
        string sql =" select HaveRole.* ,HaveRoleSkill.SkillNO1"
                   + " from DockSpack,HaveRole,HaveRoleSkill"
                   + " where DockSpack.RoleID = HaveRole.RoleID"
                   + "  And DockSpack.DockID = '"+ dockid + "';";
        DataSet ds = new DataSet();
        ds = setSQLServer.Select(sql);
        HaveRole[] haveRole = new HaveRole[ds.Tables[0].Rows.Count];
        if (ds.Tables[0].Rows.Count!=0)
        {
          
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                haveRole[i] = new HaveRole();
                haveRole[i].RoleID= ds.Tables[0].Rows[i]["RoleID"].ToString();
                haveRole[i].RoleName = ds.Tables[0].Rows[i]["RoleName"].ToString();
                haveRole[i].Nickname = ds.Tables[0].Rows[i]["Nickname"].ToString();            
                haveRole[i].Durable = int.Parse(ds.Tables[0].Rows[i]["Durable"].ToString());
                haveRole[i].TheShelling = int.Parse(ds.Tables[0].Rows[i]["TheShelling"].ToString()) + int.Parse(ds.Tables[0].Rows[i]["AddMainGun"].ToString());
                haveRole[i].AirDefense = int.Parse(ds.Tables[0].Rows[i]["AirDefense"].ToString());
                haveRole[i].AntiSubmarine = int.Parse(ds.Tables[0].Rows[i]["AntiSubmarine"].ToString());
                haveRole[i].Armor = int.Parse(ds.Tables[0].Rows[i]["Armor"].ToString());
                haveRole[i].TheTorpedo = int.Parse(ds.Tables[0].Rows[i]["TheTorpedo"].ToString()) + int.Parse(ds.Tables[0].Rows[i]["AddAuxiliary"].ToString());
                haveRole[i].Aviation = int.Parse(ds.Tables[0].Rows[i]["Aviation"].ToString()) + int.Parse(ds.Tables[0].Rows[i]["AddViceGun"].ToString());
                haveRole[i].Loading = int.Parse(ds.Tables[0].Rows[i]["Loading"].ToString());
                haveRole[i].Motor = int.Parse(ds.Tables[0].Rows[i]["Motor"].ToString());
                haveRole[i].Consumption = int.Parse(ds.Tables[0].Rows[i]["Consumption"].ToString());
                haveRole[i].Camp = int.Parse(ds.Tables[0].Rows[i]["Camp"].ToString());
                haveRole[i].Price = int.Parse(ds.Tables[0].Rows[i]["Price"].ToString());
                haveRole[i].SkillbarID = ds.Tables[0].Rows[i]["SkillbarID"].ToString();
                haveRole[i].Stars = int.Parse(ds.Tables[0].Rows[i]["Stars"].ToString());
                haveRole[i].Level = int.Parse(ds.Tables[0].Rows[i]["Level"].ToString());
                haveRole[i].Quality = int.Parse(ds.Tables[0].Rows[i]["Quality"].ToString());
                haveRole[i].Position = int.Parse(ds.Tables[0].Rows[i]["Position"].ToString());

            }
        }
       
        return haveRole;
    }
}
