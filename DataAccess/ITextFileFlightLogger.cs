using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess
{
    /// <summary>
    /// Interface specifying which members a text file-based flight logger should have.
    /// </summary>
    public interface ITextFileFlightLogger : IFlightLogger
    {
        string FilePath { get; }
    }
}
