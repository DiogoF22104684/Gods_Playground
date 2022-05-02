using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using System.Linq;

public class BattleManager : MonoBehaviour
{
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
    private bool playerDead;

      
    [SerializeField]
    private BattleConfigData battleConfig;


    private BattleEntity playerData;

    [SerializeField]
    private Transform[] enemiesSlots;



    //Not sure se isto fica aqui
    [SerializeField]
    private BattleEntityProper playerProper;

    private List<BattleEntity> enemies;

    //Maybe criar outro componente q trate disto e do resto dos menus
    [SerializeField]
    private GameObject deathPanel;
    [SerializeField]
    private GameObject dubsMenu;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        enemies = new List<BattleEntity>{ };
        
        //Instatiate things
        playerData = new BattleEntity(playerProper, battleConfig.PlayerTemplate);
        playerProper.Config(playerData);

        playerProper.onEndTurn += NextTurn;

        //Bue simplificado 
        playerProper.onDeath += ()=> 
        {
            playerDead = true;
            deathPanel.SetActive(true); 
        } ; 

        //mau
        int index = 0;
        foreach (EnemiesTemplate enTemp in battleConfig.Enemies)
        {
            //Ainda mau mas melhor
            //soparaagoraEnemies[index].gameObject.SetActive(true);
            GameObject bep = Instantiate(enTemp.Prefab,
                enemiesSlots[index].transform.position,
                enemiesSlots[index].transform.rotation);

            BattleEntityProper crtProper = 
                bep.GetComponent<BattleEntityProper>();

            BattleEntity enemyData = new BattleEntity(crtProper, enTemp);

            crtProper.Config(enemyData);
            crtProper.onEndTurn += NextTurn;
            enemies.Add(enemyData);
            (crtProper as EnemyBattleEntityProper).SetPlayers(
                new List<BattleEntity> { playerData });
            crtProper.attackTrigger += AnimationResponse;
            
            index++;
        }
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
        if (playerDead) return;
        

        CleanEntityList();
        List<BattleEntity> turnEnt = new List<BattleEntity>(enemies);
        if (!turnEnt.Any(x => !x.IsDead))
        {
            Debug.Log("Ws all around");
            dubsMenu.SetActive(true);
        }
        turnEnt.Add(playerData);



        bool endOfTurn = turnEnt.TrueForAll(x => x.turns.Stat < 1);

        if (endOfTurn)
        {


            foreach (BattleEntity be in turnEnt)
            {
                be.turns.Stat = be.turns.MaxStat;
            }

            //TurnOrderUpdate
        }

        turnEnt = turnEnt.
            Where(x => x.turns.Stat > 0).
            OrderBy(x => -x.dex.Stat).ToList();

       // foreach (BattleEntity be in turnEnt)
 
        orderDisplay.UpdateDisplay(turnEnt);

        if (turnEnt.Count > 0)
        {
            inTurnEntity = turnEnt[0];
        }

        
        selector.Config(playerProper, enemies.Select(x => x.properEntity).ToList());
    }

    private void NextTurn()
    {
        selector.PlayerHasAttacked = false;
        if (playerDead) return;
        PrepareTurnOrder();
        turnnumb++;
        print(inTurnEntity);
        inTurnEntity.properEntity.StartTurn();
    }

    private void Start()
    {
        PrepareTurnOrder();
       
        playerProper.attackTrigger += AnimationResponse;
        cameraManager = GetComponent<BattleCameraManager>();
        inTurnEntity.properEntity.StartTurn();      
    }

    
    private void AnimationResponse(DefaultAnimations anim, BattleEntity target = null)
    {
        if (target == null)
        {
            foreach (BattleEntityProper bep in selector.SelectedEntities)
            {
                bep.PlayAnimation(anim);
            }
        }
        else
        {
            target.properEntity.PlayAnimation(anim);
        }

        //Kinda scuffed very scuffed
        inTurnEntity.properEntity.EndTurn();
    }


    public void ResolvePlayerAttack(BattleMove move)
    {
        selector.PlayerHasAttacked = true;
        this.move = move;
        Roll();
    }

    private void Roll()
    {
        rollResult = Random.Range(1,7);
        GameObject diceTemp = 
            Instantiate(dice, transform.position, Quaternion.identity);

        DiceScript diceS = diceTemp.GetComponent<DiceScript>();

        if (move.Config.Mechanic == MechanicType.ActionPoints)
        {
            diceS.onResult -= SwitchCamera;
            diceS.onResult += SwitchCamera;
        }
        else
        {
            Attack(rollResult / 6f);
        }
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
        IEnumerable<BattleEntity> enemiesSelected = new List<BattleEntity>() { };

        switch (move.Config.Mechanic) {
            case MechanicType.ActionPoints:
                cameraManager.SwitchCameras();
                break;
        }

        enemiesSelected = selector.GetTargets(move.Config.Type).Select(x => x.entityData);

        //KindaDumbMasPorAgora
        

        move.Function(playerProper.entityData, enemiesSelected, roll);
        
        if (move.Config.Cooldown > 0)
            playerData.AddSkillCooldown(move);
    }
}
