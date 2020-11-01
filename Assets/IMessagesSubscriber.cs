using System;
using Utility.TinyMessenger;

namespace Assets.Code
{
    public interface IMessagesSubscriber
    {
        void ClearSubscriptions();
        void Publish<T>(T item) where T : class, ITinyMessage;
        TinyMessageSubscriptionToken Subscribe<T>(Action<T> action) where T : class, ITinyMessage;
    }

}