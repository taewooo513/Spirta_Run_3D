using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Item : MonoBehaviour // ���⼭ �������� �Լ��� 
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
