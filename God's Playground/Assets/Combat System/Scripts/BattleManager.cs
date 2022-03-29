using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private BattleMove move;

    [SerializeField]
    EntitySelector selector;
    [SerializeField]
    BattleOrderDisplay orderDisplay;

    private BattleCameraManager cameraManager;
    [SerializeField]
    private GameObject actionTimer;
    [SerializeField]
    private GameObject dice;

    private BattleEntity inTurnEntity;

    private int rollResult;

    private int turnnumb;


    //Isto é para alterar
    [SerializeField]
    private BattleEntityProper[] soparaagoraEnemies;
    //Isto é para alterar
    [SerializeField]
    private EnemiesTemplate[] enemiesTemplate;

    //Isto nao
    [SerializeField]
    private PlayerTemplate playerTemplate;
    [SerializeField]
    private BattleEntity playerData;

    //Not sure se isto fica aqui
    [SerializeField]
    private BattleEntityProper playerProper;

    [SerializeField]
    private List<BattleEntity> enemies;


    private void Awake()
    {
        enemies = new List<BattleEntity>{ };
        //Instatiate things
        playerData = new BattleEntity(playerTemplate.HP, playerProper, playerTemplate);
        playerProper.Config(playerData);

        playerProper.onEndTurn += NextTurn;

        //mau
        int index = 0;
        foreach (EnemiesTemplate temp in enemiesTemplate)
        {
            BattleEntity enemyData = new BattleEntity(temp.HP, soparaagoraEnemies[index], temp);
            soparaagoraEnemies[index].Config(enemyData);
            soparaagoraEnemies[index].onEndTurn += NextTurn;
            enemies.Add(enemyData);
            //MAU
            (soparaagoraEnemies[index] as EnemyBattleEntityProper).SetPlayers(new List<BattleEntity> { playerData });
            soparaagoraEnemies[index].attackTrigger += AnimationResponse;
            index++;
        }

        PrepareTurnOrder();
    }

    private void CleanEntityList()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i].properEntity == null)
                enemies.RemoveAt(i);
        }
    }

    private void PrepareTurnOrder()
    {
        CleanEntityList();
        List<BattleEntity> turnEnt = new List<BattleEntity>(enemies);
        turnEnt.Add(playerData);

        bool endOfTurn = turnEnt.TrueForAll(x => x.hadTurn == true);

        if (endOfTurn)
        {

           
            foreach(BattleEntity be in turnEnt)
            {
                 be.hadTurn = false;
            }

            //TurnOrderUpdate
        }


       
            turnEnt = turnEnt.
                Where(x => x.hadTurn == false).
                OrderBy(x => -x.dex).ToList();

        orderDisplay.UpdateDisplay(turnEnt);

        if (turnEnt.Count > 0)
        {
            inTurnEntity = turnEnt[0];
        }
    }

    private void NextTurn()
    {
        
        //Teste
        if (turnnumb <= 20)
        {              
            PrepareTurnOrder();
            Debug.Log(turnnumb);
            turnnumb++;
            inTurnEntity.properEntity.StartTurn();           
        }
    }

    private void Start()
    {      
        playerProper.attackTrigger += AnimationResponse;
        cameraManager = GetComponent<BattleCameraManager>();
        inTurnEntity.properEntity.StartTurn();
       
    }

    
    private void AnimationResponse(DefaultAnimations anim, BattleEntity target = null)
    {
        if(target == null)
            selector.SelectedEntity.PlayAnimation(anim);
        else
        {
            target.properEntity.PlayAnimation(anim);
        }

        //Kinda scuffed very scuffed
        inTurnEntity.properEntity.EndTurn();
    }



    public void Roll()
    {
        rollResult = Random.Range(1,7);
        GameObject diceTemp = 
            Instantiate(dice, transform.position, Quaternion.identity);

        DiceScript diceS = diceTemp.GetComponent<DiceScript>();
        diceS.onResult -= SwitchCamera;
        diceS.onResult += SwitchCamera; 
    }

    private void SwitchCamera()
    {

        cameraManager.SwitchCameras(selector.SelectedEntity.gameObject);  
        
        GameObject acTimer = 
            Instantiate(actionTimer,transform.position, 
                Quaternion.identity, cameraManager.ActiveCanvas.transform);
        
        ActionPointManager acPointManager = 
            acTimer.GetComponent<ActionPointManager>();
       
        acPointManager.Config(selector.SelectedEntity, rollResult, Attack);
        
        //Spawn points
        //Add AttackFunction to spawnPoints event

    }

    public void Attack(float roll)
    {
        cameraManager.SwitchCameras();
        move.Function(playerProper.entityData, selector.SelectedEntity.entityData, roll);
    }
}
