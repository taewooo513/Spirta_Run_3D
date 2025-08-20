using UnityEngine;
public class Player : MonoBehaviour
{
    public UIManager uiManager;

    public int maxHealth = 3;
    private int currentHealth;

    [Header("PlayerModel")]
    public SkinnedMeshRenderer hair;
    public SkinnedMeshRenderer hair1;
    public SkinnedMeshRenderer hair2;
    public SkinnedMeshRenderer clothes;

    float tim = 0;

    void Start()
    {
        CharacterManager.Instance.player = this;
        // ���� ü�� -> �ִ� ü��
        currentHealth = maxHealth;
        CharacterManager.Instance.SetGamePlayerMaterial();
        if (uiManager != null)
        {
            uiManager.UpdateHealthUI(currentHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        //if (uiManager.gmMode) return; //gm ����϶��� ����
        if (currentHealth <= 0) return;
        tim += Time.deltaTime;
        Debug.Log(tim);
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