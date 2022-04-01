using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;


[System.Serializable]
public class BattlePropertyInfo: ISerializationCallbackReceiver
{
    public PropertyInfo param { get; set; }

    [SerializeField]
    private int selected;


    /// <summary>
    /// Serialize Object
    /// </summary>
    public void OnBeforeSerialize()
    {
        List<PropertyInfo> propInfo = typeof(BattleEntity).GetProperties()
           .Where(prop => prop.IsDefined(typeof(MoveAffecterAttribute), false)).ToList();
        selected = propInfo.IndexOf(param);
    }

    /// <summary>
    /// Deserialize Object
    /// </summary>
    public void OnAfterDeserialize()
    {
        PropertyInfo[] propInfo = typeof(BattleEntity).GetProperties()
           .Where(prop => prop.IsDefined(typeof(MoveAffecterAttribute), false)).ToArray();

        param = propInfo[selected];
    }

    public BattleStat GetValue(BattleEntity entity)
    {
        return (param.GetValue(entity) as BattleStat);
    }

}
