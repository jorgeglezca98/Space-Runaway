using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour{

    public Vector3 coordinates;
    public string asteroidType;
    public bool   instantiated;
    public GameObject asteroid;

    public Asteroid(Vector3 coordinates, string asteroidType)
    {
        this.coordinates = coordinates;
        this.asteroidType = asteroidType;
    }

    public void instantiateAsteroid()
    {
        asteroid = Instantiate(Resources.Load(asteroidType, typeof(GameObject)), coordinates, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject;
        asteroid.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)), ForceMode.Impulse);
        instantiated = true;
    }

    public void destroyAsteroid()
    {
        Destroy(asteroid);
        instantiated = false;
    }

    public Vector3 getCoordinates()
    {
        return coordinates;
    }

    public string getAsteroidType()
    {
        return asteroidType;
    }

    public bool isInstantiated()
    {
        return instantiated;
    }

    public void setInstantiated(bool instantiated)
    {
        this.instantiated = instantiated;
    }
}
public class AsteroidsCreation : MonoBehaviour {

    // This ratio works great!! (Ratio = 5)
    public int areaSize;
    public int asteroidGap;
    public Camera camera;
    public GameObject spaceship;
    private List<Asteroid> asteroidsContainer = new List<Asteroid>();
    public float proximityTreshold;
    public float remoteTreshold;



    private List<string> asteroidNames = new List<string>{ "Asteroid_S_00", "Asteroid_M_00", "Asteroid_L_00",
																									"Asteroid_XL_00", "Asteroid_XXL_00", "Asteroid_S_20",
																									"Asteroid_M_20", "Asteroid_L_20", "Asteroid_XL_20",
																									"Asteroid_XXL_20", "Asteroid_M_80", "Asteroid_L_80"};
	void Awake()
	{
		int minRange = -1 * (areaSize / asteroidGap);
		int maxRange = areaSize / asteroidGap;
        int counter = 0;

		for(int i = minRange ; i < maxRange; i++){
			for(int j = minRange ; j < maxRange; j++){
				for(int k = minRange ; k < maxRange; k++){
                    int yAxis = i * asteroidGap;
                    int xAxis = j * asteroidGap;
                    int zAxis = k * asteroidGap;
                    string asteroidType = asteroidNames[Random.Range(0, 12)];
                    Asteroid asteroid = new Asteroid(new Vector3(xAxis, yAxis, zAxis), asteroidType);
                    asteroidsContainer.Add(asteroid);
                    // currentMeteor = Instantiate(Resources.Load(asteroidNames[asteroidName], typeof(GameObject)), new Vector3(xAxis,yAxis,zAxis), Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360))) as GameObject;
                    // currentMeteor.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10,11),Random.Range(-10,11),Random.Range(-10,11)), ForceMode.Impulse);
                    counter += 1;
                }
            }
		}
        Debug.Log("We've instantiated: " + counter + " objects");

    }
    // Use this for initialization
    void Start () {
	}

	void createAsteroids(){

	}

	// Update is called once per frame
	void Update () {
        initializeAsteroids();
    }


    void initializeAsteroids()
    {
        Transform spaceshipPosition = spaceship.transform;
        foreach(Asteroid asteroid in asteroidsContainer)
        {
            Vector3 asteroidCoordinates = asteroid.getCoordinates();

            if(asteroid.isInstantiated() == false){

                if (Mathf.Abs(asteroidCoordinates.x - spaceship.transform.position.x) < proximityTreshold
                 && Mathf.Abs(asteroidCoordinates.y - spaceship.transform.position.y) < proximityTreshold
                 && Mathf.Abs(asteroidCoordinates.z - spaceship.transform.position.z) < proximityTreshold)
                {
                    asteroid.instantiateAsteroid();
                }
            }else{

                if (Mathf.Abs(asteroidCoordinates.x - spaceship.transform.position.x) > remoteTreshold
                 || Mathf.Abs(asteroidCoordinates.y - spaceship.transform.position.y) > remoteTreshold
                 || Mathf.Abs(asteroidCoordinates.z - spaceship.transform.position.z) > remoteTreshold)
                {
                    asteroid.destroyAsteroid();
                }
            }


        }
    }
    
}
