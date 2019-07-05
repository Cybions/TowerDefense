using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    [SerializeField]
    private Image GoldIMG;
    [SerializeField]
    private TextMeshProUGUI GoldField;
    [SerializeField]
    private Image LifeIMG;
    [SerializeField]
    private TextMeshProUGUI LifeField;
    [SerializeField]
    private Image WaveIMG;
    [SerializeField]
    private TextMeshProUGUI WaveField;
    [SerializeField]
    private Image ArrowHelp;
    [SerializeField]
    private List<Transform> ListSpots;
    private List<TextLine> Discussion = new List<TextLine>();

    [SerializeField]
    private Button StartWaveBTN;

    int currentTuto = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Invoke("Setup", .1f);
        StartWaveBTN.interactable = false;
    }


    private void Setup()
    {
        NarrationManager.Instance.OnEndDiscussion.AddListener(NextTutorial);
    }

    public void NextTutorial()
    {
        currentTuto++;
        print(currentTuto);
        switch (currentTuto)
        {
            case 2:
                ArrowHelp.transform.DOMove(ListSpots[0].position, .2f);
                break;
            case 3:
                ArrowHelp.transform.DOMove(ListSpots[1].position, .2f);
                break;
            case 4:
                ArrowHelp.transform.DOMove(ListSpots[2].position, .2f);
                break;
            case 5:
                LevelManager.Instance.CanDoAction = true;
                TowerSelector.Instance.OnTowerBuilt.AddListener(NextTutorial);
                ArrowHelp.DOFade(0, .3f);
                break;
            case 6:
                ArrowHelp.DOFade(1, .2f);
                StartWaveBTN.interactable = true;
                ArrowHelp.transform.DOMove(ListSpots[3].position, .2f);
                break;
            case 7:
                LevelManager.Instance.OnWaveEnd.AddListener(NextTutorial);
                break;
            case 8:
                ArrowHelp.DOFade(0, .3f);
                TowerSelector.Instance.OnTowerUpgraded.AddListener(NextTutorial);
                break;
            case 10:
                StartWaveBTN.interactable = true;
                TowerSelector.Instance.ToggleUpgradeWindow(false);
                break;
            case 12:
                break;
            default:
                break;
        }

    }
}