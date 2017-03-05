using System;

namespace UIShell.MessageBusService
{
    /// <summary>
    /// 消息处理句柄。
    /// </summary>
    /// <param name="sender">消息队列。</param>
    /// <param name="message">消息。</param>
    [Serializable]
    public delegate void MessageBusHandler(object sender, object message);
}