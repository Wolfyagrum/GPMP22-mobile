using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;

    private Camera camera;
    private Rigidbody rb;

    private Vector3 movementVector;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProccesInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void FixedUpdate()
    {
        if(movementVector == Vector3.zero)
        {
            return;
        }

        rb.AddForce(movementVector * force, ForceMode.Force);//adds force based on the movement recieved from update

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private void ProccesInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosistion = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPosition = camera.ScreenToWorldPoint(touchPosistion);

            movementVector = transform.position - worldPosition;//gets the vector between player and the spaceship
            movementVector.z = 0f; //sets z value to zero so ship always stays in view

            movementVector.Normalize();
        }
        else
        {
            movementVector = Vector3.zero;
        }
    }

    private void RotateToFaceVelocity()
    {
        if (rb.velocity == Vector3.zero) { return; }

        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition = camera.WorldToViewportPoint(transform.position);

        if(viewportPosition.x > 1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        else if(viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x + -0.1f;
        }
        else if(viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if(viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y + -0.1f;
        }

        transform.position = newPosition;
    }
}
