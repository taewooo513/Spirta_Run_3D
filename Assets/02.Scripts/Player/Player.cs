using UnityEngine;

public class Player : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;

    public UIManager uiManager;


    void Start()
    {
        currentHealth = maxHealth;

        if (uiManager != null)
        {
            uiManager.UpdateHealthUI(currentHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (uiManager != null)
        {
            uiManager.UpdateHealthUI(currentHealth);
        }
        //========================================================

        Debug.Log("데미지를 입었습니다! 현재 체력: " + currentHealth);

        if (currentHealth <= 0)
        {
            //GameOver();
        }
    }
}