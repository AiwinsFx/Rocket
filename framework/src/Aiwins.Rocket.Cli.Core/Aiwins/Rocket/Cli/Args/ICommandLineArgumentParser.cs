namespace Aiwins.Rocket.Cli.Args
{
    public interface ICommandLineArgumentParser
    {
        CommandLineArgs Parse(string[] args);
    }
}