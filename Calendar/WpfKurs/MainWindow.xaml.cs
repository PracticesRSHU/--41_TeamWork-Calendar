using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace WpfKurs
{
    public partial class MainWindow : Window
    {
        List<Event> ev = new List<Event>();
        public DateTime date { get; set; }
        public string NameH { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            date = DateTime.Parse(monthC.SelectedDate.ToString());
            if (date.Date >= DateTime.Now.Date)
            {
                NameH = textBoxN.Text;
                ev.Add(new Event(date, NameH));
                listB.Items.Add($"{date} | {NameH}");
            }
            else MessageBox.Show("Check date event", "Error");
        }




        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ev.RemoveAt(listB.SelectedIndex);
                listB.Items.RemoveAt(listB.SelectedIndex);
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ev.Clear();
            listB.Items.Clear();
        }




        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            XDocument xd = new XDocument();
            XElement XE = new XElement("Events");
            for (int i = 0; i < ev.Count; i++)
            {
                XElement xe = new XElement("event");
                XAttribute xa = new XAttribute("name", $"{ev[i].Name}");
                XElement xe2 = new XElement("time", $"{ev[i].Time}");
                xe.Add(xa);
                xe.Add(xe2);
                XE.Add(xe);
            }
            xd.Add(XE);
            xd.Save("../../Events.xml");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("../../Events.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                string strN = "";
                string strT = "";
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("name");
                    if (attr != null)
                        strN = attr.Value;
                }
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "time")
                    {
                        strT = childnode.InnerText;
                    }

                }
                ev.Add(new Event(Convert.ToDateTime(strT), strN));
                listB.Items.Add($"{strT} | {strN}");
            }
        }
        public class Event
        {
            public DateTime Time { get; set; }
            public string Name { get; set; }
            public Event(DateTime time, string name)
            {
                Time = time;
                Name = name;
            }
        }

        private void btnCh_Click(object sender, RoutedEventArgs e)
        {
            PaletteHelper _paletteHelper = new PaletteHelper();
            if (btnCh.IsChecked == false)
            {
                bool isDark = true;
                ITheme theme = _paletteHelper.GetTheme();
                IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
                theme.SetBaseTheme(baseTheme);
                _paletteHelper.SetTheme(theme);
                blackGradient.Offset = 1;
                whiteGradient.Offset = 0;
            }
            if (btnCh.IsChecked == true)
            {
                bool isDark = false;
                ITheme theme = _paletteHelper.GetTheme();
                IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
                theme.SetBaseTheme(baseTheme);
                _paletteHelper.SetTheme(theme);
                blackGradient.Offset = 0;
                whiteGradient.Offset = 1;
            }
        }

        private void listB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnCh_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}