using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PlayerStats : MonoBehaviour
{
    public string Stat;
    public bool Increment;
    public CoinCollisions cc;

    void Start()
    {
        cc = GetComponent<CoinCollisions>();
        Messages.Instance.Subscribe<PlayerStat>(x => cc.Statlogic(x.Stat, x.Increment));
    }
}
