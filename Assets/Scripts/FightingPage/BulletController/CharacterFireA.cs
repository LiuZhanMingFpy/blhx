using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*

林哲

所有角色及各类预制体通用脚本

12月6日

*/
public class CharacterFireA : MonoBehaviour {
	
	void Start () {
		
	}

    GameObject target = null;

    float conDis = 100;

    void Update () {

        //圆形搜索
        Collider2D[] cols=Physics2D.OverlapCircleAll(transform.position,10,1<<LayerMask.NameToLayer("enemic"));

        Debug.Log(cols.Length);

        if (cols.Length != 0)
        {
            if (cols.Length==1)
            {
              target = cols[0].gameObject;
            }

            //取长度最小的进行设置攻击目标
            for (int i = 0; i < cols.Length; i++)
            {
                if (Vector2.Distance(transform.position,cols[i].transform.position) < conDis)
                {
                    conDis = Vector2.Distance(transform.position, cols[i].transform.position);
                    target = cols[i].gameObject;
                }
                
            }
                        
        }
        else
        {
            target = null;
        }

        Debug.Log(target.name);

        if (target)
        {
            //transform.position = Vector2.Lerp(transform.position, target.transform.position, 0.1f);
            (transform as RectTransform).LookAt((target.transform as RectTransform));
        }
       
    }
}
