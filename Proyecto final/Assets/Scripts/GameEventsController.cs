using System;
using UnityEngine;

public class GameEventsController : MonoBehaviour
{
    // You need an instance to call the events!
    public static GameEventsController eventController;

    // Health delegate.
    public event Action<float> OnHealthPctChanged = delegate { };
    public event Action<float> OnOverheatPctChanged = delegate { };
    public event Action OnMaximumOverheat = delegate { };
    public event Action OnAttackModeEnter = delegate { };
    public event Action OnAttackModeExit = delegate { };
    public event Action<string> OnPlayerDestruction = delegate { };
    public event Action<string> OnPlayerWon = delegate { };
    public event Action OnEnemyDestruction = delegate { };

    public void HealthPctChanged(float health)
    {
        if (OnHealthPctChanged != null)
        {
            OnHealthPctChanged(health);
        }
    }

    public void PlayerDestroyed()
    {
        if (OnPlayerDestruction != null)
        {
            OnPlayerDestruction("YOU LOSE!");
        }
    }

    public void EnemyDestroyed()
    {
        if (OnEnemyDestruction != null)
        {
            OnEnemyDestruction();
        }
    }

    public void PlayerWon()
    {
        if (OnPlayerWon != null)
        {
            OnPlayerWon("YOU WON!");
        }
    }

    public void OverheatPctChanged(float overheat)
    {
        if (OnOverheatPctChanged != null)
        {
            OnOverheatPctChanged(overheat);
        }
    }

    public void MaximumOverheat()
    {
        if (OnMaximumOverheat != null)
        {
            OnMaximumOverheat();
        }
    }

    public void AttackModeEnter()
    {
        if (OnAttackModeEnter != null)
        {
            OnAttackModeEnter();
        }
    }

    public void AttackModeExit()
    {
        if (OnAttackModeExit != null)
        {
            OnAttackModeExit();
        }
    }

    // Use this for initialization
    private void Awake()
    {
        eventController = this;
    }
}
