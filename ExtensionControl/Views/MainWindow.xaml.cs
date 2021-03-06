﻿using System.Windows;
using Prism.Ioc;
using Prism.Events;
using Prism.Regions;
using MahApps.Metro.Controls;
using UsbHIDControl.Views;

namespace ExtensionControl.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly IContainerExtension _container;
        private readonly IRegionManager _regionManager;

        private readonly IEventAggregator _ea;
        public MainWindow(IContainerExtension container, IRegionManager regionManager, IEventAggregator ea)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;
            _ea = ea;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _regionManager.Regions["UsbHIDControlRegion"].Add(_container.Resolve<UsbHID>());
        }
    }
}
