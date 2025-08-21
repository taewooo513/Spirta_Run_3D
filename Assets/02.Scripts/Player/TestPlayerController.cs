using System.Collections;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    // 컴포넌트 변수
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    // 이펙트 프리팹
    public GameObject jumpEffectPrefab;
    public GameObject slideEffectPrefab;

    // 설정 값
    public float sideSpeed = 10f;
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
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        originalColliderHeight = capsuleCollider.height;
        originalColliderCenter = capsuleCollider.center;

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    void Update()
    {
        // 자이로 센서로 좌우 이동
        if (Input.gyro.enabled)
        {
            float gyroInput = Input.gyro.gravity.x;
            Vector3 moveDirection = new Vector3(gyroInput, 0, 0);
            transform.Translate(moveDirection * sideSpeed * Time.deltaTime);
        }

        // 터치 입력 감지
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

    private void CheckSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        if (swipeDelta.magnitude < minSwipeDistance)
        {
            return;
        }

        if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
        {
            // 위로 스와이프 & 슬라이드 중이 아닐 때
            if (swipeDelta.y > 0 && !isSliding)
            {
                Jump();
            }
            // 아래로 스와이프 & 슬라이드 중이 아닐 때
            else if (swipeDelta.y < 0 && !isSliding)
            {
                StartCoroutine(Slide());
            }
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");

        if (jumpEffectPrefab != null)
        {
            Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    IEnumerator Slide()
    {
        isSliding = true;
        GameObject slideEffectInstance = null;

        animator.SetTrigger("Slide");
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
}