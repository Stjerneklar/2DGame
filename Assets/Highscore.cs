using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class Highscore : MonoBehaviour
{
    public int score = 0;
    public CoinCollisions cc;

    void Start()
    {
        cc = GetComponent<CoinCollisions>();
        Messages.Instance.Subscribe<MessageScore>(x => cc.Scorelogic(x.score));
    }
}
