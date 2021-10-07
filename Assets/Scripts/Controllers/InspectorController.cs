using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InspectorController : MonoBehaviour
{
    public Text tex;
    private Vector2 v2;
    public GameObject playerLookLike;
    private string[] duihua ;
    private Tweener tweener;

    void Start ()
    {
       duihua =new string[] { @"<i><b>主人,请问有什么事吗?</b></i>", @"<i><b>如果没有什么事情的话就去看看别的挑战吧！</b></i>" };
        v2 = playerLookLike.transform.position;
	}

    void OnEnable()
    {
        tweener = playerLookLike.transform.DOLocalMoveX(0, 1f, false);
        tweener.OnComplete(SaySomething);

        
    }




    void OnDisable()
    {
        playerLookLike.transform.position = v2;
        tex.text = "";
    }

    void  SaySomething()
    {
        
            
      tweener=  tex.DOText(duihua[0], 0.5f, true);
      
       tex.text = "";
     
    }

 

}
