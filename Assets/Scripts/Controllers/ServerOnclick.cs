using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class ServerOnclick : MonoBehaviour
{
    public Button SeletBtn;
   /// <summary>
   /// 传递按钮信息
   /// </summary>
    void Start()
    {  
        SeletBtn =GetComponent<Button>();
        SeletBtn.onClick.AddListener(OnPress);
    }

    void OnPress()
    {
       ServerList._instance.OnServerselect(this.gameObject);
    }
}
