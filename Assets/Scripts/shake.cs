using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class shake : MonoBehaviour {

    public void MakeItShake() {

        DOTween dt = new DOTween();
        transform.DOShakePosition(1f,3);
    }
}
