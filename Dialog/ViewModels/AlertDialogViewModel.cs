using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace Dialog.ViewModels
{
    public class AlertDialogViewModel:BindableBase,IDialogAware
    {
        private DelegateCommand<string> _closeDialogCommand;

        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(ExecuteCloseDialogCommand));


        void ExecuteCloseDialogCommand(string parameter)
        {
            ButtonResult result = ButtonResult.None;
            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.Yes;
            }
            else if (parameter?.ToLower() == "false")
            {
                result = ButtonResult.No;
            }
            RaiseRequestClose(new DialogResult(result));
        }
        
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
             RequestClose?.Invoke(dialogResult);
        }

        private string _message;

        public string Messgae
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private string _title = "Notification";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
           
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Messgae = parameters.GetValue<string>("message");
        }


        public event Action<IDialogResult>? RequestClose;
    }
}