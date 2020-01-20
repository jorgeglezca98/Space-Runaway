public class OverheatStats
{
    private float attackModeTime = 5f;
    private bool attackMode = false;
    private float playerOverheat = 0f;
    private float maxOverheat = 100f;
    private float overheatWaitTime = 5f;

    private bool isCoolingDown = false;
    private float cooldownStartTime;

    // The amount of overheat the weapon produces everytime it shots.
    public float overheatIncrement = 3f;
    // The amount of overheat the weapon cools down everytime it shots.
    public float overheatDecrement = 0.75f;
    // The amount of time in seconds the weapon get disabled when the
    // maximum overheat is achieved.
    public float maxOverheatPenalizationTime = 3f;

    public float GetCooldownStartTime()
    {
        return cooldownStartTime;
    }

    public void SetCooldownStartTime(float time)
    {
        cooldownStartTime = time;
    }

    public float GetOverheat()
    {
        return playerOverheat;
    }

    public float GetMaxOverheat()
    {
        return maxOverheat;
    }

    public void SetOverheat(float overheat)
    {
        if (overheat > 0f)
        {
            playerOverheat = overheat;
        }
        else
        {
            playerOverheat = 0f;
        }
    }

    public bool IsAttacking()
    {
        return attackMode;
    }

    public void SetAttackMode(bool attacking)
    {
        attackMode = attacking;
    }

    public float GetAttackModeTime()
    {
        return attackModeTime;
    }

    public void SetAttackModeTime(float time)
    {
        attackModeTime = time;
    }

    public bool GetIsCoolingDown()
    {
        return isCoolingDown;
    }

    public void SetIsCoolingDown(bool isCoolingDown)
    {
        this.isCoolingDown = isCoolingDown;
    }
}
