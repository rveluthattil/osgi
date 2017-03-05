using System.Collections.Generic;
using System.Diagnostics;
using UIShell.MessageBusService;

namespace UIShell.PresentationCore.Impl
{
    internal class HandlerContainer<T> where T : CommandContact, new()
    {
        private readonly Dictionary<string, List<MessageBusHandler>> _handlers =
            new Dictionary<string, List<MessageBusHandler>>();

        public HandlerContainer(IMessageBusService messageBusService)
        {
            Debug.Assert(messageBusService != null);
            messageBusService.Subscribe<T>(OnCommandFired);
        }

        private void OnCommandFired(object sender, object args)
        {
            var contact = (T) args;
            List<MessageBusHandler> handlerList;
            if (!_handlers.TryGetValue(contact.CommandName, out handlerList))
            {
                return;
            }
            handlerList.ForEach(handler => handler(sender, contact));
        }

        public void SubscribeCommandEvent(string commandName, MessageBusHandler handler)
        {
            List<MessageBusHandler> handlerList;
            if (!_handlers.TryGetValue(commandName, out handlerList))
            {
                _handlers[commandName] = handlerList = new List<MessageBusHandler>();
            }
            handlerList.Add(handler);
        }
    }

    public class CommandBusService : ICommandBusService
    {
        private readonly HandlerContainer<CommandContact> _commandContainer;
        private readonly HandlerContainer<CommandStatusContact> _commandStatusContainer;
        private readonly IMessageBusService _messageBusService;

        public CommandBusService(IMessageBusService messageBusService)
        {
            Debug.Assert(messageBusService != null);
            _messageBusService = messageBusService;
            _commandContainer = new HandlerContainer<CommandContact>(messageBusService);
            _commandStatusContainer = new HandlerContainer<CommandStatusContact>(messageBusService);
        }

        #region ICommandBusService Members

        public void PublicCommand(string commandName)
        {
            _messageBusService.Publish(new CommandContact {CommandName = commandName});
        }

        public void SubscribeCommandEvent(string commandName, MessageBusHandler handler)
        {
            _commandContainer.SubscribeCommandEvent(commandName, handler);
        }

        public void PublicCommandStatus(string commandName, CommandStatus status)
        {
            _messageBusService.Publish(new CommandStatusContact {CommandName = commandName, Status = status});
        }

        public void SubscribeCommandStatusEvent(string commandName, MessageBusHandler handler)
        {
            _commandStatusContainer.SubscribeCommandEvent(commandName, handler);
        }

        #endregion
    }
}