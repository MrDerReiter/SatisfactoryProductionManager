using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace SatisfactoryProductionManager;

public partial class MainWindow : Window
{
    private MainWindowVM Context => (MainWindowVM)DataContext;


    public MainWindow()
    {
        InitializeComponent();
    }


    private void OnUserPressedClose(object sender, RoutedEventArgs args)
    {
        Context.SaveFactoryCommand.Execute(null);
        Close();
    }

    private void OnHeaderDragMove(object sender, MouseEventArgs args)
    {
        if (args.LeftButton == MouseButtonState.Pressed) DragMove();
    }

    private void OnOverclockMouseWheel(object sender, MouseWheelEventArgs args)
    {
        args.Handled = true;

        var unitVM = (sender as TextBox)?.DataContext as ProductionUnitVM;
        if (unitVM is null) return;

        var command = args.Delta > 0 ?
            unitVM.IncreaseOverclockCommand :
            unitVM.DecreaseOverclockCommand;

        command.Execute(10);
    }
}
