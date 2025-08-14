using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;

    void Start()
    {
        // 현재 카메라의 위치와 타겟의 위치를 빼서 거리 차이를 계산하고 저장.
        offset = transform.position - target.position;
    }

    // 캐릭터가 먼저 움직이고, 그 다음에 카메라가 따라가기위한 LateUpdate.
    void LateUpdate()
    {
        // 카메라의 위치를 (타겟의 현재 위치 + 처음에 계산한 거리 차이)로 계속 업데이트.
        transform.position = target.position + offset;
    }
}