using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Dialog.ViewModels;
using Dialog.Views;
using Prism.Ioc;
using Prism.Unity;

namespace Dialog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
           containerRegistry.RegisterDialog<AlertDialog,AlertDialogViewModel>();
        }

        protected override Window CreateShell()
        {
            throw new NotImplementedException();
        }
    }
}