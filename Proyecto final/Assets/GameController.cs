using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private Vector3 PlayerInitialPosition = new Vector3(0, 0, 0);
    private Vector3 MotherShipPosition = new Vector3(0f, 0f, 3000f);
    private GameObject PlayerSpaceship;
    private GameObject EnemySpaceship;
    private GameObject MotherShip;

    enum EnemyType { Assault, Kamikaze };

    private EnemyType[] EnemiesList = { EnemyType.Assault, EnemyType.Assault, EnemyType.Kamikaze };
    private int CurrentEnemy = 0;
    public static bool EnemyIsUp = false;


    void Awake()
    {

        GameObject[] residualEnemies = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < residualEnemies.Length; i++)
        {
            Destroy(residualEnemies[i]);
        }

        PlayerSpaceship = GameObject.Find("PlayerSpaceship");
        PlayerSpaceship.transform.position = PlayerInitialPosition;
        AddScriptsToPlayer();

        MotherShip = GameObject.Find("MotherShip");
        MotherShip.transform.position = MotherShipPosition;

        GenerateEnemy();
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GenerateEnemy();
    }



    void GenerateEnemy()
    {
        if (EnemySpaceship == null)
        {
            if (CurrentEnemy >= EnemiesList.Length)
            {
                CurrentEnemy = 0;
            }
            EnemySpaceship = CreateEnemy(EnemiesList[CurrentEnemy]);
            EnemySpaceship.transform.Rotate(0, 180f, 0);
            CurrentEnemy += 1;
        }
    }

    IEnumerator WaitForObjectToBeDestroyed()
    {
        yield return new WaitForSeconds(1);
    }



    GameObject CreateEnemy(EnemyType type)
    {
        GameObject enemy = null;

        switch (type)
        {
            case EnemyType.Assault:
                enemy = Instantiate(Resources.Load("AssaultEnemy")) as GameObject;
                enemy.AddComponent<EnemyDestructionController>();
                enemy.AddComponent<AssaultArtificialIntelligence>();
                enemy.transform.position = GenerateFreePosition();
                break;

            // TODO: Put the other prefab!
            case EnemyType.Kamikaze:
                enemy = Instantiate(Resources.Load("KamikazeEnemy")) as GameObject;
                enemy.AddComponent<EnemyDestructionController>();
                enemy.AddComponent<KamikazeArtificialIntelligence>();
                enemy.transform.position = GenerateFreePosition();
                break;
        }

        return enemy;
    }

    Vector3 GenerateFreePosition()
    {
        // GameObject[] asteroids = GameObject.FindGameObjectsWithTag("asteroid");
        // GameObject asteroid = asteroids[Random.Range(0, asteroids.Length - 1)];
        // Vector3 asteroidPosition = asteroid.transform.position;
        // float x = asteroidPosition.x + 60;
        // float y = asteroidPosition.y + 60;
        // float z = asteroidPosition.z + 60;
        float x = 0;
        float y = 0;
        float z = 200;
        return new Vector3(x, y, z);
    }



    void AddScriptsToPlayer()
    {
        PlayerSpaceship.AddComponent<PlayerDestructionController>();
        PlayerSpaceship.AddComponent<SpaceshipMovement>();
        PlayerSpaceship.AddComponent<SpotlightController>();
        PlayerSpaceship.AddComponent<Dash>();
        PlayerSpaceship.transform.Find("Blaster-1").gameObject.AddComponent<Shots>();
        PlayerSpaceship.transform.Find("Blaster-2").gameObject.AddComponent<Shots>();
        GameObject.Find("FrontSpotlight").AddComponent<SpotlightController>();
        GameObject.Find("Arrow").AddComponent<EnemyPointer>();
    }

}
