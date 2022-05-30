using System.Collections;
using System.Collections.Generic;
using CombatSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/EntitiesTemplates/Player")]
public class PlayerTemplate : EntityTemplate
{

    [SerializeField]
    private Inventory inventory;
    public Inventory Inventory => inventory;

}
