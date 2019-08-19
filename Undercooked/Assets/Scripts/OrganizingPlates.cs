using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizingPlates : MonoBehaviour
{
    public Holder tablePlatesHolder;
    public float eatingTime = 10f;

    private float currentTime = 0f;
    private Holder myHolder;

    // Start is called before the first frame update
    void Start()
    {
        myHolder = GetComponent<Holder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myHolder.hasMovable())
        {
            foreach (Transform child in myHolder.movableAnchor.transform)
            {
                Plate plate = child.gameObject.GetComponent<Plate>();
                if(plate.currentTime <= eatingTime)
                {
                    plate.modifyTime(Time.deltaTime);
                    plate.cleanCanvas();
                }
                else
                {
                    plate.currentTime = 0f;
                    plate.toDirty();
                    tablePlatesHolder.setMovable(plate.gameObject.GetComponent<MovableObject>());
                    myHolder.removeMovable();
                }
                
            }
        }
    }
}
