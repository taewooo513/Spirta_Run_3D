using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private Player player;

    public GameObject jumpEffectPrefab;
    public GameObject slideEffectPrefab;
    public GameObject laneChangeEffectPrefab;

    public Vector3 laneChangeEffectOffset = new Vector3(0, 0.5f, 4f);
    public Vector3 jumpEffectOffset = new Vector3(0, 100f, 0f);

    // 설정 값
    public float forwardSpeed = 0f;
    public float jumpForce = 8f;

    // 레인 이동 관련 변수
    private int currentLane = 0;
    public float laneWidth = 4f;
    public float laneChangeSpeed = 15f; // 레인이 바뀌는 속도
    public float laneChangeEffectDelay = 0.15f; // 이펙트 지연 시간

    // 슬라이드 값
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    public float slideColliderHeight = 0.9f;
    public Vector3 slideColliderCenter = new Vector3(0, 0.45f, 0);
    public float slideDuration = 1.0f;

    // 상태 변수

    private bool isSliding = false;
    private bool isJumping = false;

    public int maxJumps = 2;
    private int jumpCount;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.1f;

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

        int previousLane = currentLane;
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayLaneChangeEffect();
            currentLane--;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayLaneChangeEffect();
            currentLane++;
        }
        //if (currentLane != previousLane)
        //{
        //    StartCoroutine(PlayLaneChangeEffect());
        //}

        currentLane = Mathf.Clamp(currentLane, -1, 1);

        Vector3 targetPosition = transform.position;
        targetPosition.x = currentLane * laneWidth;

        // 현재 위치에서 목표 위치로 부드럽게 이동 (Lerp 사용)
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * laneChangeSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            isJumping = true;
            Jump();
            if (isGrounded == true)
            {
                isJumping = false;
            }
        }

        // 슬라이드 입력 조건

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding && !isJumping/*&& isGrounded*/)
        {
            StartCoroutine(Slide());
        }
    }
    void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        jumpCount++;

        if (jumpEffectPrefab != null)
        {
            Vector3 effectPosition = transform.position + jumpEffectOffset;
            Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);
        }
        isJumping = false;
    }

    //IEnumerator PlayLaneChangeEffect()
    //{
    //    yield return new WaitForSeconds(laneChangeEffectDelay);

    //    if (laneChangeEffectPrefab != null)
    //    {
    //        Vector3 effectPosition = transform.position + laneChangeEffectOffset;
    //        Instantiate(laneChangeEffectPrefab, effectPosition, Quaternion.identity);
    //    }
    //}

    public void PlayLaneChangeEffect()
    {
        if (laneChangeEffectPrefab != null)
        {
            Instantiate(laneChangeEffectPrefab, transform.position, Quaternion.identity);
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