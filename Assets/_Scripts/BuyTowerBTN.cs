using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTowerBTN : MonoBehaviour
{
    private int MyTowerID;
    private TowerSelector TS;
    public void Init(int newID, TowerSelector selector)
    {
        MyTowerID = newID;
        TS = selector;
    }
    public void OnTowerSelected()
    {
        TS.AskTowerInfo(MyTowerID);
    }
}
