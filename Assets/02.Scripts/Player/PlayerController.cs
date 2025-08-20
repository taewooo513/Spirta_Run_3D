using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    public GameObject jumpEffectPrefab;
    public GameObject slideEffectPrefab;

    // ���� ��
    public float forwardSpeed = 8f;
    public float sideSpeed = 5f;
    public float jumpForce = 8f;

    // �����̵� �� ����� �ݶ��̴� ��
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    public float slideColliderHeight = 0.9f;
    public Vector3 slideColliderCenter = new Vector3(0, 0.45f, 0);
    public float slideDuration = 1.0f;

    //�����̵� �� ���� ���� ���� isSliding
    private bool isSliding = false;


    void Start()
    {
        // ������ �� �� ������Ʈ�� �ڵ����� ã�ƿͼ� ������ �Ҵ�
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // ���� ĸ�� �ݶ��̴��� ���̿� �߽� ��ġ�� ����
        originalColliderHeight = capsuleCollider.height;
        originalColliderCenter = capsuleCollider.center;

        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Slide");
    }

    void Update()
    {
        // �׻� ������ �̵�
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // �¿� �̵�
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * sideSpeed * Time.deltaTime);

        // ���� �Է� (�����̽� ��)
        if (Input.GetKeyDown(KeyCode.Space) && !isSliding)
        {
            Jump();
        }

        // �����̵� �Է� (���� ��Ʈ�� Ű)
        // Ű �Է� �ð����� ���ӵǾ�ߵǱ⶧���� �ڷ�ƾ�� ��� �ؾߵɰŰ���.
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

        // �����̵� ���� ������ �ִϸ��̼ǰ� ����Ʈ�� ���ÿ� ����
        animator.SetTrigger("Slide");
        if (slideEffectPrefab != null)
        {
            slideEffectInstance = Instantiate(slideEffectPrefab, transform.position, Quaternion.identity);
        }

        // �����̵������ �ݶ��̴� ����
        capsuleCollider.height = slideColliderHeight;
        capsuleCollider.center = slideColliderCenter;

        // ������ �ð���ŭ �����̵� ���¸� ����
        yield return new WaitForSeconds(slideDuration);

        // �����̵尡 ������ �ݶ��̴��� ������� ����
        capsuleCollider.height = originalColliderHeight;
        capsuleCollider.center = originalColliderCenter;

        // �����̵尡 ������ ������ ����Ʈ�� �Բ� �ı�
        if (slideEffectInstance != null)
        {
            Destroy(slideEffectInstance);
        }

        isSliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Item �±׷� �浹 ����
        if (other.CompareTag("Item")) 
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                //BuffManager.Instance.ApplySpeedBoost(item.val, item.maxTime);
                Debug.Log("������ ȹ��: " + item.name); 
                item.GetItem();
                //Debug.Log("���ǵ�� ������ ȹ��");
            }
        }
    }
}