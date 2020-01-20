using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private Vector3 playerInitialPosition = new Vector3(0, 0, 0);
    private Vector3 motherShipPosition = new Vector3(0f, 0f, 3000f);
    private GameObject playerSpaceship;
    private GameObject enemySpaceship;
    private GameObject motherShip;

    private enum EnemyType { Assault, Kamikaze };

    private EnemyType[] enemiesList = { EnemyType.Assault, EnemyType.Assault, EnemyType.Kamikaze };
    private int currentEnemy = 0;

    public static bool enemyIsUp = false;

    private void Awake()
    {
        GameObject[] residualEnemies = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < residualEnemies.Length; i++)
        {
            Destroy(residualEnemies[i]);
        }

        playerSpaceship = GameObject.Find("PlayerSpaceship");
        playerSpaceship.transform.position = playerInitialPosition;
        AddScriptsToPlayer();

        motherShip = GameObject.Find("MotherShip");
        motherShip.transform.position = motherShipPosition;

        // GenerateEnemy();
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        GenerateEnemy();
    }

    private void GenerateEnemy()
    {
        if (enemySpaceship == null)
        {
            if (currentEnemy >= enemiesList.Length)
            {
                currentEnemy = 0;
            }
            enemySpaceship = CreateEnemy(enemiesList[currentEnemy]);
            enemySpaceship.transform.Rotate(0, 180f, 0);
            currentEnemy += 1;
        }
    }

    private IEnumerator WaitForObjectToBeDestroyed()
    {
        yield return new WaitForSeconds(1);
    }

    private GameObject CreateEnemy(EnemyType type)
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

    private Vector3 GenerateFreePosition()
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

    private void AddScriptsToPlayer()
    {
        playerSpaceship.AddComponent<PlayerDestructionController>();
        playerSpaceship.AddComponent<SpaceshipMovement>();
        playerSpaceship.AddComponent<SpotlightController>();
        playerSpaceship.AddComponent<Dash>();
        playerSpaceship.transform.Find("Blaster-1").gameObject.AddComponent<Shots>();
        playerSpaceship.transform.Find("Blaster-2").gameObject.AddComponent<Shots>();
        GameObject.Find("FrontSpotlight").AddComponent<SpotlightController>();
        GameObject.Find("Arrow").AddComponent<EnemyPointer>();
    }
}
