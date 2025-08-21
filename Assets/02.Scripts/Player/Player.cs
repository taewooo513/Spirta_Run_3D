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
        currentHealth = maxHealth; //현재 체력을 최대 체력으로 초기화
        CharacterManager.Instance.SetGamePlayerMaterial();
        if (uiManager != null)
        {
            GameManager.Instance.score.SendThisUIManager(uiManager);
            uiManager.UpdateHealthUI(currentHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (uiManager.gmMode)  //gm 모드일때는 무적
        {
            Debug.Log("무적모드 : 충돌 무시");
            return;
        }

        if (isInv == true)
        {
            Debug.Log("무적 아이템");
            return;
        }
        if (currentHealth <= 0) return;
        tim += Time.deltaTime;
        Debug.Log(tim);
        // 체력을 데미지만큼 감소시킵니다.
        if (damageEffectPrefab != null)
        {
            Instantiate(damageEffectPrefab, transform.position, Quaternion.identity);
        }
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
        // Item 태그로 충돌 판정
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
        Debug.Log("게임 오버!");
        if (uiManager != null)
        {
            uiManager.GameOver();
        }
    }
}