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
using DataAccess;
using DataAccess.Models;
using DataAccess.Utility;

namespace ControlTowerWPF
{
    /// <summary>
    /// Interaction logic for FlightLogWindow.xaml
    /// </summary>
    public partial class FlightLogWindow : Window
    {
        private readonly IFlightLogHandler _flightLogHandler;
        private readonly IFlightLogUtility _flightLogUtility;
        private readonly ErrorMessageHandler _errorMessageHandler = new ErrorMessageHandler();




        // ===================== Properties ===================== //

        public IFlightLogHandler FlightLogHandler { get => _flightLogHandler; }

        public IFlightLogUtility FlightLogUtility { get => _flightLogUtility; }

        private ErrorMessageHandler ErrorMessageHandler { get => _errorMessageHandler; }

        /// <summary>
        /// List with FlightLogInfo objects that is used to store the flight log data
        /// that gets fetched from the storage.
        /// </summary>
        private List<FlightLogInfo> FilteredFlightLogInfoItems { get; set; }

        public DateTime? CurrentStartDate { get; set; }
        public DateTime? CurrentEndDate { get; set; }





        // ===================== Methods ===================== //

        public FlightLogWindow(IFlightLogHandler flightLogHandler, IFlightLogUtility flightLogUtility)
        {
            InitializeComponent();

            _flightLogHandler = flightLogHandler;
            _flightLogUtility = flightLogUtility;

            InitializeWindow();
        }

        private void InitializeWindow()
        {
            InitializeData();
            InitializeGUI();
        }

        /// <summary>
        /// Loads data from the log file and sets the ItemSource property of the listview
        /// so that data is displayed in the GUI.
        /// </summary>
        private void InitializeData()
        {
            string searchTerm = "";
            DateTime startDate = DateTime.Now.AddDays(-6).Date;
            DateTime endDate = DateTime.Now;

            FilteredFlightLogInfoItems = FlightLogUtility.FilterFlightLog(
                FlightLogHandler.FlightLogInfoItems, searchTerm, startDate, endDate);
            
            listViewLogLines.ItemsSource = FilteredFlightLogInfoItems;
        }

        /// <summary>
        /// Initializes the GUI.
        /// </summary>
        private void InitializeGUI()
        {
            InitializeDatePickers();
            SetNrOfLogLines(FilteredFlightLogInfoItems.Count);
        }

        /// <summary>
        /// Initializes the datetime pickers to cover the last 7 days.
        /// </summary>
        private void InitializeDatePickers()
        {
            DateTime? currentStartDate = DateTime.Now.AddDays(-6);
            DateTime? currentEndDate = DateTime.Now;

            CurrentStartDate = currentStartDate;
            CurrentEndDate = currentEndDate;

            DatePickerStartDate.SelectedDate = currentStartDate;
            DatePickerEndDate.SelectedDate = currentEndDate;
        }

        /// <summary>
        /// Gives a short message showing how many records in the flight log is
        /// currently being shown, and a message if no record is shown.
        /// </summary>
        private void SetNrOfLogLines(int nrOfLines)
        {
            if (FlightLogHandler.TotalNumberOfFlightLogRecords == 0)
            {
                textBlockNumberOfLogLines.Text = "There are no flight logs yet";
            }
            else if (nrOfLines == 0)
            {
                textBlockNumberOfLogLines.Text = "No flight logs match the search filter";
            }
            else
            {
                textBlockNumberOfLogLines.Text = $"{ nrOfLines } rows";
            }
        }

        /// <summary>
        /// Validates the user input when filtering flight logs.
        /// </summary>
        /// <returns>True if everything validates correctly, false otherwise.</returns>
        private bool ValidateInput()
        {
            return ValidateDateTimes();
        }

        /// <summary>
        /// Validates that provided end date comes after the start date.
        /// </summary>
        /// <returns>True if dates are correct, false otherwise.</returns>
        private bool ValidateDateTimes()
        {
            if (DatePickerStartDate.SelectedDate > DatePickerEndDate.SelectedDate)
            {
                ErrorMessageHandler.AddMessage("The end date cannot be earlier than the starting date.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Filters away all flight log entries that do not match the search string
        /// or the start- and end date.
        /// </summary>
        private void FilterFlights_Handler()
        {
            if (ValidateInput() == false)
            {
                MessageBox.Show(
                    ErrorMessageHandler.GetMessages(),
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                DatePickerStartDate.SelectedDate = CurrentStartDate;
                DatePickerEndDate.SelectedDate = CurrentEndDate;

                return;
            }

            string searchTerm = textBoxFilterByFlightCode.Text.ToLower();
            DateTime? startDate = DatePickerStartDate.SelectedDate;
            DateTime? endDate = DatePickerEndDate.SelectedDate;

            List<FlightLogInfo> list = FlightLogUtility.FilterFlightLog(
                FlightLogHandler.FlightLogInfoItems, searchTerm, startDate, endDate);

            listViewLogLines.ItemsSource = list;

            SetNrOfLogLines(list.Count);
        }




        // ===================== Events ===================== //

        private void textBoxFilterByFlightCode_KeyUp(object sender, KeyEventArgs e)
        {
            FilterFlights_Handler();
        }

        private void DatePickerStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterFlights_Handler();
        }

        private void DatePickerEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterFlights_Handler();
        }
    }
}
