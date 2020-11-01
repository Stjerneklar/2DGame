using Assets.Code.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    class PlayerStat : TinyMessage
    {
        public bool Increment;
        public string Stat;
        public PlayerStat(string stat, bool increment)
        {
            Increment = increment;
            Stat = stat;
        }
    }
}