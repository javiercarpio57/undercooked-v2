using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureDetection : MonoBehaviour
{
    List<Furniture> furnitures;
    Furniture current;
    Furniture selected;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        furnitures = new List<Furniture>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        selectFurniture();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entre en trigger con: " + other.name);
        Furniture f = other.gameObject.GetComponent<Furniture>();
        if (f != null)
        {
            furnitures.Add(f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Furniture f = other.gameObject.GetComponent<Furniture>();
        if (f != null)
        {
            furnitures.Remove(f);
            if(f == current)
            {
                current.removeHighlight();
                current = null;
            }

            Chopper chopper = f.GetComponent<Chopper>();
            if(chopper != null)
            {
                chopper.stopChopping();
                anim.SetBool("chopping", false);
            }
        }
    }

    public void selectFurniture()
    {
        if (current != null)
        {
            if(current.GetComponent<Creator>() == null)
            {
                current.removeHighlight();
            }
        }

        if (furnitures.Count > 0)
        {
            selected = null;
            float minDistance = 1000f;

            foreach (Furniture furniture in furnitures)
            {
                float distance = Vector3.Distance(transform.position, furniture.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    selected = furniture;
                }
            }

            if (selected != null)
            {
                if (selected.GetComponent<Creator>() == null)
                {
                    //Debug.Log("El mas cercano es: " + selected.name);
                    selected.Highlight();
                    current = selected;
                }
            }
        }
    }

    public Furniture getSelected()
    {
        return selected;
    }
    
}
