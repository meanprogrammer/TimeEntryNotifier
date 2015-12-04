using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatiN.Core;
using WatiN.Core.Native.Windows;

namespace TimeInChecker
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var browser = new IE("http://timerec/ATRS.aspx", true)) 
            {
                browser.ShowWindow(NativeMethods.WindowShowStyle.Hide);
                var page = browser.Page<ARTSPage>();
                page.Textbox2.TypeText("12/4/2015");
                browser.WaitForComplete();
                page.Textbox3.TypeText("12/4/2015");
                browser.WaitForComplete();
                page.DoSearch();
                browser.WaitForComplete();

                Table t = page.GridView;
                TableRowCollection cls = t.TableRows;

                List<TimeEntryItem> timeEntries = new List<TimeEntryItem>();

                for (int i = 1; i < cls.Count; i++)
                {
                    TableRow tr = cls[i];
                    TableCellCollection tds = tr.TableCells;
                    var date = tds[0].Text;
                    var time = tds[1].Text;
                    var type = tds[2].Text;
                    var sd = tds[3].Text;
                    var userid = tds[4].Text;

                    TimeEntryItem te = new TimeEntryItem() { 
                        Date = DateTime.ParseExact(date, "MM-dd-yyyy", CultureInfo.InvariantCulture),
                        Time = DateTime.Parse(time),
                        EntryType = type,
                        ScanDevice = sd,
                        UserID = Convert.ToInt32(userid)
                    };

                    timeEntries.Add(te);
                }

                var x = timeEntries;

                
            }
        }
    }
}
