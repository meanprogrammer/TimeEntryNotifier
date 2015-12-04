using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace TimeInChecker
{
    public class ARTSPage : WatiN.Core.Page
    {
        [FindBy(Name = "TextBox2")]
        public TextField Textbox2;

        [FindBy(Name = "TextBox3")]
        public TextField Textbox3;

        [FindBy(Id = "ApplyFilter")]
        public Button ApplyFilter;

        [FindBy(Id = "GridView1")]
        public Table GridView;

        public void DoSearch()
        {
            ApplyFilter.Click();
        }
    }
}
