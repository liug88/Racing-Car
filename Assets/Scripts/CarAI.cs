using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    GameObject[] paths;
    Vector3 currDestination;
    public float speed = 50;
    int currentIndex;
    int randomPath;
    Rigidbody2D rb;
    public float rotSpeed = 40;
    public bool isBoosted;

    private void Start()
    {
        paths = GameObject.FindGameObjectsWithTag("Path");
        randomPath = Random.Range(0, paths.Length);
        currDestination = paths[randomPath].transform.GetChild(0).position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, currDestination);
        if (distance < 1)
        {
            currentIndex++;
            if (currentIndex >= paths[randomPath].transform.childCount)
            {
                currentIndex = 0;
            }
            randomPath = Random.Range(0, paths.Length);
            currDestination = paths[randomPath].transform.GetChild(currentIndex).position;
        }
        else {

            Vector3 difference = currDestination - transform.position;
            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion rotationAngle = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationAngle, rotSpeed * Time.fixedDeltaTime);

            Vector3 direction = transform.TransformDirection(Vector3.up);
            rb.AddForce(direction * speed);
        }
    }


}
