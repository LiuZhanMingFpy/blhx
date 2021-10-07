using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordDamMove : MonoBehaviour {

    int num;

    void Start()
    {
        num = Random.Range(1, 3);
    }

    void FixedUpdate () {    

        switch (num)
        {
            case 1:
                transform.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-0.01f, 0.1f), ForceMode2D.Impulse);
                break;

            case 2:
                transform.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-0.03f, 0.1f), ForceMode2D.Impulse);
                break;

            case 3:
                transform.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0.01f, 0.1f), ForceMode2D.Impulse);
                break;

            default:
                break;
        }
       
        Destroy(this.gameObject, 0.8f);
	}
}
