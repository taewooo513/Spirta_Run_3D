using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ������Ʈ ����
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    // ����Ʈ ������
    public GameObject jumpEffectPrefab;
    public GameObject slideEffectPrefab;

    // ���� ��
    public float forwardSpeed = 8f;
    public float sideSpeed = 10f; // ���� �� ��ư ���� �ΰ���
    public float jumpForce = 8f;

    // �����̵� ��
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    public float slideColliderHeight = 0.9f;
    public Vector3 slideColliderCenter = new Vector3(0, 0.45f, 0);
    public float slideDuration = 1.0f;

    // ���� ����
    private bool isSliding = false;

    // �������� ������ ����
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public float minSwipeDistance = 50f;

    void Start()
    {
        // ������ �� �� ������Ʈ�� �ڵ����� ã�ƿͼ� ������ �Ҵ�
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // ���� ĸ�� �ݶ��̴��� ���̿� �߽� ��ġ�� ����
        originalColliderHeight = capsuleCollider.height;
        originalColliderCenter = capsuleCollider.center;

        // ���̷� ���� Ȱ��ȭ
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    void Update()
    {
        // --- ���� ��� ���� ---
        if (Settings.Instance.currentControlType == Settings.ControlType.Tilt)
        {
            // 1. ����(���̷�) ����
            if (Input.gyro.enabled)
            {
                float gyroInput = Input.gyro.gravity.x;
                Vector3 moveDirection = new Vector3(gyroInput, 0, 0);
                transform.Translate(moveDirection * sideSpeed * Time.deltaTime);
            }
        }
        else // 2. ��ư ����
        {
            // PC �׽�Ʈ�� Ű���� �¿� �̵�
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * sideSpeed * Time.deltaTime);
            // (���� ����Ͽ� ��/�� ��ư UI�� �ִٸ� ���⿡ ������ �߰��մϴ�)
        }

        // --- ���������� ����/�����̵� ���� ---
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

    // �������� ������ �����ϴ� �Լ�
    private void CheckSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;
        if (swipeDelta.magnitude < minSwipeDistance) return;

        if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
        {
            if (swipeDelta.y > 0 && !isSliding) // ���� ��������
            {
                Jump();
            }
            else if (swipeDelta.y < 0 && !isSliding) // �Ʒ��� ��������
            {
                StartCoroutine(Slide());
            }
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump"); // Animator Ʈ���� �̸� Ȯ��

        if (jumpEffectPrefab != null)
        {
            Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    IEnumerator Slide()
    {
        isSliding = true;
        GameObject slideEffectInstance = null;

        animator.SetTrigger("Slide"); // Animator Ʈ���� �̸� Ȯ��
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

    // ������ ȹ�� ����
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

    // ��ֹ� �浹 ����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // ���� ������ �������� ���� ���� ����
            if (Settings.Instance.IsVibrationEnabled())
            {
                Handheld.Vibrate();
            }
        }
    }
}
