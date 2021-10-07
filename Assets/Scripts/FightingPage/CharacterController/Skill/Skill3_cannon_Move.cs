using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*

    林哲

    技能3海豹控制脚本

    12月13日
*/

public class Skill3_cannon_Move : MonoBehaviour {

    Image my_pic;

	void Start () {
        //初始化图片      
        my_pic = gameObject.GetComponent<Image>();

        my_pic.sprite= Resources.Load<Sprite>("UI/FightingPage/bullet/cannon_1");

        Destroy(this.gameObject,8.5f);
	}

    //计时替换精灵图
    float time = 0;

    //产生笑脸粒子效果计时器
    float smile = 0;
    //笑脸产生的频率
    float smile_round = 0;

    void Update () {
        time += Time.deltaTime;
         
        if(time>=6.5f)
        {
            my_pic.sprite = Resources.Load<Sprite>("UI/FightingPage/bullet/cannon_2");
            time = 0;
        }

        smile += Time.deltaTime;
        if(smile>=4)
        {
            smile_round += Time.deltaTime;
            if (smile_round>0.05f)
            {
                BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/cannon/EmojiHappy", transform,new Vector3(100,100,100));
                smile_round = 0;
            }

        }

	}

    //海豹消失后产生粒子特效
    public void Destroy()
    {
        BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/cannon/NovaBlue",transform,new Vector3(150,150,150));

        BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/cannon/MagicAuraBlue", transform, new Vector3(200, 300, 1));

        BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/cannon/MagicChargeBlue", transform, new Vector3(200, 200, 100));

        BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/cannon/MagicSphereBlue", transform, new Vector3(250, 250, 250));

        //爆炸后Visuable下子物体带enemic标签的船扣血
        EventCenter.Broadcast(EGameEvent.skill3_cannon_deblood);

        BulletProManager.Instance.BulletAndBoomAnim_Pro("Prefabs/UI/FightingPage/cannon/EmojiDrool", transform, new Vector3(100, 100, 100));
      
    }

    
}
