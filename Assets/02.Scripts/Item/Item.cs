using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public partial class Item : MonoBehaviour
{
    public UnityEvent useItemAction; // 아이템 사용 시 호출하는 함수

    public float maxTime; // 버프지속시간
    public float val; // 수치 ex). 이속증가 얼마나되는지등

    [Header("Effects")]
    public GameObject collectionEffectPrefab; 
    public VideoClip videoToPlay; 

    public void GetItem()
    {
        // 이펙트 생성
        if (collectionEffectPrefab != null)
        {
            Instantiate(collectionEffectPrefab, transform.position, Quaternion.identity);
        }

        // 비디오 재생 요청
        if (videoToPlay != null)
        {
            VideoManager.Instance.PlayVideo(videoToPlay);
        }

        useItemAction?.Invoke();

        Destroy(gameObject);
    }
}
