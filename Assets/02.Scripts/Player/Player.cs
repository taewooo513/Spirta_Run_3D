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
        currentHealth = maxHealth; //현재 체력을 최대 체력으로 초기화
        CharacterManager.Instance.SetGamePlayerMaterial();
        if (uiManager != null)
        {
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

        if (currentHealth <= 0) return;
        tim += Time.deltaTime;
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