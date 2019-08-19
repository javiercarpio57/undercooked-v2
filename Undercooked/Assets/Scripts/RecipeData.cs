using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecipeData : ScriptableObject
{
    public List<Food> ingredients;
    public Sprite dishSprite;
    public float time;
    public plateContent plateContent;
}
