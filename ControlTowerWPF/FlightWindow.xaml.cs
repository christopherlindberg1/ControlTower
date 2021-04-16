using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using AppFeatures;

namespace ControlTowerWPF
{
    /// <summary>
    /// Interaction logic for FlightWindow.xaml
    /// </summary>
    public partial class FlightWindow : Window
    {
        private string _flightCode;

        public delegate void TakeOffEventHandler(object source, TakeOffEventArgs e);
        public event TakeOffEventHandler TakenOff;

        public delegate void ChangeRouteEventHandler(object source, ChangeRouteEventArgs eventArgs);
        public event ChangeRouteEventHandler RouteChanged;

        public delegate void LandEventHandler(object source, LandEventArgs e);
        public event LandEventHandler Landed;





        // ===================== Properties ===================== //

        public string FlightCode
        {
            get => _flightCode;

            set => _flightCode = value;
        }






        // ===================== Methods ===================== //

        public FlightWindow()
            : this(null)
        {
        }

        public FlightWindow(string flightCode)
        {
            InitializeComponent();

            FlightCode = flightCode;

            InitializeWindow();
        }

        /// <summary>
        /// Initializes the window.
        /// </summary>
        private void InitializeWindow()
        {
            InitializeGUI();
        }

        /// <summary>
        /// Initializes the GUI
        /// </summary>
        private void InitializeGUI()
        {
            SetAirlineImage();
            this.Title = $"Flight { FlightCode }";
            btnStartFlight.IsEnabled = true;
            comboBoxChangeRoute.IsEnabled = false;
            btnLand.IsEnabled = false;
            InitializeChangeRouteComboBox();
        }

        /// <summary>
        /// Sets the image source for the airplane to an image that corresponds to the flight code.
        /// </summary>
        private void SetAirlineImage()
        {
            if (String.IsNullOrWhiteSpace(FlightCode))
            {
                throw new InvalidOperationException(
                    "The flight code must be initialized before calling this method.");
            }

            Uri pathToImage = new Uri("/Images/Airline-unknown.jpg", UriKind.Relative);

            if (FlightCode.ToLower().Contains("dlh"))
            {
                pathToImage = new Uri("/Images/Airline-dlh.jpg", UriKind.Relative);
            }
            else if (FlightCode.ToLower().Contains("rys"))
            {
                pathToImage = new Uri("/Images/Airline-rys.jpg", UriKind.Relative);
            }
            else if (FlightCode.ToLower().Contains("sas"))
            {
                pathToImage = new Uri("/Images/Airline-sas.jpg", UriKind.Relative);
            }

            imageAirline.Source = new BitmapImage(pathToImage);
        }

        /// <summary>
        /// Initializes the ComboBox for route changes with values from 0 deg to 350 deg,
        /// with 10 degrees between each step.
        /// </summary>
        private void InitializeChangeRouteComboBox()
        {
            comboBoxChangeRoute.Items.Add("");

            for (int i = 0; i < 351; i += 10)
            {
                comboBoxChangeRoute.Items.Add($"Heading { i } deg");
            }
        }

        /// <summary>
        /// Updates the window to show that the airplane has taken off, and then triggers
        /// the TakenOff event (if there are any subscribers).
        /// </summary>
        private void TakeOff()
        {
            btnStartFlight.IsEnabled = false;
            comboBoxChangeRoute.IsEnabled = true;
            btnLand.IsEnabled = true;

            OnTakenOff();
        }


        protected virtual void OnTakenOff()
        {
            if (TakenOff != null)
            {
                TakenOff(this, new TakeOffEventArgs() { FlightCode = FlightCode });
            }
        }

        private void ChangeRoute()
        {
            if (String.IsNullOrWhiteSpace(comboBoxChangeRoute.SelectedItem.ToString()))
            {
                return;
            }

            OnChangedRoute();
        }

        protected virtual void OnChangedRoute()
        {
            if (RouteChanged != null)
            {
                RouteChanged(this, new ChangeRouteEventArgs()
                {
                    FlightCode = FlightCode,
                    Route = comboBoxChangeRoute.SelectedItem.ToString(),
                });
            }
        }

        private void Land()
        {
            btnStartFlight.IsEnabled = false;
            comboBoxChangeRoute.IsEnabled = false;
            btnLand.IsEnabled = false;

            OnLanded();
        }

        protected virtual void OnLanded()
        {
            if (Landed != null)
            {
                Landed(this, new LandEventArgs() { FlightCode = FlightCode });
            }
        }





        // ===================== Events ===================== //

        private void btnStartFlight_Click(object sender, RoutedEventArgs e)
        {
            TakeOff();
        }

        private void comboBoxChangeRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeRoute();
        }

        private void btnLand_Click(object source, RoutedEventArgs e)
        {
            Land();
        }
    }
}
