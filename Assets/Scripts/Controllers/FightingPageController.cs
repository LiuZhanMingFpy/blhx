using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
/*

林哲

战斗页面总控制管理器

12月7日

*/
public class FightingPageController : MonoBehaviour {

    //技能1属性值
	Transform skill1;
    //技能是否冷却好后执行闪光动画    
    bool skill1_isFlash = false;
    //技能是否冷却完全可使用
    bool skill1_isReady = false;
    //技能冷却时间
    float skill1_cdTime = 0;

    //技能2属性值
    Transform skill2;
    bool skill2_isFlash = false;
    bool skill2_isReady = false;
    float skill2_cdTime = 0;

    //技能3属性值
    Transform skill3;
    bool skill3_isFlash = false;
    bool skill3_isReady = false;
    float skill3_cdTime = 0;

    //战斗计时
    int minutes=0;
    float seconds=0;
    void Start () {
        #region 技能数据初始化
        //技能数据初始化
        //技能1
        skill1 = transform.Find("TolScence/Visuable/SkillList/skill1_fly");
        skill1.Find("skill_cd").GetComponent<Image>().fillAmount = skill1_cdTime;
        skill1.Find("skill1_icon").gameObject.SetActive(false);

        //技能2
        skill2 = transform.Find("TolScence/Visuable/SkillList/skill2_fish");
        skill2.Find("skill_cd").GetComponent<Image>().fillAmount = skill2_cdTime;
        skill2.Find("skill2_icon").gameObject.SetActive(false);

        //技能3
        skill3 = transform.Find("TolScence/Visuable/SkillList/skill3_cannon");
        skill3.Find("skill_cd").GetComponent<Image>().fillAmount = skill3_cdTime;
        skill3.Find("skill3_icon").gameObject.SetActive(false);
        #endregion

        #region 战斗计时初始化
        isPause = false;
        isWarning = false;
        bossWaring = 0;
        transform.Find("TolScence/Visuable/PauseShow/Time_mm").GetComponent<Text>().text = "0" + ":";
        transform.Find("TolScence/Visuable/PauseShow/Time_ss").GetComponent<Text>().text = "0"; 
        #endregion


    }

    public bool isPause = false;

    public bool isWarning = false;
    private float bossWaring = 0;

	void Update () {

        seconds += Time.deltaTime;
        //战斗计时
        #region 战斗计时
        if (seconds < 60)
        {
            transform.Find("TolScence/Visuable/PauseShow/Time_ss").GetComponent<Text>().text = ((int)seconds).ToString();
        }
        else
        {
            seconds = 0;
            minutes += 1;
            transform.Find("TolScence/Visuable/PauseShow/Time_mm").GetComponent<Text>().text = minutes.ToString() + ":";
        }
        #endregion

        //暂停切换
        #region 暂停切换
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        #endregion

        //Boss死亡慢动作播放
        #region boss死亡时间缩放计时
        if (isWarning)
        {
            Time.timeScale = 0.2f;
            bossWaring += Time.deltaTime;
            if (bossWaring > 0.5f)
            {
                Time.timeScale = 1;
                isWarning = false;
            }

        } 
        #endregion


        #region 技能1
        //技能1模块
        skill1_cdTime += Time.deltaTime * 0.04f;
        skill1_cdTime = Mathf.Clamp01(skill1_cdTime);
        skill1.Find("skill_cd").GetComponent<Image>().fillAmount = skill1_cdTime;

        if (skill1_isReady)
        {
            skill1.Find("skill1_icon").gameObject.SetActive(true);
        }
        else
        {
            skill1.Find("skill1_icon").gameObject.SetActive(false);
        }

        if (skill1_cdTime >= 1)
        {
            skill1_isReady = true;

            if (!skill1_isFlash)
            {
                Skill1_Isread();
            }

        }
        #endregion

        #region 技能2
        //技能2模块
        skill2_cdTime += Time.deltaTime * 0.1f;
        skill2_cdTime = Mathf.Clamp01(skill2_cdTime);
        skill2.Find("skill_cd").GetComponent<Image>().fillAmount = skill2_cdTime;

        if (skill2_isReady)
        {
            skill2.Find("skill2_icon").gameObject.SetActive(true);
        }
        else
        {
            skill2.Find("skill2_icon").gameObject.SetActive(false);
        }

        if (skill2_cdTime >= 1)
        {
            skill2_isReady = true;

            if (!skill2_isFlash)
            {
                Skill2_Isread();
            }

        }
        #endregion

        #region 技能3
        //技能3模块
        skill3_cdTime += Time.deltaTime * 0.03f;
        skill3_cdTime = Mathf.Clamp01(skill3_cdTime);
        skill3.Find("skill_cd").GetComponent<Image>().fillAmount = skill3_cdTime;

        if (skill3_isReady)
        {
            skill3.Find("skill3_icon").gameObject.SetActive(true);
        }
        else
        {
            skill3.Find("skill3_icon").gameObject.SetActive(false);
        }

        if (skill3_cdTime >= 1)
        {
            skill3_isReady = true;

            if (!skill3_isFlash)
            {
                Skill3_Isread();
            }

        } 
        #endregion
    }

    //技能3使用结束动画回调函数
    public void Skill3_cannon_anim()
    {
        transform.Find("Skill3_cannon_Anim").gameObject.SetActive(false);
    }

    //boss警告回调函数
    public void BossWarning()
    {
        transform.Find("BossWarning").gameObject.SetActive(false);
    }

    //暂停点击回调函数
    public void OnClick_Pause()
    {
        //Debug.Log("1");
        isPause = !isPause;
    }

    //使用技能1
    public void Skill1_Use()
    {
        if(skill1_isReady)
        {
            skill1_cdTime = 0;
            skill1_isReady = false;
            skill1_isFlash = false;           

            EventCenter.Broadcast(EGameEvent.skill1_fly);

            //Debug.Log("使用了飞机");
        }
        
    } 

    //技能1恢复动画
    private void Skill1_Isread()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(skill1.Find("skill1_reload").GetComponent<Image>().DOFade(1, 0.5f));
        seq.Append(skill1.Find("skill1_reload").GetComponent<Image>().DOFade(0, 0.5f));
        skill1_isFlash = true;
        //Debug.Log(skill1_isRead);
    }

    //使用技能2
    public void Skill2_Use()
    {
        if (skill2_isReady)
        {
            skill2_cdTime = 0;
            skill2_isReady = false;
            skill2_isFlash = false;
            
            EventCenter.Broadcast(EGameEvent.skill2_strength);

            //Debug.Log("使用了狂暴");
        }

    }

    //技能2恢复动画
    private void Skill2_Isread()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(skill2.Find("skill2_reload").GetComponent<Image>().DOFade(1, 0.5f));
        seq.Append(skill2.Find("skill2_reload").GetComponent<Image>().DOFade(0, 0.5f));
        skill2_isFlash = true;
        //Debug.Log(skill1_isRead);
    }

    //使用技能3
    public void Skill3_Use()
    {
        if (skill3_isReady)
        {
            skill3_cdTime = 0;
            skill3_isReady = false;
            skill3_isFlash = false;
            //TODO释放技能动画,调用事件系统Broadcast

            EventCenter.Broadcast(EGameEvent.skill3_cannon);

            //激活技能3动画效果
            transform.Find("Skill3_cannon_Anim").gameObject.SetActive(true);
            //Debug.Log("使用了高射炮");
        }

    }

    //技能3恢复动画
    private void Skill3_Isread()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(skill3.Find("skill3_reload").GetComponent<Image>().DOFade(1, 0.5f));
        seq.Append(skill3.Find("skill3_reload").GetComponent<Image>().DOFade(0, 0.5f));
        skill3_isFlash = true;
        //Debug.Log(skill1_isRead);
    }
}
