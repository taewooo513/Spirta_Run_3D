using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Item : MonoBehaviour // 여기서 버프사용시 함수만 
{
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
        GameManager.Instance.AddScore((int)val);
    }

    public void GetTrophyItem()
    {

    }
}
