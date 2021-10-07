using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossData
{
    public BossData(float hp)
    {
        this.hp = hp;
    }

    public float hp;

}

public class BossController : MonoBehaviour {

    private BossData data;

    private const float const_hp=5000;

    private Transform fire1;
    private Transform fire2;
    private Transform boom;
    private Transform fire4;
    private Transform fire5;
    private Transform fire6;

   
    private Image my_hp;
	void Start () {
        data = new BossData(const_hp);

        fire1 = transform.Find("fire1");
        fire2 = transform.Find("fire2");
        fire4 = transform.Find("fire4");

        boom = transform.Find("boom");

        fire5 = transform.Find("fire5");
        fire6 = transform.Find("fire6");

        my_hp = transform.Find("hp").GetComponent<Image>();


       
        EventCenter.AddListener(EGameEvent.skill3_cannon_deblood, Skill3_cannon_deblood);
    }

    //方向移动计时器
    private float changeDir;

    //子弹发射方式1计时器
    float time_atk1 = 0;

    //子弹发射方式2计时器
    float time_atk2 = 0;


    //表情渲染计时器
    float time_emotion = 0;

    void FixedUpdate()
    {
        //船只移动
        Vector3 v1 = new Vector3(0, 1f, 0) * Time.deltaTime;
        Vector3 v2 = new Vector3(0, -1f, 0) * Time.deltaTime;
        Vector3 v3 = new Vector3(-1, 0, 0) * Time.deltaTime;

        if (transform.position.x < 9f)
        {
            changeDir += Time.deltaTime;
            if (changeDir > 3)
            {
                changeDir = 0;
            }
            else if (changeDir > 1.5f)
            {
                transform.Translate(v1);
            }
            else
            {
                transform.Translate(v2);
            }
        }
        else
        {
            transform.Translate(v3);
        }

    }

    //血量归0后是否可以销毁
    bool isdestroy = false;

    //血量归0时间缩放计时器
    float timeDeScale = 0;

    void Update () {
        //Debug.Log(transform.position);

        //血量归0
        if (data.hp <= 0)
        {
            
            StopAllCoroutines();
            BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/cannon/MagicSphereBlue", transform, new Vector3(250, 250, 250));
            BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/cannon/MagicChargeBlue", transform, new Vector3(200, 200, 200));
            BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/cannon/NovaBlue", transform, new Vector3(100, 100, 100));

            EventCenter.Broadcast(EGameEvent.bullet_stop);

            transform.parent.Find("FightingPage").GetComponent<FightingPageController>().isWarning = true;

            GameObject timer = GameObject.Instantiate(Resources.Load("Prefabs/UI/FightingPage/Enemic/timer"), GameObject.Find("Canvas").transform.Find("FightingPage/TolScence/Visuable")) as GameObject;
                //Find("Canvas").transform.Find("FightingPage/TolScence/Visuable")
           
            Destroy(this.gameObject);
            Debug.Log(data.hp);
            return;

        }

        my_hp.fillAmount = data.hp / const_hp;

        time_emotion += Time.deltaTime;

        //表情渲染
        if(time_emotion>3)
        {
            if (data.hp / const_hp > 0.8)
            {
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/Emojis/EmojiCool", transform, new Vector3(100, 100, 100));
            }
            else if (data.hp / const_hp > 0.6)
            {
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/Emojis/EmojiSad", transform, new Vector3(100, 100, 100));
            }
            else if (data.hp / const_hp > 0.4)
            {
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/Emojis/EmojiAngry", transform, new Vector3(100, 100, 100));
            }
            else if (data.hp / const_hp > 0.2)
            {
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/Emojis/EmojiCry", transform, new Vector3(100, 100, 100));
            }
            else 
            {
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/Emojis/EmojiSick", transform, new Vector3(100, 100, 100));
            }

            time_emotion = 0;
        }
        

        //boss方向移动
        if (transform.position.x < 9f)
        {
           
            time_atk1 += Time.deltaTime;
            time_atk2 += Time.deltaTime;
           
            //攻击方式1
            if (time_atk1 > 2f)
            {
                BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/bullet/bullet_boss1", fire1);
                BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/bullet/bullet_boss1", fire2);

                //BulletPoorManager.Instance.GetGo_boss1("Prefabs/UI/FightingPage/bullet/bullet_boss1", fire1);
                //BulletPoorManager.Instance.GetGo_boss1("Prefabs/UI/FightingPage/bullet/bullet_boss1", fire2);

                time_atk1 = 0;
            }

            //攻击方式2
            if (time_atk2 > 0.5f)
            {
                BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/bullet/bullet_boss2", fire4);
                BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/bullet/bullet_boss2", fire5);
                BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/bullet/bullet_boss2", fire6);

                //BulletPoorManager.Instance.GetGo_boss2("Prefabs/UI/FightingPage/bullet/bullet_boss2", fire4);
                //BulletPoorManager.Instance.GetGo_boss2("Prefabs/UI/FightingPage/bullet/bullet_boss2", fire5);
                //BulletPoorManager.Instance.GetGo_boss2("Prefabs/UI/FightingPage/bullet/bullet_boss2", fire6);
            
                time_atk2 = 0;
            }

           
        }
        
        

    }

    void Skill3_cannon_deblood()
    {
        data.hp -= BulletProManager.Instance.data_cannon.atk;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "bullet_blue")
        {
            //Debug.Log(BulletProManager.Instance.data.atk);

            //实例化爆炸效果
            data.hp -= BulletProManager.Instance.data.atk;
            GameObject bullet_blue = BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_blue", col.transform, new Vector3(100, 100, 1));

            //伤害字体生成
            BulletProManager.Instance.CharacterDam_Words(10, transform, "DamWords", "3");

            //击中反馈闪白shader
            StartCoroutine(ShaderEffect.Instance.WhiteReturn(transform));

            //BulletPoorManager.Instance.ReturnGo_blue(col.gameObject);         
            Destroy(col.gameObject);
        }
        else if (col.transform.tag == "bullet_red")
        {
            data.hp -= BulletProManager.Instance.data.atk;
            GameObject bullet_red = BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_red", col.transform, new Vector3(100, 100, 1));

            BulletProManager.Instance.CharacterDam_Words(10, transform, "DamWords", "5");

            StartCoroutine(ShaderEffect.Instance.WhiteReturn(transform));
            //BulletPoorManager.Instance.ReturnGo_red(col.gameObject);
            Destroy(col.gameObject);
        }
        else if (col.transform.tag == "bullet_purple")
        {
            data.hp -= BulletProManager.Instance.data.atk;
            GameObject bullet_purple = BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_purple", col.transform, new Vector3(100, 100, 1));

            BulletProManager.Instance.CharacterDam_Words(10, transform, "DamWords", "6");

            StartCoroutine(ShaderEffect.Instance.WhiteReturn(transform));
            //BulletPoorManager.Instance.ReturnGo_purple(col.gameObject);
            Destroy(col.gameObject);
        }
        else if (col.transform.tag == "boom_skill1")
        {
            StartCoroutine(ShaderEffect.Instance.WhiteReturn(transform));
            data.hp -= BulletProManager.Instance.data_boom.atk;
        }

        //Debug.Log(data.hp);

    }

    

}
