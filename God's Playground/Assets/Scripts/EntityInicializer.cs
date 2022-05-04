using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Playables;

public class EntityInicializer : MonoBehaviour
{
    [SerializeField]
    private BattleConfigData battleData;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    public GameObject[] enemyPREFABS;

    [SerializeField]
    private PlayableDirector transition;
    [SerializeField]
    private Spawn_Area_script spawnArea;
    [SerializeField]
    private List<GameObject> currentEnemies;

    private List<BattleTransitioner> bts =>
            currentEnemies.Select(x => x.GetComponent<BattleTransitioner>()).ToList();

    public List<GameObject> CurrentEnemies { get => currentEnemies; set => currentEnemies = value; }


    // Start is called before the first frame update
    void Start()
    {
        battleData.onEnterBattle = null;
        battleData.onEnterBattle += SaveEnemyData;
        player.transform.position = battleData.PlayerPos;
        InstatiateEnemies();
    }

    public void SaveEnemyData(BattleTransitioner bt)
    {
        battleData.enemyInScene = bts.Select(x => x.Id).ToList();
        battleData.enemyPos = bts.Select(x => x.transform.position).ToList();
        battleData.currentEnemy = currentEnemies.IndexOf(bt.gameObject);
    }

    public void InstatiateEnemies()
    {
        
        currentEnemies = new List<GameObject> { };
        for (int i1 = 0; i1 < battleData.enemyInScene.Count; i1++)
        {
            if (i1 == battleData.currentEnemy) continue;
            int i = (int)battleData.enemyInScene[i1];
            GameObject enemy = Instantiate(enemyPREFABS[i], battleData.enemyPos[i1], Quaternion.identity);
            BattleTransitioner bt = enemy.GetComponent<BattleTransitioner>();
            bt.Id = i;
            bt.Transition = transition;
            enemy.GetComponent<Enemy_AI>().PatrolZone = spawnArea;
            currentEnemies.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
