using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

                string startdate = string.Format("{0}/{1}/{2}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year);
                string enddate = string.Format("{0}/{1}/{2}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year);

                page.Textbox2.TypeText(startdate);
                browser.WaitForComplete();
                page.Textbox3.TypeText(enddate);
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

                string to = "vdudan.contractor@adb.org";
                string from = "noreply@adb.org";
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Using the new SMTP client.";
                message.Body = @"Using this new feature, you can send an e-mail message from an application very easily.";

                // Credentials are necessary if the server requires the client 
                // to authenticate before it will send e-mail on the client's behalf.

                var client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("mean.programmer.d@gmail.com", "")
                };

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                                ex.ToString());
                }  
                
            }
        }
    }
}
