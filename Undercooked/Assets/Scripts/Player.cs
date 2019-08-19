using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float force = 65f;
    private Vector3 moveTo;
    private Rigidbody rb;
    private Animator anim;    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        float horizontal = -Input.GetAxis("Horizontal");
        float vertical = -Input.GetAxis("Vertical");

        moveTo = new Vector3(horizontal, 0, vertical);

        if (moveTo.magnitude > 0.1)
        {
            rb.AddForce(force * moveTo);
            transform.forward = moveTo;
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }
}
