using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class CanvasText : MonoBehaviour
{
    public GameObject canvasObj = null;
    private int collide_status = 0;
    private int sensorOffsetValueChecked = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Text whatToWrite = canvasObj.GetComponent<Text> ();
        if(sensorOffsetValueChecked == 0){
            whatToWrite.text = "Please press Y button when you decide your initial position.";
            if(OVRInput.Get(OVRInput.RawButton.Y)){
                sensorOffsetValueChecked = 1;
            }
        } else {
            if (collide_status == 0){
                whatToWrite.text = "";
            } else {
                whatToWrite.text = "YOU WIN!" + "\n" + "YOU ARE A SEEKER!!";  
            }
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        collide_status = 1;
    } 
}
