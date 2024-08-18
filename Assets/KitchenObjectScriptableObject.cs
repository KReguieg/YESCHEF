using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObject", menuName = "ScriptableObjects/KitchenScriptableObject", order = 1)]
public class KitchenObjectScriptableObject : ScriptableObject
{
    public Transform prefab;
    public string objectName;
}
