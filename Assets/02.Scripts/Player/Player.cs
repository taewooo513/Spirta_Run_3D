using UnityEngine;
public class Player : MonoBehaviour
{
    public UIManager uiManager;

    public int maxHealth = 3;
    private int currentHealth;

    public GameObject coinEffectPrefab;
    public GameObject damageEffectPrefab;
    //public GameObject laneChangeEffectPrefab;

    [Header("PlayerModel")]
    public SkinnedMeshRenderer hair;
    public SkinnedMeshRenderer hair1;
    public SkinnedMeshRenderer hair2;
    public SkinnedMeshRenderer clothes;

    public bool isInv = false;
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

        if (isInv == true)
        {
            Debug.Log("���� ������");
            return;
        }
        if (currentHealth <= 0) return;
        tim += Time.deltaTime;
        Debug.Log(tim);
        // ü���� ��������ŭ ���ҽ�ŵ�ϴ�.
        if (damageEffectPrefab != null)
        {
            Instantiate(damageEffectPrefab, transform.position, Quaternion.identity);
        }
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

    public void AddHeal()
    {
        currentHealth++;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
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
                item.GetItem();
            }
            if (coinEffectPrefab != null)
            {
                Instantiate(coinEffectPrefab, transform.position, Quaternion.identity);
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