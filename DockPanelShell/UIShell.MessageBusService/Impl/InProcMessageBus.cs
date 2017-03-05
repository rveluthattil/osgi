using System;
using UIShell.OSGi.Collection;

namespace UIShell.MessageBusService.Impl
{
    /// <summary>
    /// 进程内通信的message bus的简单实现，本对象是线程安全的。
    /// </summary>
    internal class InProcMessageBus : IMessageBusService
    {
        private readonly ThreadSafeDictionary<Type, ThreadSafeList<MessageBusHandler>> _container =
            new ThreadSafeDictionary<Type, ThreadSafeList<MessageBusHandler>>();

        #region IMessageBusService 成员

        public void Publish<TMessage>(TMessage message)
        {
            Publish(typeof (TMessage), message);
        }

        public void Publish(Type messageType, object message)
        {
            _container.Lock(value =>
                                {
                                    ThreadSafeList<MessageBusHandler> handlers;
                                    if (value.TryGetValue(messageType, out handlers))
                                    {
                                        foreach (MessageBusHandler item in handlers.ToArray())
                                        {
                                            item(this, message);
                                        }
                                    }
                                });
        }

        public void Subscribe<TMessage>(MessageBusHandler handler)
        {
            Subscribe(typeof (TMessage), handler);
        }

        public void Subscribe(Type messageType, MessageBusHandler handler)
        {
            _container.Lock(value =>
                                {
                                    ThreadSafeList<MessageBusHandler> handlers;
                                    if (value.TryGetValue(messageType, out handlers))
                                    {
                                        handlers.Add(handler);
                                    }
                                    else
                                    {
                                        handlers = new ThreadSafeList<MessageBusHandler>();
                                        value.Add(messageType, handlers);
                                        handlers.Add(handler);
                                    }
                                });
        }

        public void Unsubscribe<TMessage>(MessageBusHandler handler)
        {
            Unsubscribe(typeof (TMessage), handler);
        }

        public void Unsubscribe(Type messageType, MessageBusHandler handler)
        {
            _container.Lock(value =>
                                {
                                    ThreadSafeList<MessageBusHandler> handlers;
                                    if (value.TryGetValue(messageType, out handlers))
                                    {
                                        handlers.Remove(handler);
                                    }
                                });
        }

        public void Unsubscribe<TMessage>()
        {
            Unsubscribe(typeof (TMessage));
        }

        public void Unsubscribe(Type messageType)
        {
            _container.Remove(messageType);
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _container.Clear();
        }

        #endregion
    }
}