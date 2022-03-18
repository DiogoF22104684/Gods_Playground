using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{

    public System.Action onResult;

    public void DiceResult()
    {
        onResult?.Invoke();
        Destroy(gameObject);
    }
}
