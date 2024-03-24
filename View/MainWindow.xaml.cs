﻿using Prism.Commands;
using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.ViewModel;
using System.ComponentModel;
using System.Windows;


namespace SatisfactoryProductionManager.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CloseWindowButton.Command = new DelegateCommand(Close);
            Closing += SaveBeforeClosing;
            Loaded += MainWindow_Loaded;

            var context = DataContext as MainWindowVM;
            context.PropertyChanged += Context_ProductionBlockChanged;
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
    }
}
