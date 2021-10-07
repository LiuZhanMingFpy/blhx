using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderEffect
{
    private static ShaderEffect instance;

    public static ShaderEffect Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new ShaderEffect();
            }
            return instance;
        }      
    }

    public IEnumerator WhiteReturn(Transform tran)
    {
        Material my_material = Resources.Load<Material>("UI/FightingPage/Shader/white");

        tran.GetComponent<Image>().material = my_material;

        yield return new WaitForSeconds(0.4f);

        tran.GetComponent<Image>().material = null;
    }
 
}
