using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;

using Room_Creator;

namespace hubert_drogosz 
{
    /// <summary>
    /// Interaction logic for HiFiWindow.xaml
    /// </summary>
    public partial class HDHiFiWindow : Window,IGetFurnitureModel
    {
        private HDHifi hifi;

        private Transform3D offsetTransform;
        private Transform3D rotationXTransform;
        private Transform3D rotationYTransform;
        private Transform3D rotationZTransform;
        private ObservableCollection<Uri> songs = new ObservableCollection<Uri>();

        public ModelVisual3D getFurniture()
        {
            mainViewport.Children.Remove(hifi);
            return hifi;
        }

        public HDHiFiWindow()
        {
            InitializeComponent();
            combos.ItemsSource = songs;
            songs.Add(new Uri("Hubert_Drogosz/Resources/cos1.wav", UriKind.Relative));
            songs.Add(new Uri("Hubert_Drogosz/Resources/katy.wav", UriKind.Relative));
            var bitmap = new BitmapImage(new Uri("Hubert_Drogosz/Resource/drewno.png", UriKind.Relative));
            hifi = new HDHifi(new Point3D(0, 0, 0), new Size3D(2, 2, 1), new SolidColorBrush(Colors.Black)
                ,new SolidColorBrush(Colors.LightCyan)
                ,bitmap );

           
            mainViewport.Children.Add(hifi);

        }
        
        private void remakeTransform()
        {
            Transform3DGroup newTransform = new Transform3DGroup();
            if (offsetTransform != null)
                newTransform.Children.Add(offsetTransform);
            if (rotationXTransform != null)
                newTransform.Children.Add(rotationXTransform);
            if (rotationYTransform != null)
                newTransform.Children.Add(rotationYTransform);
            if (rotationZTransform != null)
                newTransform.Children.Add(rotationZTransform);

            mainViewport.Camera.Transform = newTransform;
            mVisual.Transform = newTransform;
        }

        private void offsetChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            offsetTransform = new TranslateTransform3D(offX.Value, offY.Value, offZ.Value);
            this.remakeTransform();
        }

        private void rotationChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            rotationXTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), rotX.Value));
            rotationYTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), rotY.Value));
            rotationZTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), rotZ.Value));

            this.remakeTransform();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            offX.Value = 0;
            offY.Value = 0;
            offZ.Value = 0;

            rotX.Value = 0;
            rotY.Value = 0;
            rotZ.Value = 0;

            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            hifi.SetVolume(Volume.Value/Volume.Maximum);
        }

        private void lengthValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (hifi != null)
            {
                mainViewport.Children.Remove(hifi);
                hifi = new HDHifi(new Point3D(0, 0, 0), new Size3D(lengthX.Value, lengthY.Value, lengthZ.Value), new SolidColorBrush(Colors.Black)
                    , new SolidColorBrush(Colors.LightCyan)
                    , new BitmapImage(new Uri("Resource/drewno.png", UriKind.Relative)));
                mainViewport.Children.Add(hifi);
            }
        }


        private void combos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hifi.StartPlaying(combos.SelectedItem as Uri);
        }

    }
}
