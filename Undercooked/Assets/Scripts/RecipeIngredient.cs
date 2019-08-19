using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecipeIngredient : ScriptableObject
{
    
    public FoodType foodType;
    public FoodStatus foodStatus;
    public Sprite icon;
    public Material soupMaterial;
}
