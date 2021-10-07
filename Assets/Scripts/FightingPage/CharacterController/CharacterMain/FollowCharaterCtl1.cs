using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*

第二角色控制器脚本

12月5日

*/
internal class Character2Data
{
    public Character2Data(float hp)
    {
        this.hp = hp;
    }

    public float hp;
}
public class FollowCharaterCtl1 : MonoBehaviour {
    private Character2Data data;

    public float const_hp;

    private Image my_hp;

    private Transform mainCharacter;

    private Vector3 tarV;

    private RectTransform bar;
    LineupServer lineupServer = new LineupServer();
    UserInfoServer userInfoServer = new UserInfoServer();
    RoleServer roleServer = new RoleServer();
    Lineup lineup;
    HaveRole ForwardCenter;
    string Nickname;
    void Start () {
        string userid=ServerByUser.Instance().UserID;
        //string userid = "F5D6B68E-C617-4EAD-B988-BF5422E3D3FC";
        UserInfo userInfo = userInfoServer.GetUserInfo(userid);
        lineup = lineupServer.GetLineup(userInfo.LineupID);
        ForwardCenter = roleServer.GetRoleInfo(lineup.ForwardCenter);
        Nickname = ForwardCenter.Nickname;
        Debug.Log(Nickname);
        if (Nickname == null)
        {
            gameObject.SetActive(false);
        }
        transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InterceptPage/Character/"+ Nickname + "c");
        const_hp = ForwardCenter.Durable;
        data = new Character2Data(const_hp);

        my_hp = transform.Find("hp").GetComponent<Image>();

        mainCharacter = transform.parent.Find("character1");

        bar = transform.parent.Find("DragArea/bg/bar") as RectTransform;

        //开启子弹发射
        isShot = true;
        //Debug.Log(mainCharacter.name);
    }

    float bullet_purple=0;

    //是否开启子弹发射
    public bool isShot;

    //子弹发射频率
    public float atk_fre = 1f;

    //狂暴技能计时器
    public float time_skill2 = 0;

    //是否在狂暴状态
    public bool isStrength = false;

    //产生后浪计时器
    [HideInInspector]
    public float wave1 = 0;
    [HideInInspector]
    public float wave5 = 0;  
 
	void Update () {

        if (data.hp<=0)
        {
            StopAllCoroutines();
        }

        //狂暴技能计时
        if (isStrength == true)
        {
            time_skill2 += Time.deltaTime;
            if (time_skill2 >= 4)
            {
                isStrength = false;
                time_skill2 = 0;
                atk_fre = 1;
            }

        }

        my_hp.fillAmount = data.hp / const_hp;

        //跟随角色character1
        tarV = mainCharacter.position - new Vector3(0.8f, 0, 0);

        transform.position = Vector3.Lerp(gameObject.transform.position, tarV, 0.05f);

        //如果进行移动则替换角色表情/并旋转
        if (bar.localPosition.x > 3f)
        {
            //transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InterceptPage/Character/6_smile");
            //transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InterceptPage/Character/" + Nickname + "c");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.1f);
        }
        else if (bar.localPosition.x < -3f)
        {
            //transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InterceptPage/Character/6_smile");
            //transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InterceptPage/Character/" + Nickname + "c");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 5), 0.1f);
        }
        else
        {
            //transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InterceptPage/Character/" + Nickname + "c");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.1f);
        }

        if (isShot)
        {
            bullet_purple += Time.deltaTime;
            wave1 += Time.deltaTime;
            wave5 += Time.deltaTime;
        }
       
        //紫色子弹1秒1发
        if (bullet_purple >= 0.3f/atk_fre)
        {
            BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/bullet/bullet_purple", transform.Find("fire1"));

            BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/bullet/bullet_purple", transform.Find("fire2"));

            bullet_purple = 0;
        }
       
        //移动边界限制
        AllCharacterCtl.CharacterMove_BoardLimit(transform);

       
        //水后浪生成
        #region 水后浪
        if (wave1 > 0.2f)
        {
            BulletProManager.Instance.FishorWave_Produce("Prefabs/UI/FightingPage/Characters/wave1", transform.Find("fish2"));
            wave1 = 0;
        }

        if (wave5 > 0.4f)
        {
            BulletProManager.Instance.FishorWave_Produce("Prefabs/UI/FightingPage/Characters/wave1_white", transform.Find("fish2"));
            wave5 = 0;
        }
        #endregion

    }

    //碰撞检测
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "bullet_yellow")
        {
            //子弹爆照效果生成
            data.hp -= BulletProManager.Instance.data_enemic.atk;
            GameObject bullet_green = BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_yellow", col.transform, new Vector3(100, 100, 100));

            //BulletPoorManager.Instance.ReturnGo_yellow(col.gameObject);

            //子弹伤害字体生辰
            BulletProManager.Instance.CharacterDam_Words(10, transform, "DamWords", "4");

            //击中反馈闪白shader
            StartCoroutine(ShaderEffect.Instance.WhiteReturn(transform));
            Destroy(col.gameObject);
        }
        else if (col.transform.tag == "bullet_orange")
        {
            data.hp -= BulletProManager.Instance.data_enemic.atk;
            GameObject bullet_green = BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_green", col.transform, new Vector3(100, 100, 100));

            BulletProManager.Instance.CharacterDam_Words(10, transform, "DamWords", "3");

            //BulletPoorManager.Instance.ReturnGo_orange(col.gameObject);
            StartCoroutine(ShaderEffect.Instance.WhiteReturn(transform));
            Destroy(col.gameObject);
        }
        else if (col.transform.tag == "bullet_boss1")
        {
            data.hp -= BulletProManager.Instance.data_boss.atk1;
            GameObject bullet_green = BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_purple", col.transform, new Vector3(100, 100, 100));

            BulletProManager.Instance.CharacterDam_Words(10, transform, "DamWords", "6");

            //BulletPoorManager.Instance.ReturnGo_boss1(col.gameObject);
            StartCoroutine(ShaderEffect.Instance.WhiteReturn(transform));
            Destroy(col.gameObject);
        }
        else if (col.transform.tag == "bullet_boss2")
        {
            data.hp -= BulletProManager.Instance.data_boss.atk2;
            GameObject bullet_green = BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_red", col.transform, new Vector3(100, 100, 100));

            BulletProManager.Instance.CharacterDam_Words(10, transform, "DamWords", "6");

            //BulletPoorManager.Instance.ReturnGo_boss2(col.gameObject);
            StartCoroutine(ShaderEffect.Instance.WhiteReturn(transform));
            Destroy(col.gameObject);
        }
        else if (col.transform.tag == "bullet_green")
        {
            BulletProManager.Instance.CharacterDam_Words(10, transform, "DamWords", "5");
            data.hp -= BulletProManager.Instance.data_enemic.atk;
            GameObject bullet_green = BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_green", col.transform, new Vector3(100, 100, 100));
            StartCoroutine(ShaderEffect.Instance.WhiteReturn(transform));
            //BulletPoorManager.Instance.ReturnGo_green(col.gameObject);
            Destroy(col.gameObject);
        }

        //Debug.Log(data.hp);

    }
}
