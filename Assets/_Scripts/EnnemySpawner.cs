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

    public void StartWave()
    {
        foreach (ParticleSystem ps in My_particles)
        {
            ps.Play();
        }
    }
    public void StopWave()
    {
        foreach (ParticleSystem ps in My_particles)
        {
            ps.Stop();
        }
    }

    public Ennemy SpawnEnnemy(Ennemy ennemy)
    {
        Ennemy newEn = Instantiate(ennemy, SpawnTransform);
        newEn.Setup(Path);
        return newEn;
    }
}
