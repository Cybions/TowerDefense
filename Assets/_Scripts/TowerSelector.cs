using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TowerSelector : MonoBehaviour
{
    public static TowerSelector Instance;
    public List<Tower> ConstructibleTowers;
    public bool isOpen = false;
    [SerializeField]
    private Transform Buy;
    [SerializeField]
    private TowerInfoPanel TIP;
    [SerializeField]
    private Transform contentScroll;
    [SerializeField]
    private BuyTowerBTN Pref_BTN;

    private Tile CurrentTile;
    private List<BuyTowerBTN> BuyList;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        BuyList = new List<BuyTowerBTN>();
    }

    public void AskNewTower(Tile tileAsking)
    {
        CurrentTile = tileAsking;
        ToggleBuyWindow(true);
    }
    public void AskUpgrade(Tile tileAsking)
    {

    }
    public void ConstructTower(int TowerID)
    {
        CurrentTile.My_Tower = new Tower(ConstructibleTowers[TowerID]);
        Instantiate(CurrentTile.My_Tower.T_Prefab, CurrentTile.TowerSpot);
        ToggleBuyWindow(false);
        GameManager.Instance.ChangeGold(-CurrentTile.My_Tower.T_Price);
    }
    public void ToggleBuyWindow(bool open)
    {
        if (open)
        {
            isOpen = true;
            foreach (BuyTowerBTN btn in BuyList)
            {
                Destroy(btn.gameObject);
            }
            BuyList = new List<BuyTowerBTN>();
            int i = 0;
            foreach(Tower tw in ConstructibleTowers)
            {
                BuyTowerBTN newBTN = Instantiate(Pref_BTN, contentScroll);
                newBTN.Init(i, this);
                BuyList.Add(newBTN);
                i++;
            }
            Buy.transform.DOScale(Vector3.one, .8f);
        }
        else
        {
            Buy.transform.DOScale(Vector3.zero, .8f);
            TIP.CloseInfo();
            isOpen = false;
        }
    }
    public void AskTowerInfo(int TowerID)
    {
        TIP.DisplayInfo(ConstructibleTowers[TowerID],TowerID);
    }
}
