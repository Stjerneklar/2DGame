using Assets.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utility
{
    public class Singleton<T> : MessagesSubscriber where T : new()
    {
        private static T _instance;
        
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;        
            }

            set
            {
                if (_instance != null)
                {
                    throw new NotImplementedException();
                }

                _instance = value;
            }
        }

    }
}
