using Caliburn.Micro;

namespace Kiosk.ViewModels
{
    public class BaseViewModel: Screen
    {
        public bool CanExecuteCommand(object parameter)
        {
            return true;
        }
    }
}
