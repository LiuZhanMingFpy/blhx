using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillList : MonoBehaviour {

    Transform character1;
    Transform character2;
    Transform character3;

    Transform fly_Pro1;
    Transform fly_Pro2;
    Transform fly_Pro3;

    Transform cannon_Pro;
    void Start()
    {
        character1 = transform.Find("character1");
        character2 = transform.Find("character2");
        character3 = transform.Find("character3");

        fly_Pro1 = transform.Find("skill1_fly_Pro/fly_Pro1");
        fly_Pro2 = transform.Find("skill1_fly_Pro/fly_Pro2");
        fly_Pro3 = transform.Find("skill1_fly_Pro/fly_Pro3");

        cannon_Pro = transform.Find("skill3_cannon_Pro");

        EventCenter.AddListener(EGameEvent.skill2_strength, Skill2);
        EventCenter.AddListener(EGameEvent.skill3_cannon, Skill3);
        EventCenter.AddListener(EGameEvent.skill1_fly, Skill1);

        EventCenter.AddListener(EGameEvent.bullet_stop,StopShot);       
    }

    //释放飞机技能_1
    private void Skill1()
    {
        if (fly_Pro1)
        {
            BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/boom/skill1_fly",fly_Pro1);
        }
        if (fly_Pro2)
        {
            BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/boom/skill1_fly", fly_Pro2);
        }
        if (fly_Pro3)
        {
            BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/boom/skill1_fly", fly_Pro3);
        }
    }
    //释放狂暴技能_2 
    private void Skill2()
    {
        if (character1)
        {
           character1.GetComponent<MainCharacterCtl>().atk_fre+=2;
           character1.GetComponent<MainCharacterCtl>().isStrength = true;
        }
        if (character2)
        {
            character2.GetComponent<FollowCharaterCtl1>().atk_fre += 2;
            character2.GetComponent<FollowCharaterCtl1>().isStrength = true;
        }
        if (character3)
        {
            character3.GetComponent<FollowCharaterCtl2>().atk_fre += 13;
            character3.GetComponent<FollowCharaterCtl2>().isStrength = true;
        }

    }

    //释放高射炮技能_3
    private void Skill3()
    {
       BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/cannon/cannon_1",cannon_Pro,new Vector3(1,1,1));
       
    }

    //战斗结束之后停止子弹发射和波浪的产生
    private void StopShot()
    {
        character1.GetComponent<MainCharacterCtl>().isShot = false;

        character2.GetComponent<FollowCharaterCtl1>().isShot = false;

        character3.GetComponent<FollowCharaterCtl2>().isShot = false;


    }
}
