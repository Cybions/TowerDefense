using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShakePoition();
    }

    void ShakePoition()
    {
        transform.DOShakeRotation(5, 10f).OnComplete(delegate { ShakePoition(); });
    }

}
