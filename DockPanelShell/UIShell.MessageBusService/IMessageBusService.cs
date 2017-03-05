using System;

namespace UIShell.MessageBusService
{
    /// <summary>
    /// 消息总线服务。
    /// </summary>
    public interface IMessageBusService : IDisposable
    {
        /// <summary>
        /// 发布一条消息。该消息对应的消息主题为TMessage类型全名称。
        /// </summary>
        /// <typeparam name="TMessage">消息类型。</typeparam>
        /// <param name="message">消息对象。</param>
        void Publish<TMessage>(TMessage message);

        void Publish(Type messageType, object message);

        /// <summary>
        /// 订阅指定类型的消息。
        /// </summary>
        /// <typeparam name="TMessage">消息类型。相应的消息主题为TMessage类型全名称。</typeparam>
        /// <param name="handler">消息处理句柄。</param>
        void Subscribe<TMessage>(MessageBusHandler handler);

        void Subscribe(Type messageType, MessageBusHandler handler);

        /// <summary>
        /// 注销一个消息处理句柄。
        /// </summary>
        /// <typeparam name="TMessage">消息类型。相应的消息主题为TMessage类型全名称。</typeparam>
        /// <param name="handler">消息句柄。</param>
        void Unsubscribe<TMessage>(MessageBusHandler handler);

        void Unsubscribe(Type messageType, MessageBusHandler handler);


        /// <summary>
        /// 注销指定类型消息的所有处理句柄。
        /// </summary>
        /// <typeparam name="TMessage">消息类型。</typeparam>
        void Unsubscribe<TMessage>();

        void Unsubscribe(Type messageType);
    }
}