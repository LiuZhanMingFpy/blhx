using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightingPageView : MonoBehaviour {

    LineupServer lineupServer = new LineupServer();
    UserInfoServer userInfoServer = new UserInfoServer();
    RoleServer roleServer = new RoleServer();
    public Transform ListFather;
    public GameObject headFramePrefab;
    void Start () {
        string userid=ServerByUser.Instance().UserID;
        //string userid = "F5D6B68E-C617-4EAD-B988-BF5422E3D3FC";
        GetLineUp(userid);
    }
    UserInfo userInfo;
    public void GetLineUp(string userid)
    {


        userInfo = userInfoServer.GetUserInfo(userid);
        Lineup lineup = lineupServer.GetLineup(userInfo.LineupID);

        //后排
        HaveRole BehindFront = roleServer.GetRoleInfo(lineup.BehindFront);      //上
        ShowHeroList(BehindFront, ListFather);
        HaveRole BehindCenter = roleServer.GetRoleInfo(lineup.BehindCenter);    //中
        ShowHeroList(BehindCenter, ListFather);
        HaveRole BehindGuard = roleServer.GetRoleInfo(lineup.BehindGuard);     //下
        ShowHeroList(BehindGuard, ListFather);

        //posList.Add(pos1.name);
    }
    public void ShowHeroList(HaveRole hr, Transform father)
    {
        if (hr.Nickname != null)
        {
            GameObject obj = GameObject.Instantiate(headFramePrefab, father);
            obj.transform.Find("frame_out/head_pic").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/FightingPage/Headpic/" + hr.Nickname);
            obj.name = hr.RoleName;
        }
    }
}
