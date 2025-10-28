using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatScript : MonoBehaviour
{
    BoatControls inputActions;
    InputAction InputAction;
    public Transform targetLeft;
    public Transform targetRight;
    private float step = 10f;
    public Rigidbody rb;
    private float force = 5;

    private void Awake()
    {
        inputActions = new BoatControls();
        rb = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        inputActions.Boat.Left.performed += Test;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Boat.Left.performed -= Test;
        inputActions.Disable();
    }

    private void Test(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LeftTurn();
            Left();
        }
    }

    public void LeftTurn()
    {
        //transform.position = Vector3.Lerp(transform.position, targetLeft.position, step * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetLeft.rotation, step * Time.deltaTime);
    }

    public void Left()
    {
        rb.AddForce(new Vector3(transform.position.x * force, 0, transform.position.z * force));
        //rb.AddForce(Vector3.forward * force);
    }
}
