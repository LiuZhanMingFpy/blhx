using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用户与服务器关联实体
/// </summary>
public class ServerByUser
{
    private static ServerByUser instance;

    public static ServerByUser Instance()
    {
        if (instance==null)
        {
            instance = new ServerByUser();
        }

        return instance;
    }


    /// <summary>
    /// 服务器ID
    /// </summary>
    public string ServerID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string UserID { get; set; }
}
