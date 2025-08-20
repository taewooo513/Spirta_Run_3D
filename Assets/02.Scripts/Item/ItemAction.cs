using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Item : MonoBehaviour // ���⼭ �������� �Լ��� 
{
    [SerializeField]
    public void GetSpeedItem()
    {
        GameManager.Instance.GetSpeedUpItem(val, maxTime);
    }

    public void GetInvItem()
    {
        GameManager.Instance.GetInvItem(maxTime);
    }

    public void GetCoinItem()
    {
        //AudioManager.Instance.PlayCoinSound();
        GameManager.Instance.score.AddScore((int)val);
        Debug.Log("���� ȹ��: " + val);
        Destroy(gameObject);
    }

    public void GetTrophyItem()
    {

    }
}
