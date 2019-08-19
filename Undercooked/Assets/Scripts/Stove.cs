using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    
    private Holder stoveHolder;

    void Start()
    {
        stoveHolder = GetComponent<Holder>();
    }

    // Update is called once per frame
    void Update()
    {
        Container container = stoveHolder.GetComponentInChildren<Container>();
        if(container != null)
        {
            Cooking cooking = container.GetComponent<Cooking>();
            if (container.getListCount() > 0) {

                if(container.getCurrentTime() <= container.getTotalTime())
                {
                    cooking.modifyTime(Time.deltaTime);
                    cooking.imageStatus.sprite = cooking.statusCanvas.none;
                    container.isReady = false;
                }
                else if(container.getCurrentTime() > container.getTotalTime() && container.currentBurningTime <= container.totalBurningTime)
                {
                    cooking.imageStatus.sprite = cooking.statusCanvas.ready;
                    foreach (Food f in container.getCurrentFood())
                    {
                        if (f.getStatus() == FoodStatus.INSPOT)
                        {
                            f.changeState(f.currentState.nextState);
                            container.addCookedFood(f);
                        }
                    }
                    cooking.increasingBurningTime(Time.deltaTime);

                    if(container.currentBurningTime/container.totalBurningTime >= 0.5)
                    {
                        cooking.imageStatus.sprite = cooking.statusCanvas.burning;
                    }

                    if(container.getListCount() == 3)
                    {
                        container.isReady = true;
                    }
                }
                else
                {
                    cooking.imageStatus.sprite = cooking.statusCanvas.burned;
                    container.potState = potState.BURNED;
                    container.changePotState();
                    container.isReady = false;
                }
            }
        }
    }


}
