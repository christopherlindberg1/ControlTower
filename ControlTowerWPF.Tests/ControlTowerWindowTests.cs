using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ControlTowerWPF;
using AppFeatures;

namespace ControlTowerWPF.Tests
{
    public class ControlTowerWindowTests
    {
        private static MainWindow _mainWindow = new MainWindow(new FlightLogger(null));
    }
}
