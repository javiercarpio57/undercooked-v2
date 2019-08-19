using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooking : MonoBehaviour
{
    public Canvas iconCanvas;

    Container container;

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Image foregroundImage;
    
    public StatusCanvas statusCanvas;
    [HideInInspector]
    public Image imageStatus;
   
    private void Start()
    {
        container = GetComponent<Container>();
        desactivateCanvas();
        statusCanvas.enabled = false;
        imageStatus = statusCanvas.GetComponent<Image>();
    }

    public void activateCanvas()
    {
        setInitialTime();
        canvas.enabled = true;
    }

    public void desactivateCanvas()
    {
        canvas.enabled = false;
    }

    public void modifyTime(float changeTime)
    {
        container.setCurrentTime(container.getCurrentTime() + changeTime);

        float currentTimePct = (float)container.getCurrentTime() / (float)container.getTotalTime();
        foregroundImage.fillAmount = currentTimePct;
    }

    public void increasingBurningTime(float t)
    {
        container.currentBurningTime += t;
        float currentTimePct = (float)container.currentBurningTime / (float)container.totalBurningTime;
    }

    public void setInitialTime()
    {
        foregroundImage.fillAmount = container.getCurrentTime() / container.getTotalTime();
    }

    public bool isActivated()
    {
        return canvas.enabled;
    }

    public void setforegroundImageAmount(float pct)
    {
        foregroundImage.fillAmount = pct;
    }
}
