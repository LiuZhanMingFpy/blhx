using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    林哲

    技能1导弹飞行脚本

    12月12日
*/
public class Skill1_boom_Move : MonoBehaviour {
   
    private float timing=0;

	void Update () {

        transform.Translate(0.1f,0,0);

        timing += Time.deltaTime;
        if (timing>0.8f)
        {
            BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/boom/boom_collision",transform);
          
            BoomPro(Random.Range(0,4));
       
            Destroy(this.gameObject);
        }
        
	}

    //产生爆炸效果预制体加载
    internal void BoomPro(int num)
    {
        switch (num)
        {
            case 0:
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/ExploreEffect/bullet_green_smoke", transform);
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/ExploreEffect/fly_exploresmoke_Green", transform);
                break;

            case 1:
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/ExploreEffect/bullet_blue_smoke", transform);
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/ExploreEffect/fly_exploresmoke_White", transform);
                break;

            case 2:
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/ExploreEffect/bullet_red_smoke", transform);
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/ExploreEffect/fly_exploresmoke_Red", transform);
                break;

            case 3:
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/ExploreEffect/bullet_purple_smoke", transform);
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/ExploreEffect/fly_exploresmoke_Purple", transform);
                break;

            default:
            break;
        }

    }
}
