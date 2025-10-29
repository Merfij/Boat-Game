using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatScript : MonoBehaviour
{
    BoatControls inputActions;
    InputAction InputAction;
    public Transform targetLeft;
    public Transform targetRight;
    public Rigidbody rb;
    public float force = 5;
    public float torque = 5;
    public float forceWall = 100;
    public float torqueWall = 100;
    float moveY;
    public float angle = 50f;

    private void Awake()
    {
        inputActions = new BoatControls();
        rb = GetComponent<Rigidbody>();
        inputActions.Enable();
    }
    private void OnEnable()
    {
        inputActions.Boat.Left.performed += RotateLeft;
        inputActions.Boat.Right.performed += RotateRight;
        inputActions.Enable();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall")){
            if (transform.rotation.y < 0)
            {
                rb.AddTorque(transform.up * torqueWall * Time.deltaTime, ForceMode.VelocityChange);
                rb.AddForce(transform.forward * -forceWall * Time.deltaTime, ForceMode.VelocityChange);
                if(transform.rotation.y > 0)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                }
            }
            if (transform.rotation.y > 0)
            {
                rb.AddTorque(transform.up * -torqueWall * Time.deltaTime, ForceMode.VelocityChange);
                rb.AddForce(transform.forward * -forceWall * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }

    private void FixedUpdate()
    {
        if (transform.eulerAngles.y > angle && transform.eulerAngles.y < 179)
        {
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
        }
        if (transform.eulerAngles.y > 179 && transform.eulerAngles.y < 360 - angle)
        {
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(transform.rotation.x, -angle, transform.rotation.z);
        }
    }

    private void RotateLeft(InputAction.CallbackContext context)
    {
        rb.AddTorque(transform.up * torque * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddForce(transform.forward * force * Time.deltaTime, ForceMode.VelocityChange);
    }
    private void RotateRight(InputAction.CallbackContext context)
    {
        rb.AddTorque(transform.up * -torque * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddForce(transform.forward * force * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void OnDisable()
    {

        inputActions.Disable();
    }

    //public override float ReadValue(ref InputAction.CallbackContext context)
    //{
    //    var firstPArt = context.ReadValue<float>(firstPArt);
    //}

    //if (Input.GetKey(KeyCode.Joystick1Button1))
    //{
    //    rb.AddTorque(transform.up * torque *  Time.deltaTime, ForceMode.VelocityChange);
    //    rb.AddForce(transform.forward * force * Time.deltaTime, ForceMode.VelocityChange);
    //}
    //if (Input.GetKey(KeyCode.Joystick1Button0))
    //{
    //    rb.AddTorque(transform.up * -torque * Time.deltaTime, ForceMode.VelocityChange);
    //    rb.AddForce(transform.forward * force * Time.deltaTime, ForceMode.VelocityChange);
    //}
}
