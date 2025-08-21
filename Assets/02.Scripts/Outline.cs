using TMPro;
using UnityEngine;

namespace _02.Scripts
{
    public class Outline : MonoBehaviour
    {
        public TextMeshProUGUI tmpText;

        void Start()
        {
            // �ؽ�Ʈ�� Outline ����
            tmpText.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 2f);
            tmpText.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, Color.black);
        }
    }
}

