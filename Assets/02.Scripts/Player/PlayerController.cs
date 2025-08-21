using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    public GameObject jumpEffectPrefab;
    public GameObject slideEffectPrefab;

    // 설정 값
    public float forwardSpeed = 0f; 
    public float jumpForce = 8f;

    // 레인 이동 관련 변수
    private int currentLane = 0; // 현재 레인 (-1: 왼쪽, 0: 중앙, 1: 오른쪽)
    public float laneWidth = 4f;
    public float laneChangeSpeed = 15f; // 레인이 바뀌는 속도

    // 슬라이드 값
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    public float slideColliderHeight = 0.9f;
    public Vector3 slideColliderCenter = new Vector3(0, 0.45f, 0);
    public float slideDuration = 1.0f;

    // 상태 변수
    private bool isSliding = false;

    // 더블 점프 & 지면 체크 변수
    public int maxJumps = 2;
    private int jumpCount;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalColliderHeight = capsuleCollider.height;
        originalColliderCenter = capsuleCollider.center;

        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Slide");
    }

    void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance);

        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentLane--;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            currentLane++;
        }

        // 현재 레인이 -1 ~ 1 사이를 벗어나지 않도록 제한
        currentLane = Mathf.Clamp(currentLane, -1, 1);

        // 목표 x 위치 계산 (-4, 0, 4)
        Vector3 targetPosition = transform.position;
        targetPosition.x = currentLane * laneWidth;

        // 현재 위치에서 목표 위치로 부드럽게 이동 (Lerp 사용)
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * laneChangeSpeed);


        // 점프 입력 조건
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps && !isSliding)
        {
            Jump();
        }

        // 슬라이드 입력 조건
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && !isSliding)
        {
            StartCoroutine(Slide());
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        jumpCount++;
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
