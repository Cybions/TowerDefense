using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tile : MonoBehaviour
{
    public bool isInteracible = true;
    public Tower My_Tower = null;
    public Transform TowerSpot;

    private Vector3 OriginPos;
    private Vector3 DestinationPos;
    private Tweener MovementTweener;
    public bool isAtOrigin = true;
    private void Start()
    {
        if (!isInteracible) { this.enabled = false; }
        OriginPos = transform.position;
        DestinationPos = OriginPos;
        DestinationPos.y += .2f;
        MovementTweener = transform.DOMove(OriginPos, 0f);
    }
    private void OnMouseOver()
    {
        if (isAtOrigin)
        {
            isAtOrigin = false;
            MovementTweener.Kill();
            MovementTweener = transform.DOMove(DestinationPos, .8f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            AskSelector();
        }
    }
    private void OnMouseExit()
    {
        if (!isAtOrigin)
        {
            isAtOrigin = true;
            MovementTweener.Kill();
            MovementTweener = transform.DOMove(OriginPos, 0f);
        }
    }

    private void AskSelector()
    {
        if(My_Tower == null && !TowerSelector.Instance.isOpen)
        {
            TowerSelector.Instance.AskNewTower(this);
        }
    }
}
