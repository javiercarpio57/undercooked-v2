using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum potState
{
    EMPTY,
    TOMATOED,
    ONIONED,
    MUSHROOMED,
    BURNED
}

public class Container : MonoBehaviour
{
    public potState potState;

    public Image addIngredientIcon;

    public bool isReady = false;

    [SerializeField]
    private List<FoodType> allowed;
    private List<Food> currentFoods;

    [SerializeField]
    private List<Image> images;

    [SerializeField]
    private GameObject parent;
    
    public List<GameObject> gameObjects;

    private float totalTime;
    private float currentTime;

    [HideInInspector]
    public float totalBurningTime;
    [HideInInspector]
    public float currentBurningTime;

    private List<Food> cookedFood;

    private Cooking cook;

    // Start is called before the first frame update
    void Start()
    {
        currentFoods = new List<Food>();
        cookedFood = new List<Food>();

        totalTime = 0f;
        currentTime = 0f;

        totalBurningTime = 0f;
        currentBurningTime = 0f;

        cook = GetComponent<Cooking>();
    }
    
    public bool verifyFood(GameObject movable)
    {
        Food food = movable.GetComponent<Food>();
        if(food != null)
        {
            if(food.getStatus() == FoodStatus.CUT && getListCount() < 3 && potState != potState.BURNED)
            {
                return allowed.Contains(food.getType());
            }
        }

        return false;
    }

    public void addFood(Food food)
    {
        Debug.Log("agregando comida");
        Sprite icon = food.getImage();
        changeIcon(currentFoods.Count, icon);
        
        setParent(food);
        currentFoods.Add(food);

        totalTime += food.currentState.processingTime - 3 * getListCount();

        if(currentFoods.Count == 1)
        {
            if(food.getType() == FoodType.TOMATO)
            {
                potState = potState.TOMATOED;
            }else if(food.getType() == FoodType.ONION)
            {
                potState = potState.ONIONED;
            }
            else if (food.getType() == FoodType.MUSHROM)
            {
                potState = potState.MUSHROOMED;
            }
            changePotState();
        }
    }

    public void addCookedFood(Food food)
    {
        cookedFood.Add(food);
        totalBurningTime += food.currentState.processingTime;
    }

    private void setParent(Food food)
    {
        food.transform.SetParent(parent.transform);
    }

    private void changeIcon(int pos, Sprite sprite)
    {
        images[pos].sprite = sprite;
    }

    public int getListCount()
    {
        return currentFoods.Count;
    }

    public void desactivateEmpty()
    {
        gameObjects[0].SetActive(false);
    }

    public void setActive(int pos)
    {
        gameObjects[pos].SetActive(true);
    }

    public void returnToEmpty()
    {
        foreach (GameObject g in gameObjects)
        {
            g.SetActive(false);
        }
        gameObjects[0].SetActive(true);

        potState = potState.EMPTY;
        isReady = false;
        currentFoods.Clear();
        totalTime = 0f;
        currentTime = 0f;
        totalBurningTime = 0f;
        currentBurningTime = 0f;

        for (int i = 0; i < cook.iconCanvas.transform.childCount; i++)
        {
            Image image = cook.iconCanvas.transform.GetChild(i).GetComponent<Image>();

            image.sprite = addIngredientIcon.sprite;
        }
        cook.imageStatus.sprite = cook.statusCanvas.none;
        cook.setforegroundImageAmount(0f);

        foreach(Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public GameObject getParent()
    {
        return parent;
    }

    public float getCurrentTime()
    {
        return currentTime;
    }

    public float getTotalTime()
    {
        return totalTime;
    }

    public void setCurrentTime(float newTime)
    {
        currentTime = newTime;
    }

    public List<Food> getCurrentFood()
    {
        return currentFoods;
    }

    public void setCurrentFood(List<Food> foodList)
    {
        currentFoods = foodList;
    }

    public void desactivateAllPots() {
        foreach(GameObject g in gameObjects)
        {
            g.SetActive(false);
        }
    }

    public void changePotState()
    {
        desactivateAllPots();
        if(potState == potState.EMPTY)
        {
            setActive(0);
        }else if(potState == potState.TOMATOED)
        {
            setActive(1);
        }else if (potState == potState.ONIONED)
        {
            setActive(2);
        }else if (potState == potState.MUSHROOMED)
        {
            setActive(3);
        }else if (potState == potState.BURNED)
        {
            setActive(4);
        }
    }

    public bool canGiveToPlate()
    {
        if(potState == potState.TOMATOED || potState == potState.ONIONED 
            || potState == potState.MUSHROOMED || getListCount() == 3)
        {
            if (isReady)
            {
                foreach (Food food1 in currentFoods)
                {
                    foreach (Food food2 in currentFoods)
                    {
                        if (food1.getType() != food2.getType())
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        return false;
    }

    public potState getPotContent()
    {
        return potState;
    }


}
