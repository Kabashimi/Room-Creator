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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

using Room_Creator;

namespace rafal_rogoda
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RRwindow : Window, IGetFurnitureModel
    {
        RRdrawingMethods Lamp = new RRdrawingMethods();

        ModelVisual3D mVisual = new ModelVisual3D();
        ModelVisual3D temporaryModel = new ModelVisual3D();
         
        public RRwindow()
        {
            
            InitializeComponent();
            
                Create();
           
        }
        /// <summary>
        /// tworzy Model i modyfikuje go.
        /// </summary>
        private void Create()
        {

            double orgX ,orgY ,orgZ ,OsizX ,OsizY ,OsizZ ,RsizX ,RsizY ,RsizZ;
            orgX = orgY = orgZ = OsizX = OsizY = OsizZ = RsizX = RsizY = RsizZ = 0;

            double.TryParse(SizeX2.Text, out OsizX);
            double.TryParse(SizeY2.Text, out OsizY);
            double.TryParse(SizeZ2.Text, out OsizZ);

            double.TryParse(SizeX1.Text, out RsizX);
            double.TryParse(SizeY1.Text, out RsizY);
            double.TryParse(SizeZ1.Text, out RsizZ);

            Point3D orgin = new Point3D(orgX, orgY, orgZ);
            Size3D sizeO = new Size3D(OsizX, OsizY, OsizZ);
            Size3D sizeR = new Size3D(RsizX, RsizY, RsizZ);

            bool IsOn = Light.IsChecked == true ? true : false;

            Model3DGroup Model = Lamp.DrawCuboids(orgin, sizeO);
            Model.Children.Add(Lamp.DrawCover(orgin, sizeR, IsOn));

            temporaryModel.Content = Model;

            if (mainViewport.Children.Count !=  2 )
            {

                mainViewport.Children.Add(temporaryModel);
            }

        }


        ////////////////////////////////

        /// <summary>
        /// Obsługa modelu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rotate(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Transform3DGroup Transform3DGroup_transform = new Transform3DGroup();

            TranslateTransform3D translate = new TranslateTransform3D(SliderOffsetX.Value, SliderOffsetY.Value, SliderOffsetZ.Value);
            Transform3DGroup_transform.Children.Add(translate);

            AxisAngleRotation3D AxisAngleRotator_rot_x = new AxisAngleRotation3D(new Vector3D(1, 0, 0), SliderRotateX.Value);
            AxisAngleRotation3D AxisAngleRotator_rot_y = new AxisAngleRotation3D(new Vector3D(0, 1, 0), SliderRotateY.Value);
            AxisAngleRotation3D AxisAngleRotator_rot_z = new AxisAngleRotation3D(new Vector3D(0, 0, 1), SliderRotateZ.Value);

            Transform3DGroup_transform.Children.Add(new RotateTransform3D(AxisAngleRotator_rot_x));
            Transform3DGroup_transform.Children.Add(new RotateTransform3D(AxisAngleRotator_rot_y));
            Transform3DGroup_transform.Children.Add(new RotateTransform3D(AxisAngleRotator_rot_z));

            this.Lighting.Transform = Transform3DGroup_transform;
            this.mainViewport.Camera.Transform = Transform3DGroup_transform;
        }

        /// <summary>
        /// Wprowadzenie zmian i reset pozycji modelu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reload(object sender, RoutedEventArgs e)
        {
            SliderOffsetX.Value = SliderOffsetY.Value = SliderOffsetZ.Value = SliderRotateX.Value = SliderRotateY.Value = SliderRotateZ.Value = 0;
            Create();
        }

        /// <summary>
        /// Zapisuje Model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save(object sender, RoutedEventArgs e)
        {
            mVisual.Content = temporaryModel.Content;
        }

        /// <summary>
        /// Zwraca Zapisany model
        /// </summary>
        /// <returns></returns>
        public ModelVisual3D getFurniture()
        {
            return mVisual;
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
