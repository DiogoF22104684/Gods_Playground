using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

[Serializable]
public class ParamsContainer
{
    [SerializeField]
    string String;
    [SerializeField]
    int Int;
    [SerializeField]
    int Enum;
    [SerializeField]
    bool Bool;
    [SerializeField]
    UnityEngine.Object Obj;

    [SerializeField]
    string type;
    [SerializeField]
    string qualifiedName;

    public ParamsContainer(object obj, System.Type type)
    {
        this.Type = type.ToString();
        this.QualifiedName = type.AssemblyQualifiedName;

        if (type.IsSubclassOf(typeof(UnityEngine.Object)))
        {
            Obj1 = obj as UnityEngine.Object;
        }
        else if (type == typeof(int))
        {
            Int1 = Convert.ToInt32(obj);
        }
        else if (type == typeof(string))
        {
            String1 = Convert.ToString(obj);
        }
        else if (type == typeof(bool))
        {
            Bool1 = Convert.ToBoolean(obj);
        }
        else if (type.IsEnum)
        {
            Enum1 = Convert.ToInt32(obj);
        }      
    }

    public string String1 { get => String; set => String = value; }
    public int Int1 { get => Int; set => Int = value; }
    public UnityEngine.Object Obj1 { get => Obj; set => Obj = value; }
    public string Type { get => type; set => type = value; }
    public string QualifiedName { get => qualifiedName; set => qualifiedName = value; }
    public bool Bool1 { get => Bool; set => Bool = value; }
    public int Enum1 { get => Enum; set => Enum = value; }

    public object GetValue()
    {
        if (Type == typeof(string).ToString())
            return String1;
        else if (Type == typeof(int).ToString())
            return Int1;
        else if (Type == typeof(bool).ToString())
            return Bool1;
        else if (Obj1 != null)
            return Obj1;
        else if (System.Type.GetType(QualifiedName).IsEnum)
            return Enum1;
        return Enum1;
    }
}
