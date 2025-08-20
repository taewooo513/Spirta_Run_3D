using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Item : MonoBehaviour // 여기서 버프사용시 함수만 
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
        Debug.Log("코인 획득: " + val);
        Destroy(gameObject);
    }

    public void GetTrophyItem()
    {

    }
}
