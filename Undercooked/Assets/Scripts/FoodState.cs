using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodStatus
{
    RAW,
    CUT,
    INSPOT,
    COOKED,
    BURNED
}

public class FoodState : MonoBehaviour
{
    public FoodStatus status;
    public GameObject mesh;
    public float processingTime;
    public FoodState nextState;

    [HideInInspector]
    public float currentTime;
    private Food parentFood;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
        parentFood = GetComponentInParent<Food>();
    }
    
    public void processFood(float time)
    {
        currentTime += time;
        if(currentTime >= processingTime)
        {
            parentFood.changeState(nextState);
        }
    }

    public void changeToCook()
    {
        parentFood.changeState(nextState);
    }
}
