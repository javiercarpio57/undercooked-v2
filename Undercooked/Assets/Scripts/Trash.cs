using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public void destroyObject(MovableObject food)
    {
        Food recycledFood = food.GetComponent<Food>();
        
        if(recycledFood != null)
        {
            recycledFood.delete();
        }
    }
}
