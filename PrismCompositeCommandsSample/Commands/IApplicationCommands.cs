using Prism.Commands;

namespace PrismCompositeCommandsSample.Commands
{
    public interface IApplicationCommands
    {
        CompositeCommand GetCurrentAllTimeCommand { get; }
    }
}