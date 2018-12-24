
public class CommandMessage
{
    public CommandType Command { get; private set; }

    public bool Silent { get; private set; }

    public CommandMessage(CommandType cmd, bool silent = true)
    {
        Command = cmd;
        Silent = silent;
    }
}
