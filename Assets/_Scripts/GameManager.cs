using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private TextMeshProUGUI LifeField;
    [SerializeField]
    private TextMeshProUGUI WaveField;
    [SerializeField]
    private TextMeshProUGUI GoldField;

    public Image Blackscreen;


    public Button StartWaveBTN;

    private void Start()
    {
        Instance = this;
    }

    public void StartWave()
    {
        LevelManager.Instance.StartNewWave();
        StartWaveBTN.interactable = false;
    }

    public void OnEndWave()
    {
        StartWaveBTN.interactable = !LevelManager.Instance.isTutorial;
    }

    public void OnEndLevel()
    {
        if (LevelNarration.Instance.canTrigger()) { return; }
        EndLevel();
    }

    public void ChangeGold(float amount)
    {
        GoldField.text = amount.ToString();
    }

    public void ChangeWave(int Current, int Max)
    {
        WaveField.text = Current.ToString() + "/" + Max.ToString();
    }

    public void ChangeLife(float amount)
    {
        LifeField.text = amount.ToString();
        LifeField.transform.DOShakePosition(.2f);
    }

    public void EndLevel()
    {
        Blackscreen.DOFade(1, 1.2f);
    }
}
