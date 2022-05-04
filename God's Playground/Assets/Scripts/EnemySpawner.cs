using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private float spawnRate;
    WaitForSeconds wfs;
    [SerializeField]
    private int maxEnemies;
    [SerializeField]
    private Spawn_Area_script spawnArea;
    [SerializeField]
    private GameObject enemyPREFAB;
    [SerializeField]
    int id;

    // Start is called before the first frame update
    void Start()
    {
        wfs = new WaitForSeconds(spawnRate);
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return wfs;
            int currEnemyNumb = 
                GameObject.FindObjectsOfType<BattleTransitioner>().ToList().Count();
            if(currEnemyNumb < maxEnemies)
                spawnArea.InstatiateEnemy(enemyPREFAB, id);
        }
    }

}
