using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ennemy : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public float Life;
    public float Gold = 2;

    public List<Transform> Path = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup(List<Transform> NewPath)
    {
        Path = NewPath;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        Tweener Movement;
        int i = 0;
        while (Path.Count > i)
        {
            transform.DOLookAt(Path[i].position, .8f);
            Movement = transform.DOMove(Path[i].position, GetTimeBySpeed(Path[i].position)).SetEase(Ease.Linear);
            yield return new WaitWhile(Movement.IsPlaying);
            i++;
        }
        LevelManager.Instance.OnEnnemyReachTown(this);
    }

    private float GetTimeBySpeed(Vector3 Destination)
    {
        return  (Vector3.Distance(transform.position, Destination)/ Speed);
    }

    public void TakeDamage(float amount)
    {
        Life -= amount;
        if(Life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        LevelManager.Instance.OnEnnemyDied(this);
    }
}
