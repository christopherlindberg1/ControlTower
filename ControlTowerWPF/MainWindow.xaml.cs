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
        public List<Flight> Flights { get; set; } = new List<Flight>();

        public MainWindow()
        {
            InitializeComponent();

            InitializeSampleFlights();
        }

        private void InitializeSampleFlights()
        {
            Flights.Add(new Flight() { FlightCode = "KLM 298", Status = "Sent to runway", Time = "1 minute ago" });
            Flights.Add(new Flight() { FlightCode = "CA 86", Status = "Landed", Time = "3 minutes ago" });
            Flights.Add(new Flight() { FlightCode = "SAS 59P", Status = "Now heading 200 deg", Time = "3 minutes ago" });
            Flights.Add(new Flight() { FlightCode = "BK 842", Status = "Now heading 125 dev", Time = "6 minutes ago" });
            listViewFlights.ItemsSource = Flights;
        }
    }
}
