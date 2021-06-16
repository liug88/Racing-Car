using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class move : NetworkBehaviour
{
    public float speed = 10;
    public float rotSpeed = 10;
    public bool altControl;
    public Animator anim;
    private float lastH;
    private float lastV;
    public bool isBoosted;

    

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
            return;
        
        
        transform.tag = "Player";
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector2(0, v);

        if (altControl == false)
        {
            direction = transform.TransformDirection(direction);
            rb.AddForce(direction * speed);
            rb.AddTorque(-h * rotSpeed);
        }
        else
        {
            direction = new Vector3(h, v, 0);
            if (direction != Vector3.zero)
            {
                // Set the new rotation
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            rb.AddForce(direction * speed);
        }
    }
}
