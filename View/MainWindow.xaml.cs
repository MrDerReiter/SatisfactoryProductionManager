using Prism.Commands;
using SatisfactoryProductionManager.ViewModel;
using System.ComponentModel;
using System.Windows;


namespace SatisfactoryProductionManager.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CloseWindowButton.Command = new DelegateCommand(Close);

            var context = DataContext as MainWindowVM;
            context.PropertyChanged += Context_ProductionBlockChanged;
        }

        private void Context_ProductionBlockChanged(object sender, PropertyChangedEventArgs args)
        {
            var context = sender as MainWindowVM;

            if (context.ActiveBlock == null)
            {
                ProductionRequestButton.Visibility = Visibility.Hidden;
                OutputLabel.Visibility = Visibility.Hidden;
                InputLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                ProductionRequestButton.Visibility = Visibility.Visible;
                OutputLabel.Visibility = Visibility.Visible;
                InputLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
