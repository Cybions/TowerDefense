using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class TowerSelector : MonoBehaviour
{
    public static TowerSelector Instance;
    public List<Tower> ConstructibleTowers;
    public bool isOpenBuy = false;
    public bool isOpenUpg = false;
    [SerializeField]
    private Transform Buy;
    [SerializeField]
    private Transform Upgrade;
    [SerializeField]
    private TowerInfoPanel TIP;
    [SerializeField]
    private Transform contentScroll;
    [SerializeField]
    private BuyTowerBTN Pref_BTN;

    private Tile CurrentTile;
    private List<BuyTowerBTN> BuyList;
    public UnityEvent OnTowerBuilt;
    public UnityEvent OnTowerUpgraded;
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
        CurrentTile = tileAsking;
        ToggleUpgradeWindow(true);
        print("ASKED");
    }
    public void ConstructTower(int TowerID)
    {
        CurrentTile.My_Tower = new Tower(ConstructibleTowers[TowerID]);
        Instantiate(CurrentTile.My_Tower.T_Prefab, CurrentTile.TowerSpot);
        ToggleBuyWindow(false);
        LevelManager.Instance.ChangeGold(-CurrentTile.My_Tower.T_Price);
        OnTowerBuilt.Invoke();
    }
    public void UpgradeTower()
    {
        OnTowerUpgraded.Invoke();
    }
    public void ToggleBuyWindow(bool open)
    {
        if (open)
        {
            isOpenBuy = true;
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
            Buy.transform.DOScale(Vector3.one, .2f);
        }
        else
        {
            Buy.transform.DOScale(Vector3.zero, .2f);
            TIP.CloseInfo();
            isOpenBuy = false;
        }
    }
    public void ToggleUpgradeWindow(bool open)
    {
        if (open)
        {
            isOpenUpg = true;
            Upgrade.transform.DOScale(Vector3.one, .2f);
        }
        else
        {
            Upgrade.transform.DOScale(Vector3.zero, .2f);
            isOpenUpg = false;
        }
    }
    public void AskTowerInfo(int TowerID)
    {
        TIP.DisplayInfo(ConstructibleTowers[TowerID],TowerID);
    }
}
