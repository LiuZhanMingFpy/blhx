using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEndPageView : MonoBehaviour {

    public GameObject getItemsPage;
    
    public void ShowGetItems()
    {
        getItemsPage.SetActive(true);
        gameObject.SetActive(false);

    }

}
