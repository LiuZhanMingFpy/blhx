using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*

    林哲

    技能1飞机飞行脚本

    12月12日
*/

public class Skill1_fly_Move : MonoBehaviour {

    //通用计时器
    private float common = 0;

    //开始投弹的计时器
    private float start = 1.5f;

    //投弹计时器
    private float boom = 0;

    //投弹后的频率
    private float release = 0.3f;

    //结束投弹的计时器
    private float end = 4;

	void Update () {
        transform.Translate(-0.1f,0f,0);

        common += Time.deltaTime;
            
        if (transform.position.x<=-8.5f)
        {
            Destroy(this.gameObject);           
        }

        if (common>end)
        {
            return;
        }

        if (common>start)
        {
            boom += Time.deltaTime;
            if (boom>release)
            {
                BulletProManager.Instance.Bullet_Produce("Prefabs/UI/FightingPage/boom/boom_boss", transform.Find("fire"));               
                boom = 0;
            }
        }
	}


}
