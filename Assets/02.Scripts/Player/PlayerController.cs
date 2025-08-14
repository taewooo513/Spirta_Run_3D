using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 캐릭터의 전진 속도
    public float forwardSpeed = 8f;
    // 캐릭터의 좌우 이동 속도
    public float sideSpeed = 5f;
    void Update()
    {
        // Time.deltaTime을 곱해줘서 컴퓨터 성능과 상관없이 일정한 속도를 유지
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // 좌우 키보드 입력(A, D 또는 ←, →)을 받는다.
        float horizontalInput = Input.GetAxis("Horizontal");

        // 좌우 입력 값에 따라 캐릭터를 옆으로 이동시킨다.
        transform.Translate(Vector3.right * horizontalInput * sideSpeed * Time.deltaTime);
    }
}