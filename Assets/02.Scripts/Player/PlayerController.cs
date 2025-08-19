using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // 슬라이드 입력 (왼쪽 컨트롤 키)
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(Slide());
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
    }

    IEnumerator Slide()
    {
        // 슬라이드 애니메이션 시작
        animator.SetTrigger("Slide");

        // 콜라이더를 슬라이드용으로 변경
        capsuleCollider.height = slideColliderHeight;
        capsuleCollider.center = slideColliderCenter;

        // 지정된 시간(slideDuration)만큼 기다림
        yield return new WaitForSeconds(slideDuration);

        // 콜라이더를 원래대로 복구
        capsuleCollider.height = originalColliderHeight;
        capsuleCollider.center = originalColliderCenter;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Item 태그로 충돌 판정
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