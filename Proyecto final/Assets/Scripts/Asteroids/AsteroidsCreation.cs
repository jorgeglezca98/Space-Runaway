using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsCreation : MonoBehaviour {

    public GameObject spaceship;
    private List<GameObject> asteroids;
    private enum Axis { yAxis, xAxis, zAxis };

    /* Recommended settings for the parameters:
     * asteroidSeparation = 100
     * secureZone = 20
     * areaSideSize = 3
     */

    // Separation between each asteroid.
    public int asteroidSeparation;
    // Initially we use this value to maintain the asteriods away enough from the spaceship
    // avoiding them to initialize in the same position as the spaceship.
    public int secureZone;
    // This value specifies the number of asteroids that we want from the origin to each
    // direction ( +x, -x, +y .. ) or in other words, it specifies half the length of
    // each edge that conforms the cube of asteroids around the spaceship.
    public int areaSideSize;
    // We use a threshold to indicate when the asteroids are far away enought and should be
    // moved to the opposite extreme relative to their current position.
    private int threshold;



    // Here is where the name of all the possible asteroid gameobjects are stored.
    private List<string> asteroidNames = new List<string>{ "Asteroid_S_00", "Asteroid_M_00", "Asteroid_L_00",
															"Asteroid_XL_00", "Asteroid_XXL_00", "Asteroid_S_20",
															"Asteroid_M_20", "Asteroid_L_20", "Asteroid_XL_20",
															"Asteroid_XXL_20", "Asteroid_M_80", "Asteroid_L_80"};
    void Awake(){
        asteroids = new List<GameObject>();
        threshold = (areaSideSize - 1) * asteroidSeparation + secureZone;
        initializeAsteroids();
    }
    void Start () {
        spaceship.transform.position = new Vector3(0, 0, 0);
    }

    // In this function all asteroids are initialized in their initial positions.
    void initializeAsteroids(){

        spaceship.transform.position = new Vector3(0, 0, 0);
        Vector3 spaceshipPosition = spaceship.transform.position;
        int asteroidIndex = 0;

        // When the loop goes from -areaSideSize+2 is because we want the asteroids field to be
        // a little shorter in one of each axis side, avoiding them to be mapped in a position
        // where an asteroid already exists.
        for (int x = -areaSideSize + 2; x < 0; x++){
            for (int y = -areaSideSize  + 2; y < areaSideSize; y++){
                for (int z = -areaSideSize + 2 ; z < 0; z++){
                    // We substract the secureZone to avoid the asteroids being instantiated in the
                    // position where the spaceship is.
                    float xPosition = spaceshipPosition.x + (x * asteroidSeparation) - secureZone;
                    float yPosition = spaceshipPosition.y + (y * asteroidSeparation);
                    float zPosition = spaceshipPosition.z + (z * asteroidSeparation) - secureZone;
                    asteroids.Add((GameObject)Instantiate(Resources.Load(asteroidNames[Random.Range(0, 12)], typeof(GameObject)), new Vector3(xPosition, yPosition, zPosition), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject);
                    //asteroids[asteroidIndex].GetComponent<Collider>().enabled = false;
                    asteroids[asteroidIndex].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                    asteroids[asteroidIndex].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);
                    asteroidIndex += 1;
                }
            }
        }

        for (int x = 0; x < areaSideSize; x++){
            for (int y = -areaSideSize + 2; y < areaSideSize; y++){
                for (int z = 0; z < areaSideSize; z++){
                    float xPosition = spaceshipPosition.x + (x * asteroidSeparation) + secureZone;
                    float yPosition = spaceshipPosition.y + (y * asteroidSeparation);
                    float zPosition = spaceshipPosition.z + (z * asteroidSeparation) + secureZone;
                    asteroids.Add((GameObject)Instantiate(Resources.Load(asteroidNames[Random.Range(0, 12)], typeof(GameObject)), new Vector3(xPosition, yPosition, zPosition), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject);
                    //asteroids[asteroidIndex].GetComponent<Collider>().enabled = false;
                    asteroids[asteroidIndex].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                    asteroids[asteroidIndex].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);
                    asteroidIndex += 1;
                }
            }
        }

        for (int x = -areaSideSize + 2; x < 0; x++){
            for (int y = -areaSideSize + 2; y < areaSideSize; y++){
                for (int z = 0; z < areaSideSize; z++) {
                    float xPosition = spaceshipPosition.x + (x * asteroidSeparation) - secureZone;
                    float yPosition = spaceshipPosition.y + (y * asteroidSeparation);
                    float zPosition = spaceshipPosition.z + (z * asteroidSeparation) + secureZone;
                    asteroids.Add((GameObject)Instantiate(Resources.Load(asteroidNames[Random.Range(0, 12)], typeof(GameObject)), new Vector3(xPosition, yPosition, zPosition), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject);
                    //asteroids[asteroidIndex].GetComponent<Collider>().enabled = false;
                    asteroids[asteroidIndex].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                    asteroids[asteroidIndex].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);
                    asteroidIndex += 1;
                }
            }
        }

        for (int x = 0; x < areaSideSize; x++){
            for (int y = -areaSideSize + 2 ; y < areaSideSize; y++){
                for (int z = -areaSideSize + 2; z < 0; z++){
                    float xPosition = spaceshipPosition.x + (x * asteroidSeparation) + secureZone;
                    float yPosition = spaceshipPosition.y + (y * asteroidSeparation);
                    float zPosition = spaceshipPosition.z + (z * asteroidSeparation) - secureZone;
                    asteroids.Add((GameObject)Instantiate(Resources.Load(asteroidNames[Random.Range(0, 12)], typeof(GameObject)), new Vector3(xPosition, yPosition, zPosition), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject);
                   // asteroids[asteroidIndex].GetComponent<Collider>().enabled = false;
                    asteroids[asteroidIndex].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                    asteroids[asteroidIndex].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);
                    asteroidIndex += 1;
                }
            }
        }

    }

    void refreshAsteroids()
    {

        foreach (GameObject asteroid in asteroids)
        {
            Vector3 asteroidPosition = asteroid.transform.position;
            Vector3 spaceshipPosition = spaceship.transform.position;

            float xAsteroidPosition = asteroidPosition.x;
            float yAsteroidPosition = asteroidPosition.y;
            float zAsteroidPosition = asteroidPosition.z;

                if (Mathf.Abs(asteroidPosition.x - spaceshipPosition.x) > threshold)
                {
                    int xAsteroidOppositeDirection = asteroidOppositeDirection(Axis.xAxis, asteroid);
                    xAsteroidPosition = spaceshipPosition.x + (xAsteroidOppositeDirection * threshold);
                }

                if (Mathf.Abs(asteroidPosition.y - spaceshipPosition.y) > threshold)
                {
                      int yAsteroidOppositeDirection = asteroidOppositeDirection(Axis.yAxis, asteroid);
                      yAsteroidPosition = spaceshipPosition.y + (yAsteroidOppositeDirection * threshold);
            }

                if (Mathf.Abs(asteroidPosition.z - spaceshipPosition.z) > threshold)
                {
                     int zAsteroidOppositeDirection = asteroidOppositeDirection(Axis.zAxis, asteroid);
                     zAsteroidPosition = spaceshipPosition.z + (zAsteroidOppositeDirection * threshold);
            }


            Vector3 newAsteroidPosition = new Vector3(xAsteroidPosition, yAsteroidPosition, zAsteroidPosition);
            asteroid.transform.position = newAsteroidPosition;

        }

    }

    int asteroidOppositeDirection(Axis axis, GameObject asteroid)
    {

        float substraction = 0.0f;
        switch (axis)
        {
            case Axis.xAxis:
                substraction = spaceship.transform.position.x - asteroid.transform.position.x;
                break;

            case Axis.yAxis:
                substraction = spaceship.transform.position.y - asteroid.transform.position.y;
                break;

            case Axis.zAxis:
                substraction = spaceship.transform.position.z - asteroid.transform.position.z;
                break;

        }

        if (substraction < 0){
            return -1;
        }else return 1;


    }

    // Not being currently used but could be needed in the future.
    int getSpaceshipDirection(Axis axis)
    {
        switch (axis)
        {
            case Axis.xAxis:
                if (spaceship.transform.eulerAngles.z > 0 && spaceship.transform.eulerAngles.z < 180)
                {
                    return -1;
                }
                else
                {
                    return 1;

                }
                break;

            case Axis.yAxis:
                if (spaceship.transform.eulerAngles.x > 270 && spaceship.transform.eulerAngles.x < 360)
                {

                    return 1;
                }
                else
                {

                    return -1;
                }
                break;

            case Axis.zAxis:
                if (spaceship.transform.rotation.y > -0.5 && spaceship.transform.rotation.y < 0.5)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
                break;

            default:
                Debug.LogError("Error!");
                return -2;

        }
    }

    // Update is called once per frame
    void Update()
    {
       refreshAsteroids();
    }


}
