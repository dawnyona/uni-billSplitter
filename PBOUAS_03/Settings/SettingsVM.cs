using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;


namespace PBOUAS_03
{
    public class SettingsVM : INotifyPropertyChanged
    {
        // Properties
        public CommandHandler CloseButton { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        // Observable
        private static bool _isDark;
        public bool IsDark
        {
            get { return _isDark; }
            set
            {
                if (this.MutateVerbose(ref _isDark, value, e => PropertyChanged?.Invoke(this, e)))
                {
                    ApplyBase(value);
                    MainWindow.Frame.Content = new homePage();
                }
            }
        }

        // Constructor
        public SettingsVM()
        {
            CloseButton = new CommandHandler(closeWindow);

        }

        // Methods
        private void ApplyBase(bool isDark)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }

        private void closeWindow()
        {
            MainWindow.SettingsWin.Close();

        }

    }
}
