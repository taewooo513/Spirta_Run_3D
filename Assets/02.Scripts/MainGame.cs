using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    // Start is called before the first frame update 구조를 잘못짜서 세팅여기서해주겠습니다
    void Start()
    {
        PatternManager.Instance.StartGame();
        GameManager.Instance.GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.UpdateDistance();
        GameManager.Instance.UpdateTime();
    }
}
