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
using System.Windows.Media.Media3D;
using System.Windows.Threading;

using Room_Creator;

namespace MacbookModal.pbrewczynski
{

    public partial class PBWindow : Window, IGetFurnitureModel
    {

        private Vector3D lookDirectionVector;
        private Point3D positionPoint;
        
        private ModelVisual3D macBookModel;

        private ModelVisual3D macBookBottom;
        private ModelVisual3D macBookScreen;


        //Declarations required for audio out and the MP3 stream

        public ModelVisual3D getFurniture()
        {
            this.mainViewport.Children.Clear();

            ModelVisual3D model = new ModelVisual3D();

            model.Children.Add(this.macBookBottom);
            model.Children.Add(this.macBookScreen);


            //model.Children.Add(this.mainViewport.Children[0]);
            //model.Children.Add(this.mainViewport.Children[1]);

           // model.Children.Add(this.mainViewport.Children.)
                
            //    = this.mainViewport.Children;
           
            return model;
        }
        public string getName()
        {
            return "MacBook";
        }




        private DispatcherTimer timer; 
        public PBWindow()
        {
            
            InitializeComponent();
            
            this.lookDirectionVector = ((PerspectiveCamera)this.mainViewport.Camera).LookDirection;
            this.positionPoint = ((PerspectiveCamera)this.mainViewport.Camera).Position;

            this.drawMacbook();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            timer.Tick += timer_Tick;
            timer.Start();





            //waveOutDevice = new WaveOut();
            //audioFileReader = new AudioFileReader("MacbookAirAudio.mp3");
        }


        private double currentZ = 0;
        private double currentScreenAngle = 0;
        private double currentPivot = 0;
        private double hackTimer = 0; 
        void timer_Tick(object sender, EventArgs e)
        {
            
            if (currentZ <= 10)
            {
                this.currentZ += 0.02;
                this.changeZCamera(this.currentZ);
            } else if(hackTimer < 100) {
                hackTimer += 1;
            }
            else if(currentScreenAngle >= -100)
            {
                this.currentScreenAngle -= 1;
                this.angleScreen(this.currentScreenAngle);
            }
            else if (hackTimer < 200)
            {
                hackTimer += 1;
            } 
            else if(this.currentPivot <= 70) {
                this.currentPivot += 1;
                this.pivotTheCamera(this.currentPivot);
            } else {
                this.timer.Stop();
            }

            

            //throw new NotImplementedException();
        }

        private void drawMacbook()
        {
            this.cubeButtonClick();
        }

        /*private Model3DGroup CreateTriangleModel(Point3D p0, Point3D p1, Point3D p2)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            Vector3D normal = CalculateNormal(p0, p1, p2);

            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);

            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.DarkKhaki));

            GeometryModel3D model = new GeometryModel3D(mesh, material);

            Model3DGroup group = new Model3DGroup();
            return group;
        }

        private Vector3D CalculateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);

            Vector3D v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p2.Z);

            return Vector3D.CrossProduct(v0, v1);
        }*/

        private Model3DGroup CreateTriangleModel(Point3D p0, Point3D p1, Point3D p2)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            mesh.TextureCoordinates.Add(new Point(0, 1));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(1, 0));
            mesh.TextureCoordinates.Add(new Point(0, 0));


            /*Vector3D normal = CalculateNormal(p0, p1, p2);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);*/


            ImageBrush colors_brush = new ImageBrush();
            colors_brush.ImageSource = new BitmapImage(new Uri("pbrewczynski/Textures/aluminium.jpg", UriKind.Relative));
            DiffuseMaterial material = new DiffuseMaterial(colors_brush);

            GeometryModel3D model = new GeometryModel3D(
                mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }
        private Vector3D CalculateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D v0 = new Vector3D(
                p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(
                p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }


       




        private void cubeButtonClick()
        {

    



            Model3DGroup cube = new Model3DGroup();
           /* Point3D p0 = new Point3D(0,     0,  0);
            Point3D p1 = new Point3D(12.8,  0,  0);
            Point3D p2 = new Point3D(12.8,  0,  12.8);
            Point3D p3 = new Point3D(0,     0,  12.8);
            Point3D p4 = new Point3D(0,     0.68, 0);
            Point3D p5 = new Point3D(12.8,  0.68, 0);
            Point3D p6 = new Point3D(12.8,     0.68, 12.8);
            Point3D p7 = new Point3D(0,     0.68, 12.8);*/

            Point3D p0 = new Point3D(0   ,    0, 0    );
            Point3D p1 = new Point3D(12.8,    0, 0    );
            Point3D p2 = new Point3D(12.8,    0, 8.94 );
            Point3D p3 = new Point3D(0   ,    0, 8.94 );
            Point3D p4 = new Point3D(0   , 0.544, 0    );
            Point3D p5 = new Point3D(12.8, 0.544, 0    );
            Point3D p6 = new Point3D(12.8, 0.28, 8.94 );
            Point3D p7 = new Point3D(0   , 0.28, 8.94 );


            Point3D sp0 = new Point3D(0,    0.544, 0);
            Point3D sp1 = new Point3D(12.8, 0.544, 0);
            Point3D sp2 = new Point3D(12.8, 0.28 , 8.94);
            Point3D sp3 = new Point3D(0,    0.28 , 8.94);
            Point3D sp4 = new Point3D(0,    0.68 , 0);
            Point3D sp5 = new Point3D(12.8, 0.68 , 0);
            Point3D sp6 = new Point3D(12.8, 0.42 , 8.94);
            Point3D sp7 = new Point3D(0   , 0.42 , 8.94);



            // Macbook bottom
            //front side triangles
            cube.Children.Add(CreateTriangleModel(p3, p2, p6));
            cube.Children.Add(CreateTriangleModel(p3, p6, p7));
            //right side triangles
            cube.Children.Add(CreateTriangleModel(p2, p1, p5));
            cube.Children.Add(CreateTriangleModel(p2, p5, p6));
            //back side triangles
            cube.Children.Add(CreateTriangleModel(p1, p0, p4));
            cube.Children.Add(CreateTriangleModel(p1, p4, p5));
            //left side triangles
            cube.Children.Add(CreateTriangleModel(p0, p3, p7));
            cube.Children.Add(CreateTriangleModel(p0, p7, p4));
            //top side triangles
            cube.Children.Add(CreateTriangleModel(p7, p6, p5));
            cube.Children.Add(CreateTriangleModel(p7, p5, p4));
            //bottom side triangles
            cube.Children.Add(CreateTriangleModel(p2, p3, p0));
            cube.Children.Add(CreateTriangleModel(p2, p0, p1));


            // Macbook screen 

            Model3DGroup screen = new Model3DGroup();

            screen.Children.Add(CreateTriangleModel(sp3, sp2, sp6));
            screen.Children.Add(CreateTriangleModel(sp3, sp6, sp7));
            //right side triangles
            screen.Children.Add(CreateTriangleModel(sp2, sp1, sp5));
            screen.Children.Add(CreateTriangleModel(sp2, sp5, sp6));
            //back side triangles
            screen.Children.Add(CreateTriangleModel(sp1, sp0, sp4));
            screen.Children.Add(CreateTriangleModel(sp1, sp4, sp5));
            //left side triangles
            screen.Children.Add(CreateTriangleModel(sp0, sp3, sp7));
            screen.Children.Add(CreateTriangleModel(sp0, sp7, sp4));
            //top side triangles
            screen.Children.Add(CreateTriangleModel(sp7, sp6, sp5));
            screen.Children.Add(CreateTriangleModel(sp7, sp5, sp4));
            //bottom side triangles
            screen.Children.Add(CreateTriangleModel(sp2, sp3, sp0));
            screen.Children.Add(CreateTriangleModel(sp2, sp0, sp1));





            ModelVisual3D model = new ModelVisual3D();
            model.Content = cube;

            ModelVisual3D screenModel = new ModelVisual3D();
            screenModel.Content = screen;


            this.macBookBottom = model;
            this.macBookModel = model;
            this.macBookScreen = screenModel;

            this.mainViewport.Children.Add(model);
            this.mainViewport.Children.Add(screenModel);

            
        }

        private void triangleButtonClick(object sender, RoutedEventArgs e)
        {

            MeshGeometry3D triangleMesh = new MeshGeometry3D();

            Point3D point0 = new Point3D(0, 0, 0);
            Point3D point1 = new Point3D(20, 0, 0);
            Point3D point2 = new Point3D(0, 0, 5);

            triangleMesh.Positions.Add(point0);
            triangleMesh.Positions.Add(point1);
            triangleMesh.Positions.Add(point2);

            triangleMesh.TriangleIndices.Add(0);
            triangleMesh.TriangleIndices.Add(2);
            triangleMesh.TriangleIndices.Add(1);

            Vector3D normal = new Vector3D(0, 1, 0);
            triangleMesh.Normals.Add(normal);
            triangleMesh.Normals.Add(normal);
            triangleMesh.Normals.Add(normal);


            
            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.Aqua));

            GeometryModel3D triangleModel = new GeometryModel3D(triangleMesh, material);

            ModelVisual3D model = new ModelVisual3D();

            model.Content = triangleModel;

            this.mainViewport.Children.Add(model);

            


        }

        

        private void xSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            Slider thisSlider = (Slider)sender;

            this.lookDirectionVector.X = thisSlider.Value;
            this.xValueLabel.Content = thisSlider.Value.ToString("0.##");


            ((PerspectiveCamera)this.mainViewport.Camera).LookDirection = this.lookDirectionVector;
        }

        private void ySliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            Slider thisSlider = (Slider)sender;

            this.lookDirectionVector.Y = thisSlider.Value;
            this.yValueLabel.Content = thisSlider.Value.ToString("0.##");

            ((PerspectiveCamera)this.mainViewport.Camera).LookDirection = this.lookDirectionVector;
        }


        private void zSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            Slider thisSlider = (Slider)sender;

            this.lookDirectionVector.Z = thisSlider.Value;
            this.zValueLabel.Content = thisSlider.Value.ToString("0.##");

            ((PerspectiveCamera)this.mainViewport.Camera).LookDirection = this.lookDirectionVector;
        }

        private void changeZCamera(double value)
        {
            this.lookDirectionVector.Z = value;
            //this.zValueLabel.Content = thisSlider.Value.ToString("0.##");


            this.positionPoint.Z = value;
            this.zPositionValueLabel.Content = value.ToString("0.##");

            ((PerspectiveCamera)this.mainViewport.Camera).Position = this.positionPoint;
        }

        private void xPositionSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider thisSider = (Slider)sender;
            this.positionPoint.X = thisSider.Value;
            this.xPositionValueLabel.Content = thisSider.Value.ToString("0.##");

            ((PerspectiveCamera)this.mainViewport.Camera).Position = this.positionPoint;
        }

        private void yPositionSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider thisSider = (Slider)sender;
            this.positionPoint.Y = thisSider.Value;
            this.yPositionValueLabel.Content = thisSider.Value.ToString("0.##");

            ((PerspectiveCamera)this.mainViewport.Camera).Position = this.positionPoint;
        }

        private void zPositionSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider thisSider = (Slider)sender;
            this.positionPoint.Z = thisSider.Value;
            this.zPositionValueLabel.Content = thisSider.Value.ToString("0.##");

            ((PerspectiveCamera)this.mainViewport.Camera).Position = this.positionPoint;
        }

        private void xRotationSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            Slider thisSlider = (Slider)sender;


            var rotateTransform3D = new RotateTransform3D();
            var axisAngleRotation3D = new AxisAngleRotation3D();

            axisAngleRotation3D.Axis = new Vector3D(1, 0, 0);
            axisAngleRotation3D.Angle = thisSlider.Value;

            rotateTransform3D.Rotation = axisAngleRotation3D;

            var myTransform3DGroup = new Transform3DGroup();
            myTransform3DGroup.Children.Add(rotateTransform3D);

            this.macBookScreen.Transform = myTransform3DGroup;


        }

        private void angleScreen(double angle) {


            var rotateTransform3D = new RotateTransform3D();
            var axisAngleRotation3D = new AxisAngleRotation3D();

            axisAngleRotation3D.Axis = new Vector3D(1, 0, 0);
            axisAngleRotation3D.Angle = angle;

            rotateTransform3D.Rotation = axisAngleRotation3D;

            var myTransform3DGroup = new Transform3DGroup();
            myTransform3DGroup.Children.Add(rotateTransform3D);

            this.macBookScreen.Transform = myTransform3DGroup;
        }


        private void zRotationSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider thisSlider = (Slider)sender;


            var rotateTransform3D = new RotateTransform3D();
            var axisAngleRotation3D = new AxisAngleRotation3D();

            axisAngleRotation3D.Axis = new Vector3D(0, 1, 0);
            axisAngleRotation3D.Angle = thisSlider.Value;

            rotateTransform3D.Rotation = axisAngleRotation3D;

            var myTransform3DGroup = new Transform3DGroup();
            myTransform3DGroup.Children.Add(rotateTransform3D);


            this.mainViewport.Camera.Transform = myTransform3DGroup;

           // this.macBookScreen.Transform = myTransform3DGroup;
           // this.macBookBottom.Transform = myTransform3DGroup;
        }
        
        private void pivotTheCamera(double angle) { 


            var rotateTransform3D = new RotateTransform3D();
            var axisAngleRotation3D = new AxisAngleRotation3D();

            axisAngleRotation3D.Axis = new Vector3D(0, 1, 0);
            axisAngleRotation3D.Angle = angle;

            rotateTransform3D.Rotation = axisAngleRotation3D;

            var myTransform3DGroup = new Transform3DGroup();
            myTransform3DGroup.Children.Add(rotateTransform3D);


            this.mainViewport.Camera.Transform = myTransform3DGroup;
        }


       

        
    }
}
