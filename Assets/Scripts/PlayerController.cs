using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;
    public float speed;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z);
        if(Input.GetKey(KeyCode.W)){
            rb.AddForce(rb.transform.forward * speed, ForceMode.Impulse);
            //player.transform.Rotate(rotationSpeed, 0, 0);
            player.GetComponent<Rigidbody>().AddRelativeTorque(rotationSpeed, 0, 0);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Rotate(0,rotationSpeed, 0);
            player.GetComponent<Rigidbody>().AddRelativeTorque(0, rotationSpeed, 0);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(0,-rotationSpeed, 0);
            player.GetComponent<Rigidbody>().AddRelativeTorque(0, -rotationSpeed, 0);
        }
        if(Input.GetKey(KeyCode.S)){
            rb.AddForce(-rb.transform.forward * speed, ForceMode.Impulse);
            //player.transform.Rotate(-rotationSpeed, 0, 0);
            player.GetComponent<Rigidbody>().AddRelativeTorque(-rotationSpeed, 0, 0);
        }
        
    }
}
