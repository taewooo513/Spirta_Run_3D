using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    // Start is called before the first frame update ������ �߸�¥�� ���ÿ��⼭���ְڽ��ϴ�
    public GameObject airPlain;
    void Start()
    {
        PatternManager.Instance.StartGame();
        GameManager.Instance.GameStart();
        Invoke("AirAdd", 10);
    }

    void AirAdd()
    {
        Instantiate(airPlain, new Vector3(0, 13, 0), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.UpdateDistance();
        GameManager.Instance.UpdateTime();
    }
}
