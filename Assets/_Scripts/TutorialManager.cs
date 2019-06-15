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

    int currentTuto = -1;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Invoke("Setup", .1f);
        StartWaveBTN.interactable = false;
    }


    private void Setup()
    {
        LevelManager.Instance.OnTutoCompleted.AddListener(NextTutorial);
    }

    public void NextTutorial()
    {
        currentTuto++;
        switch (currentTuto)
        {
            case 0:
                Presentations();
                break;
            case 1:
                Terrain();
                break;
            case 2:
                Gold();
                break;
            case 3:
                Life();
                break;
            case 4:
                Wave();
                break;
            case 5:
                LevelManager.Instance.CanDoAction = true;
                TowerSelector.Instance.OnTowerBuilt.AddListener(NextTutorial);
                ArrowHelp.DOFade(0, .3f);
                break;
            case 6:
                ArrowHelp.DOFade(1, .2f);
                StartWaveBTN.interactable = true;
                BeginWave();
                break;
            case 7:
                LevelManager.Instance.OnWaveEnd.AddListener(NextTutorial);
                break;
            case 8:
                EndWave();
                break;
            case 9:
                //WAIT
                break;
            case 10:
                EndTutorial();
                break;
        }

    }

    private void Presentations()
    {
        Discussion = new List<TextLine>();
        createDiscussion(1,TextLine.Side.right, "Capitaine ! Capitaine !!");
        createDiscussion(0, TextLine.Side.left, "Que se passe t-il ?");
        createDiscussion(1, TextLine.Side.right, "Les monstres sont à nos portes ! Une bonne partie de nos hommes sont tombés.");
        createDiscussion(0, TextLine.Side.left, "!!?");
        createDiscussion(0, TextLine.Side.left, "Impossible ! Nous devons protéger le roi. Faites moi un rapport soldat.");
        SendDiscussion();
    }
    private void Terrain()
    {
        Discussion = new List<TextLine>();
        createDiscussion(1, TextLine.Side.right, "Nous devons mettre en place une stratégie de défense.");
        createDiscussion(0, TextLine.Side.left, "Nous avons beaucoup de place pour placer nos tours.");
        SendDiscussion();
    }
    private void Gold()
    {
        Discussion = new List<TextLine>();
        ArrowHelp.transform.DOMove(ListSpots[0].position, .2f);
        createDiscussion(1, TextLine.Side.right, "Nous avons un peu d'argent pour mettre en place ces tours.");
        SendDiscussion();
    }
    private void Life()
    {
        Discussion = new List<TextLine>();
        ArrowHelp.transform.DOMove(ListSpots[1].position, .2f);
        createDiscussion(0, TextLine.Side.left, "Le roi nous fait confiance pour protéger les villageois.");
        SendDiscussion();
    }
    private void Wave()
    {
        Discussion = new List<TextLine>();
        ArrowHelp.transform.DOMove(ListSpots[2].position, .2f);
        createDiscussion(1, TextLine.Side.right, "D'après nos éclaireurs, l'ennemi s'est organisé en deux vagues.");
        createDiscussion(0, TextLine.Side.left, "Un million de vagues ne suffirait pas contre nous !");
        createDiscussion(0, TextLine.Side.left, "Préparez vous soldat, je vous indiquerais où placer nos défenses.");
        createDiscussion(1, TextLine.Side.right, "A vos ordres.");
        SendDiscussion();
    }
    private void BeginWave()
    {
        Discussion = new List<TextLine>();
        ArrowHelp.transform.DOMove(ListSpots[3].position, .2f);
        createDiscussion(0, TextLine.Side.left, "Tenez vos positions, ils arrivent !");
        SendDiscussion();
    }
    private void EndWave()
    {
        Discussion = new List<TextLine>();
        createDiscussion(1, TextLine.Side.right, "Capitaine ! Nous avons réussi !");
        createDiscussion(0, TextLine.Side.left, "Ne vous réjouissez pas trop vite. Quel l'état de la situation ?");
        createDiscussion(1, TextLine.Side.right, "Nous avons assez d'argent pour améliorer notre tour.");
        createDiscussion(0, TextLine.Side.left, "Et qu'est-ce que cela va apporter ?");
        createDiscussion(1, TextLine.Side.right, "Tout dépends du batiment. Pour améliorer la tour, il suffit de la sélectionner puis demander à nos ingénieurs d'améliorer leur tour.");
        SendDiscussion();
    }
    private void EndTutorial()
    {
        Discussion = new List<TextLine>();
        createDiscussion(0, TextLine.Side.left, "Prennez donc ça !");
        SendDiscussion();
    }
    void createDiscussion(int index, TextLine.Side side, string text)
    {
        TextLine tl = new TextLine();
        tl.CharacterIndex = index;
        tl.CharacterSide = side;
        tl.CharacterTextLine = text;
        Discussion.Add(tl);
    }
    void SendDiscussion()
    {
        NarrationManager.Instance.NewDiscussion(Discussion,true);
    }
}
