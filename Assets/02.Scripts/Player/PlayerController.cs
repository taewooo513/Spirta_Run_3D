using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // �����̵� �Է� (���� ��Ʈ�� Ű)
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
        // �����̵� �ִϸ��̼� ����
        animator.SetTrigger("Slide");

        // �ݶ��̴��� �����̵������ ����
        capsuleCollider.height = slideColliderHeight;
        capsuleCollider.center = slideColliderCenter;

        // ������ �ð�(slideDuration)��ŭ ��ٸ�
        yield return new WaitForSeconds(slideDuration);

        // �ݶ��̴��� ������� ����
        capsuleCollider.height = originalColliderHeight;
        capsuleCollider.center = originalColliderCenter;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Item �±׷� �浹 ����
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