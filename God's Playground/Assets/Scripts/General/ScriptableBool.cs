using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scriptables/Variables/Bool")]
public class ScriptableBool : ScriptableObject
{
    [SerializeField]
    private bool value;

    public bool Value { get => value; set => this.value = value; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Start();
    }

    public void Start()
    {
        value = false;
    }

}
