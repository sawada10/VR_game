using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public float forwardSpeed = 250f, strafeSpeed = 75f, hoverSpeed = 50f;
    public float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    public float forwardAcceleration = 1f, strafeAcceleration = 1f, hoverAcceleration = 1f;

    public float lookRotateSpeed = 20f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 20f, rollAcceleration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        // ROLL CONTROL
        /* forward direction = z axis
         * right direction = x axis
         * up direction = y axis */
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        /* Difference between center and mouse position equals to (lookInput.x - screenCenter.x)
         * Divide the difference so as to make it the value between -1 and 1
         */
        mouseDistance.x = (lookInput.x - screenCenter.x) / lookInput.x;
        mouseDistance.y = (lookInput.y - screenCenter.y) / lookInput.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
        
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        // MOVE CONTROL
        // Input.GetAxisRaw : ���̎��ɂ��ẴL�[�����input�ɉ����� -1 or 0 (no press) or 1�������D
        // Mathf.Lerp(start, goal, how much increase/decrease (0 ~ 1))
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        /* transform.forward : vector for forward = (0 0 1)
         * transform.right   : vector for right   = (1 0 0)
         * transform.up      : vector for upward  = (0 1 0) */
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;

    }
}
