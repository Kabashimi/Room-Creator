using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace kamil_florczyk
{
    public partial class PSWindow : Window, IGetFurnitureModel
    {
        private ModelVisual3D mv3d = new ModelVisual3D();
        private const double precisionEllipse = 7.5;
        
        public PSWindow()
        {
            InitializeComponent();
            mv3d.Content = GetFullModel();
            this.mainViewport.Children.Add(mv3d);
        }

        public ModelVisual3D getFurniture()
        {
            this.mainViewport.Children.Remove(mv3d);
            return mv3d;
        }

        private Model3DGroup EllipseModel3D(Point3D start, double radius, double depth, Color c1, Color c2)
        {
            Model3DGroup group = new Model3DGroup();

            MeshGeometry3D meshFront = new MeshGeometry3D();
            MeshGeometry3D meshBack = new MeshGeometry3D();

            Action<MeshGeometry3D, double, bool> addPositions = (mesh, offsetZ, direction) =>
            {
                Point3D centrum = start;
                centrum.Offset(0, 0, offsetZ);
                mesh.Positions.Add(centrum);
                if (direction)
                {
                    for (double i = 0; i <= 360.0; i += (double)precisionEllipse)
                    {
                        Point3D p = start;
                        p.Offset(Math.Sin(i * Math.PI / 180) * radius, Math.Cos(i * Math.PI / 180) * radius, offsetZ);
                        mesh.Positions.Add(p);
                    }
                }
                else
                {
                    for (double i = 360.0; i >= 0; i -= (double)precisionEllipse)
                    {
                        Point3D p = start;
                        p.Offset(Math.Sin(i * Math.PI / 180) * radius, Math.Cos(i * Math.PI / 180) * radius, offsetZ);
                        mesh.Positions.Add(p);
                    }
                }

                for (int k = 2; k <= (360 / precisionEllipse); k++)
                {
                    mesh.TriangleIndices.Add(0);
                    mesh.TriangleIndices.Add(k);
                    mesh.TriangleIndices.Add(k - 1);
                }
                mesh.TriangleIndices.Add(0);
                mesh.TriangleIndices.Add(1);
                mesh.TriangleIndices.Add((int)(360 / precisionEllipse));
            };

            addPositions(meshFront, 0, true);
            addPositions(meshBack, -depth, false);

            Material material = new DiffuseMaterial(new SolidColorBrush(c1));
            GeometryModel3D modelFront = new GeometryModel3D(meshFront, material);
            GeometryModel3D modelBack = new GeometryModel3D(meshBack, material);
            group.Children.Add(modelFront);
            group.Children.Add(modelBack);

            for (double i = precisionEllipse; i <= 360.0; i += (double)precisionEllipse)
            {
                Point3D firstFront = start;
                Point3D secondFront = start;

                secondFront.Offset(Math.Sin(i * Math.PI / 180) * radius, Math.Cos(i * Math.PI / 180) * radius, 0);
                firstFront.Offset(Math.Sin((i - precisionEllipse) * Math.PI / 180) * radius, Math.Cos((i - precisionEllipse) * Math.PI / 180) * radius, 0);

                Point3D firstBack = firstFront; firstBack.Offset(0, 0, -depth);
                Point3D secondBack = secondFront; secondBack.Offset(0, 0, -depth);

                group.Children.Add(RectangleModel2D(secondFront, secondBack, firstBack, firstFront, c2));
            }
            return group;
        }

        private Model3DGroup GetFullModel(bool additionalLoad = false)
        {
            Model3DGroup group = new Model3DGroup();

            //group.Children.Add(RectangleModel3D(new Point3D(-2, -0.1, 1), 15, 0.1, 7.5, Colors.Gray));

            // lewe podstawy
            group.Children.Add(RectangleModel3D(new Point3D(0, 0, 0), 2, 0.5, 0.5, Colors.Black));
            group.Children.Add(RectangleModel3D(new Point3D(0, 0, -5), 2, 0.5, 0.5, Colors.Black));

            // lewe słupki
            group.Children.Add(RectangleModel3D(new Point3D(1 - 0.25, 0.5, 0), 0.5, 8, 0.5, Colors.Gray));
            group.Children.Add(RectangleModel3D(new Point3D(1 - 0.25, 0.5, -5), 0.5, 8, 0.5, Colors.Gray));

            // belka
            group.Children.Add(RectangleModel3D(new Point3D(1 - 0.25, 4, -0.5), 0.5, 0.5, 4.5, Colors.Gray));

            // belka pod ławke
            group.Children.Add(RectangleModel3D(new Point3D(1 - 0.25, 4, -2.5), 9, 0.5, 0.5, Colors.Gray));

            // podstawka pod środkowy słupek
            group.Children.Add(RectangleModel3D(new Point3D(9, 0, -2.5 + 0.5 - 0.25), 1, 0.2, 1, Colors.Black));

            // środkowy słupek
            group.Children.Add(RectangleModel3D(new Point3D(9 + 0.25, 0, -2.5), 0.5, 4, 0.5, Colors.Gray));

            // ławka - element tułowia w górę
            group.Children.Add(RectangleModel3D(new Point3D(0.75, 4.5, -1.75), 6, 0.2, 2, Colors.Honeydew));

            // ławka - dalej
            group.Children.Add(RectangleModel3D(new Point3D(0.75 + 6 + 0.1, 4.5, -1.75), 3, 0.2, 2, Colors.Honeydew));

            // uchwyty
            group.Children.Add(RectangleModel3D(new Point3D(1 - 0.25 - 0.15, 0.5+8, 0), 0.7, 0.1, 0.5, Colors.Plum));
            group.Children.Add(RectangleModel3D(new Point3D(1 - 0.25 - 0.15, 0.5 + 8, 0), 0.1, 0.5, 0.5, Colors.Plum));
            group.Children.Add(RectangleModel3D(new Point3D(1 - 0.25 - 0.15+0.7, 0.5 + 8, 0), 0.1, 0.5, 0.5, Colors.Plum));

            group.Children.Add(RectangleModel3D(new Point3D(1 - 0.25 - 0.15, 0.5 + 8, -5), 0.7, 0.1, 0.5, Colors.Plum));
            group.Children.Add(RectangleModel3D(new Point3D(1 - 0.25 - 0.15, 0.5 + 8, -5), 0.1, 0.5, 0.5, Colors.Plum));
            group.Children.Add(RectangleModel3D(new Point3D(1 - 0.25 - 0.15 + 0.7, 0.5 + 8, -5), 0.1, 0.5, 0.5, Colors.Plum));

            // gryf
            group.Children.Add(EllipseModel3D(new Point3D(1, 0.5 + 8 + 0.25, 3.5), 0.2, 12, Colors.DarkSlateGray, Colors.DarkSlateGray));

            // obciążenie
            group.Children.Add(EllipseModel3D(new Point3D(1, 0.5 + 8 + 0.25, 3 - 1.5), 1.4, 0.3, Colors.DarkRed, Colors.DarkRed));
            group.Children.Add(EllipseModel3D(new Point3D(1, 0.5 + 8 + 0.25, -8 + 1.5), 1.4, 0.3, Colors.DarkRed, Colors.DarkRed));

            if (additionalLoad)
            {
                group.Children.Add(EllipseModel3D(new Point3D(1, 0.5 + 8 + 0.25, 3 - 1.5 + 0.3), 0.8, 0.3, Colors.DarkBlue, Colors.DarkBlue));
                group.Children.Add(EllipseModel3D(new Point3D(1, 0.5 + 8 + 0.25, -8 + 1.5 - 0.3), 0.8, 0.3, Colors.DarkBlue, Colors.DarkBlue));
            }
            return group;
        }

        private Model3DGroup RectangleModel3D(Point3D start, double width, double height, double depth, Color color)
        {
            Model3DGroup group = new Model3DGroup();
            Point3D[] points = new Point3D[4];

            Debug.WriteLine("[ " + start.X + ", " + start.Y + ", " + start.Z + "; " + color.ToString() + " ]");

            //front
            points[0] = start;
            points[1] = start; points[1].Offset(width, 0, 0);
            points[2] = start; points[2].Offset(width, height, 0);
            points[3] = start; points[3].Offset(0, height, 0);
            group.Children.Add(RectangleModel2D(points[0], points[1], points[2], points[3], color));

            //right
            points[0] = start; points[0].Offset(width, 0, 0);
            points[1] = start; points[1].Offset(width, 0, -depth);
            points[2] = start; points[2].Offset(width, height, -depth);
            points[3] = start; points[3].Offset(width, height, 0);
            group.Children.Add(RectangleModel2D(points[0], points[1], points[2], points[3], color));

            //left
            points[0] = start; points[0].Offset(0, 0, -depth);
            points[1] = start; 
            points[2] = start; points[2].Offset(0, height, 0);
            points[3] = start; points[3].Offset(0, height, -depth);
            group.Children.Add(RectangleModel2D(points[0], points[1], points[2], points[3], color));
            
            //top
            points[0] = start; points[0].Offset(0, height, 0);
            points[1] = start; points[1].Offset(width, height, 0);
            points[2] = start; points[2].Offset(width, height, -depth);
            points[3] = start; points[3].Offset(0, height, -depth);
            group.Children.Add(RectangleModel2D(points[0], points[1], points[2], points[3], color));

            //bottom
            points[0] = start; points[0].Offset(0, 0, 0);
            points[1] = start; points[1].Offset(width, 0, 0);
            points[2] = start; points[2].Offset(width, 0, -depth);
            points[3] = start; points[3].Offset(0, 0, -depth);
            group.Children.Add(RectangleModel2D(points[3], points[2], points[1], points[0], color));

            //back
            points[0] = start; points[0].Offset(0, 0, -depth);
            points[1] = start; points[1].Offset(width, 0, -depth);
            points[2] = start; points[2].Offset(width, height, -depth);
            points[3] = start; points[3].Offset(0, height, -depth);
            group.Children.Add(RectangleModel2D(points[3], points[2], points[1], points[0], color));

            return group;
        }

        private Model3DGroup RectangleModel2D(Point3D leftDown, Point3D rightDown, Point3D rightUp, Point3D leftUp, Color color)
        {
            Model3DGroup group = new Model3DGroup();
            MeshGeometry3D mesh = new MeshGeometry3D();

            mesh.Positions.Add(leftDown);
            mesh.Positions.Add(rightDown);
            mesh.Positions.Add(rightUp);
            mesh.Positions.Add(leftUp);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            Material material = new DiffuseMaterial(new SolidColorBrush(color));
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            group.Children.Add(model);
            return group;
        }

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /*var rotateTransform3D = new RotateTransform3D();
            var axisAngleRotation3D = new AxisAngleRotation3D();

            axisAngleRotation3D.Axis = new Vector3D(1, 0, 0);
            axisAngleRotation3D.Angle = rotateX.Value;

            rotateTransform3D.Rotation = axisAngleRotation3D;

            var myTransform3DGroup = new Transform3DGroup();
            myTransform3DGroup.Children.Add(rotateTransform3D);
            mv3d.Transform = myTransform3DGroup;*/
            Transform3DGroup transform = new Transform3DGroup();
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), rotateX.Value)));
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), rotateY.Value)));
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), rotateZ.Value)));
            transform.Children.Add(new TranslateTransform3D(offsetX.Value, offsetY.Value, offsetZ.Value));
            this.mainViewport.Camera.Transform = transform;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked.Value)
                mv3d.Content = GetFullModel(true);
            else
                mv3d.Content = GetFullModel(false);
        }

        private void Accept_CLick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
