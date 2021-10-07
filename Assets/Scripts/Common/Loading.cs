using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(StartLoading("Battle"));
	}

    IEnumerator StartLoading(string str)
    {
        float i = 0;
        AsyncOperation acop = SceneManager.LoadSceneAsync(str);
        acop.allowSceneActivation = false;
        while (i<=100)
        {
            i++;
            yield return new WaitForEndOfFrame();

        }
        acop.allowSceneActivation = true;
    }

}
