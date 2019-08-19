using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum plateContent
{
    NONE,
    TOMATO,
    ONION,
    MUSHROOM,
    WASTE
}

public class Plate : MonoBehaviour
{
    public plateContent plateContent;
    public Canvas canvas;
    public List<GameObject> gameObjects;

    [HideInInspector]
    public float currentTime = 0f;

    public void receiveContent(Canvas original)
    {
        canvas.enabled = true;
        for (int i = 0; i < original.transform.childCount; i++)
        {
            Image originalImage = original.transform.GetChild(i).GetComponent<Image>();
            Image currentImage = canvas.transform.GetChild(i).GetComponent<Image>();

            currentImage.sprite = originalImage.sprite;
        }
    }

    public void setPlateContent(potState potState)
    {
        desactivateAllPlates();

        if(potState == potState.TOMATOED)
        {
            setActive(1);
            plateContent = plateContent.TOMATO;
        }
        else if (potState == potState.ONIONED)
        {
            setActive(2);
            plateContent = plateContent.ONION;
        }
        else if (potState == potState.MUSHROOMED)
        {
            setActive(3);
            plateContent = plateContent.MUSHROOM;
        }
    }

    public void desactivateAllPlates()
    {
        foreach (GameObject g in gameObjects)
        {
            g.SetActive(false);
        }
    }

    public void setActive(int pos)
    {
        gameObjects[pos].SetActive(true);
    }

    public void returnToClean()
    {
        plateContent = plateContent.NONE;
        desactivateAllPlates();
        gameObjects[0].SetActive(true);
    }

    public void toDirty()
    {
        plateContent = plateContent.WASTE;
        desactivateAllPlates();
        gameObjects[5].SetActive(true);
    }

    public void toEating()
    {
        desactivateAllPlates();
        gameObjects[4].SetActive(true);
    }

    public void cleanCanvas()
    {
        canvas.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    public void modifyTime(float time)
    {
        currentTime += time;

    }
}
