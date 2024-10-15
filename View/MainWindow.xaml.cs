using FactoryManagementCore.Production;
using Prism.Commands;
using SatisfactoryProductionManager.ViewModel;
using SatisfactoryProductionManager.ViewModel.ProductionModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;


namespace SatisfactoryProductionManager.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CloseWindowButton.Command = new DelegateCommand(Close);
            MouseMove += MainWindow_MouseMove;
            Loaded += MainWindow_Loaded;
            Closing += SaveBeforeClosing;

            var context = DataContext as MainWindowVM;
            context.PropertyChanged += Context_ProductionBlockChanged;
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs args)
        {
            if (args.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void ScrollViewerBar_MouseMove(object sender, MouseEventArgs args)
        {
            args.Handled = true;
        }

        private void RequestButtonShowHideTrigger(MainWindowVM context)
        {
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DataContext as MainWindowVM;
            RequestButtonShowHideTrigger(context);
        }

        private void Context_ProductionBlockChanged(object sender, PropertyChangedEventArgs args)
        {
            var context = sender as MainWindowVM;
            RequestButtonShowHideTrigger(context);
        }

        private void SaveBeforeClosing(object sender, CancelEventArgs e)
        {
            ProductionManager.SaveFactory();
        }

        private void OverclockMouseWheelControl(object sender, MouseWheelEventArgs args)
        {
            args.Handled = true;
            var context = (sender as TextBox).DataContext as ProductionUnitVM;
            var command = args.Delta > 0 ? context.IncreaseOverclock : context.DecreaseOverclock;
            command.Execute(10);
        }
    }
}
