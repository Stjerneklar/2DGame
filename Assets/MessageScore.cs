using Assets.Code.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    class MessageScore : TinyMessage
    {
        public int score = 0;

        public MessageScore(int thescore)
        {
            score = thescore;
        }
    }
}