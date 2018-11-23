using System;
using System.Collections.Generic;
using System.Globalization;
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
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
//using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        LocationConverter locConverter = new LocationConverter();

        public static object Mode { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            //Set focus on the map
            myMap.Focus();

            // Displays the current latitude and longitude as the map animates.
            myMap.ViewChangeOnFrame += new EventHandler<MapEventArgs>(ViewMap_ViewChangeOnFrame);
            // The default animation level: navigate between different map locations.
            //viewMap.AnimationLevel = AnimationLevel.Full;
            //Set focus to map
            //myMap.Focus();



        }




        private void ViewMap_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            // Gets the map object that raised this event.
            Map map = sender as Map;
            // Determine if we have a valid map object.
            if (map != null)
            {
                // Gets the center of the current map view for this particular frame.
                Location mapCenter = map.Center;

                // Updates the latitude and longitude values, in real time,
                // as the map animates to the new location.
                txtLatitude.Text = string.Format(CultureInfo.InvariantCulture,
                  "{0:F5}", mapCenter.Latitude);
                txtLongitude.Text = string.Format(CultureInfo.InvariantCulture,
                    "{0:F5}", mapCenter.Longitude);
            }
        }
        private void ChangeMapView_Click(object sender, RoutedEventArgs e)
        {
            // Parse the information of the button's Tag property
            string[] tagInfo = ((Button)sender).Tag.ToString().Split(' ');
            Location center = (Location)locConverter.ConvertFrom(tagInfo[0]);
            // double zoom = System.Convert.ToDouble(tagInfo[1]);
            double zoom = double.Parse(tagInfo[1], CultureInfo.InvariantCulture);

            // Set the map view
            myMap.SetView(center, zoom);

        }

        private void btnToggleMap_Click(object sender, RoutedEventArgs e)
        {
            if (myMap.Mode.ToString() == "Microsoft.Maps.MapControl.WPF.RoadMode")
            {

                //Set the map mode to Aerial with labels
                myMap.Mode = new AerialMode(true);
            }
            else if (myMap.Mode.ToString() == "Microsoft.Maps.MapControl.WPF.AerialMode")
            {
                //Set the map mode to RoadMode
                myMap.Mode = new RoadMode();
            }
        }

        private void AnimationLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)(((ComboBox)sender).SelectedItem);
            string v = cbi.Content as string;
            if (!string.IsNullOrEmpty(v) && myMap != null)
            {
                AnimationLevel newLevel = (AnimationLevel)Enum.Parse(typeof(AnimationLevel), v, true);
                myMap.AnimationLevel = newLevel;
            }
        }
    }
}

