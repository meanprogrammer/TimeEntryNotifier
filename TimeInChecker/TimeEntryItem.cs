using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeInChecker
{
    public class TimeEntryItem
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string EntryType { get; set; }
        public string ScanDevice { get; set; }
        public int UserID { get; set; }
    }
}
