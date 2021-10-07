using UnityEngine;
using System.Collections;

public enum EGameEvent
{
    skill1_fly,
    skill2_strength,
    skill3_cannon,
    skill3_cannon_deblood,

    bullet_stop,

    ADD_SCORE = 1,//添加积分
    UPDATE_SCORE,//更新积分
    DAMAGE ,//受到伤害
    UPDATE_HP,//更新血量
    UPDATE_COUNTDOWN,//更新倒计时
    GAME_STATE,//游戏状态（准备、游戏中、GameOver）
}
