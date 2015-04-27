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

namespace jan_dudek
{
    /// <summary>
    /// Interaction logic for Tapczan1.xaml
    /// </summary>
    public partial class JDTapczan : Window, IGetFurnitureModel
    {
        private ModelVisual3D mv3d;
        /*public ModelVisual3D Mv3d
        {
            get
            {
                return mv3d;
            }
        }*/

        public ModelVisual3D getFurniture()
        {
            this.mainViewport.Children.Remove(mv3d);
            return mv3d;
        }
        

        private Model3DGroup Segments;
        private Material Mat;



        // private Transform3DGroup scaletapczan;  ///typ referencyjny!!!

        public JDTapczan()
        {
            Mat = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));
            Segments = new Model3DGroup();
            InitializeComponent();
            mv3d = new ModelVisual3D();
            mv3d.Content = MakeIt();
            // mv3d.Transform = scaletapczan;

            this.mainViewport.Children.Add(mv3d);
        }



        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PrepareTransform();
        }

        private void PrepareTransform()
        {
            Transform3DGroup Transform3DGroup_transform = new Transform3DGroup();
            Transform3DGroup_transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), RotX.Value)));
            Transform3DGroup_transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), RotY.Value)));
            Transform3DGroup_transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), RotZ.Value)));
            Transform3DGroup_transform.Children.Add(new TranslateTransform3D(OffX.Value, OffY.Value, OffZ.Value));

            this.mainViewport.Camera.Transform = Transform3DGroup_transform;
            this.Light.Transform = Transform3DGroup_transform;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OffX.Value = 0;
            OffY.Value = 0;
            OffZ.Value = 0;

            RotX.Value = 0;
            RotY.Value = 0;
            RotZ.Value = 0;

            PrepareTransform();

        }

        private Model3DGroup MakeIt()
        {
            Model3DGroup model = new Model3DGroup();



            model.Children.Add(MakeBackWall());
            model.Children.Add(makeLeftWall());
            model.Children.Add(Segments);
            ScaleModel();

            return model;
        }

        private Model3DGroup MakeBackWall()
        {

            Model3DGroup group = new Model3DGroup();

            double lenghtStep = 0.25;

            MeshGeometry3D mesh = new MeshGeometry3D();

            List<Point3D> pointsFrontDown = new List<Point3D>();
            List<Point3D> pointsBackDown = new List<Point3D>();
            List<Point3D> pointsFrontUp = new List<Point3D>();
            List<Point3D> pointsBackUp = new List<Point3D>();

            for (int i = 0; i < 25; i++)//RYZYKOWNIE_25!!!
            {
                pointsFrontDown.Add(new Point3D(lenghtStep * i, 0, 0.2));
                pointsBackDown.Add(new Point3D(lenghtStep * i, 0, 0));

            }
            for (int i = 0; i < 23; i++)
            {
                pointsFrontUp.Add(new Point3D(lenghtStep * i, Math.Log10(6 - lenghtStep * i) + 2, 0.2));
                pointsBackUp.Add(new Point3D(lenghtStep * i, Math.Log10(6 - lenghtStep * i) + 2, 0));
            }


            #region frontWall
            mesh.Positions.Add(pointsFrontDown[0]);
            mesh.Positions.Add(pointsFrontDown[24]);
            for (int i = 22; i > -1; i--)
            {
                mesh.Positions.Add(pointsFrontUp[i]);
            }

            for (int i = 0; i < 24; i++)
            {
                mesh.TriangleIndices.Add(0);
                mesh.TriangleIndices.Add(i);
                mesh.TriangleIndices.Add(i + 1);
            }

            Material material = Mat;
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            group.Children.Add(model);

            #endregion
            #region backWall
            mesh = new MeshGeometry3D();

            mesh.Positions.Add(pointsBackDown[0]);
            for (int i = 0; i < 23; i++)
            {
                mesh.Positions.Add(pointsBackUp[i]);
            }
            mesh.Positions.Add(pointsBackDown[24]);

            for (int i = 0; i < 24; i++)
            {
                mesh.TriangleIndices.Add(0);
                mesh.TriangleIndices.Add(i);
                mesh.TriangleIndices.Add(i + 1);
            }

            material = Mat;
            model = new GeometryModel3D(mesh, material);
            group.Children.Add(model);
            #endregion
            #region laftWall
            mesh = new MeshGeometry3D();

            mesh.Positions.Add(pointsBackDown[0]);
            mesh.Positions.Add(pointsFrontDown[0]);
            mesh.Positions.Add(pointsFrontUp[0]);
            mesh.Positions.Add(pointsBackUp[0]);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            material = Mat;
            model = new GeometryModel3D(mesh, material);
            group.Children.Add(model);

            #endregion
            #region downWall

            mesh = new MeshGeometry3D();

            mesh.Positions.Add(pointsFrontDown[0]);
            mesh.Positions.Add(pointsBackDown[0]);
            mesh.Positions.Add(pointsBackDown[24]);
            mesh.Positions.Add(pointsFrontDown[24]);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            material = Mat;
            model = new GeometryModel3D(mesh, material);
            group.Children.Add(model);

            #endregion
            #region logWall

            mesh = new MeshGeometry3D();

            for (int i = 0; i < 23; i++)
            {
                mesh.Positions.Add(pointsFrontUp[i]);
            }

            for (int i = 0; i < 23; i++)
            {
                mesh.Positions.Add(pointsBackUp[i]);
            }

            for (int i = 0; i < 23; i++)
            {
                mesh.TriangleIndices.Add(i);
                mesh.TriangleIndices.Add(i + 1);
                mesh.TriangleIndices.Add(i + 23);
                mesh.TriangleIndices.Add(i);
                mesh.TriangleIndices.Add(i + 23);
                mesh.TriangleIndices.Add(i + 22);
            }



            material = Mat;
            model = new GeometryModel3D(mesh, material);
            group.Children.Add(model);

            #endregion
            #region rightWall
            mesh = new MeshGeometry3D();

            mesh.Positions.Add(pointsFrontUp[22]);
            mesh.Positions.Add(pointsFrontDown[24]);
            mesh.Positions.Add(pointsBackDown[24]);
            mesh.Positions.Add(pointsBackUp[22]);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            material = Mat;
            model = new GeometryModel3D(mesh, material);
            group.Children.Add(model);

            #endregion

            return group;
        }

        private Model3DGroup makeLeftWall()
        {
            Model3DGroup group = new Model3DGroup();

            double width = 2;
            double heigth = 2;

            Point3D p1 = new Point3D(0, 0, 0.2);
            Point3D p2 = new Point3D(0, 0, width);
            Point3D p3 = new Point3D(0.5, 0, width);
            Point3D p4 = new Point3D(0.5, 0, 0.2);
            Point3D p5 = new Point3D(0, heigth, 0.2);
            Point3D p6 = new Point3D(0, heigth, width);
            Point3D p7 = new Point3D(0.5, heigth, width);
            Point3D p8 = new Point3D(0.5, heigth, 0.2);
            group.Children.Add(MakeOneWall(p1, p2, p6, p5));
            group.Children.Add(MakeOneWall(p2, p3, p7, p6));
            group.Children.Add(MakeOneWall(p3, p4, p8, p7));
            group.Children.Add(MakeOneWall(p5, p6, p7, p8));
            group.Children.Add(MakeOneWall(p1, p4, p3, p2));



            return group;
        }

        private Model3DGroup MakeOneWall(Point3D p0, Point3D p1, Point3D p2, Point3D p3)
        {
            Model3DGroup group = new Model3DGroup();
            MeshGeometry3D mesh = new MeshGeometry3D();

            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);



            Material material = Mat;
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            group.Children.Add(model);

            return group;
        }

        private void Length_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void Width1_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }


        private void ScaleModel()
        {
            try
            {
                Transform3DGroup Tf3DG = new Transform3DGroup();
                Tf3DG.Children.Add(new ScaleTransform3D(Length.Value, 1, Width1.Value, 0, 0, 0));       //SLIDERY NIE MAJĄ PRZYPISANEJ WARTOŚC PODCZAS INITIALIZACJI


                mv3d.Transform = Tf3DG;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Console.WriteLine("Finished");
            }
        }

        private void Length_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScaleModel();

        }

        private void Length_TouchUp(object sender, TouchEventArgs e)
        {
            ScaleModel();
        }

        private void Width1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScaleModel();
        }

        private void MakeSegments()
        {
            try
            {
                Model3DGroup model = new Model3DGroup();

                double segLength = 5.5 / SegmentsCount.Value;
                double segWidth = 1.8;

                for (int i = 0; i < SegmentsCount.Value; i++)
                {
                    model.Children.Add(MakeSegment(new Point3D(segLength * i, 0, 0.2), segLength));
                }

                Segments = model;
            }
            catch (Exception e)
            {

            }
        }

        private Model3DGroup MakeSegment(Point3D p0, double lenght)
        {
            double heihth = 1;
            Point3D p1 = new Point3D(p0.X, p0.Y, p0.Z + 1.8);
            Point3D p2 = new Point3D(p0.X + lenght, p0.Y, p0.Z + 1.8);
            Point3D p3 = new Point3D(p0.X + lenght, p0.Y, p0.Z);
            Point3D p4 = new Point3D(p0.X, p0.Y + heihth, p0.Z);
            Point3D p5 = new Point3D(p0.X, p0.Y + heihth, p0.Z + 1.8);
            Point3D p6 = new Point3D(p0.X + lenght, p0.Y + heihth, p0.Z + 1.8);
            Point3D p7 = new Point3D(p0.X + lenght, p0.Y + heihth, p0.Z);
            double cut = 0.1;
            Point3D p8 = new Point3D(p0.X + cut, p0.Y + cut + heihth, p0.Z + cut);
            Point3D p9 = new Point3D(p0.X + cut, p0.Y + cut + heihth, p0.Z - cut + 1.8);
            Point3D p10 = new Point3D(p0.X - cut + lenght, p0.Y + cut + heihth, p0.Z - cut + 1.8);
            Point3D p11 = new Point3D(p0.X - cut + lenght, p0.Y + cut + heihth, p0.Z + cut);

            Model3DGroup group = new Model3DGroup();

            group.Children.Add(MakeOneWall(p0, p1, p5, p4));
            group.Children.Add(MakeOneWall(p1, p2, p6, p5));
            group.Children.Add(MakeOneWall(p2, p3, p7, p6));
            group.Children.Add(MakeOneWall(p3, p0, p4, p7));
            group.Children.Add(MakeOneWall(p3, p2, p1, p0));

            group.Children.Add(MakeOneWall(p4, p5, p9, p8));
            group.Children.Add(MakeOneWall(p5, p6, p10, p9));
            group.Children.Add(MakeOneWall(p6, p7, p11, p10));
            group.Children.Add(MakeOneWall(p7, p4, p8, p11));
            group.Children.Add(MakeOneWall(p8, p9, p10, p11));

            return group;
        }

        private void SegmentsCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                MakeSegments();
                mv3d.Content = MakeIt();
            }
            catch (Exception ex)
            {

            }
        }

        private void ComboColor_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            

        }

        private Material CheckColor()
        {
            Material Mate = new DiffuseMaterial(new SolidColorBrush(Colors.Red));
            
                if (ComboColor.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Red")
                {
                    Mate = new DiffuseMaterial(new SolidColorBrush(Colors.Red));
                }
                else if (ComboColor.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Blue")
                {
                    Mate = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));
                }
                else if (ComboColor.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Green")
                {
                    Mate = new DiffuseMaterial(new SolidColorBrush(Colors.Green));
                }
                
            
            
            return Mate;
        }

        private void ComboColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Mat = CheckColor();
                MakeSegments();
                mv3d.Content = MakeIt();
            }
            catch (Exception ex)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
