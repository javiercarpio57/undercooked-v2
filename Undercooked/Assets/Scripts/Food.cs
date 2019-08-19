using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FoodType
{
    TOMATO,
    ONION,
    MUSHROM
}

public class Food : MonoBehaviour
{

    public FoodType type;
    public FoodState currentState;
    public FoodState initialState;
    public Creator pool;

    
    public Sprite sprite;

    public void assingPool(Creator creator)
    {
        pool = creator;
    }

    private void OnEnable()
    {
        changeState(initialState);
    }

    public void delete()
    {
        pool.returnToPool(this.gameObject);
    }

    public void changeState(FoodState newState)
    {
        currentState.gameObject.SetActive(false);
        newState.gameObject.SetActive(true);
        newState.currentTime = 0;
        currentState = newState;
    }

    public void processFood(float time)
    {
        currentState.processFood(time);
    }

    public FoodStatus getStatus()
    {
        return currentState.status;
    }

    public FoodType getType()
    {
        return type;
    }

    public void changeToCook()
    {
        currentState.changeToCook();
    }

    public Sprite getImage()
    {
        return sprite;
    }
}
