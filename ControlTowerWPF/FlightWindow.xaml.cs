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

namespace ControlTowerWPF
{
    /// <summary>
    /// Interaction logic for FlightWindow.xaml
    /// </summary>
    public partial class FlightWindow : Window
    {
        public FlightWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            InitializeGUI();
        }

        private void InitializeGUI()
        {
            InitializeChangeRouteComboBox();
        }

        private void InitializeChangeRouteComboBox()
        {
            comboBoxChangeRoute.Items.Add("");

            for (int i = 10; i < 361; i += 10)
            {
                comboBoxChangeRoute.Items.Add($"Heading { i } deg");
            }
        }
    }
}
