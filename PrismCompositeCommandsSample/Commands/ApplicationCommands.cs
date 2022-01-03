using Prism.Commands;
using Prism.Regions;

namespace PrismCompositeCommandsSample.Commands
{
    public class ApplicationCommands:IApplicationCommands
    {
        private CompositeCommand _getCurrentAllTimeCommand = new CompositeCommand();
        public CompositeCommand GetCurrentAllTimeCommand
        {
            get => _getCurrentAllTimeCommand;
        }
    }
}