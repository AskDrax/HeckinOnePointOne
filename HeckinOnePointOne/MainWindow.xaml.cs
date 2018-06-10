using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Globalization;
using System.Diagnostics;

namespace HeckinOnePointOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PowerCollection thePowerCollection = new PowerCollection();

        private Stopwatch timer;

        public string TimerText;

        private HorizontalAlignment powerAlignment;

        public HorizontalAlignment PowerAlignment
        {
            get { return powerAlignment; }
            set
            {
                powerAlignment = value;
                NotifyStaticPropertyChanged("PowerAlignment");
            }
        }

        public PowerCollection ThePowerCollection
        {
            get { return thePowerCollection; }
            set
            {
                thePowerCollection = value;
                NotifyStaticPropertyChanged("ThePowerCollection");      
            }
        }

        public MainWindow()
        {
            timer = new Stopwatch();
            InitializeComponent();
        }

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        public static void InitEventHandler()
        {
            //StaticPropertyChanged += OnStaticPropertyChanged;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PowersListBox.ItemsSource = ThePowerCollection;
            //TextBlock tb = (TextBlock)PowersListBox.FindName("PowersListText");
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            thePowerCollection.Clear();
            CalculatedTimerText.Text = "Calculating 1.1^0 to 1.1^" + NumberOfPowersInputBox.Text + "...";

            timer.Reset();
            timer.Start();

            thePowerCollection.CalculatePowers(Convert.ToInt32(NumberOfPowersInputBox.Text));

            timer.Stop();

            double elapsed = timer.Elapsed.TotalMilliseconds;
            string elapsedString;

            if (elapsed >= 1000)
            {
                elapsedString = (elapsed / 1000).ToString() + " Seconds.";
            }
            else
            {
                elapsedString = elapsed.ToString() + " Milliseconds.";
            }

            CalculatedTimerText.Text = ("Calculated  [ 1.1^0 to 1.1^" + thePowerCollection.Last().PowerOf.ToString() + " ]  in " + elapsedString);
        }

        private void NumberOfPowersInputBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int decimalNumber;

            e.Handled = !int.TryParse(e.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out decimalNumber);
        }

        private void LButton_Click(object sender, RoutedEventArgs e)
        {
            //PowerAlignment = HorizontalAlignment.Left;
            PowersListBox.HorizontalContentAlignment = HorizontalAlignment.Left;
        }

        private void RButton_Click(object sender, RoutedEventArgs e)
        {
            //PowerAlignment = HorizontalAlignment.Right;
            PowersListBox.HorizontalContentAlignment = HorizontalAlignment.Right;
        }
    }
}
