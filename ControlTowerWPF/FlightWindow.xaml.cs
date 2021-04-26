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
using AppFeatures.FlightActionsEventArgs;

namespace ControlTowerWPF
{
    /// <summary>
    /// Interaction logic for FlightWindow.xaml
    /// </summary>
    public partial class FlightWindow : Window
    {
        private string _flightCode;
        private readonly IAirlineImageGenerator _airlineImageGenerator;

        public delegate void TakeOffEventHandler(object source, TakeOffEventArgs e);
        public event TakeOffEventHandler TakenOff;

        public delegate void ChangeRouteEventHandler(object source, ChangeRouteEventArgs eventArgs);
        public event ChangeRouteEventHandler RouteChanged;

        public delegate void LandEventHandler(object source, LandEventArgs e);
        public event LandEventHandler Landed;




        // ===================== Properties ===================== //

        private string FlightCode { get => _flightCode; }

        private IAirlineImageGenerator AirlineImageGenerator { get => _airlineImageGenerator; }




        // ===================== Methods ===================== //

        public FlightWindow(string flightCode, IAirlineImageGenerator airlineImageGenerator)
        {
            InitializeComponent();

            _flightCode = flightCode;
            _airlineImageGenerator = airlineImageGenerator;

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
            SetAirlineImage(FlightCode);
            this.Title = $"Flight { FlightCode }";
            btnStartFlight.IsEnabled = true;
            comboBoxChangeRoute.IsEnabled = false;
            btnLand.IsEnabled = false;
            InitializeChangeRouteComboBox();
        }

        /// <summary>
        /// Sets the image source for the airplane to an image that corresponds to the flight code.
        /// </summary>
        private void SetAirlineImage(string flightCode)
        {
            if (String.IsNullOrWhiteSpace(flightCode))
            {
                throw new InvalidOperationException(
                    "The flight code must be initialized before calling this method.");
            }

            Uri pathToImage = AirlineImageGenerator.GetImageUri(flightCode);

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
        /// Updates the GUI to show that the airplane has taken off, and then calls
        /// the method that raise the TakenOff event.
        /// </summary>
        private void TakeOff()
        {
            btnStartFlight.IsEnabled = false;
            comboBoxChangeRoute.IsEnabled = true;
            btnLand.IsEnabled = true;

            OnTakenOff();
        }

        /// <summary>
        /// Raises the TakenOff event if there are any subscribers.
        /// </summary>
        protected virtual void OnTakenOff()
        {
            if (TakenOff != null)
            {
                TakenOff(this, new TakeOffEventArgs() { FlightCode = FlightCode });
            }
        }

        /// <summary>
        /// Updates the GUI as necessary when the airplane changes route, and then calls
        /// the method that raise the RouteChanged event.
        /// </summary>
        private void ChangeRoute()
        {
            if (String.IsNullOrWhiteSpace(comboBoxChangeRoute.SelectedItem.ToString()))
            {
                return;
            }

            OnChangedRoute();
        }

        /// <summary>
        /// Raises the RouteChanged event if there are any subscribers.
        /// </summary>
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

        /// <summary>
        /// Updates the the GUI as necessary when the airplane lands, and then calls
        /// the method that raise the Landed event.
        /// </summary>
        private void Land()
        {
            btnStartFlight.IsEnabled = false;
            comboBoxChangeRoute.IsEnabled = false;
            btnLand.IsEnabled = false;

            OnLanded();
        }

        /// <summary>
        /// Raises the Landed event if there are any subscribers.
        /// </summary>
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
