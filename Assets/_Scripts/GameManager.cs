using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Ennemy> EnnemiesInLevel;
    public float Player_Gold = 650;
    [SerializeField]
    private TextMeshProUGUI GoldField;
    [SerializeField]
    private List<EnnemySpawner> ennemySpawners;
    [SerializeField]
    private List<Wave> ListWave;

    

    private void Start()
    {
        Instance = this;
        EnnemiesInLevel = new List<Ennemy>();
        Invoke("SpawnWave", 10);
    }

    void SpawnWave()
    {
        foreach(EnnemySpawner es in ennemySpawners)
        {
            es.Spawn(ListWave[0]);
        }
    }

    public void ChangeGold(float amount)
    {
        Player_Gold += amount;
        GoldField.text = "Gold: " + Player_Gold.ToString();
        print("Changed " + amount + " Gold");
    }
}
