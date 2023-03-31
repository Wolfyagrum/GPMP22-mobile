using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedGainPerSecond = 0.2f;
    [SerializeField] private float turnSpeed = 200f;

    private int steerValue;

    // Update is called once per frame
    void Update()
    {
        speed += speedGainPerSecond * Time.deltaTime;

        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    //when player colides with an Obbject with the tag Obstacle return to main menu
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }
    }

    //can be called outside the script to change steer value making it steer faster or slower
    public void Steer(int value)
    {
        steerValue = value;
    }




}
