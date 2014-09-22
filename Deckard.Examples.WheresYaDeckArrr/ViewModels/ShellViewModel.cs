namespace Deckard.Examples.WheresYaDeckArrr.ViewModels
{
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell 
    {
        private const string WindowTitleDefault = "Where's Ya Deck, Arrr?!";

        private string _windowTitle = WindowTitleDefault;

        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                NotifyOfPropertyChange(() => WindowTitle);
            }
        }
    }
}