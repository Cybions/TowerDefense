using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControler : MonoBehaviour
{
    [SerializeField]
    private Transform Gun;
    private List<Ennemy> EnnemiesInRange;
    private LineRenderer myLine;
    private bool canShot = true;
    private float damage = 20;
    private float Delay = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        EnnemiesInRange = new List<Ennemy>();
        GetComponent<SphereCollider>().radius = 2.5f;
        myLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnnemiesInRange.Count <= 0) { myLine.SetWidth(0f, 0f); return; }
        Ennemy Target = GetTarget();
        if(Target == null) { myLine.SetWidth(0f, 0f); return; }
        myLine.SetWidth(.2f, .2f);
        myLine.SetPosition(0, Gun.transform.position);
        myLine.SetPosition(1, Target.transform.position);
        if (canShot)
        {
            Target.TakeDamage(damage);
            StartCoroutine(Shot());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ennemy")
        {
            EnnemiesInRange.Add(other.GetComponent<Ennemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ennemy")
        {
            EnnemiesInRange.Remove(other.GetComponent<Ennemy>());
        }
    }

    private IEnumerator Shot()
    {
        canShot = false;
        yield return new WaitForSeconds(Delay);
        canShot = true;
    }

    public Ennemy GetTarget()
    {
        int i = 0;
        while (i < EnnemiesInRange.Count)
        {
            if (LevelManager.Instance.isEnnemyAlive(EnnemiesInRange[i]))
            {
                return EnnemiesInRange[i];
            }
            i++;
        }
        return null;
    }
}
