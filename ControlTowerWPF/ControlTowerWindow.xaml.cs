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
using DataAccess;
using DataAccess.Serialization;
using AppFeatures.Models;

namespace ControlTowerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ErrorMessageHandler _errorMessageHandler = new ErrorMessageHandler();





        // ===================== Properties ===================== //
        
        private ErrorMessageHandler ErrorMessageHandler
        {
            get => _errorMessageHandler;
        }

        public FlightLogger FlightLogger { get; set; } = new FlightLogger();





        // ===================== Methods ===================== //

        public MainWindow()
        {
            InitializeComponent();

            List<string> lines = DataAccess.Utility.TextFileUtility.GetAllLines(FilePaths.SampleFlightLogFilePath);

            MessageBox.Show(lines.Count.ToString());

            //MessageBox.Show(FilePaths.SampleFlightLogFilePath);

            //Close();
        }

        public bool ValidateInput()
        {
            return ValidateFlightCode();
        }

        public bool ValidateFlightCode()
        {
            if (textBoxFlightCode.Text.Length > 10)
            {
                ErrorMessageHandler.AddMessage("The flight code cannot be longer than 10 characters.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxFlightCode.Text))
            {
                ErrorMessageHandler.AddMessage("The flight code cannot be empty");
                return false;
            }

            return true;
        }

        private void ClearInputFields()
        {
            textBoxFlightCode.Text = "";
        }

        private void SendAirplaneToRunway()
        {
            string flightCode = textBoxFlightCode.Text;
            
            AddAirplaneToGuiWhenSent(flightCode);

            FlightWindow window = new FlightWindow(flightCode);
            window.Show();

            // This object (MainWindow) subscribing to events in FlightWindow instance
            window.TakenOff += OnTakenOff;
            window.RouteChanged += OnRouteChanged;
            window.Landed += OnLanded;

            // FlightLogger subscribing to events in the FlightWindow instance
            window.TakenOff += FlightLogger.OnTakeOff;
            window.Landed += FlightLogger.OnLanded;
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

            listViewFlights.Items.Add(flightInfo);
        }

        

        





        // ===================== Event handler methods ===================== //

        /// <summary>
        /// The event for sending airplanes to the runway calls this method.
        /// It validates the user input and sends the airplane to the runway if the
        /// input is ok.
        /// </summary>
        private void SendAirplaneToRunway_EventHandler()
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
        /// The event for responding to FlightWindows's event 'TakenOff' calls this method.
        /// </summary>
        /// <param name="source">Object that sent out the event message.</param>
        /// <param name="e">Object containing data for the TakenOff event.</param>
        private void OnTakenOff_EventHandler(object source, TakeOffEventArgs e)
        {
            FlightInfo flightInfo = new FlightInfo()
            {
                FlightCode = e.FlightCode,
                Status = "Took off",
            };

            listViewFlights.Items.Add(flightInfo);
        }

        /// <summary>
        /// The event for responding to FlightWindows's event 'RouteChanged' calls this method.
        /// </summary>
        /// <param name="source">Object that sent out the event message.</param>
        /// <param name="e">>Object containing data for the RouteChange event.</param>
        private void OnRouteChanged_EventHandler(object source, ChangeRouteEventArgs e)
        {
            FlightInfo flightInfo = new FlightInfo()
            {
                FlightCode = e.FlightCode,
                Status = e.Route,
            };

            listViewFlights.Items.Add(flightInfo);
        }

        /// <summary>
        /// The event for responding to FlightWindows's event 'Landed' calls this method.
        /// </summary>
        /// <param name="source">Object that sent out the event message.</param>
        /// <param name="e">>Object containing data for the RouteChange event.</param>
        private void OnLanded_EventHandler(object source, LandEventArgs e)
        {
            FlightInfo flightInfo = new FlightInfo()
            {
                FlightCode = e.FlightCode,
                Status = e.Status,
            };

            listViewFlights.Items.Add(flightInfo);
        }

        private void WindowClosing_EventHandler(CancelEventArgs e)
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
            SendAirplaneToRunway_EventHandler();
        }

        private void OnTakenOff(object source, TakeOffEventArgs e)
        {
            OnTakenOff_EventHandler(source, e);
        }

        private void OnRouteChanged(object source, ChangeRouteEventArgs e)
        {
            OnRouteChanged_EventHandler(source, e);
        }

        private void OnLanded(object source, LandEventArgs e)
        {
            OnLanded_EventHandler(source, e);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            WindowClosing_EventHandler(e);
        }
    }
}
