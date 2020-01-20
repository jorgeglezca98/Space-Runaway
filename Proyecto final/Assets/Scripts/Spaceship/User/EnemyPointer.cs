using System;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    private GameObject enemy;

    private float lastHealth;
    private DateTime lastShot;

    private Renderer arrowRenderer;
    private DestructionController playerDestructionController;

    private void Start()
    {
        arrowRenderer = gameObject.GetComponent<Renderer>();
        arrowRenderer.material.color = Color.blue;

        playerDestructionController = GameObject.Find("PlayerSpaceship").GetComponent<PlayerDestructionController>();
        lastHealth = playerDestructionController.Stats.GetHealth();

        lastShot = DateTime.Now;
    }

    private void Update()
    {
        if (!enemy)
        {
            enemy = GameObject.FindWithTag("enemy");
        }
        if (enemy)
        {
            arrowRenderer.enabled = true;

            transform.LookAt(enemy.transform);
            transform.Rotate(90, 0, 0);

            if (lastHealth != playerDestructionController.Stats.GetHealth())
            {
                lastHealth = playerDestructionController.Stats.GetHealth();
                lastShot = DateTime.Now;
                arrowRenderer.material.color = Color.red;
            }
            else if ((DateTime.Now - lastShot).TotalSeconds >= 1f)
            {
                arrowRenderer.material.color = Color.blue;
            }
        }
        else
        {
            arrowRenderer.enabled = false;
        }
    }
}
