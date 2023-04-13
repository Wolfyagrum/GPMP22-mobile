using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpanwer : MonoBehaviour
{

    [SerializeField] private GameObject[] asteroidPreFabs;
    [SerializeField] private float SecondsBetweenAsteroid = 1.5f;
    [SerializeField] private Vector2 forceRange;

    private Camera Camera;
    private float timer;

    private void Start()
    {
        Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            print("hi");
            SpanwnAsteroid();
            timer += SecondsBetweenAsteroid; //Resets timer
        }
    }

    private void SpanwnAsteroid()
    {
        int side = Random.Range(0, 4);

        Vector2 spawnPoint = Vector2.zero;
        Vector2 Direction = Vector2.zero;

        switch (side)
        {
            case 0:
                //Left of the screen case
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                Direction = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1:
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                Direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                Direction = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                //Top of screen case
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                Direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

                Vector3 worldSpawnPoint = Camera.ViewportToWorldPoint(spawnPoint);
                worldSpawnPoint.z = 0f;

                GameObject TempAsteroid = Instantiate(asteroidPreFabs[Random.Range(0,asteroidPreFabs.Length)], worldSpawnPoint, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))); //spawn a astroid from the prefab array and give it a random rotation
                Rigidbody rb = TempAsteroid.GetComponent<Rigidbody>();

                rb.velocity = Direction.normalized * Random.Range(forceRange.x,forceRange.y);
    }
}
