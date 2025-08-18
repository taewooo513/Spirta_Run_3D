using System.Collections;
using UnityEngine;
using _02.Scripts.map; // Map 스크립트의 네임스페이스를 추가해줍니다.

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(gameObject); }
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        // GameManager를 통해 현재 맵의 정보를 가져옵니다.
        Map currentMap = GameManager.Instance.map[0];

        if (currentMap == null)
        {
            Debug.LogError("GameManager에 Map이 등록되지 않았습니다!");
            yield break; // 맵이 없으면 코루틴 즉시 종료
        }

        // 현재 맵의 원래 속도를 저장.
        float originalSpeed = currentMap.speed;

        // 맵의 속도를 변경.
        currentMap.speed = originalSpeed * multiplier;

        // 지정된 시간만큼 대기.
        yield return new WaitForSeconds(duration);

        // 맵의 속도를 원래대로 복구합니다.
        if (GameManager.Instance.map != null)
        {
            GameManager.Instance.map[0].speed = originalSpeed;
        }
    }
}