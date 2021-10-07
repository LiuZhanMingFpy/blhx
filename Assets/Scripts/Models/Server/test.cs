using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
public class test : MonoBehaviour {

    // Use this for initialization
    //void Start () {
    //       string AccountNumber = "system1";
    //       string PassWord = "123";
    //       ///注册接口
    //       RegisterServer registerServer = new RegisterServer();      
    //        bool isok=registerServer.Register(AccountNumber, PassWord);
    //       if (isok)
    //       {
    //           Debug.Log("成功");
    //       }
    //       else {
    //           Debug.Log("失败");
    //       }

    //       ///登陆接口
    //       LoginServer loginServer = new LoginServer();    

    //       string userid = loginServer.LoginVerification(AccountNumber, PassWord);

    //       if (userid != null)
    //       {
    //           //如果登陆成功 读取数据
    //           UserInfoServer userInfoServer = new UserInfoServer();
    //           UserInfo userInfo = userInfoServer.GetUserInfo(userid);
    //           if (userInfo != null)
    //           {              
    //               ShopServer shopServer = new ShopServer();
    //              bool how= shopServer.Buy(userInfo.UserID, 500,100, "Gold");
    //               if (how)
    //               {
    //                   Debug.Log("用户名:" + userInfo.UserName + ",拥有资源:" + userInfo.Resources + ",拥有金币:" + userInfo.Gold + ",拥有钻石:" + userInfo.Diamonds);
    //               }

    //           }
    //           else { Debug.Log("暂无数据"); }
    //       }
    //       else
    //       {
    //           Debug.Log("账号密码错误");
    //       }



    //   }

    void Start() {

    }
}
