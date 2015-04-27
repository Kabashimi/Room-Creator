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

using Maciej_Galos;
using Adam_Jasiolek;
using kamil_florczyk;
using jan_dudek;
using hubert_drogosz;
using rafal_rogoda;
using MacbookModal.pbrewczynski;
using System.Windows.Media.Media3D;

namespace Room_Creator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String[] furnitureNames = { 
                                              "Biurko Macieja",
                                              "Krzesło",
                                              "Ławka do ćwiczeń",
                                              "Tapczan",
                                              "Wieża",
                                              "Lampa",
                                              "MacBook"
                                          };

        private ModelVisual3D mv3d;
        private double cameraRotation;
        public List<ModelVisual3D> furnitureModels = new List<ModelVisual3D>();


        public MainWindow()
        {
            InitializeComponent();
            mv3d = new ModelVisual3D();
            mv3d.Content = PrepareRoom();
            cameraRotation = 0;

            this.mainViewport.Children.Add(mv3d);
        }

        private void HandleFurnitureWindow(Window furnitureWindow)
        {
            if (furnitureWindow is IGetFurnitureModel)
            {
                furnitureWindow.ShowDialog();
                if (furnitureWindow.DialogResult.HasValue)
                {
                    furnitureModels.Add((furnitureWindow as IGetFurnitureModel).getFurniture());
                    FurnituresLoaded.Items.Add(furnitureNames[FurnituresCombobox.SelectedIndex]);
                    //mainViewport.Children.Add((furnitureWindow as IGetFurnitureModel).getFurniture());
                    mainViewport.Children.Add(furnitureModels[furnitureModels.Count - 1]); ///  BINDOWANIE  LIST ->> LISTBOX
                }
            }
        }

        private void FurnituresListbox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox listbox = sender as ComboBox;
            foreach (String furnitureName in furnitureNames)
            {
                listbox.Items.Add(furnitureName);
            }
        }

        private void FurnituresListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox listbox = sender as ComboBox;
            switch (listbox.SelectedIndex)
            {
                case 0: HandleFurnitureWindow(new MGWindow1()); break;
                case 1: HandleFurnitureWindow(new AJ_ChairModel()); break;
                case 2: HandleFurnitureWindow(new PSWindow()); break;
                case 3: HandleFurnitureWindow(new JDTapczan()); break;
                case 4: HandleFurnitureWindow(new HDHiFiWindow()); break;
                case 5: HandleFurnitureWindow(new RRwindow()); break;
                case 6: HandleFurnitureWindow(new PBWindow()); break;
                default: break;
            }
        }

       

        private Model3DGroup PrepareRoom()
        {
            Model3DGroup group = new Model3DGroup();

            double X = 10;
            double Y = 7;
            double Z = 10;
            Material Mat1 = new DiffuseMaterial(new SolidColorBrush(Colors.Brown));
            Material Mat2 = new DiffuseMaterial(new SolidColorBrush(Colors.LightSkyBlue));

            Point3D p0 = new Point3D(-X,0,-Z);
            Point3D p1 = new Point3D(-X,0,Z);
            Point3D p2 = new Point3D(X,0,Z);
            Point3D p3 = new Point3D(X,0,-Z);

            Point3D p4 = new Point3D(-X,Y,-Z);
            Point3D p5 = new Point3D(-X,Y,Z);
            Point3D p6 = new Point3D(X,Y,Z);
            Point3D p7 = new Point3D(X,Y,-Z);

            group.Children.Add(MakeOneWall(p0,p1,p2,p3,Mat1));
            group.Children.Add(MakeOneWall(p0, p4, p5, p1, Mat2));
            group.Children.Add(MakeOneWall(p4, p0, p3, p7, Mat2));
            group.Children.Add(MakeOneWall(p6, p7, p3, p2, Mat2));
            group.Children.Add(MakeOneWall(p5, p6, p2, p1, Mat2));

            return group;
        }

        private Model3DGroup MakeOneWall(Point3D p0, Point3D p1, Point3D p2, Point3D p3, Material Mat)
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

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            
        }

        private void RotateCamera(double rotX)
        {
            Transform3DGroup transform = new Transform3DGroup();
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), CamRot.Value)));

            this.mainViewport.Camera.Transform = transform;
        }

        private void CamRot_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RotateCamera(0);
        }

        private void FurnituresLoaded_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = FurnituresLoaded.SelectedIndex;
            //PRZESTAWIENIE SLIDERÓW
            PrepareSliders(index);
            
        }

        private void PrepareSliders(int index)
        {
            //Console.WriteLine(furnitureModels[index].Transform.Value.OffsetX);
            XTranzition.Value = furnitureModels[index].Transform.Value.OffsetX;
            YTranzition.Value = furnitureModels[index].Transform.Value.OffsetY;
            ZTranzition.Value = furnitureModels[index].Transform.Value.OffsetZ;
            Scale.Value = 1;
            Rotation.Value = 0;
        }

        private void ZTranzition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (FurnituresLoaded.SelectedIndex > -1)
                {
                    Transform3DGroup Transform3DGroup_transform = new Transform3DGroup();
                    Transform3DGroup_transform.Children.Add(new TranslateTransform3D(XTranzition.Value, YTranzition.Value, ZTranzition.Value));
                    Transform3DGroup_transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), Rotation.Value)));
                    Transform3DGroup_transform.Children.Add(new ScaleTransform3D(Scale.Value, Scale.Value, Scale.Value));
                    furnitureModels[FurnituresLoaded.SelectedIndex].Transform = Transform3DGroup_transform;
                }
            }
            catch (NullReferenceException ex)
            {

            }
        }

        

    }
}
