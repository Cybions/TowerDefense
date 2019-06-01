using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [SerializeField]
    private bool SpawnAtStart = true;
    [SerializeField]
    private List<ParticleSystem> My_particles;
    [SerializeField]
    private float spawnDelay = 1.2f;
    [SerializeField]
    private Transform SpawnTransform;
    [SerializeField]
    private List<Transform> Path;
    private float Delay;
    private Wave CurrentWave;

    public void Spawn(Wave newWave)
    {
        foreach(ParticleSystem ps in My_particles)
        {
            ps.Play();
        }
        CurrentWave = newWave;
        if(CurrentWave.CustomSpawnDelay >= 0)
        {
            Delay = CurrentWave.CustomSpawnDelay;
        }
        else
        {
            Delay = spawnDelay;
        }
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        foreach(Ennemy en in CurrentWave.EnnemyList)
        {
            Ennemy newEn = Instantiate(en, SpawnTransform);
            newEn.Setup(Path);
            GameManager.Instance.EnnemiesInLevel.Add(newEn);
            yield return new WaitForSeconds(Delay);
        }
    }
}
