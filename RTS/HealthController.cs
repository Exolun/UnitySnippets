using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public float MaxHealth = 1000;

    private float currentHealth;
    private bool isDead;

    public void InflictDamage(float amount)
    {
        this.currentHealth -= amount;
    }

    void Start()
    {
        this.currentHealth = MaxHealth;
        this.isDead = false;
    }

    void Update()
    {
        if (this.currentHealth <= 0 && !this.isDead)
        {
            this.triggerDeath();
        }
    }

    private void triggerDeath()
    {
        this.isDead = true;
        Destroy(this.gameObject);
    }
}
