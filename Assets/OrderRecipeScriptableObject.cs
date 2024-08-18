using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrderRecipe", menuName = "ScriptableObjects/OrderRecipeScriptableObject", order = 3)]
public class OrderRecipeScriptableObject : ScriptableObject
{
    public KitchenObjectScriptableObject[] order;
}