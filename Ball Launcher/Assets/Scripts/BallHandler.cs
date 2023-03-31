using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private float detachDelay;
    [SerializeField] private float respawnDelay;

    private Rigidbody2D currentBallRidigBody;
    private SpringJoint2D currentBallSpringJoint;
    private Camera mainCamera;
    private bool isDragging;

    private void Start()
    {
        mainCamera = Camera.main; //gets a refrence to the camera with the main tag
        SpawnNewBall(); // spawns the first ball the player will use to shoot
    }
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        if(currentBallRidigBody == null)// if there is no ball it will return and not run any code
        {
            return;
        }

        if (Touch.activeTouches.Count == 0) //Checks if there is zero fingers touching the screen
        {
            if(isDragging) //if dragging is true while no fingers are touching the screen launch the ball
            {
                LaunchBall();
            }

            isDragging = false; //set dragging to false

            return;
        }

        isDragging = true;
        currentBallRidigBody.isKinematic = true;

        //gets the center point of all touches and places the ball on that position
        Vector2 touchPosition = new Vector2();

        foreach(Touch touch in Touch.activeTouches)
        {
            touchPosition += touch.screenPosition;
        }

        touchPosition /= Touch.activeTouches.Count;

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRidigBody.position = worldPosition;

    }

    //sets the balls body type to not kinematic and after a dely detach it from the spring joint
    private void LaunchBall()
    {
        currentBallRidigBody.isKinematic = false;
        currentBallRidigBody = null;

        Invoke(nameof(DetachBall), detachDelay);
    }

    //detaches the ball from the springjoint
    private void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;

        Invoke(nameof(SpawnNewBall), respawnDelay);
    }

    //spawns a new ball and sets the ball on the current ridigbody variable and springjoint variable
    private void SpawnNewBall()
    {
        GameObject tempBall = Instantiate(ballPrefab, pivot.position, Quaternion.identity);

        currentBallRidigBody = tempBall.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = tempBall.GetComponent<SpringJoint2D>();

        currentBallSpringJoint.connectedBody = pivot;
    }
}
