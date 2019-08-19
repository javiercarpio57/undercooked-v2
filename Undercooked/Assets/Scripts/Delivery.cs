using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delivery : MonoBehaviour
{
    public List<RecipeData> possibleRecipe;
    public Recipe baseRecipe;
    public Canvas canvas;
    public List<Recipe> recipeList;
    public int maxRecipes = 5;
    public int timeRecipes = 15;

    private float elapsedTime = 240f;
    private int totalPoints = 0;
    public Text points;
    public Text time;

    FoodType food;

    public Animator anim;
    public Text text;
    private float animTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generateRecipe());
        points.text = totalPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        points.text = totalPoints.ToString();

        elapsedTime -= Time.deltaTime;
        time.text = FormatTime(elapsedTime);

        if(canvas.transform.childCount > 1)
        {
            if (recipeList[0].elapsedTime >= recipeList[0].requiredTime)
            {
                recipeList.Remove(recipeList[0]);
                canvas.transform.GetChild(2).parent = null;
                totalPoints += -5;
                activatePanels();
            }
        }

        if (anim.GetBool("showUP"))
        {
            if (anim.GetBool("showUP"))
            {
                if (animTime < 1.5)
                {
                    animTime += Time.deltaTime;
                }
                else
                {
                    animTime = 0f;
                    anim.SetBool("showUP", false);
                }
            }
        }
    }

    public void playAnimation()
    {
        anim.SetBool("showUP", true);
    }

    public void activatePanels()
    {
        for (int i = 0; i < recipeList.Count; i++)
        {
            recipeList[i].setInitialPosition(recipeList[i].getFinalPosition());
            recipeList[i].setFinalPosition(new Vector3(i * 150 + 90, -10, 0));
            recipeList[i].setInitialTime();
            recipeList[i].setDistance();
            recipeList[i].moving = true;
        }
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void receivePlate(plateContent content)
    {

        for (int i = 0; i < recipeList.Count; i++)
        {
            if (recipeList[i].plateContent == content)
            {
                int point = 2 * Mathf.FloorToInt(6 * (float)(recipeList[i].timePercentage / 1.5));
                totalPoints += point;
                text.text = "+" + point.ToString();
                recipeList.Remove(recipeList[i]);
                canvas.transform.GetChild(i + 2).parent = null;
                totalPoints += 20;
                playAnimation();
                break;
            }
        }
    }

    IEnumerator generateRecipe()
    {
        while(true)
        {
            if(recipeList.Count < maxRecipes)
            {
                int randomIndex = Random.Range(0, possibleRecipe.Count);
                Recipe recipe = Instantiate(baseRecipe, canvas.transform);
                recipe.setup(possibleRecipe[randomIndex], recipeList.Count);
                recipeList.Add(recipe);
            }
            yield return new WaitForSeconds(timeRecipes);
        }
    }
}
