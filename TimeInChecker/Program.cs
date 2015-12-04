using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatiN.Core;

namespace TimeInChecker
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var browser = new IE("http://timerec/ATRS.aspx", true)) 
            {
                var page = browser.Page<ARTSPage>();
                page.Textbox2.TypeText("12/4/2015");
                browser.WaitForComplete();
                page.Textbox3.TypeText("12/4/2015");
                browser.WaitForComplete();
                page.DoSearch();
                browser.WaitForComplete();

                Table t = page.GridView;
                TableRowCollection cls = t.TableRows;

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
                        Date = DateTime.Parse(date),
                        Time = DateTime.Parse(time),
                        EntryType = type,
                        ScanDevice = sd,
                        UserID = Convert.ToInt32(userid)
                    };
                }
            }
        }
    }
}
