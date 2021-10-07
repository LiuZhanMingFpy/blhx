using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*

林哲

水波浪移动脚本

12月5日

*/
public class WaveMove : MonoBehaviour {

	void Start () {
		
	}
	
      	

	void Update () {
        
        transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(1f, 0, 0), 0.1f);     
        
	}
}
