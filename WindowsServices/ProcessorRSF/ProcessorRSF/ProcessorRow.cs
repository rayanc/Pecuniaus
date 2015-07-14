using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processor
{
    /// <summary>
    /// Class to store one CSV row
    /// </summary>
    public class ProcessorRow : List<string>
    {
        public string LineText { get; set; }
    }

}
