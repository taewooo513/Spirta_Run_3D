using UnityEngine;
using UnityEngine.Events;

public partial class Item : MonoBehaviour
{
    [Header("Item Settings")]
    public float maxTime; // ���� ���ӽð�
    public float val;     // ȿ�� ��ġ (����, �ӵ� ���� ��)

    [Header("Effects")]
    public GameObject collectionEffectPrefab;

    [Header("Actions")]
    public UnityEvent useItemAction;
    public void GetItem()
    {
        Debug.Log("�Լ��� ȣ��Ǿ����ϴ�!");
        if (collectionEffectPrefab != null)
        {
            Instantiate(collectionEffectPrefab, transform.position, Quaternion.identity);
        }
        // �ν����Ϳ� ����� �߰� ���
        useItemAction?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
    }
}
