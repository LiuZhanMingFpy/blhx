using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*

林哲

子弹飞行控制脚本

12月5日

*/

public class BulletMove : MonoBehaviour {

    void FixedUpdate()
    {
        Vector3 v = new Vector3(12.5f,0,0)*Time.deltaTime;
        transform.Translate(v);
    }

	void Update () {
      
        AllCharacterCtl.BulletMove_BoardLimit(this.transform);
	}
}
