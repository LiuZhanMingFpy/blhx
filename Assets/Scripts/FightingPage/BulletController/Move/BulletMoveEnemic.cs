using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveEnemic : MonoBehaviour {

    void FixedUpdate()
    {
        Vector3 v = new Vector3(10f, 0, 0) * Time.deltaTime;
        transform.Translate(v);
    }

	void Update () {
    
        AllCharacterCtl.BulletMove_BoardLimit(transform);
	}
}
