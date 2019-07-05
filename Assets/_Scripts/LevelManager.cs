using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
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
    private bool isMenu = false;
    public bool isTutorial = false;
    private bool infiniteSpawnended = false;
    public int CurrentWaveNumber = 0;
    public float PlayerGold = 460.0f;
    private Wave CurrentWave;
    private bool isSpawning = false;
    private List<Ennemy> CurrentEnnemies;
    public UnityEvent OnTutoCompleted;
    public UnityEvent OnWaveEnd;
    public UnityEvent OnLevelEnd;
    public UnityEvent OnLevelStart;
    public bool CanDoAction = true;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if (isMenu) { CurrentWave = Waves[CurrentWaveNumber]; StartCoroutine(InfiniteWaveSpawner()); return; }
        Invoke("Infos", .1f);
        if (isTutorial)
        {
            CanDoAction = false;
            Invoke("StartTuto", 1);
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
        GameManager.Instance.Blackscreen.DOFade(0, 1.2f).OnComplete(delegate {

            OnLevelStart.Invoke(); });
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
        if (!isMenu)
        {
            ChangeGold(ennemy.Gold);
        }
            
        CurrentEnnemies.Remove(ennemy);
        Destroy(ennemy.gameObject);
        CheckIfEnd();
    }

    public void OnEnnemyReachTown(Ennemy ennemy)
    {
        if (!isMenu)
        {
            TownLife -= ennemy.Damage;
            Infos();
        }     
        CurrentEnnemies.Remove(ennemy);
        Destroy(ennemy.gameObject);     
        CheckIfEnd();
    }

    private void CheckIfEnd()
    {
        if (CurrentEnnemies.Count <= 0 && !isSpawning)
        {
            if (isMenu) { infiniteSpawnended = true; return; }
            if (Waves.Count > CurrentWaveNumber)
            {
                GameManager.Instance.OnEndWave();
                OnWaveEnd.Invoke();
            }
            else
            {
                GameManager.Instance.OnEndLevel();
                OnLevelEnd.Invoke();
            }
        }
    }

    public bool isEnnemyAlive(Ennemy ennemy)
    {
        foreach(Ennemy en in CurrentEnnemies)
        {
            if (en == ennemy) { return true; }
        }
        return false;
    }

    private IEnumerator InfiniteWaveSpawner()
    {
        while (isMenu)
        {
            infiniteSpawnended = false;
            StartCoroutine(WaveSpawner());
            yield return null;
            yield return new WaitWhile(() => !infiniteSpawnended);
        }
    }
}
