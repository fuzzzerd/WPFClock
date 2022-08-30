using System;
using System.Windows;
using System.ComponentModel;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace WPFClock;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public String _TheCurrentTimeString;
    public String TheCurrentTimeString
    {
        get { return _TheCurrentTimeString; }
        set { _TheCurrentTimeString = value; OnPropertyChanged(nameof(TheCurrentTimeString)); }
    }

    public String AppTitle 
    {
        get
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var versionInfo = FileVersionInfo.GetVersionInfo(assemblyLocation);
            var productVersion = versionInfo.ProductVersion;

            return $"{Environment.MachineName} ({productVersion})";
        }
    }

    Timer tmr;
    public MainWindow()
    {
        InitializeComponent();

        this.DataContext = this;

        tmr = new Timer(new TimerCallback((x) =>
        {
            TheCurrentTimeString = DateTime.Now.ToLongTimeString();
        }));

        tmr.Change(0, 500);
    }


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    /// <summary>
    /// Raises this object's PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The property that has a new value.</param>
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChangedEventHandler handler = this.PropertyChanged;
        if (handler != null)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }
    }
    #endregion
}