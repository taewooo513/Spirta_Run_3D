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
        if (collectionEffectPrefab != null)
        {
            Instantiate(collectionEffectPrefab, transform.position, Quaternion.identity);
        }
        // 인스펙터에 연결된 추가 기능
        Debug.Log("ㅁㅈ");
        useItemAction?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
    }
}
