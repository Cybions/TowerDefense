using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class TowerInfoPanel : MonoBehaviour
{
    [SerializeField]
    private TowerSelector TS;
    [SerializeField]
    private TextMeshProUGUI NameField;
    [SerializeField]
    private TextMeshProUGUI DescriptionField;
    [SerializeField]
    private TextMeshProUGUI PriceField;
    [SerializeField]
    private Button PriceBTN;

    private Tweener AnimationTw;
    private int CurrentTowerID;
    public void DisplayInfo(Tower tower, int TowerID)
    {
        NameField.text = tower.T_Name;
        DescriptionField.text = tower.T_Description;
        PriceField.text = tower.T_Price.ToString();
        PriceBTN.interactable = (GameManager.Instance.Player_Gold >= tower.T_Price);
        CurrentTowerID = TowerID;
        AnimationTw = transform.DOScale(Vector3.one, .8f);
    }
    public void CloseInfo()
    {
        AnimationTw.Kill();
        transform.DOScale(Vector3.zero, 0f);
    }
    public void ConfirmConstruction()
    {
        TS.ConstructTower(CurrentTowerID);
    }
}
