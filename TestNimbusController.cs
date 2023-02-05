using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

using UnityEngine.UI;

public class TestNimbusController : MonoBehaviour
{
    public float forwardSpeed = 0f;
    public Vector3 curPosition;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float pitchAngle = 0f, yawAngle = 0f, rollAngle = 0f;
    private string logger;
    private float waitTime = 0.1f;
    private float timer;
    private Vector3 HMDPosition, offsetHMDPosition, relativeHMDPosition;
    private Vector3 LeftHandPosition, offsetLeftHandPosition, relativeLeftHandPosition;

    private int sensorOffsetValueChecked = 0;
    private float forwardMoveAcceleration = 1f, pitchAcceleration = 1f, rollAcceleration = 1f, yawAcceleration = 1f; 

    // スニッチを探しやすいように動きを止められるモードを作る
    public int stopMode = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 90, 0, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        if (sensorOffsetValueChecked == 0){
            GetInitialSensorValue();
        }
        else{
            StopControl();
            if (stopMode == 0){
                SensorUpdate();
                //GetSensor();
                GetJoystick();
            }
            // SensorUpdate();
            // GetSensor();
            // GetJoystick();
            RotateControl();
            MoveControl();
        } 
    }

    private void RotateControl() {
        transform.Rotate(pitchAngle * Time.deltaTime, yawAngle * Time.deltaTime, rollAngle * Time.deltaTime, Space.Self);
        // transform.Rotate(pitchAngle * Time.deltaTime, 0, rollAngle * Time.deltaTime, Space.Self);
        // transform.Rotate(0, yawAngle * Time.deltaTime, 0, Space.World);
    }

    private void MoveControl(){
        curPosition = transform.position;
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, forwardSpeed, forwardMoveAcceleration * Time.deltaTime);
        // activeStrafeSpeed += strafeMoveAcceleration * Time.deltaTime;
        // activeHoverSpeed += hoverMoveAcceleration * Time.deltaTime;
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        // transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        // transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
    }

    private void StopControl(){
        // Xボタンを押すと動きを止めることができる。
        if(OVRInput.Get(OVRInput.RawButton.X) && stopMode == 0){
            stopMode = 1;
            yawAngle = 0f;
            pitchAngle = 0f;
            rollAngle = 0f;
            forwardSpeed = 0f;
        } 
        else if (OVRInput.Get(OVRInput.RawButton.X) && stopMode == 1){
            stopMode = 0;
        }
    }

    private void SensorUpdate(){
        HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
        LeftHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
        relativeHMDPosition = HMDPosition - offsetHMDPosition;
        relativeLeftHandPosition = LeftHandPosition - offsetLeftHandPosition;
    }

    private void GetInitialSensorValue(){
        if (OVRInput.Get(OVRInput.RawButton.Y) && sensorOffsetValueChecked == 0){
            offsetHMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
            offsetLeftHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
            sensorOffsetValueChecked = 1;
        }
    }

    private void GetJoystick(){
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickUp))
        {
            Debug.Log("左アナログスティックを上に倒した");
            forwardSpeed = 50f;
        }
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickDown))
        {
            Debug.Log("左アナログスティックを下に倒した");
            forwardSpeed = -50f;
        }
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft))
        {
            Debug.Log("左アナログスティックを左に倒した");
            // rollAngle = - 20f;
        }
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickRight))
        {
            Debug.Log("左アナログスティックを右に倒した");
            // rollAngle = 20f;
        }
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickUp))
        {
            Debug.Log("右アナログスティックを上に倒した");
            // hoverMoveAcceleration = 2f;
            pitchAngle = -10f; 
        }
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickDown))
        {
            Debug.Log("右アナログスティックを下に倒した");
            pitchAngle = 10f;
        }
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight))
        {
            Debug.Log("右アナログスティックを右に倒した");
            // hoverMoveAcceleration = 2f;
            yawAngle = 20f; 
        }
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft))
        {
            Debug.Log("右アナログスティック左に倒した");
            yawAngle = - 20f;
        }
        
     /* ------------------------------------------------------------ */
     // script for the case where joysticks are released
        if (OVRInput.GetUp(OVRInput.RawButton.LThumbstickUp))
        {forwardSpeed = 0f;}
        if (OVRInput.GetUp(OVRInput.RawButton.LThumbstickDown))
        {forwardSpeed = 0f;}
        if (OVRInput.GetUp(OVRInput.RawButton.RThumbstickUp))
        {pitchAngle = 0f;}
        if (OVRInput.GetUp(OVRInput.RawButton.RThumbstickDown))
        {pitchAngle = 0f;}
        if (OVRInput.GetUp(OVRInput.RawButton.RThumbstickRight))
        {yawAngle = 0f;}
        if (OVRInput.GetUp(OVRInput.RawButton.RThumbstickLeft))
        {yawAngle = 0f;}

    /* ---------------------------------------------------------------*/

        if (OVRInput.Get(OVRInput.RawButton.X))
        {
            Debug.Log("Xボタンを押した");
            activeForwardSpeed = 0f;
            // activeHoverSpeed = 0f;
            // activeStrafeSpeed = 0f;
            forwardSpeed = 0f;
            // strafeMoveAcceleration = 0f;
            // hoverMoveAcceleration = 0f;
            pitchAngle = 0f;
            yawAngle = 0f;
        }
    }

    private void GetSensor(){
        // yawの回転
        // 角度があんまりない時は回らない
        if (relativeHMDPosition[0] <= 0.10 && relativeLeftHandPosition[0] >= -0.10){
            yawAngle = 0f;
        }
        // 少し回転角がある時
        if (relativeLeftHandPosition[0] > 0.10 && relativeLeftHandPosition[0] <= 0.20){
            yawAngle = 10f;
        }
        if (relativeLeftHandPosition[0] < -0.10 && relativeLeftHandPosition[0] >= - 0.20){
            yawAngle = -10f;
        }
        // 結構回転角がある時
        if (relativeLeftHandPosition[0] > 0.20 && relativeLeftHandPosition[0] <= 0.30){
            yawAngle = 20f;
        }
        if (relativeLeftHandPosition[0] < -0.20 && relativeLeftHandPosition[0] >= - 0.30){
            yawAngle = -20f;
        }
        // かなり回転角がある時
        if (relativeLeftHandPosition[0] > 0.30 && relativeLeftHandPosition[0] <= 0.40){
            yawAngle = 30f;
        }
        if (relativeLeftHandPosition[0] < -0.30 && relativeLeftHandPosition[0] >= - 0.40){
            yawAngle = -30f;
        }
        // まじで回転角がある時
        if (relativeLeftHandPosition[0] > 0.40){
            yawAngle = 60f;
        }
        if (relativeLeftHandPosition[0] < -0.40){
            yawAngle = -60f;
        }

        // pitchの回転
        // 角度があまりない時は回らない
        if (relativeLeftHandPosition[2] <= 0.07 && relativeLeftHandPosition[2] >= -0.02){
            pitchAngle = 0f;
        }
        // 少し回転角がある時
        if (relativeLeftHandPosition[2] > 0.07 && relativeLeftHandPosition[2] <= 0.20){
            pitchAngle = 20f;
        }
        if (relativeLeftHandPosition[2] < -0.02 && relativeLeftHandPosition[2] >= -0.05){
            pitchAngle = - 10f;
        }
        // 結構回転角がある時
        if (relativeLeftHandPosition[2] > 0.20 && relativeLeftHandPosition[2] < 0.30){
            pitchAngle = 40f;
        }
        if (relativeLeftHandPosition[2] < -0.05 && relativeLeftHandPosition[2] >= -0.10){
            pitchAngle = - 20f;
        }
        // かなり回転角がある時
        if (relativeLeftHandPosition[2] > 0.30){
            pitchAngle = 50f;
        }
        if (relativeLeftHandPosition[2] < -0.10){
            pitchAngle = - 30f;
        }

        // roll の回転
        if (relativeHMDPosition[0] > -0.10 && relativeHMDPosition[0] < 0.10){
            rollAngle = 0f;
        }
        if (relativeHMDPosition[0] < -0.15 && relativeHMDPosition[0] >= -0.10) {
            rollAngle = 20f;
        }
        if (relativeHMDPosition[0] > 0.15 && relativeHMDPosition[0] <=0.10) {
            rollAngle = - 20f;
        }
        if (relativeHMDPosition[0] < -0.25 && relativeHMDPosition[0] >= -0.30) {
            rollAngle = 30f;
        }
        if (relativeHMDPosition[0] > 0.25 && relativeHMDPosition[0] <= 0.30) {
            rollAngle = - 30f;
        }
        if (relativeHMDPosition[0] < -0.30) {
            rollAngle = 40f;
        }
        if (relativeHMDPosition[0] > 0.30) {
            rollAngle = - 40f;
        }

        // 前進
        if (relativeHMDPosition[2] < 0.10){
            forwardSpeed = 0f;
        }
        if (relativeHMDPosition[2] < 0.15 && relativeHMDPosition[2] >= 0.10){
            forwardSpeed = 10f;
        }
        if (relativeHMDPosition[2] < 0.2 && relativeHMDPosition[2] >= 0.15){
            forwardSpeed = 30f;
        }
        if (relativeHMDPosition[2] < 0.25 && relativeHMDPosition[2] >= 0.20){
            forwardSpeed = 40f;
        }
        if (relativeHMDPosition[2] < 0.30 && relativeHMDPosition[2] >= 0.25) {
            forwardSpeed = 50f;
        }
        if (relativeHMDPosition[2] >= 0.35) {
            forwardSpeed = 60f;
        }
    }
}
