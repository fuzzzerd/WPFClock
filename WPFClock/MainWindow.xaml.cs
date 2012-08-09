using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;

namespace WPFClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public String _TheCurrenTimeString;
        public String TheCurrenTimeString
        {
            get { return _TheCurrenTimeString; }
            set { _TheCurrenTimeString = value; OnPropertyChanged("TheCurrenTimeString"); }
        }

        Timer tmr;
        public MainWindow()
        {
            InitializeComponent();

            this.Title = "Time on " + Environment.MachineName;

            this.DataContext = this;

            tmr = new Timer(new TimerCallback((x) =>
            {
                TheCurrenTimeString = DateTime.Now.ToLongTimeString();
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
}
