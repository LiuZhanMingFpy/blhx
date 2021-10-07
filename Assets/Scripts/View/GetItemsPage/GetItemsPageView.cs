using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemsPageView : MonoBehaviour {

    public GameObject getExp;

    public void ShowGetExp()
    {
        getExp.SetActive(true);
        gameObject.SetActive(false);

    }

}
