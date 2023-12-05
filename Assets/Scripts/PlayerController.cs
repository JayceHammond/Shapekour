using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cinemachine;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    //public GameObject player;
    private Rigidbody rb;
    public List<GameObject> shapes = new();
    private GameObject player;
    public CinemachineFreeLook cam;
    public float speed;
    private float minSize = 0.5f;
    public float maxJump;
    public float waitTime;
    public float shrinkFactor;
    public float growFactor;
    public float rotationSpeed;
    public int shape;
    private float lastHeight;
    public float flightSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = Instantiate(shapes[shape], transform.position, transform.rotation, transform);
        cam.Follow = player.transform;
        
    }

    // Update is called once per frame
    void Update(){
        changeShape(shape);
    }
    void FixedUpdate()
    {
        if(shape == 0){
            ballControls();
        }
        if(shape == 1){
            springControls();
        }
        if(shape == 2){
            planeControls();
        }
        
        player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
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
        cam.Follow = player.transform;
        cam.LookAt = player.transform;
        lastHeight = player.transform.position.y;
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            StopCoroutine(Scale());
            player.transform.localScale = new Vector3(1, 1, 1);
            shape = 0;
            Destroy(GameObject.Find(shapes[currShape].name + "(Clone)"));
            player = Instantiate(shapes[shape], new Vector3(transform.position.x, lastHeight + 0.5f, transform.position.z), transform.rotation, transform);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            shape = 1;
            Destroy(GameObject.Find(shapes[currShape].name + "(Clone)"));
            player = Instantiate(shapes[shape], player.transform.position, transform.rotation, transform);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            shape = 2;
            Destroy(GameObject.Find(shapes[currShape].name + "(Clone)"));
            player = Instantiate(shapes[shape], player.transform.position, transform.rotation, transform);
        }
        
    }
    void springControls(){
        if(Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(Scale());
        }
    }

    void planeControls(){
        player.GetComponent<Rigidbody>().AddForce(transform.up * (-9.81f/100f),ForceMode.Force);
        if(Input.GetKey(KeyCode.W)){
            rb.AddForce(transform.forward * flightSpeed, ForceMode.Force);
        }
        
    }

    IEnumerator Scale()
    {
        float timer = 0;
        bool jumped = false;
        // we scale all axis, so they will have the same value, 
        // so we can work with a float instead of comparing vectors
        while(minSize < player.transform.localScale.y && shape == 1)
        {
            timer += Time.deltaTime;
            player.transform.localScale -= new Vector3(0, 1.5f, 0) * Time.deltaTime * shrinkFactor;
            yield return null;
        }
        // reset the timer

        yield return new WaitForSeconds(waitTime);
        timer = 0;
        while(3 > player.transform.localScale.y && shape == 1)
        {
            timer += Time.deltaTime;
            player.transform.localScale += new Vector3(0, 1, 0) * Time.deltaTime * growFactor;
            if(jumped == false){
                player.GetComponent<Rigidbody>().AddForce(player.transform.up * maxJump, ForceMode.Impulse);
        
                jumped = true;
            }
            yield return null;
        }

        timer = 0;
        yield return new WaitForSeconds(waitTime);
    }
    
}
