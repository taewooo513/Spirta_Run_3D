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
        currentHealth = maxHealth; //���� ü���� �ִ� ü������ �ʱ�ȭ
        CharacterManager.Instance.SetGamePlayerMaterial();
        if (uiManager != null)
        {
            GameManager.Instance.score.SendThisUIManager(uiManager);
            uiManager.UpdateHealthUI(currentHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (uiManager.gmMode)  //gm ����϶��� ����
        {
            Debug.Log("������� : �浹 ����");
            return;
        }
        
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
    
    private void OnTriggerEnter(Collider other)
    {
        // Item �±׷� �浹 ����
        if (other.CompareTag("Item")) 
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                Debug.Log("������ ȹ��: " + item.name); 
                item.GetItem();
            }
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