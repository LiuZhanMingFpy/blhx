using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterceptPageMiddleView : MonoBehaviour
{
    public GameObject battlePage;
    public GameObject water;
    public GameObject watercam;
    public GameObject interceptPage;

    public GameObject star;
    public GameObject Prefabs;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Transform pos5;
    public Transform pos6;

    private string post1;
    private string post2;
    private string post3;
    private string post4;
    private string post5;
    private string post6;

    public GameObject headPrefabs;
    public Transform headIconFather;
    LineupServer lineupServer = new LineupServer();
    UserInfoServer userInfoServer = new UserInfoServer();
    RoleServer roleServer = new RoleServer();

    void Awake()
    {
        string userid=ServerByUser.Instance().UserID;
        //string userid = "F5D6B68E-C617-4EAD-B988-BF5422E3D3FC";
        GetLineUp(userid);


    }

    void Start()
    {


    }

    void Update()
    {

        post1 = pos1.GetChild(0).name;    
        post2 = pos2.GetChild(0).name;
        post3 = pos3.GetChild(0).name;
        post4 = pos4.GetChild(0).name;
        post5 = pos5.GetChild(0).name;
        post6 = pos6.GetChild(0).name;

    }
    UserInfo userInfo;
    public void GetLineUp(string userid)
    {


        userInfo = userInfoServer.GetUserInfo(userid);
        Lineup lineup = lineupServer.GetLineup(userInfo.LineupID);
        //前排 
        HaveRole ForwardFront = roleServer.GetRoleInfo(lineup.ForwardFront);       //上
        post4 = ShowHero(ForwardFront, pos4);
        ShowHeroList(ForwardFront, headIconFather);
        HaveRole ForwardCenter = roleServer.GetRoleInfo(lineup.ForwardCenter);      //中
        post5 = ShowHero(ForwardCenter, pos5);
        ShowHeroList(ForwardCenter, headIconFather);
        HaveRole ForwardGuard = roleServer.GetRoleInfo(lineup.ForwardGuard);       //下
        post6 = ShowHero(ForwardGuard, pos6);
        ShowHeroList(ForwardGuard, headIconFather);
        //后排
        HaveRole BehindFront = roleServer.GetRoleInfo(lineup.BehindFront);      //上
        post1 = ShowHero(BehindFront, pos1);
        ShowHeroList(BehindFront, headIconFather);
        HaveRole BehindCenter = roleServer.GetRoleInfo(lineup.BehindCenter);    //中
        post2 = ShowHero(BehindCenter, pos2);
        ShowHeroList(BehindCenter, headIconFather);
        HaveRole BehindGuard = roleServer.GetRoleInfo(lineup.BehindGuard);     //下
        post3 = ShowHero(BehindGuard, pos3);
        ShowHeroList(BehindGuard, headIconFather);

        //posList.Add(pos1.name);
    }
    public string ShowHero(HaveRole hr, Transform pos)
    {

        if (hr.Nickname == null)
        {
            GameObject obj = GameObject.Instantiate(Prefabs, pos);
            obj.name = Prefabs.name;
            obj.GetComponent<CanvasGroup>().alpha = 0;
            return null;
        }
        else
        {
            GameObject obj = GameObject.Instantiate(Prefabs, pos);
            obj.name = hr.RoleID;
            obj.transform.Find("cha_main").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InterceptPage/Character/" + hr.Nickname + "c");

            obj.transform.Find("level/Text").GetComponent<Text>().text = hr.Level.ToString();
            Transform stars = obj.transform.Find("stars");
            for (int i = 0; i < hr.Stars; i++)
            {
                GameObject realstar = GameObject.Instantiate(star, stars);
            }
            return hr.RoleID;
        }
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

    public void OnClickToFIghtingPage()
    {

        lineupServer.SaveLineup(userInfo.LineupID, post1, post2, post3, post4, post5, post6);
        battlePage.SetActive(true);
        water.SetActive(true);
        watercam.SetActive(true);
        interceptPage.SetActive(false);



    }
   
}
