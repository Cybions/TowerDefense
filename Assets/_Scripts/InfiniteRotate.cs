using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfiniteRotate : MonoBehaviour
{
    [SerializeField]
    private float Delay = 2f;
    bool isRotating = false;
    private Vector3 Destination = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (!isRotating)
        {
            StartRotation();
            isRotating = true;
        }
    }

    private void StartRotation()
    {
        transform.DORotate(GetDestination(), Delay).SetEase(Ease.Linear).OnComplete(delegate { isRotating = false; });
    }

    private Vector3 GetDestination()
    {

        Destination.y += 180;

        return Destination;
    }
}
