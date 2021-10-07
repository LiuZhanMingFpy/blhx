using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleForePageView : MonoBehaviour {
    public GameObject headPrefabs;
    public Transform headIconFather;
    RoleServer roleServer = new RoleServer();
    LineupServer lineupServer = new LineupServer();
    UserInfoServer userInfoServer = new UserInfoServer();
    void Awake()
    {
        string userid=ServerByUser.Instance().UserID;
        //string userid = "F5D6B68E-C617-4EAD-B988-BF5422E3D3FC";
        GetLineUp(userid);
    }
    UserInfo userInfo;
    public void GetLineUp(string userid)
    {


        userInfo = userInfoServer.GetUserInfo(userid);
        Lineup lineup = lineupServer.GetLineup(userInfo.LineupID);
        //前排 
        HaveRole ForwardFront = roleServer.GetRoleInfo(lineup.ForwardFront);       //上
     
        ShowHeroList(ForwardFront, headIconFather);
        HaveRole ForwardCenter = roleServer.GetRoleInfo(lineup.ForwardCenter);      //中
  
        ShowHeroList(ForwardCenter, headIconFather);

        HaveRole ForwardGuard = roleServer.GetRoleInfo(lineup.ForwardGuard);       //下
   

        ShowHeroList(ForwardGuard, headIconFather);
        //后排
        HaveRole BehindFront = roleServer.GetRoleInfo(lineup.BehindFront);      //上
     
        ShowHeroList(BehindFront, headIconFather);
        HaveRole BehindCenter = roleServer.GetRoleInfo(lineup.BehindCenter);    //中

        ShowHeroList(BehindCenter, headIconFather);
        HaveRole BehindGuard = roleServer.GetRoleInfo(lineup.BehindGuard);     //下
    
        ShowHeroList(BehindGuard, headIconFather);

        //posList.Add(pos1.name);
    }
    public void ShowHeroList(HaveRole hr, Transform father)
    {
        if (hr.Nickname != null)
        {
            GameObject obj = GameObject.Instantiate(headPrefabs, father);
            obj.transform.Find("characterPic").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleForePage/Character/" + hr.Nickname + "d");
            obj.name = hr.RoleName;
            obj.transform.Find("level/Text").GetComponent<Text>().text = hr.Level.ToString();
        }
    }
}
