using System.Collections;
using UnityEngine;
using _02.Scripts.map; // Map ��ũ��Ʈ�� ���ӽ����̽��� �߰����ݴϴ�.

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
        // GameManager�� ���� ���� ���� ������ �����ɴϴ�.
        Map currentMap = GameManager.Instance.map[0];

        if (currentMap == null)
        {
            Debug.LogError("GameManager�� Map�� ��ϵ��� �ʾҽ��ϴ�!");
            yield break; // ���� ������ �ڷ�ƾ ��� ����
        }

        // ���� ���� ���� �ӵ��� ����.
        float originalSpeed = currentMap.speed;

        // ���� �ӵ��� ����.
        currentMap.speed = originalSpeed * multiplier;

        // ������ �ð���ŭ ���.
        yield return new WaitForSeconds(duration);

        // ���� �ӵ��� ������� �����մϴ�.
        if (GameManager.Instance.map != null)
        {
            GameManager.Instance.map[0].speed = originalSpeed;
        }
    }
}