using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // �ν����� â���� 3���� ��Ʈ �̹����� ������ �迭
    public Image[] heartImages;
    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].enabled = true; // ��Ʈ �̹����� �Ҵ�
            }
            // �׷��� �ʴٸ� (�Ҿ���� ü�¿� �ش��ϴ� ��Ʈ���)
            else
            {
                heartImages[i].enabled = false; // ��Ʈ �̹����� ����
            }
        }
    }
}