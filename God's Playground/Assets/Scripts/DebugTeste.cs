using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugTeste : MonoBehaviour
{

    [SerializeField]
    private BattleConfigData bcd;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("Overworld");
            Destroy(gameObject);
        }
    }
}
