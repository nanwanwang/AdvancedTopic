using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using PrismCompositeCommandsSample.Commands;

namespace PrismCompositeCommandsSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApplicationCommands, ApplicationCommands>();
        }

        protected override Window CreateShell()
        {
            throw new NotImplementedException();
        }
    }
}