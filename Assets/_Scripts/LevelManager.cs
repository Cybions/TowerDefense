using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private float TownLife = 100;
    [SerializeField]
    private List<Wave> Waves;
    [SerializeField]
    private EnnemySpawner ennemySpawner;

    [SerializeField]
    private bool isTutorial = false;

    public int CurrentWaveNumber = 0;
    public float PlayerGold = 460.0f;
    private Wave CurrentWave;
    private bool isSpawning = false;
    private List<Ennemy> CurrentEnnemies;
    public UnityEvent OnTutoCompleted;
    public UnityEvent OnWaveEnd;
    public bool CanDoAction = true;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Invoke("Infos", .1f);
        if (isTutorial)
        {
            CanDoAction = false;
            Invoke("StartTuto", 1f);
        }
    }

   
    void StartTuto()
    {
        OnTutoCompleted.Invoke();
    }
    private void Infos()
    {
        GameManager.Instance.ChangeWave(CurrentWaveNumber, Waves.Count);
        GameManager.Instance.ChangeLife(TownLife);
    }

    public void StartNewWave()
    {
        CurrentWave = Waves[CurrentWaveNumber];
        CurrentWaveNumber++;
        Infos();
        StartCoroutine(WaveSpawner());
    }

    private IEnumerator WaveSpawner()
    {
        isSpawning = true;
        ennemySpawner.StartWave();
        CurrentEnnemies = new List<Ennemy>();
        yield return new WaitForSeconds(2.0f);
        foreach (Ennemy ennemy in CurrentWave.EnnemyList)
        {
            CurrentEnnemies.Add(ennemySpawner.SpawnEnnemy(ennemy));
            yield return new WaitForSeconds(CurrentWave.SpawnDelay);
        }
        ennemySpawner.StopWave();
        isSpawning = false;
    }

    public void ChangeGold(float amount)
    {
        PlayerGold += amount;
        GameManager.Instance.ChangeGold(PlayerGold);
    }

    public void OnEnnemyDied(Ennemy ennemy)
    {
        ChangeGold(ennemy.Gold);        
        CurrentEnnemies.Remove(ennemy);
        Destroy(ennemy.gameObject);
        if (CurrentEnnemies.Count <= 0 && !isSpawning)
        {
            if(Waves.Count > CurrentWaveNumber)
            {
                GameManager.Instance.OnEndWave();
                OnWaveEnd.Invoke();
            }
            else
            {
                GameManager.Instance.OnEndLevel();
            }
        }
    }

    public void OnEnnemyReachTown(Ennemy ennemy)
    {
        TownLife -= ennemy.Damage;
        CurrentEnnemies.Remove(ennemy);
        Destroy(ennemy.gameObject);
        Infos();
    }

    public bool isEnnemyAlive(Ennemy ennemy)
    {
        foreach(Ennemy en in CurrentEnnemies)
        {
            if (en == ennemy) { return true; }
        }
        return false;
    }


}
