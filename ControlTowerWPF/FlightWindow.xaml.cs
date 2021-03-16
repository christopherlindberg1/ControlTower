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

        private void InitializeWindow()
        {
            InitializeGUI();
        }

        private void InitializeGUI()
        {
            this.Title = $"Flight { FlightCode }";
            btnStartFlight.IsEnabled = true;
            comboBoxChangeRoute.IsEnabled = false;
            btnLand.IsEnabled = false;
            InitializeChangeRouteComboBox();
        }

        private void InitializeChangeRouteComboBox()
        {
            comboBoxChangeRoute.Items.Add("");

            for (int i = 0; i < 351; i += 10)
            {
                comboBoxChangeRoute.Items.Add($"Heading { i } deg");
            }
        }

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





        // ===================== Event handler methods ===================== //






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
