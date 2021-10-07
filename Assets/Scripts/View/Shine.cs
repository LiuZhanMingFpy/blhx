using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 此脚本若需要哪个物体发光即挂在其身上即可
/// </summary>
public class Shine : MonoBehaviour
{
  
    private float i=-5;

	void Start () {
		
	}
	
	void Update ()
	{

	    i += Time.deltaTime*8;
	    gameObject.GetComponent<Image>().material.SetFloat("_percent", i);
	    if (i>=50)
	    {
	        i = -5;
	    }
    }
}
