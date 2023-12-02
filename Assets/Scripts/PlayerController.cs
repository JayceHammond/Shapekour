using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    //public GameObject player;
    private Rigidbody rb;
    public List<GameObject> shapes = new();
    private GameObject player;
    public float speed;
    public float rotationSpeed;
    public int shape;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = Instantiate(shapes[0], this.transform.position, this.transform.rotation, this.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z);
        changeShape(shape);
        if(shape == 0){
            ballControls();
        }
        print(player.name);
    }

    void ballControls(){
        if(Input.GetKey(KeyCode.W)){
            rb.AddForce(rb.transform.forward * speed, ForceMode.Impulse);
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
            player.GetComponent<Rigidbody>().AddRelativeTorque(-rotationSpeed, 0, 0);
        }
    }
    void changeShape(int currShape){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            Debug.Log("change");
            shape = 0;
            Destroy(GameObject.Find(shapes[currShape].name + "(Clone)"));
            player = Instantiate(shapes[shape],this.transform.position, this.transform.rotation, this.transform);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            Debug.Log("change");
            shape = 1;
            Destroy(GameObject.Find(shapes[currShape].name + "(Clone)"));
            player = Instantiate(shapes[shape], this.transform.position, this.transform.rotation, this.transform);
        }
        
    }
}
