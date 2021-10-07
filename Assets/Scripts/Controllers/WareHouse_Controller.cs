using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouse_Controller : MonoBehaviour
{


    public GameObject WH; 
	void Start () {
		
	}

    public void CloseWare()
    {
        WareHouse_View WHV = WH.GetComponent<WareHouse_View>();
        WHV.CloseWareHouse();
    }

}
