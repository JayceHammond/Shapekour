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
    private float minSize = 0.5f;
    public float maxJump;
    public float waitTime;
    public float shrinkFactor;
    public float growFactor;
    public float rotationSpeed;
    public int shape;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = Instantiate(shapes[shape], this.transform.position, this.transform.rotation, this.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        changeShape(shape);
        if(shape == 0){
            ballControls();
        }
        if(shape == 1){
            springControls();
        }
        print(player.name);
        player.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z);
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
            player = Instantiate(shapes[shape], this.transform.position, this.transform.rotation, this.transform);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            Debug.Log("change");
            shape = 1;
            Destroy(GameObject.Find(shapes[currShape].name + "(Clone)"));
            player = Instantiate(shapes[shape], this.transform.position, this.transform.rotation, this.transform);
            //Debug.Break();
        }
        
    }
    void springControls(){
        if(Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(Scale());
            
        }
    }

    IEnumerator Scale()
    {
        float timer = 0;
        bool jumped = false;
        // we scale all axis, so they will have the same value, 
        // so we can work with a float instead of comparing vectors
        while(minSize < player.transform.localScale.y)
        {
            timer += Time.deltaTime;
            player.transform.localScale -= new Vector3(0, 1.5f, 0) * Time.deltaTime * shrinkFactor;
            yield return null;
        }
        // reset the timer

        yield return new WaitForSeconds(waitTime);
        timer = 0;
        while(3 > player.transform.localScale.y)
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
