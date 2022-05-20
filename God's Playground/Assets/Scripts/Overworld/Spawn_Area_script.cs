using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Spawn_Area_script : MonoBehaviour
{
    [SerializeField] [Range(1,10)]float radius;
    //[SerializeField]
    //private PlayableDirector director;
    //[SerializeField]
    //private EntityInicializer inici;

    public bool InArea(Vector3 pos)
    {
        return  Mathf.Pow(pos.x - transform.position.x, 2) + 
                Mathf.Pow(pos.y - transform.position.y, 2) <=
                Mathf.Pow(radius, 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 
        radius);
    }

    public Vector3 GetRndInside()
    {
        Vector3 rndPoint = UnityEngine.Random.insideUnitCircle * radius;

        return rndPoint.z(transform.position.z) + transform.position.z(0);
    }

    public void InstatiateEnemy(GameObject enemyPREFAB, Vector3 pos, int id)
    {
        //GameObject enemy = Instantiate(enemyPREFAB, pos, Quaternion.identity);
        //BattleTransitioner bt = enemy.GetComponent<BattleTransitioner>();
        //bt.Id = id;
        //bt.Transition = director;
        //enemy.GetComponent<Enemy_AI>().PatrolZone = this;
        //inici.CurrentEnemies.Add(enemy);
    }

    //Id should be inside the template an we should instatiate using only the template.
    public void InstatiateEnemy(GameObject enemyPREFAB, int id)
    {
        //InstatiateEnemy(enemyPREFAB, GetRndInside(), id);
    }
}
