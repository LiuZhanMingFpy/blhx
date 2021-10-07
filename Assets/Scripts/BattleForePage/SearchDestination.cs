using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchDestination : MonoBehaviour {
    public GameObject mainCam;
    public GameObject interceptPage;
    private NavMeshAgent my_agent;
    public GameObject BattleForePage;
    private Animator my_anim;

	void Start () {

        my_agent = transform.GetComponent<NavMeshAgent>();

        my_anim = transform.GetComponent<Animator>();
    }
	
	
	void Update () {

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(r,out hit,100,1<<LayerMask.NameToLayer("UI")))
            {
                my_agent.SetDestination(hit.transform.position);
                Destroy(BulletProManager.Instance.Prefabs("Prefabs/BattleForePage/define",hit.transform,Vector3.one),0.8f); 
            }

        }

        if (my_agent.velocity != Vector3.zero)
        {
            transform.Find("MagicZoneYellow").gameObject.SetActive(false);
            my_anim.SetFloat("Speed", 0.2f);
        }
        else
        {
            transform.Find("MagicZoneYellow").gameObject.SetActive(true);
            my_anim.SetFloat("Speed",0);
        }

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "enemic")
        {
            mainCam.GetComponent<Camera>().orthographic = true;
            mainCam.transform.rotation = Quaternion.identity;
            interceptPage.SetActive(true);
            BattleForePage.SetActive(false);
            gameObject.transform.parent.gameObject.SetActive(false);
            Debug.Log("跳转排阵界面");
            //摧毁自身gameobject的船
        }

    }
}
