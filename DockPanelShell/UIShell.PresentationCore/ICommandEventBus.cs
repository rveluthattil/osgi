using UIShell.MessageBusService;

namespace UIShell.PresentationCore
{
    public interface ICommandBusService
    {
        void PublicCommand(string commandName);
        void PublicCommandStatus(string commandName, CommandStatus status);
        void SubscribeCommandEvent(string commandName, MessageBusHandler handler);
        void SubscribeCommandStatusEvent(string commandName, MessageBusHandler handler);
    }
}