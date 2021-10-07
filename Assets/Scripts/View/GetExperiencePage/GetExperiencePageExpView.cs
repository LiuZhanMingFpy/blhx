using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GetExperiencePageExpView : MonoBehaviour {

    Text username;
    Text level;
    Text add;

    float exp_tol;//经验条总长度
    float exp_my;//自身已有的经验
    float exp_get;//战斗结算获得的经验
    UserInfo userInfo;
    RoleServer roleServer = new RoleServer();
    LineupServer lineupServer = new LineupServer();
    UserInfoServer userInfoServer = new UserInfoServer();
    public GameObject getExpFrame1;
    public GameObject getExpFrame2;
    public GameObject getExpFrame3;
    public GameObject getExpFrame4;
    public GameObject getExpFrame5;
    public GameObject getExpFrame6;
    public GameObject star;
    void Start () {
        //获取id
        string userid=ServerByUser.Instance().UserID;
        //string userid = "F5D6B68E-C617-4EAD-B988-BF5422E3D3FC";
        GetLineUp(userid);
        


        //获取组件
        username = transform.Find("username").GetComponent<Text>();

        level= transform.Find("Lv_level").GetComponent<Text>();

        add= transform.Find("add").GetComponent<Text>();

        exp_my = transform.Find("experience").GetComponent<Image>().fillAmount;

        exp_tol = 1000;

        exp_get = 800;
    }


    bool isGetExp = false;
    float time = 0;

	void Update () {

        if(isGetExp)
        {
            return;
        }

        transform.Find("experience").GetComponent<Image>().fillAmount=exp_my;

        time += Time.deltaTime;

        if(time<5f)
        {
            //Debug.Log(exp_my);
            exp_my = Mathf.Lerp(exp_my, exp_get / exp_tol, 0.05f);           
        }
        else
        {
            isGetExp = true;
        }
        


	}

    public void BacktoMain()
    {
        GameObject ddm = GameObject.Find("DontDestroyMe");
        Destroy(ddm);
        SceneManager.LoadScene("Main1");
    }
    public void GetLineUp(string userid)
    {


        userInfo = userInfoServer.GetUserInfo(userid);
        Lineup lineup = lineupServer.GetLineup(userInfo.LineupID);
        //前排 
        HaveRole ForwardFront = roleServer.GetRoleInfo(lineup.ForwardFront);       //上
        ShowHeroList(ForwardFront, getExpFrame1.transform);


        HaveRole ForwardCenter = roleServer.GetRoleInfo(lineup.ForwardCenter);      //中
        ShowHeroList(ForwardCenter, getExpFrame2.transform);


        HaveRole ForwardGuard = roleServer.GetRoleInfo(lineup.ForwardGuard);       //下
        ShowHeroList(ForwardGuard, getExpFrame3.transform);


        //后排
        HaveRole BehindFront = roleServer.GetRoleInfo(lineup.BehindFront);      //上
        ShowHeroList(BehindFront, getExpFrame4.transform);

        HaveRole BehindCenter = roleServer.GetRoleInfo(lineup.BehindCenter);    //中
        ShowHeroList(BehindCenter, getExpFrame5.transform);

        HaveRole BehindGuard = roleServer.GetRoleInfo(lineup.BehindGuard);     //下
        ShowHeroList(BehindGuard, getExpFrame6.transform);


        //posList.Add(pos1.name);
    }
    public void ShowHeroList(HaveRole hr, Transform father)
    {
        if (hr.Nickname != null)
        {
            Transform starFather = father.Find("stars");
            HorizontalLayoutGroup starGroup = starFather.GetComponent<HorizontalLayoutGroup>();
            switch (hr.Stars)
            {
                case 1:

                    for (int j = 0; j < hr.Stars; j++)
                    {
                        GameObject realstar = GameObject.Instantiate(star, starFather);
                    }

                    starGroup.spacing = -140;
                    break;
                case 2:
                    
                    for (int j = 0; j < hr.Stars; j++)
                    {
                        GameObject realstar = GameObject.Instantiate(star, starFather);
                    }

                    starGroup.spacing = -120;
                    break;
                case 3:
                  
                    for (int j = 0; j < hr.Stars; j++)
                    {
                        GameObject realstar = GameObject.Instantiate(star, starFather);
                    }

                    starGroup.spacing = -100;
                    break;
                case 4:
                  
                    for (int j = 0; j < hr.Stars; j++)
                    {
                        GameObject realstar = GameObject.Instantiate(star, starFather);
                    }

                    starGroup.spacing = -80;
                    break;
                case 5:
                 
                    for (int j = 0; j < hr.Stars; j++)
                    {
                        GameObject realstar = GameObject.Instantiate(star, starFather);
                    }

                    starGroup.spacing = -110;
                    break;
                case 6:

                    for (int j = 0; j < hr.Stars; j++)
                    {
                        GameObject realstar = GameObject.Instantiate(star, starFather);
                    }

                    starGroup.spacing = -120;
                    break;
            }


            //for (int i = 0; i < hr.Stars; i++)
            //{

            //    GameObject starreal = GameObject.Instantiate(star, starfather);

            //}
            father.Find("character").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/GetExperiencePage/Character/"+hr.Nickname);
            father.Find("Lv_level").GetComponent<Text>().text = hr.Level.ToString();
        }
        else
        {
            father.gameObject.SetActive(false);
        }
    }
}
