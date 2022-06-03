using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class ActionPointManager : MonoBehaviour
{
    public System.Action<float> onCompletion;
    private WaitForSeconds waitTimer;

    private List<GameObject> actionPoints;

    [SerializeField]
    private float timer;
    private BattleEntityProper targetEntity;
    [SerializeField]
    private GameObject actionPointsPREFABS; 
    int rollResult;
    int clickedPoints;

    internal void ClickPoint(int index)
    {
        clickedPoints++;
        GameObject ap = actionPoints[index]; 
        Destroy(ap);
    }

    // Start is called before the first frame update
    void Start()
    {
        waitTimer = new WaitForSeconds(timer);
        actionPoints = new List<GameObject> { };
    }

    public void Config(BattleEntityProper targetEntity, int rollResult,
        System.Action<float> completionFunc)
    {
        
        if(!(targetEntity is EnemyBattleEntityProper))
        {
            StartCoroutine("Timer");
            return;
        }

        EnemyBattleEntityProper enemyentity = targetEntity as EnemyBattleEntityProper; 

        clickedPoints = 0;
        StartCoroutine(DelayBeforeBlend(0.1f, () =>
        { 
            this.targetEntity = targetEntity;
            this.onCompletion = completionFunc;
            this.rollResult = rollResult;

            //Not great
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            CinemachineBrain brain = camera.GetComponent<CinemachineBrain>();

            float blendDuration = brain.ActiveBlend.Duration;

            StartCoroutine(DelayBeforeBlend(blendDuration,() =>
            {
                StartCoroutine("Timer");
                IEnumerable<Vector3> points =
                    enemyentity.ActionPoints.GetRandomPoints(rollResult);

                int i = 0;
                foreach (Vector3 v in points)
                {
                    
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(v);
                    GameObject ap =
                        Instantiate(actionPointsPREFABS, screenPos, Quaternion.identity,
                        gameObject.transform);

                    actionPoints.Add(ap);
                    ActionPoint apScript = ap.GetComponent<ActionPoint>();
                    apScript.Config(i, this);
                    i++;
                }
            }));
        }));
    }

    IEnumerator DelayBeforeBlend(float time, System.Action function)
    {
        yield return new WaitForSeconds(time);
        function?.Invoke();
    }

    IEnumerator Timer()
   {
        yield return waitTimer;
        foreach (GameObject g in actionPoints)
        {
            Destroy(g);
        }
        actionPoints.Clear();
        onCompletion?.Invoke(clickedPoints/6f);
   }
}
