using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*

林哲

敌方舰队船类1控制脚本

12月5日

*/
public class Boat1Data
{
    public Boat1Data(float hp)
    {
        this.hp = hp;
    }

    public float hp;
    
}

public class Boat1Controller : MonoBehaviour {
    private Image my_hp;

    private const float const_hp = 300;

    public Boat1Data data;

    public float atk_CdTime=5f;
	
	void Start () {

        my_hp = transform.Find("hp").GetComponent<Image>();

        data = new Boat1Data(const_hp);

        EventCenter.AddListener(EGameEvent.skill3_cannon_deblood, Skill3_cannon_deblood);
      
    }

    float waveback_1 = 5f;

    float waveback_2 = 5f;

    bool smoke_1 = true;
    bool smoke_2 = true;
    bool smoke_3 = true;
    void FixedUpdate()
    {
        //船只移动
        Vector3 t = new Vector3(-1f, -0.5f, 0) * Time.deltaTime;

        transform.Translate(t);
        
    }

    void Update () {

        my_hp.fillAmount = data.hp / const_hp;

        if (data.hp<=0)
        {
            StopAllCoroutines();
            BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_orange_soul",transform,new Vector3(100,100,100));
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

        //transform.Translate(-0.02f, -0.01f, 0);

        //船只到达指定坐标开始发动攻击
        if (transform.position.x<12)
        {
            atk_CdTime += Time.deltaTime;

            if (atk_CdTime>1.5f)
            {
                BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/bullet/bullet_yellow", transform.Find("fire"));

                //BulletPoorManager.Instance.GetGo_yellow("Prefabs/UI/FightingPage/bullet/bullet_yellow", transform.Find("fire"));

                atk_CdTime = 0;
            }
          
        }     

    }

    void Skill3_cannon_deblood()
    {

        data.hp -= BulletProManager.Instance.data_cannon.atk;
    }

    Material my_material;
  
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "bullet_blue")
        {
            //Debug.Log(BulletProManager.Instance.data.atk);

            //实例化爆炸效果
            data.hp -= BulletProManager.Instance.data.atk;
            GameObject bullet_blue = BulletProManager.Instance.BulletExploreAnim_enemic("Prefabs/UI/FightingPage/ExploreEffect/bullet_blue",col.transform,new Vector3(100,100,1));

            //伤害字体生成
            BulletProManager.Instance.CharacterDam_Words(10,transform,"DamWords","3");

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
