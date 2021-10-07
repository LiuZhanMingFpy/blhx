using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

林哲

敌方舰队船类4控制脚本

12月10日

*/
public class Boat4Data
{
    public Boat4Data(float hp)
    {
        this.hp = hp;
    }

    public float hp;

}

public class Boat4Controller : MonoBehaviour
{
    public const float const_hp = 300;

    private Image my_hp;

    public Boat4Data data;

    private float atk_CdTime = 5f;

    void Start()
    {
        my_hp = transform.Find("hp").GetComponent<Image>();

        data = new Boat4Data(const_hp);

        EventCenter.AddListener(EGameEvent.skill3_cannon_deblood, Skill3_cannon_deblood);
    }
    float changeDir = 0;

    float waveback_1 = 5f;

    float waveback_2 = 5f;

    void FixedUpdate()
    {
        //船只移动

        Vector3 v1 = new Vector3(0, -1f, 0) * Time.deltaTime;
        Vector3 v2 = new Vector3(0, 1f, 0) * Time.deltaTime;
        Vector3 v3 = new Vector3(-1, 0, 0) * Time.deltaTime;

        if (transform.position.x < 6f)
        {
            changeDir += Time.deltaTime;
            if (changeDir > 4)
            {
                changeDir = 0;
            }
            else if (changeDir > 2)
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

    void Update()
    {
        my_hp.fillAmount = data.hp / const_hp;

        if (data.hp <= 0)
        {
            StopAllCoroutines();
            BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_yellow_soul", transform, new Vector3(100, 100, 100));
            Destroy(this.gameObject);
            return;
        }

        waveback_1 += Time.deltaTime;

        waveback_2 += Time.deltaTime;

        //产生波浪
        #region 产生水波浪
        if (waveback_1 > 0.2f)
        {
            BulletProManager.Instance.FishorWave_Produce("Prefabs/UI/FightingPage/Enemic/wave1_enemic", transform.Find("back"));
            waveback_1 = 0;
        }
        #endregion

        //如果船未被摧毁，移动至Canvas左侧则销毁
        if (transform.position.x < -7)
        {
            data.hp = 0;
        }

        //船只移动到变向
        //if (transform.position.x < 6f)
        //{
        //    changeDir += Time.deltaTime;
        //    if (changeDir > 4)
        //    {
        //        changeDir = 0;
        //    }
        //    else if (changeDir > 2)
        //    {
        //        transform.Translate(0, -0.02f, 0);
        //    }
        //    else
        //    {
        //        transform.Translate(0, 0.02f, 0);
        //    }
        //}
        //else
        //{
        //    transform.Translate(-0.02f, 0, 0);
        //}
        

        //Debug.Log(transform.position.y);

        //船只到达指定坐标开始发动攻击
        if (transform.position.x < 12)
        {
            atk_CdTime += Time.deltaTime;

            if (atk_CdTime > 2f)
            {
                BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/bullet/bullet_orange", transform.Find("fire1"));

                BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/bullet/bullet_orange", transform.Find("fire2"));

                //BulletPoorManager.Instance.GetGo_orange("Prefabs/UI/FightingPage/bullet/bullet_orange", transform.Find("fire1"));

                //BulletPoorManager.Instance.GetGo_orange("Prefabs/UI/FightingPage/bullet/bullet_orange", transform.Find("fire2"));

                atk_CdTime = 0;
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

