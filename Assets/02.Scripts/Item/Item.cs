using UnityEngine;
using UnityEngine.Events;

public partial class Item : MonoBehaviour
{
    [Header("Item Settings")]
    public float maxTime; // 버프 지속시간
    public float val;     // 효과 수치 (점수, 속도 배율 등)

    [Header("Effects")]
    public GameObject collectionEffectPrefab;

    [Header("Actions")]
    public UnityEvent useItemAction;
    public void GetItem()
    {
        Debug.Log("함수가 호출되었습니다!");
        if (collectionEffectPrefab != null)
        {
            Instantiate(collectionEffectPrefab, transform.position, Quaternion.identity);
        }
        // 인스펙터에 연결된 추가 기능
        useItemAction?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
    }
}
