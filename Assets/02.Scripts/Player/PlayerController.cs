using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ĳ������ ���� �ӵ�
    public float forwardSpeed = 8f;
    // ĳ������ �¿� �̵� �ӵ�
    public float sideSpeed = 5f;
    void Update()
    {
        // Time.deltaTime�� �����༭ ��ǻ�� ���ɰ� ������� ������ �ӵ��� ����
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // �¿� Ű���� �Է�(A, D �Ǵ� ��, ��)�� �޴´�.
        float horizontalInput = Input.GetAxis("Horizontal");

        // �¿� �Է� ���� ���� ĳ���͸� ������ �̵���Ų��.
        transform.Translate(Vector3.right * horizontalInput * sideSpeed * Time.deltaTime);
    }
}