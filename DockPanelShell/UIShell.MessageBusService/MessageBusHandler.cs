using System;

namespace UIShell.MessageBusService
{
    /// <summary>
    /// ��Ϣ��������
    /// </summary>
    /// <param name="sender">��Ϣ���С�</param>
    /// <param name="message">��Ϣ��</param>
    [Serializable]
    public delegate void MessageBusHandler(object sender, object message);
}