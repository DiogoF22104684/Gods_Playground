using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

public class TesteAttack : MonoBehaviour
{
    [SerializeField]
    private BattleMove move;
    [SerializeField]
    private BattleEntityProper battle;
    [SerializeField]
    private BattleEntityProper target;


    private BattleCameraManager cameraManager;
    [SerializeField]
    private GameObject actionTimer;
    [SerializeField]
    private GameObject dice;
    private int rollResult;

    private void Start()
    {
        battle.attackTrigger += AnimationResponse;
        cameraManager = GetComponent<BattleCameraManager>();
    }

    private void AnimationResponse(DefaultAnimations anim)
    {
        target.PlayAnimation(anim);
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

        cameraManager.SwitchCameras(target.gameObject);  
        GameObject acTimer = 
            Instantiate(actionTimer,transform.position, 
                Quaternion.identity, cameraManager.ActiveCanvas.transform);
        ActionPointManager acPointManager = 
            acTimer.GetComponent<ActionPointManager>();
        acPointManager.Config(target, rollResult, Attack);
        
        //Spawn points
        //Add AttackFunction to spawnPoints event

    }

    public void Attack(float roll)
    {
        cameraManager.SwitchCameras();
        move.Function(battle.entityData, target.entityData, roll);
    }
}
