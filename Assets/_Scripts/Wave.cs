using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWave", menuName = "TD/Wave", order = 2)]
public class Wave : ScriptableObject
{
    public List<Ennemy> EnnemyList;
    public float CustomSpawnDelay = -1f;
}
