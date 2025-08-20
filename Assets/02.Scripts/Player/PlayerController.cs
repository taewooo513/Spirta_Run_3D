using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 컴포넌트 변수
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    // 이펙트 프리팹
    public GameObject jumpEffectPrefab;
    public GameObject slideEffectPrefab;

    // 설정 값
    public float forwardSpeed = 8f;
    public float sideSpeed = 10f; // 기울기 및 버튼 조작 민감도
    public float jumpForce = 8f;

    // 슬라이드 값
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    public float slideColliderHeight = 0.9f;
    public Vector3 slideColliderCenter = new Vector3(0, 0.45f, 0);
    public float slideDuration = 1.0f;

    // 상태 변수
    private bool isSliding = false;

    // 스와이프 감지용 변수
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public float minSwipeDistance = 50f;

    void Start()
    {
        // 시작할 때 각 컴포넌트를 자동으로 찾아와서 변수에 할당
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // 원래 캡슐 콜라이더의 높이와 중심 위치를 저장
        originalColliderHeight = capsuleCollider.height;
        originalColliderCenter = capsuleCollider.center;

        // 자이로 센서 활성화
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    void Update()
    {
        // --- 조작 방식 선택 ---
        if (Settings.Instance.currentControlType == Settings.ControlType.Tilt)
        {
            // 1. 기울기(자이로) 조작
            if (Input.gyro.enabled)
            {
                float gyroInput = Input.gyro.gravity.x;
                Vector3 moveDirection = new Vector3(gyroInput, 0, 0);
                transform.Translate(moveDirection * sideSpeed * Time.deltaTime);
            }
        }
        else // 2. 버튼 조작
        {
            // PC 테스트용 키보드 좌우 이동
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * sideSpeed * Time.deltaTime);
            // (실제 모바일용 좌/우 버튼 UI가 있다면 여기에 로직을 추가합니다)
        }

        // --- 스와이프로 점프/슬라이드 조작 ---
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                CheckSwipe();
            }
        }
    }

    // 스와이프 방향을 판정하는 함수
    private void CheckSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;
        if (swipeDelta.magnitude < minSwipeDistance) return;

        if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
        {
            if (swipeDelta.y > 0 && !isSliding) // 위로 스와이프
            {
                Jump();
            }
            else if (swipeDelta.y < 0 && !isSliding) // 아래로 스와이프
            {
                StartCoroutine(Slide());
            }
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump"); // Animator 트리거 이름 확인

        if (jumpEffectPrefab != null)
        {
            Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    IEnumerator Slide()
    {
        isSliding = true;
        GameObject slideEffectInstance = null;

        animator.SetTrigger("Slide"); // Animator 트리거 이름 확인
        if (slideEffectPrefab != null)
        {
            slideEffectInstance = Instantiate(slideEffectPrefab, transform.position, Quaternion.identity);
        }

        capsuleCollider.height = slideColliderHeight;
        capsuleCollider.center = slideColliderCenter;

        yield return new WaitForSeconds(slideDuration);

        capsuleCollider.height = originalColliderHeight;
        capsuleCollider.center = originalColliderCenter;

        if (slideEffectInstance != null)
        {
            Destroy(slideEffectInstance);
        }

        isSliding = false;
    }

    // 아이템 획득 로직
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                item.GetItem();
            }
        }
    }

    // 장애물 충돌 로직
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 진동 설정이 켜져있을 때만 진동 실행
            if (Settings.Instance.IsVibrationEnabled())
            {
                Handheld.Vibrate();
            }
        }
    }
}
