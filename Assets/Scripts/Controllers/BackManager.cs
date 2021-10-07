using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackManager : MonoBehaviour {
    public GameObject ExitMessage;
    public Button Sure;
    public Button Canel;
    private GameObject Exit;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Canel.onClick.AddListener(Canel_Onclick);
        Sure.onClick.AddListener(SureOnClick);
        Exit = transform.Find("Canvas").gameObject;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            if (ExitMessage == null)
            {
               
                StartCoroutine("ResetQuitMessage");
            }
            else
            {
                Exit.SetActive(true);
            }
        }

      
    }

    IEnumerable ResetQuitMessage()
    {
        yield return  new  WaitForSeconds(1.0f);
        if (ExitMessage != null)
        {
            Destroy(ExitMessage);
        }
    }

     public  void Canel_Onclick()
    {
        Exit.SetActive(false);
    }

    public void SureOnClick()
    {
        Application.Quit();
    }
}
