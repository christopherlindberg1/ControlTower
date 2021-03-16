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

        //public List<Flight> Flights { get; set; } = new List<Flight>();




        // ===================== Methods ===================== //

        public MainWindow()
        {
            InitializeComponent();

            //InitializeSampleFlights();
        }

        private void InitializeFlightsListView()
        {

        }

        //private void InitializeSampleFlights()
        //{
            

        //    listViewFlights.ItemsSource = Flights;

        //    Flights.Add(new Flight()
        //    {
        //        FlightCode = "KLM 298",
        //        Status = "Sent to runway",
        //        DateTime = new DateTime(2021, 3, 10, 7, 43, 9)
        //    });
        //    Flights.Add(new Flight()
        //    {
        //        FlightCode = "CA 86",
        //        Status = "Landed",
        //        DateTime = new DateTime(2021, 3, 10, 7, 42, 12)
        //    });
        //    Flights.Add(new Flight()
        //    {
        //        FlightCode = "SAS 59P",
        //        Status = "Now heading 200 deg",
        //        DateTime = new DateTime(2021, 3, 10, 7, 40, 5)
        //    });
        //    Flights.Add(new Flight()
        //    {
        //        FlightCode = "BK 842",
        //        Status = "Now heading 125 dev",
        //        DateTime = new DateTime(2021, 3, 10, 7, 36, 52)
        //    });
        //}

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
            FlightWindow window = new FlightWindow(textBoxFlightCode.Text);
            window.Show();

            // Subscribing to events in FlightWindow object
            window.TakenOff += OnTakeOff;
            window.RouteChanged += OnRouteChange;
            window.Landed += OnLand;
        }

        





        // ===================== Event handler methods ===================== //

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

        private void OnTakeOff_EventHandler(object source, TakeOffEventArgs args)
        {
            FlightInfo flightInfo = new FlightInfo()
            {
                FlightCode = args.FlightCode,
                Status = "Took off",
                DateTime = DateTime.Now,
            };

            listViewFlights.Items.Add(flightInfo);
        }

        private void OnRouteChange_EventHandler(object source, ChangeRouteEventArgs args)
        {
            FlightInfo flightInfo = new FlightInfo()
            {
                FlightCode = args.FlightCode,
                Status = args.Route,
                DateTime = DateTime.Now,
            };

            listViewFlights.Items.Add(flightInfo);
        }

        private void OnLand_EventHandler(object source, LandEventArgs args)
        {
            FlightInfo flightInfo = new FlightInfo()
            {
                FlightCode = args.FlightCode,
                Status = args.Status,
                DateTime = DateTime.Now,
            };

            listViewFlights.Items.Add(flightInfo);
        }




        // ===================== Events ===================== //

        private void btnSendAirplaneToRunway_Click(object source, RoutedEventArgs e)
        {
            SendAirplaneToRunway_EventHandler();
        }

        private void OnTakeOff(object source, TakeOffEventArgs args)
        {
            OnTakeOff_EventHandler(source, args);
        }

        private void OnRouteChange(object source, ChangeRouteEventArgs args)
        {
            OnRouteChange_EventHandler(source, args);
        }

        private void OnLand(object source, LandEventArgs args)
        {
            OnLand_EventHandler(source, args);
        }


    }
}
