using UnityEngine;
public class Player : MonoBehaviour
{
    public UIManager uiManager;

    public int maxHealth = 3; 
    private int currentHealth;

    void Start()
    {
        CharacterManager.Instance.player = this;
        // 현재 체력 -> 최대 체력
        currentHealth = maxHealth;

        if (uiManager != null)
        {
            uiManager.UpdateHealthUI(currentHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0) return;

        // 체력을 데미지만큼 감소시킵니다.
        currentHealth -= damageAmount;

        if (uiManager != null)
        {
            uiManager.UpdateHealthUI(currentHealth);
        }

        Debug.Log("데미지를 입었습니다! 현재 체력: " + currentHealth);

        // 체력이 0 이하가 되면 게임 오버 처리
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("게임 오버!");

        if (uiManager != null)
        {
            uiManager.GameOver();
        }
    }
}