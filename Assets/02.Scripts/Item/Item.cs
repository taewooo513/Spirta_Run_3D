using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public partial class Item : MonoBehaviour
{
    public UnityEvent useItemAction; // ������ ��� �� ȣ���ϴ� �Լ�

    public float maxTime; // �������ӽð�
    public float val; // ��ġ ex). �̼����� �󸶳��Ǵ�����

    [Header("Effects")]
    public GameObject collectionEffectPrefab; 
    public VideoClip videoToPlay; 

    public void GetItem()
    {
        // ����Ʈ ����
        if (collectionEffectPrefab != null)
        {
            Instantiate(collectionEffectPrefab, transform.position, Quaternion.identity);
        }

        // ���� ��� ��û
        if (videoToPlay != null)
        {
            VideoManager.Instance.PlayVideo(videoToPlay);
        }

        useItemAction?.Invoke();

        Destroy(gameObject);
    }
}
