using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFloorController : MonoBehaviour {

    Transform character;

	void Start () {

        character = GameObject.Find("/Main Camera").transform.Find("unitychan");

       

	}

    GameObject zone;
  
	void Update () {


       

        

    }



    bool isPro = false;
 
    

    //void OnTriggerExit(Collider col)
    //{
    //    if (col.tag=="character")
    //    {
    //        isPro = false;
    //        Destroy(zone);
    //    }

    //}   
}

