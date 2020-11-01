using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.TinyMessenger;

namespace Assets.Code.Utility
{
    class TinyMessage : ITinyMessage
    {
        public object Sender => throw new NotImplementedException();
    }

    class TinyInfoMessage<T> : TinyMessage 
    {
        public static TinyInfoMessage<T> Get(T data) {
            return new TinyInfoMessage<T>()
            {
                info = data
            };
        }
        public T info;
    }

    class TinyTupleMessage<T, T2> : TinyMessage
    {
        public T header;
        public T2 info;
    }
}
