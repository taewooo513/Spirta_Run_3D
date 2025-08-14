using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;

    void Start()
    {
        // ���� ī�޶��� ��ġ�� Ÿ���� ��ġ�� ���� �Ÿ� ���̸� ����ϰ� ����.
        offset = transform.position - target.position;
    }

    // ĳ���Ͱ� ���� �����̰�, �� ������ ī�޶� ���󰡱����� LateUpdate.
    void LateUpdate()
    {
        // ī�޶��� ��ġ�� (Ÿ���� ���� ��ġ + ó���� ����� �Ÿ� ����)�� ��� ������Ʈ.
        transform.position = target.position + offset;
    }
}