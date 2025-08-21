using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Item : MonoBehaviour // ���⼭ �������� �Լ��� 
{
    [SerializeField]
    public void GetSpeedItem()
    {
        GameManager.Instance.GetSpeedUpItem(val, maxTime);
        Destroy(gameObject);
    }

    public void GetInvItem()
    {
        GameManager.Instance.GetInvItem(maxTime, val);
        Destroy(gameObject);
    }

    public void GetCoinItem()
    {
        AudioManager.Instance.PlayCoinSound();
        GameManager.Instance.score.AddScore((int)val);
        Destroy(gameObject);
    }

    public void GetHealItem()
    {
        CharacterManager.Instance.player.AddHeal();
    }

    public void GetTrophyItem()
    {

    }
}
