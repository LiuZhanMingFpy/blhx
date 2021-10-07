using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour {
     GameObject endofbattle;
	// Use this for initialization
	void Start () {
        endofbattle = GameObject.Find("Canvas").transform.Find("BattleEndPage").gameObject;
        Invoke("ShowEndOfBattle", 4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ShowEndOfBattle() { endofbattle.SetActive(true); Destroy(this.gameObject); }
}
