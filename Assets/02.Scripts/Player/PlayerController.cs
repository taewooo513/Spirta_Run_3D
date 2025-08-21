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
    public float forwardSpeed = 8f;
    public float sideSpeed = 5f;
    public float jumpForce = 8f;

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

        // 땅에 닿아있다면, 점프 횟수를 초기화
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // 항상 앞으로 이동
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // 좌우 이동
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * sideSpeed * Time.deltaTime);

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

        // 점프 횟수를 1 증가시킵니다.
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
