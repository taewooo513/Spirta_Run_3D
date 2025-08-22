using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    [Header("VFX Prefabs")]
    public GameObject jumpEffectPrefab;
    public GameObject slideEffectPrefab;

    //설정 값
    [Header("Movement Settings")]
    public float jumpForce = 8f;
    public float tiltSensitivity = 20f; // 기울기 민감도

    [Header("Slide Settings")]
    public float slideDuration = 1.0f;
    public float slideColliderHeight = 0.9f;
    public Vector3 slideColliderCenter = new Vector3(0, 0.45f, 0);

    //상태 변수 ---
    private bool isSliding = false;
    private Coroutine slideCoroutine;

    //더블 점프 & 지면 체크 변수
    [Header("Jump Settings")]
    public int maxJumps = 2;
    private int jumpCount;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.1f;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public float minSwipeDistance = 50f;

    private float originalColliderHeight;
    private Vector3 originalColliderCenter;

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
        // 지면 체크 및 점프 횟수 초기화
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance);
        if (isGrounded)
        {
            jumpCount = 0;
        }

        HandlePhoneInput();
    }

    // 폰 조작 (기울기 + 스와이프)
    private void HandlePhoneInput()
    {
        // 기울기로 좌우 이동
        if (Input.gyro.enabled)
        {
            float gyroInput = Input.gyro.gravity.x;
            transform.Translate(new Vector3(gyroInput, 0, 0) * tiltSensitivity * Time.deltaTime);
        }

        // 스와이프로 점프/슬라이드
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) startTouchPosition = touch.position;
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                CheckSwipeForActions();
            }
        }
    }

    // 스와이프 (점프/슬라이드만)
    private void CheckSwipeForActions()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;
        if (swipeDelta.magnitude < minSwipeDistance) return;

        if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
        {
            if (swipeDelta.y > 0 && jumpCount < maxJumps) Jump();
            else if (swipeDelta.y < 0 && isGrounded && !isSliding)
            {
                slideCoroutine = StartCoroutine(Slide());
            }
        }
    }

    void Jump()
    {
        if (isSliding)
        {
            if (slideCoroutine != null) StopCoroutine(slideCoroutine);
            isSliding = false;
            capsuleCollider.height = originalColliderHeight;
            capsuleCollider.center = originalColliderCenter;
        }
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        jumpCount++;
        if (jumpEffectPrefab != null) Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);
    }

    IEnumerator Slide()
    {
        isSliding = true;
        GameObject slideEffectInstance = null;
        animator.SetTrigger("Slide");
        if (slideEffectPrefab != null) slideEffectInstance = Instantiate(slideEffectPrefab, transform.position, Quaternion.identity);
        capsuleCollider.height = slideColliderHeight;
        capsuleCollider.center = slideColliderCenter;
        yield return new WaitForSeconds(slideDuration);
        capsuleCollider.height = originalColliderHeight;
        capsuleCollider.center = originalColliderCenter;
        if (slideEffectInstance != null) Destroy(slideEffectInstance);
        isSliding = false;
    }
}
