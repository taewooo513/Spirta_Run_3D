using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 인스펙터 창에서 3개의 하트 이미지를 연결할 배열
    public Image[] heartImages;
    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].enabled = true; // 하트 이미지를 켠다
            }
            // 그렇지 않다면 (잃어버린 체력에 해당하는 하트라면)
            else
            {
                heartImages[i].enabled = false; // 하트 이미지를 끈다
            }
        }
    }
}