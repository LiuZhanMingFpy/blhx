using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgImageMove : MonoBehaviour {
		
	void FixedUpdate () {

        Vector3 v = new Vector3(-3.25f, 0, 0)*Time.deltaTime;

        (transform as RectTransform).Translate(v);

        //Debug.Log(transform.position.x);

        if (transform.position.x<-13.71f)
        {
            transform.position += new Vector3(35.56f,0,0);
        }

	}
}
