public class LifeStats
{
    private float playerHealth = 100f;
    private float maxHealth = 100f;

    public LifeStats(float maxHealth)
    {
        this.maxHealth = maxHealth;
        playerHealth = maxHealth;
    }

    public float GetHealth()
    {
        return playerHealth;
    }

    public void SetHealth(float health)
    {
        playerHealth = health;
        float healthPct = playerHealth / maxHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
