namespace UIShell.PresentationCore
{
    public class CommandContact
    {
        public string CommandName { get; set; }
    }

    public class CommandStatusContact : CommandContact
    {
        public CommandStatus Status { get; set; }
    }
}