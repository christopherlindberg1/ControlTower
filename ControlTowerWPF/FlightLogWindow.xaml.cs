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
using System.Windows.Shapes;
using AppFeatures;
using AppFeatures.Models;
using DataAccess;
using DataAccess.Utility;

namespace ControlTowerWPF
{
    /// <summary>
    /// Interaction logic for FlightLogWindow.xaml
    /// </summary>
    public partial class FlightLogWindow : Window
    {
        private ErrorMessageHandler ErrorMessageHandler { get; } = new ErrorMessageHandler();

        /// <summary>
        /// List with FlightLogInfo objects that is used to store the flight log data
        /// that gets fetched from the storage.
        /// </summary>
        private List<FlightLogInfo> FlightLogInfoItems { get; set; }

        /// <summary>
        /// List with FlightLogInfo objects that is based on FlightLogInfoItems.
        /// This one is used for binding to the GUI. It is this list that is modified
        /// when filtering.
        /// </summary>
        private List<FlightLogInfo> DisplayFlightLogInfoItems { get; set; }


        public FlightLogWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            InitializeData();
            InitializeGUI();
        }

        private void InitializeData()
        {
            FlightLogInfoItems = FlightLogger.GetFlightLogInfoItems();

            DisplayFlightLogInfoItems = new List<FlightLogInfo>(FlightLogInfoItems);

            listViewLogLines.ItemsSource = DisplayFlightLogInfoItems;
        }

        private void InitializeGUI()
        {
            InitializeDatePickers();
            SetNrOfLogLines();
        }

        private void InitializeDatePickers()
        {
            DatePickerStartDate.SelectedDate = DateTime.Now.AddDays(-7);
            DatePickerEndDate.SelectedDate = DateTime.Now;
        }

        private void SetNrOfLogLines()
        {
            if (FlightLogInfoItems.Count == 0)
            {
                textBlockNumberOfLogLines.Text = "There are no flight logs yet";
                return;
            }

            if (DisplayFlightLogInfoItems.Count == 0)
            {
                textBlockNumberOfLogLines.Text = "No match";
            }
            else
            {
                textBlockNumberOfLogLines.Text = $"{ DisplayFlightLogInfoItems.Count } rows";
            }
        }

        private bool ValidateInput()
        {
            return ValidateDateTimes();
        }

        private bool ValidateDateTimes()
        {
            if (DatePickerStartDate.SelectedDate > DatePickerEndDate.SelectedDate)
            {
                ErrorMessageHandler.AddMessage("The end date cannot be earlier than the starting date.");
                return false;
            }

            return true;
        }




        private void FilterFlights_EventHandler()
        {
            if (ValidateInput() == false)
            {
                MessageBox.Show(
                    ErrorMessageHandler.GetMessages(),"" +
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }

            string searchTerm = textBoxFilterByFlightCode.Text.ToLower();

            if (String.IsNullOrWhiteSpace(searchTerm))
            {
                DisplayFlightLogInfoItems = new List<FlightLogInfo>(FlightLogInfoItems);
            }

            // Since default time is 00:00:00 I'll add 23:59:59 so that all flights on this date
            // gets included in the search, regardless of what time of day the event happened.
            DateTime? endDate = DatePickerEndDate.SelectedDate;
            // Adds 1 day minus 1 millisecond
            TimeSpan timeToAdd = new TimeSpan(0, 23, 59, 59, 999);
            endDate += timeToAdd;

            DisplayFlightLogInfoItems = FlightLogInfoItems.Where(
                x => x.FlightCode.ToLower().Contains(searchTerm)).Where(
                x => x.DateTime >= DatePickerStartDate.SelectedDate).Where(
                x => x.DateTime <= endDate).ToList();

            listViewLogLines.ItemsSource = DisplayFlightLogInfoItems;

            SetNrOfLogLines();
        }




        // ===================== Events ===================== //

        private void textBoxFilterByFlightCode_KeyUp(object sender, KeyEventArgs e)
        {
            FilterFlights_EventHandler();
        }

        private void DatePickerEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterFlights_EventHandler();
        }

        private void DatePickerStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterFlights_EventHandler();
        }
    }
}
