using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour {

    Transform target = null;

    private float waveback_1 = 0;
    void FixedUpdate()
    {
        transform.Translate(0.02f,0,0);

        Collider2D[] cols=Physics2D.OverlapCircleAll(transform.position,10);

        if (cols.Length>0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag=="character")
                {
                    target = cols[i].transform;
                }
            }
        }

        //Debug.Log(target.position-transform.position);
        
        if (target)
        {   
            //TODO

            //if (Vector3.Dot((transform as RectTransform).forward,(target as RectTransform).localPosition - (transform as RectTransform).localPosition) > 0)
            //{
                //Debug.Log("1");
                //transform.position = Vector3.Lerp(transform.position, target.position, 0.01f);
            //}
            transform.Translate(0.03f, -0.01f, 0);
        }
        else
        {
            transform.Translate(0.02f, 0, 0);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,180),0.05f);

        AllCharacterCtl.BulletMove_BoardLimit(transform);

        waveback_1 += Time.deltaTime;

        if (waveback_1 > 0.2f)
        {
            BulletProManager.Instance.FishorWave_Produce("Prefabs/UI/FightingPage/Enemic/wave1_enemic", transform.Find("wave"));
            waveback_1 = 0;
        }

    }

	
}
