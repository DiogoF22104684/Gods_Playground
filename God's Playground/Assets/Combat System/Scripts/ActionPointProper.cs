using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionPointProper : MonoBehaviour
{
    [SerializeField]
    List<GameObject> actionPointPos;
    
    public IEnumerable<Vector3> GetRandomPoints(int numb)
    {
        if (actionPointPos == null) return null;
        if (actionPointPos.Count < 1) return null;
        List<Vector3> rndPoint = 
            actionPointPos.OrderBy(x => Random.Range(0,40)).
            Select(x => x.transform.position).ToList();
  
        return rndPoint.GetRange(0, numb); 
    }
}
