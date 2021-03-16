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
using DataAccess;
using DataAccess.Utility;

namespace ControlTowerWPF
{
    /// <summary>
    /// Interaction logic for FlightLogWindow.xaml
    /// </summary>
    public partial class FlightLogWindow : Window
    {
        public FlightLogWindow()
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
            List<string> logLines = TextFileUtility.GetAllLines(FilePaths.SampleFlightLogFilePath);

            for (int i = 0; i < logLines.Count; i++)
            {
                listBoxLogLines.Items.Add(logLines[i]);
            }
        }   
    }
}
