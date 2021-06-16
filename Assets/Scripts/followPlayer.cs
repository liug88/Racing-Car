using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class followPlayer : NetworkBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            if (player)
            {
                Vector3 newPostition = player.position;
                newPostition.z = transform.position.z;
                transform.position = newPostition;
            }
            else {
            GameObject objectFound = GameObject.FindGameObjectWithTag("Player");
            if (objectFound) {
                player = objectFound.transform;
            }
            }
        
    }
}
