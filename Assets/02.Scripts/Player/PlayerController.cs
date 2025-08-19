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

    // 슬라이드 시 변경될 콜라이더 값
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    public float slideColliderHeight = 0.9f;
    public Vector3 slideColliderCenter = new Vector3(0, 0.45f, 0);
    public float slideDuration = 1.0f;

    //슬라이드 중 점프 막기 위한 isSliding
    private bool isSliding = false;


    void Start()
    {
        // 시작할 때 각 컴포넌트를 자동으로 찾아와서 변수에 할당
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // 원래 캡슐 콜라이더의 높이와 중심 위치를 저장
        originalColliderHeight = capsuleCollider.height;
        originalColliderCenter = capsuleCollider.center;

        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Slide");
    }

    void Update()
    {
        // 항상 앞으로 이동
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // 좌우 이동
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * sideSpeed * Time.deltaTime);

        // 점프 입력 (스페이스 바)
        if (Input.GetKeyDown(KeyCode.Space) && !isSliding)
        {
            Jump();
        }

        // 슬라이드 입력 (왼쪽 컨트롤 키)
        // 키 입력 시간동안 지속되어야되기때문에 코루틴을 사용 해야될거같다.
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding)
        {
            StartCoroutine(Slide());
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

        // 슬라이드 시작 시점에 애니메이션과 이펙트를 동시에 실행
        animator.SetTrigger("Slide");
        if (slideEffectPrefab != null)
        {
            slideEffectInstance = Instantiate(slideEffectPrefab, transform.position, Quaternion.identity);
        }

        // 슬라이드용으로 콜라이더 변경
        capsuleCollider.height = slideColliderHeight;
        capsuleCollider.center = slideColliderCenter;

        // 지정된 시간만큼 슬라이드 상태를 유지
        yield return new WaitForSeconds(slideDuration);

        // 슬라이드가 끝나면 콜라이더를 원래대로 복구
        capsuleCollider.height = originalColliderHeight;
        capsuleCollider.center = originalColliderCenter;

        // 슬라이드가 끝나는 시점에 이펙트도 함께 파괴
        if (slideEffectInstance != null)
        {
            Destroy(slideEffectInstance);
        }

        isSliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Item 태그로 충돌 판정
        if (other.CompareTag("Item")) 
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                //BuffManager.Instance.ApplySpeedBoost(item.val, item.maxTime);
                Debug.Log("아이템 획득: " + item.name); 
                item.GetItem();
                //Debug.Log("스피드업 아이템 획득");
            }
        }
    }
}