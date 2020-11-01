using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Utility;
using Utility.TinyMessenger;

namespace Assets.Code
{
    public class MessagesSubscriber : IMessagesSubscriber
    {
        [JsonIgnore]
        public List<TinyMessageSubscriptionToken> messageTokens = new List<TinyMessageSubscriptionToken>();

        public TinyMessageSubscriptionToken Subscribe<T>(Action<T> action) where T : class, ITinyMessage
        {
            var token = Messages.Instance.Subscribe<T>(action);
            messageTokens.Add(token);
            return token;
        }

        public bool UnsubscribeToken(TinyMessageSubscriptionToken token)
        {
            if (messageTokens.Contains(token))
            {
                Messages.Instance.Unsubscribe(token);
                messageTokens.Remove(token);
                return true;
            }

            return false;
        }

        public TinyMessageSubscriptionToken SubscribeWithToken<T>(Action<T> action) where T : class, ITinyMessage
        {
            var token = Messages.Instance.Subscribe<T>(action);
            messageTokens.Add(token);
            return token;
        }

        public void Unsubscribe() => messageTokens.ForEach(Messages.Instance.Unsubscribe);
        public void Unsubscribe(ref TinyMessageSubscriptionToken token)
        {
            Messages.Instance.Unsubscribe(token);
            messageTokens.Remove(token);
            token = null;
        }

        public void Publish<T>(T item) where T : class, ITinyMessage => Messages.Instance.Publish(item);

        public void ClearSubscriptions() => messageTokens.ForEach(x => Messages.Instance.Unsubscribe(x));
    }
}
