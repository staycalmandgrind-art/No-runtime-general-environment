using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    [SerializeField] private int maxHealth = 200;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log($"{name} took {damage} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log($"{name} is knocked out. Resetting health for dummy testing.");
            currentHealth = maxHealth;
        }
    }
}
