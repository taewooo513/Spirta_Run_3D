using UnityEngine;
public class Player : MonoBehaviour
{
    public UIManager uiManager;

    public int maxHealth = 3; 
    private int currentHealth;

    void Start()
    {
        CharacterManager.Instance.player = this;
        // ���� ü�� -> �ִ� ü��
        currentHealth = maxHealth;

        if (uiManager != null)
        {
            uiManager.UpdateHealthUI(currentHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0) return;

        // ü���� ��������ŭ ���ҽ�ŵ�ϴ�.
        currentHealth -= damageAmount;

        if (uiManager != null)
        {
            uiManager.UpdateHealthUI(currentHealth);
        }

        Debug.Log("�������� �Ծ����ϴ�! ���� ü��: " + currentHealth);

        // ü���� 0 ���ϰ� �Ǹ� ���� ���� ó��
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("���� ����!");

        if (uiManager != null)
        {
            uiManager.GameOver();
        }
    }
}