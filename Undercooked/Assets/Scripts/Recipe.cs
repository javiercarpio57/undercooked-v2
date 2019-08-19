using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    public Image dish;
    public List<Image> ingredientImages;
    public Image progressbarForeground;
    public Gradient gradient;
    public plateContent plateContent;

    [HideInInspector]
    public float requiredTime;
    [HideInInspector]
    public float elapsedTime;
    //private List<RecipeIngredient> ingredients; //seria un food

    private Vector3 finalPosition;
    private Vector3 initialPosition;

    float initialTime;
    float distance;
    float speed = 500;

    [HideInInspector]
    public bool moving;

    RectTransform rectTransform;

    [HideInInspector]
    public float timePercentage;

    private Animator anim;

    private void Start()
    {
        timePercentage = 0f;
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    public void setup(RecipeData recipe, int index)
    {
        dish.sprite = recipe.dishSprite;
        plateContent = recipe.plateContent;
        for (int i = 0; i < ingredientImages.Count; i++)
        {
            ingredientImages[i].sprite = recipe.ingredients[i].sprite;
        }
        requiredTime = recipe.time;

        initialPosition = new Vector3(1250, -10, 0);
        finalPosition = new Vector3(index * 150 + 90, -10, 0);
        rectTransform.anchoredPosition = initialPosition;
        initialTime = Time.time;
        distance = Vector3.Distance(initialPosition, finalPosition);
        moving = true;
    }

    private void Update()
    {
        if (moving)
        {
            float currentDistance = (Time.time - initialTime) * speed;
            float percentage = currentDistance / distance;
            rectTransform.anchoredPosition = Vector3.Lerp(initialPosition, finalPosition, percentage);

            if (percentage >= 1)
            {
                moving = false;
            }
        }


        if (!moving)
        {
            elapsedTime += Time.deltaTime;
            timePercentage = 1 - (elapsedTime / requiredTime);
            progressbarForeground.fillAmount = timePercentage;
            progressbarForeground.color = gradient.Evaluate(timePercentage);

            if(timePercentage < 0.25)
            {
                anim.SetBool("Tremble", true);
            }
        }



    }

    public Vector3 getFinalPosition()
    {
        return finalPosition;
    }

    public void setInitialTime()
    {
        initialTime = Time.time;
    }

    public void setInitialPosition(Vector3 newInitialPosition)
    {
        initialPosition = newInitialPosition;
    }

    public void setFinalPosition(Vector3 newFinalPosition)
    {
        finalPosition = newFinalPosition;
    }

    public void setDistance()
    {
        distance = Vector3.Distance(initialPosition, finalPosition);
    }
}
