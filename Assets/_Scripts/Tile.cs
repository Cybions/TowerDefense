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

    private ParticleSystem Tile_Particles;

    private void Start()
    {
        if (!isInteracible) { Destroy(this); }
        OriginPos = transform.position;
        DestinationPos = OriginPos;
        DestinationPos.y += .2f;
        MovementTweener = transform.DOMove(OriginPos, 0f);
        Tile_Particles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnMouseOver()
    {
        if (isAtOrigin && SystemInfo.deviceType == DeviceType.Desktop)
        {
            isAtOrigin = false;
            //MovementTweener.Kill();
            //MovementTweener = transform.DOMove(DestinationPos, .8f);
            Tile_Particles.Play();
        }
        if (Input.GetMouseButtonDown(0) && !NarrationManager.Instance.isSpeaking && LevelManager.Instance.CanDoAction)
        {
            AskSelector();
        }
    }
    private void OnMouseExit()
    {
        if (!isAtOrigin)
        {
            isAtOrigin = true;
            //MovementTweener.Kill();
            //MovementTweener = transform.DOMove(OriginPos, 0f);
            Tile_Particles.Stop();
            Invoke("ClearParticles", .1f);
        }
    }

    void ClearParticles()
    {
        Tile_Particles.Clear();
    }

    private void AskSelector()
    {
        if(My_Tower == null && !TowerSelector.Instance.isOpenBuy)
        {
            TowerSelector.Instance.AskNewTower(this);
        }
        if(My_Tower != null && !TowerSelector.Instance.isOpenUpg)
        {
            TowerSelector.Instance.AskUpgrade(this);
        }
    }
}
