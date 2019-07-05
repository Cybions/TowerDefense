using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class NarrationManager : MonoBehaviour
{
    public static NarrationManager Instance;

    [SerializeField]
    private Image LeftSideIcon;
    [SerializeField]
    private Image RightSideIcon;
    [SerializeField]
    private Image Background;
    [SerializeField]
    private TextMeshProUGUI TextFieldLeft;
    [SerializeField]
    private TextMeshProUGUI TextFieldRight;
    [SerializeField]
    private TextMeshProUGUI NameFieldLeft;
    [SerializeField]
    private TextMeshProUGUI NameFieldRight;

    public UnityEvent OnEndDiscussion;

    private List<TextLine> CurrentDiscussion;
    public bool isSpeaking = false;
    private Tweener NarrationAction = null;
    private bool istuto;
    private void Start()
    {
        Instance = this;
        CurrentDiscussion = new List<TextLine>();
        ResetPanel(Color.clear);
    }

    public void NewDiscussion(Discussion discussion, bool isTuto = false)
    {
        istuto = isTuto;
        CurrentDiscussion = discussion.TextList;
        StartCoroutine(Speak());
    }

    private void Update()
    {
        if(NarrationAction != null && Input.anyKeyDown && isSpeaking)
        {
            NarrationAction.Complete();
        }
    }

    private IEnumerator Speak()
    {
        isSpeaking = true;
        foreach (TextLine line in CurrentDiscussion)
        {
            TextMeshProUGUI field;
            TextMeshProUGUI name;
            ResetPanel(line.Speaker.BackgroundColor);
            yield return new WaitForSeconds(0.5f);
            if (line.CharacterSide == TextLine.Side.left)
            {
                LeftSideIcon.sprite = line.Speaker.Icon;
                NarrationAction = LeftSideIcon.DOFade(1, .3f);
                field = TextFieldLeft;
                name = NameFieldLeft;
            }
            else
            {
                RightSideIcon.sprite = line.Speaker.Icon;
                NarrationAction = RightSideIcon.DOFade(1, .3f);
                field = TextFieldRight;
                name = NameFieldRight;
            }
            yield return new WaitWhile(() => NarrationAction.IsComplete());
            field.text = line.CharacterTextLine;
            name.text = line.Speaker.Name;
            if (line.DoScreenShake) { ScreenShake(); }
            name.DOFade(1, .3f);
            NarrationAction = field.DOFade(1, .3f);
            yield return new WaitWhile(() => NarrationAction.IsComplete());
            NarrationAction = null;
            yield return new WaitUntil(() => Input.anyKeyDown);            
        }
        ResetPanel(Color.clear);
        isSpeaking = false;
        if (istuto)
        {
            TutorialManager.Instance.NextTutorial();
        }
        Invoke("EndDiscussion", .5f);
    }

    private void ResetPanel(Color BackgroundColor)
    {
        LeftSideIcon.DOFade(0, .3f);
        RightSideIcon.DOFade(0, .3f);
        Background.DOColor(BackgroundColor, .3f);
        TextFieldLeft.DOFade(0, .3f).OnComplete(delegate { TextFieldLeft.text = ""; });
        TextFieldRight.DOFade(0, .3f).OnComplete(delegate { TextFieldRight.text = ""; });
        NameFieldRight.DOFade(0, .3f).OnComplete(delegate { TextFieldLeft.text = ""; });
        NameFieldLeft.DOFade(0, .3f).OnComplete(delegate { TextFieldLeft.text = ""; });
    }

    public void ScreenShake()
    {
        Background.transform.parent.DOShakePosition(.5f, 15f,20);
    }

    public void EndDiscussion()
    {
        OnEndDiscussion.Invoke();
    }
}
