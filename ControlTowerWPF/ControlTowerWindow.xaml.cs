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

        public List<Flight> Flights { get; set; } = new List<Flight>();




        // ===================== Methods ===================== //

        public MainWindow()
        {
            InitializeComponent();

            InitializeSampleFlights();

            FlightWindow window = new FlightWindow();
            window.Show();
        }

        private void InitializeSampleFlights()
        {
            Flights.Add(new Flight() {
                FlightCode = "KLM 298",
                Status = "Sent to runway",
                DateTime = new DateTime(2021, 3, 10, 7, 43, 9)
            });
            Flights.Add(new Flight() {
                FlightCode = "CA 86",
                Status = "Landed", 
                DateTime = new DateTime(2021, 3, 10, 7, 42, 12)
            });
            Flights.Add(new Flight() { 
                FlightCode = "SAS 59P", 
                Status = "Now heading 200 deg", 
                DateTime = new DateTime(2021, 3, 10, 7, 40, 5) 
            });
            Flights.Add(new Flight() {
                FlightCode = "BK 842",
                Status = "Now heading 125 dev",
                DateTime = new DateTime(2021, 3, 10, 7, 36, 52) 
            });

            listViewFlights.ItemsSource = Flights;
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
            MessageBox.Show("Sending airplane to runway");
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
                // Open new window and send airplane to runway.
                SendAirplaneToRunway();

                ClearInputFields();
            }
        }




        // ===================== Events ===================== //

        private void btnSendAirplaneToRunway_Click(object source, RoutedEventArgs e)
        {
            SendAirplaneToRunway_EventHandler();
            
        }
    }
}
