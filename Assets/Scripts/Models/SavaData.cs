using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
 



/// <summary>
/// 注册界面控制
/// </summary>
public class SavaData
{
    RegisterServer registerServer;
    public string serverId;
    private static SavaData instance;
    public bool Isok;
    public static SavaData  Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SavaData();
            }

            return instance;
        }
    }
    
    /// <summary>
    /// 把注册信息存档
    /// </summary>
    /// <param name="isRes"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
   public void Save(bool isRes, Account user)//存到json 存到服务器
   {
        registerServer = new RegisterServer();
        string filePath = Application.streamingAssetsPath + "/JsonPerson.json";//加载json的数据
        File.WriteAllText(filePath, JsonMapper.ToJson(user));
        //FileInfo file = new FileInfo(filePath);
        if (isRes)
        {
            bool isok = registerServer.Register(user.AccountNumber, user.PassWord);
            if (isok)
            {
                
                Debug.Log("成功");
                Isok = true;
            }
            else
            {
                Isok = false;
                Debug.Log("失败");
            }
        }
        //StreamWriter sw = file.CreateText();
        // JsonData data = new JsonData();
        //data = data;
        //sw.WriteLine(Json_User);
       
        //sw.Close();
        //sw.Dispose();
    

   }
    /// <summary>
    /// 保存登录的服务器id
    /// </summary>
    public void Save(ServerHasBeenLogin server)
    {
        string filePath = Application.streamingAssetsPath + "/Server.json";
        
        File.WriteAllText(filePath, JsonMapper.ToJson(server));
    }
}
