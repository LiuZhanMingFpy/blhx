using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*

林哲

子弹属性管理生成器（单例模式）

12月5日

*/


//技能3海豹导弹信息
public class CannonData
{
    public float atk;

    public CannonData(float atk)
    {
        this.atk = atk;
    }
}

//技能1导弹信息
public class BoomData
{
    public float atk;

    public BoomData(float atk)
    {
        this.atk = atk;
    }
}

//敌方Boss子弹信息
public class BulletEnemicBossData
{
    public BulletEnemicBossData(float atk1,float atk2,float fish)
    {
        this.atk1 = atk1;
        this.atk2 = atk2;
        this.fish = fish;
    }

    public float atk1;
    public float atk2;
    public float fish;
}

//敌方小船子弹信息
public class BulletEnemicData
{
    public BulletEnemicData(float atk)
    {
        this.atk = atk;
    }

    public float atk;
}

//我方子弹信息
public class BulletData
{
    public BulletData(float atk)
    {
        this.atk = atk;
    }

    public float atk;
}

public class BulletProManager
{
    private static BulletProManager instance;

    private BulletProManager()
    {
        data_boss = new BulletEnemicBossData(10, 10, 50);

        data_cannon = new CannonData(2000);

        data = new BulletData(10);

        data_enemic = new BulletEnemicData(10);

        data_boom = new BoomData(100);
    }

    public BulletEnemicBossData data_boss;

    public CannonData data_cannon;

    public BoomData data_boom;

    public BulletData data;

    public BulletEnemicData data_enemic;

    #region 单例
    public static BulletProManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BulletProManager();
            }
            return instance;
        }
    }
    #endregion

    //子弹生成
    public GameObject Bullet_Produce(string Pro_path, Transform Pro_position)
    {
        GameObject go = Resources.Load<GameObject>(Pro_path);

        GameObject bullet = Object.Instantiate(go) as GameObject;

        bullet.name = go.name;

        bullet.transform.SetParent(GameObject.Find("/Canvas").transform);

        bullet.transform.SetAsLastSibling();

        bullet.transform.localScale = new Vector3(1, 1, 1);

        (bullet.transform as RectTransform).position = (Pro_position as RectTransform).position;

        (bullet.transform as RectTransform).rotation = (Pro_position as RectTransform).rotation;

        //GameObject.Destroy(bullet,3);
        return bullet;
    }

    //水波纹
    public GameObject FishorWave_Produce(string Pro_path, Transform Pro_position)
    {
        GameObject go = Resources.Load<GameObject>(Pro_path);

        GameObject bullet = Object.Instantiate(go) as GameObject;

        bullet.name = go.name;

        bullet.transform.SetParent(GameObject.Find("/Canvas").transform);

        bullet.transform.SetAsLastSibling();

        bullet.transform.localScale = new Vector3(1, 1, 1);



        if (bullet.transform.tag == "wave")
        {
            bullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }

        if (bullet.transform.tag == "waveback")
        {
            bullet.transform.localScale = new Vector3(1, 1, 1);
        }

        (bullet.transform as RectTransform).position = (Pro_position as RectTransform).position;

        //(bullet.transform as RectTransform).rotation = (Pro_position as RectTransform).rotation;


        if (bullet.transform.tag == "wave")
        {
            GameObject.Destroy(bullet, 1);
        }

        if (bullet.transform.tag == "waveback")
        {
            GameObject.Destroy(bullet, 0.3f);
        }

        GameObject.Destroy(bullet, 3);

        return bullet;
    }

    //敌方爆炸动画生成
    public GameObject BulletExploreAnim_enemic(string Pro_path, Transform Pro_Pos, Vector3 Scale)
    {
        GameObject go = Resources.Load<GameObject>(Pro_path);

        GameObject bullet = Object.Instantiate(go) as GameObject;

        bullet.name = go.name;

        bullet.transform.SetParent(GameObject.Find("/Canvas").transform);

        bullet.transform.SetAsLastSibling();

        bullet.transform.localScale = Scale;

        bullet.transform.position = Pro_Pos.position + new Vector3(0, 0, -1);

        //(bullet.transform as RectTransform).position = (Pro_Pos as RectTransform).position;

        return bullet;
    }

    //导弹/子弹爆炸动画生成
    public void BulletAndBoomAnim_Pro(string Pro_path, Transform Pro_Pos)
    {
        GameObject go = Resources.Load<GameObject>(Pro_path);

        GameObject bullet = Object.Instantiate(go) as GameObject;

        bullet.name = go.name;

        bullet.transform.SetParent(GameObject.Find("/Canvas").transform);

        bullet.transform.SetAsLastSibling();

        bullet.transform.localScale = new Vector3(150, 150, 150);

        bullet.transform.position = Pro_Pos.position;

        //bullet.transform.rotation = Pro_Pos.rotation;

        //GameObject.Destroy(bullet,3);       
    }

    public void BulletAndBoomAnim_Pro(string Pro_path, Transform Pro_Pos, Vector3 Scale)
    {
        GameObject go = Resources.Load<GameObject>(Pro_path);

        GameObject bullet = Object.Instantiate(go) as GameObject;

        bullet.name = go.name;

        bullet.transform.SetParent(GameObject.Find("/Canvas").transform);

        bullet.transform.SetAsLastSibling();

        bullet.transform.localScale = Scale;

        bullet.transform.position = Pro_Pos.position + new Vector3(0, 0, -1);

        bullet.transform.rotation = Pro_Pos.rotation;

        //GameObject.Destroy(bullet,3);       
    }

    //预制体加载
    public GameObject Prefabs(string path, Transform pos, Vector3 scale)
    {
        GameObject go = Resources.Load<GameObject>(path);

        GameObject bullet = Object.Instantiate(go) as GameObject;

        bullet.name = go.name;

        //bullet.transform.SetParent(GameObject.Find("/Main Camera").transform);

        bullet.transform.SetAsLastSibling();

        bullet.transform.localScale = scale;

        bullet.transform.position = pos.position;

        return bullet;
    }

    //我方子弹造成伤害产生字体
    public GameObject CharacterDam_Words(int atk,Transform Pro_position,string bullet_color,string word_type)
    {
        int num = Random.Range(atk, atk + 4);

        GameObject go = Resources.Load<GameObject>("Prefabs/UI/FightingPage/"+ bullet_color);

        GetNumber0To9(num, go, word_type);

        //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type4_1");

        //go.transform.Find("word2").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type4_1");

        GameObject bullet = Object.Instantiate(go) as GameObject;

        bullet.name = go.name;

        bullet.transform.SetParent(GameObject.Find("/Canvas").transform);

        bullet.transform.SetAsLastSibling();

        bullet.transform.localScale = new Vector3(1, 1, 1);

        (bullet.transform as RectTransform).position = (Pro_position as RectTransform).position;

        (bullet.transform as RectTransform).rotation = (Pro_position as RectTransform).rotation;

        //GameObject.Destroy(bullet,3);
        return bullet;
    }

    //取随机0~9的数值函数并实例化数字
    private void GetNumber0To9(int num,GameObject go,string type)
    {
        string str = "";
        //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type4_1");

        //go.transform.Find("word2").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type4_1");
        //十位数
        switch (num / 10)
        {
            case 0:
                str = "type" + type + "_0";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_0");
                break;

            case 1:
                str = "type" + type + "_1";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_1");
                break;

            case 2:
                str = "type" + type + "_2";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_2");
                break;

            case 3:
                str = "type" + type + "_3";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_3");
                break;

            case 4:
                str = "type" + type + "_4";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_4");
                break;

            case 5:
                str = "type" + type + "_5";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_5");
                break;

            case 6:
                str = "type" + type + "_6";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_6");
                break;

            case 7:
                str = "type" + type + "_7";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_7");
                break;

            case 8:
                str = "type" + type + "_8";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_8");
                break;

            case 9:
                str = "type" + type + "_9";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_9");
                break;

            default:
                break;
        }
        go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/" + str);

        //个位数
        switch (num%10)
        {
            case 0:
                str = "type" + type + "_0";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_0");
                break;

            case 1:
                str = "type" + type + "_1";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_1");
                break;

            case 2:
                str = "type" + type + "_2";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_2");
                break;

            case 3:
                str = "type" + type + "_3";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_3");
                break;

            case 4:
                str = "type" + type + "_4";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_4");
                break;

            case 5:
                str = "type" + type + "_5";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_5");
                break;

            case 6:
                str = "type" + type + "_6";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_6");
                break;

            case 7:
                str = "type" + type + "_7";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_7");
                break;

            case 8:
                str = "type" + type + "_8";
                //go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_8");
                break;

            case 9:
                str = "type" + type + "_9";
                // go.transform.Find("word1").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/type3_9");
                break;

            default:
                break;
        }
        go.transform.Find("word2").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Common/Numbers/" + str);


    }

    //地方子弹造成伤害产生字体
    public GameObject EnemicDam_Words(int atk)
    {
        return null;
    }
}
