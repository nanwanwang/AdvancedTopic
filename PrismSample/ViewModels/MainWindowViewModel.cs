using System;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Prism.Commands;
using Prism.Mvvm;
using PrismSample.Sevices;

namespace PrismSample.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {

        private string _foo = "122";

        public string Foo
        {
            get => _foo;
            set => SetProperty(ref _foo, value);
        }
        
        private bool _isCanExecute;

        public bool IsCanExecute
        {
            get => _isCanExecute;
            set
            {
                SetProperty(ref _isCanExecute, value);
                GetCurrentTimeCommand.RaiseCanExecuteChanged();
            }
        }

        private string _currentTime;

        public string CurrentTime
        {
            get => _currentTime;
            set => SetProperty(ref _currentTime, value);
        }

        private DelegateCommand<object> _getCurrentTimeCommand;
        public DelegateCommand<object> GetCurrentTimeCommand =>
            _getCurrentTimeCommand ?? (_getCurrentTimeCommand = new DelegateCommand<object>(ExecuteGetCurrentTimeCommand,CanExecuteGetCurrentTimeCommand));
        
        // 简洁的写法
        // public DelegateCommand GetCurrentTimeCommand =>
        //     _getCurrentTimeCommand ?? (_getCurrentTimeCommand = new DelegateCommand(ExecuteGetCurrentTimeCommand).ObservesCanExecute(()=>IsCanExecute));

        private DelegateCommand<object> _textChangedCommand;

        public DelegateCommand<object> TextChangedCommand => _textChangedCommand ??
                                                             (_textChangedCommand =
                                                                 new DelegateCommand<object>(
                                                                     ExecuteTextChangedCommand));


        private DelegateCommand _asyncCommand;
        public DelegateCommand AsyncCommand => _asyncCommand ?? (_asyncCommand = new DelegateCommand(async ()=>await  ExampleMethodAsync()));

        async Task ExampleMethodAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
                this.CurrentTime = "hello prism!";
            });
        }
        
        void ExecuteGetCurrentTimeCommand(object parameter)
        {
            this.CurrentTime = ((Button)parameter)?.Name+ DateTime.Now.ToString();
        }

        bool CanExecuteGetCurrentTimeCommand(object parameter)
        {
            return IsCanExecute;
        }

        void ExecuteTextChangedCommand(object parameter)
        {
            this.CurrentTime = Foo + ((TextBox)parameter)?.Name;
        }
    }
}