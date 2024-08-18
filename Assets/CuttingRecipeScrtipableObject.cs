using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipe", menuName = "ScriptableObjects/CuttingRecipeScriptableObject", order = 2)]
public class CuttingRecipeScrtipableObject : ScriptableObject
{
    public KitchenObjectScriptableObject inObject;
    public KitchenObjectScriptableObject outObject;
    
}
