using AppFeatures;
using System;
using System.Collections.Generic;
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
using DataAccess.Models;
using AppFeatures.FlightActionsEventArgs;

namespace ControlTowerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IFlightLogHandler _flightLogHandler;
        private readonly IFlightLogUtility _flightLogUtility;
        private readonly IAirlineImageGenerator _airlineImageGenerator;
        private readonly ErrorMessageHandler _errorMessageHandler = new ErrorMessageHandler();




        // ===================== Properties ===================== //

        public IFlightLogHandler FlightLogHandler { get => _flightLogHandler; }

        public IFlightLogUtility FlightLogUtility { get => _flightLogUtility; }

        public IAirlineImageGenerator AirlineImageGenerator { get => _airlineImageGenerator; }

        private ErrorMessageHandler ErrorMessageHandler { get => _errorMessageHandler; }

        public FlightLogWindow FlightLogWindow { get; set; }




        // ===================== Methods ===================== //

        public MainWindow(
            IFlightLogHandler flightLogHandler,
            IFlightLogUtility flightLogUtility,
            IAirlineImageGenerator airlineImageGenerator)
        {
            InitializeComponent();

            _flightLogHandler = flightLogHandler;
            _flightLogUtility = flightLogUtility;
            _airlineImageGenerator = airlineImageGenerator;
        }

        public bool ValidateInput()
        {
            return ValidateFlightCode(textBoxFlightCode.Text);
        }

        public bool ValidateFlightCode(string flightCode)
        {
            if (string.IsNullOrWhiteSpace(flightCode))
            {
                ErrorMessageHandler.AddMessage("The flight code cannot be empty");
                return false;
            }

            if (flightCode.Length > 10)
            {
                ErrorMessageHandler.AddMessage("The flight code cannot be longer than 10 characters.");
                return false;
            }

            return true;
        }

        private void ClearInputFields()
        {
            textBoxFlightCode.Text = "";
        }

        /// <summary>
        /// Sends airplane to runway and sets up subscribers to the airplane's events.
        /// </summary>
        private void SendAirplaneToRunway()
        {
            string flightCode = textBoxFlightCode.Text;
            
            AddAirplaneToGuiWhenSent(flightCode);

            FlightWindow window = new FlightWindow(flightCode, AirlineImageGenerator);
            window.Show();

            // This object (MainWindow) is subscribing to events in a FlightWindow instance
            window.TakenOff += OnTakenOff;
            window.RouteChanged += OnRouteChanged;
            window.Landed += OnLanded;

            // FlightLogger subscribing to events in the FlightWindow instance
            window.TakenOff += FlightLogHandler.OnTakeOff;
            window.Landed += FlightLogHandler.OnLanded;
        }

        /// <summary>
        /// Adds a row in the listview for airplane statuses and shows that the
        /// airplane has been sent to the runway.
        /// </summary>
        private void AddAirplaneToGuiWhenSent(string flightCode)
        {
            FlightInfo flightInfo = new FlightInfo()
            {
                FlightCode = flightCode,
                Status = "Sent to runway"
            };

            listViewFlights.Items.Insert(0, flightInfo);
        }

        /// <summary>
        /// The event for sending airplanes to the runway calls this method.
        /// It validates the user input and sends the airplane to the runway if the
        /// input is ok.
        /// </summary>
        private void SendAirplaneToRunway_Handler()
        {
            if (ValidateInput() == false)
            {
                MessageBox.Show(
                    ErrorMessageHandler.GetMessages(),
                    "Info",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }
            else
            {
                SendAirplaneToRunway();
                ClearInputFields();
            }
        }

        /// <summary>
        /// Handles FlightWindows's event 'TakenOff'.
        /// </summary>
        /// <param name="source">Object that sent out the event message.</param>
        /// <param name="e">Object containing data for the TakenOff event.</param>
        private void OnTakenOff_Handler(object source, TakeOffEventArgs e)
        {
            FlightInfo flightInfo = new FlightInfo()
            {
                FlightCode = e.FlightCode,
                Status = "Took off",
            };

            listViewFlights.Items.Insert(0, flightInfo);
        }

        /// <summary>
        /// Handles FlightWindows's event 'RouteChanged'.
        /// </summary>
        /// <param name="source">Object that sent out the event message.</param>
        /// <param name="e">>Object containing data for the RouteChange event.</param>
        private void OnRouteChanged_Handler(object source, ChangeRouteEventArgs e)
        {
            FlightInfo flightInfo = new FlightInfo()
            {
                FlightCode = e.FlightCode,
                Status = e.Route,
            };

            listViewFlights.Items.Insert(0, flightInfo);
        }

        /// <summary>
        /// Handles FlightWindows's event 'Landed'.
        /// </summary>
        /// <param name="source">Object that sent out the event message.</param>
        /// <param name="e">>Object containing data for the RouteChange event.</param>
        private void OnLanded_Handler(object source, LandEventArgs e)
        {
            FlightInfo flightInfo = new FlightInfo()
            {
                FlightCode = e.FlightCode,
                Status = e.Status,
            };

            listViewFlights.Items.Insert(0, flightInfo);
        }

        private void OpenFlightLogWindow_Handler()
        {
            FlightLogWindow = new FlightLogWindow(FlightLogHandler, FlightLogUtility);
            FlightLogWindow.Show();
        }

        private void WindowClosing_Handler(CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to close the program?",
                "Information",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }




        // ===================== Events ===================== //

        private void btnSendAirplaneToRunway_Click(object source, RoutedEventArgs e)
        {
            SendAirplaneToRunway_Handler();
        }

        private void OnTakenOff(object source, TakeOffEventArgs e)
        {
            OnTakenOff_Handler(source, e);
        }

        private void OnRouteChanged(object source, ChangeRouteEventArgs e)
        {
            OnRouteChanged_Handler(source, e);
        }

        private void OnLanded(object source, LandEventArgs e)
        {
            OnLanded_Handler(source, e);
        }

        private void btnViewFlightLog_Click(object sender, RoutedEventArgs e)
        {
            OpenFlightLogWindow_Handler();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            WindowClosing_Handler(e);
        }
    }
}
