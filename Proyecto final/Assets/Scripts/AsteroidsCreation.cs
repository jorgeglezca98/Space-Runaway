using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsCreation : MonoBehaviour {

    public GameObject spaceship;
    private GameObject[] asteroids;
    private enum Axis { yAxis, xAxis, zAxis };

    public int threshold;
    public int asteroidSeparation;
    public int secureZone;
    public int areaSideSize;
    



    private List<string> asteroidNames = new List<string>{ "Asteroid_S_00", "Asteroid_M_00", "Asteroid_L_00",
																									"Asteroid_XL_00", "Asteroid_XXL_00", "Asteroid_S_20",
																									"Asteroid_M_20", "Asteroid_L_20", "Asteroid_XL_20",
																									"Asteroid_XXL_20", "Asteroid_M_80", "Asteroid_L_80"};
    void Awake(){
        asteroids = new GameObject[(areaSideSize*2)*(areaSideSize * 2)*(areaSideSize * 2)];
        initializeAsteroids();
        Debug.Log("Hello!\n");
    }
    void Start () {
	}

	void initializeAsteroids(){
        
        Vector3 spaceshipPosition = spaceship.transform.position;
        int asteroidIndex = 0;

        for (int x = -areaSideSize; x < 0; x++){
            for (int y = -areaSideSize; y < areaSideSize; y++){
                for (int z = -areaSideSize; z < 0; z++){
                    float xPosition = spaceshipPosition.x + (x * asteroidSeparation) - secureZone;
                    float yPosition = spaceshipPosition.y + (y * asteroidSeparation);
                    float zPosition = spaceshipPosition.z + (z * asteroidSeparation) - secureZone;
                    asteroids[asteroidIndex] = (GameObject)Instantiate(Resources.Load(asteroidNames[Random.Range(0, 12)], typeof(GameObject)), new Vector3(xPosition, yPosition, zPosition), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject;
            //        asteroids[asteroidIndex].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);
                    asteroidIndex += 1;
                }
            }
        }

        for (int x = 0; x < areaSideSize; x++){
            for (int y = -areaSideSize; y < areaSideSize; y++){
                for (int z = 0; z < areaSideSize; z++){
                    float xPosition = spaceshipPosition.x + (x * asteroidSeparation) + secureZone;
                    float yPosition = spaceshipPosition.y + (y * asteroidSeparation);
                    float zPosition = spaceshipPosition.z + (z * asteroidSeparation) + secureZone;
                    asteroids[asteroidIndex] = (GameObject)Instantiate(Resources.Load(asteroidNames[Random.Range(0, 12)], typeof(GameObject)), new Vector3(xPosition, yPosition, zPosition), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject;
             //       asteroids[asteroidIndex].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);
                    asteroidIndex += 1;
                }
            }
        }

        for (int x = -areaSideSize; x < 0; x++){
            for (int y = -areaSideSize; y < areaSideSize; y++){
                for (int z = 0; z < areaSideSize; z++) {
                    float xPosition = spaceshipPosition.x + (x * asteroidSeparation) - secureZone;
                    float yPosition = spaceshipPosition.y + (y * asteroidSeparation);
                    float zPosition = spaceshipPosition.z + (z * asteroidSeparation) + secureZone;
                    asteroids[asteroidIndex] = (GameObject)Instantiate(Resources.Load(asteroidNames[Random.Range(0, 12)], typeof(GameObject)), new Vector3(xPosition, yPosition, zPosition), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject;
               //     asteroids[asteroidIndex].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);
                    asteroidIndex += 1;
                }
            }
        }

        for (int x = 0; x < areaSideSize; x++){
            for (int y = -areaSideSize; y < areaSideSize; y++){
                for (int z = -areaSideSize; z < 0; z++){
                    float xPosition = spaceshipPosition.x + (x * asteroidSeparation) + secureZone;
                    float yPosition = spaceshipPosition.y + (y * asteroidSeparation);
                    float zPosition = spaceshipPosition.z + (z * asteroidSeparation) - secureZone;
                    asteroids[asteroidIndex] = (GameObject)Instantiate(Resources.Load(asteroidNames[Random.Range(0, 12)], typeof(GameObject)), new Vector3(xPosition, yPosition, zPosition), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject;
         //           asteroids[asteroidIndex].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);
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

            int xAxisDirection = getSign(Axis.xAxis);
            int yAxisDirection = getSign(Axis.yAxis);
            int zAxisDirection = getSign(Axis.zAxis);

            float xAsteroidPosition = asteroidPosition.x;
            float yAsteroidPosition = asteroidPosition.y;
            float zAsteroidPosition = asteroidPosition.z;

            if (!asteroidIsInFront(Axis.xAxis, asteroidPosition.x))
            {
                if (Mathf.Abs(asteroidPosition.x - spaceshipPosition.x) > threshold)
                {
                    xAsteroidPosition = spaceshipPosition.x + (xAxisDirection * threshold);
                }
            }

            if (!asteroidIsInFront(Axis.yAxis, asteroidPosition.y))
            {
                if (Mathf.Abs(asteroidPosition.y - spaceshipPosition.y) > threshold)
                {
                    yAsteroidPosition = spaceshipPosition.y + (yAxisDirection * threshold);
                }
            }

            if (!asteroidIsInFront(Axis.zAxis, asteroidPosition.z))
            {
                if (Mathf.Abs(asteroidPosition.z - spaceshipPosition.z) > threshold)
                {
                    zAsteroidPosition = spaceshipPosition.z + (zAxisDirection * threshold);
                }
            }

            Vector3 newAsteroidPosition = new Vector3(xAsteroidPosition, yAsteroidPosition, zAsteroidPosition);
            asteroid.transform.position = newAsteroidPosition;
        //    asteroid.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);

        }

    }

    bool asteroidIsInFront(Axis axis, float asteroidCoordinate)
    {
        Vector3 spaceshipPosition = spaceship.transform.position;
        int currentAxisDirection = 0;

        switch (axis)
        {
            case Axis.xAxis:
                currentAxisDirection = getSign(Axis.xAxis);
                break;

            case Axis.yAxis:
                currentAxisDirection = getSign(Axis.yAxis);
                break;

            case Axis.zAxis:
                currentAxisDirection = getSign(Axis.zAxis);
                break;

        }

        int substraction = (int)(spaceshipPosition.x - asteroidCoordinate);

        if (currentAxisDirection > 0)
        {
            if (substraction > 0)
            {
                return true;
            }
            else return false;
        }
        else if (currentAxisDirection < 0)
        {
            if (substraction > 0)
            {
                return false;
            }
            else return true;
        }
        else
        {
            Debug.Log("Not facing");
            return true;
        }

        Debug.LogError("Error, no axis selected\n");
    }

    int getSign(Axis axis)
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
        //Debug.Log("X axis: ");
        //asteroidIsInFront(Axis.xAxis, 1);

        //Debug.Log("Y axis: ");
        //asteroidIsInFront(Axis.yAxis, 1);

        //Debug.Log("Z axis: ");
        //asteroidIsInFront(Axis.zAxis, 1);

        // Debug.Log(spaceship.transform.rotation.x);


        // Debug.Log(spaceship.transform.up);
    }


}
