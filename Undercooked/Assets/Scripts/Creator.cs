using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public GameObject food;
    public GameObject poolParent;

    Holder myHolder;
    private Animator anim;
    private float time = 0f;

    private Queue<GameObject> pool;
    private int poolSize = 5;

    private void Start()
    {
        pool = new Queue<GameObject>();
        increasePool();

        myHolder = GetComponent<Holder>();
        anim = GetComponentInChildren<Animator>();
    }

    private void increasePool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject ingredient = Instantiate(food, poolParent.transform);
            Food newFood = ingredient.GetComponent<Food>();
            newFood.assingPool(this);

            ingredient.SetActive(false);
            pool.Enqueue(ingredient);
        }
    }

    public void returnToPool(GameObject ingredient)
    {
        ingredient.SetActive(false);
        ingredient.gameObject.transform.SetParent(poolParent.transform);
        pool.Enqueue(ingredient);
    }

    private void Update()
    {
        if (anim.GetBool("open"))
        {
            if(time < 1.0)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = 0f;
                anim.SetBool("open", false);
            }
        }
    }

    public MovableObject createFood()
    {
        if(pool.Count == 0)
        {
            increasePool();
        }

        if(myHolder.movableAnchor.transform.childCount == 0)
        {
            anim.SetBool("open", true);
            GameObject movable = pool.Dequeue();
            movable.SetActive(true);
            //myHolder.setMovable(movable.GetComponent<MovableObject>());

            return movable.GetComponent<MovableObject>();
        }

        return null;
    }

    public void quitAnimation()
    {
        float time = 0;

    }
}
