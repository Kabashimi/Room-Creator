using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Maciej_Galos
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MGWindow1 : Window, IGetFurnitureModel
    {
        
        private MGTextureViewModel textureViewModel = new MGTextureViewModel();
        private ModelVisual3D model;
        public ModelVisual3D getFurniture()
        {
            this.mainViewport.Children.Clear();
            return model;
        }
        public string getName()
        {
            return "Biurko Macieja 2014";
        }
        public void setModel(ModelVisual3D _model)
        {
            this.model = _model;
            this.mainViewport.Children.Add(this.model);
            this.mainViewport.UpdateLayout();
        }
        
        public MGWindow1()
        {
            InitializeComponent();
            textureViewModel = (MGTextureViewModel)this.DataContext;
            this.LoadTextures();
            X_silder.ValueChanged += this.Generate;
            Y_silder.ValueChanged += this.Generate;
            Z_silder.ValueChanged += this.Generate;
            Legs_distance_slider.ValueChanged += this.Generate;
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TranslateTransform3D translate = new TranslateTransform3D(s_o_x.Value, s_o_y.Value, s_o_z.Value);
            mainViewport.Camera.Transform = translate;
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TranslateTransform3D translate = new TranslateTransform3D(s_o_x.Value, s_o_y.Value, s_o_z.Value);
            AxisAngleRotation3D AxisAngleRotator_x = new AxisAngleRotation3D(new Vector3D(0, 1, 0), s_r_x.Value);
            AxisAngleRotation3D AxisAngleRotator_y = new AxisAngleRotation3D(new Vector3D(1, 0, 0), s_r_y.Value);
            AxisAngleRotation3D AxisAngleRotator_z = new AxisAngleRotation3D(new Vector3D(0, 0, 1), s_r_z.Value);
            Transform3DGroup Transform3DGroup_transform = new Transform3DGroup();
            Transform3DGroup_transform.Children.Add(new RotateTransform3D(AxisAngleRotator_x));
            Transform3DGroup_transform.Children.Add(new RotateTransform3D(AxisAngleRotator_y));
            Transform3DGroup_transform.Children.Add(new RotateTransform3D(AxisAngleRotator_z));
            Transform3DGroup_transform.Children.Add(translate);
            this.mainViewport.Camera.Transform = Transform3DGroup_transform;
            this.Light.Transform = Transform3DGroup_transform;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            s_o_x.Value = 0;
            s_o_y.Value = 0;
            s_o_z.Value = 0;
            s_r_x.Value = 0;
            s_r_y.Value = 0;
            s_r_z.Value = 0;
        }
       
        private Model3DGroup RectangleModel(Point3D p0, Point3D p1, Point3D p2, Point3D p3)
        {
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
            mesh.TextureCoordinates.Add(new Point(0, 1));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(1, 0));
            mesh.TextureCoordinates.Add(new Point(0, 0));
            ImageBrush colors_brush = new ImageBrush();
            colors_brush.ImageSource = new BitmapImage(new Uri(textureViewModel.Textures[cbtextrue.SelectedIndex].adress, UriKind.Relative));
            DiffuseMaterial material = new DiffuseMaterial(colors_brush);
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }

        private  Model3DGroup Cube(Point3D origin, Size3D size)
        {
            Model3DGroup model = new Model3DGroup();
            Point3D p0 = new Point3D(origin.X - size.X / 2, origin.Y - size.Y / 2, origin.Z + size.Z / 2);
            Point3D p1 = new Point3D(p0.X + size.X, p0.Y, p0.Z);
            Point3D p2 = new Point3D(p1.X, p1.Y + size.Y, p1.Z);
            Point3D p3 = new Point3D(p0.X, p2.Y, p2.Z);
            Point3D p4 = new Point3D(p0.X, p0.Y, p0.Z - size.Z);
            Point3D p5 = new Point3D(p1.X, p1.Y, p1.Z - size.Z);
            Point3D p6 = new Point3D(p2.X, p2.Y, p2.Z - size.Z);
            Point3D p7 = new Point3D(p3.X, p3.Y, p3.Z - size.Z);
            model.Children.Add(RectangleModel(p0, p1, p2, p3));
            model.Children.Add(RectangleModel(p1, p5, p6, p2));
            model.Children.Add(RectangleModel(p5, p4, p7, p6));
            model.Children.Add(RectangleModel(p4, p0, p3, p7));
            model.Children.Add(RectangleModel(p0, p4, p5, p1));
            model.Children.Add(RectangleModel(p3, p2, p6, p7));
            return model;
        }

       

        private void Generate(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.mainViewport.Children.Contains(model))
                this.mainViewport.Children.Remove(model);
            model = new ModelVisual3D();
            model.Content = Table(new Point3D(0, 0, 0), new Size3D(X_silder.Value, Y_silder.Value, Z_silder.Value));
            this.mainViewport.Children.Add(model);
        }
        

        private Model3DGroup Table(Point3D origin, Size3D size)
        {
            Model3DGroup model = new Model3DGroup();
            Size3D tableSize = new Size3D(size.X * 8, 1, size.Z * 8);
            Size3D legSize = new Size3D(size.X/2, size.Y * 5, size.Z/2);
            model.Children.Add(this.Cube(new Point3D(origin.X,origin.Y-legSize.Y/2,origin.Z), new Size3D(tableSize.X*Legs_distance_slider.Value/100-legSize.X,1,tableSize.Z*Legs_distance_slider.Value/100-legSize.Z)));
            model.Children.Add(this.Cube(origin,tableSize));
            model.Children.Add(this.Cube(new Point3D(origin.X - tableSize.X / 2 * Legs_distance_slider.Value / 100 + legSize.X, origin.Y - legSize.Y / 2, origin.Z - tableSize.Z / 2 * Legs_distance_slider.Value / 100 + legSize.Z), legSize));
            model.Children.Add(this.Cube(new Point3D(origin.X + tableSize.X / 2 * Legs_distance_slider.Value / 100 - legSize.X, origin.Y - legSize.Y / 2, origin.Z - tableSize.Z / 2 * Legs_distance_slider.Value / 100 + legSize.Z), legSize));
            model.Children.Add(this.Cube(new Point3D(origin.X - tableSize.X / 2 * Legs_distance_slider.Value / 100 + legSize.X, origin.Y - legSize.Y / 2, origin.Z + tableSize.Z / 2 * Legs_distance_slider.Value / 100 - legSize.Z), legSize));
            model.Children.Add(this.Cube(new Point3D(origin.X + tableSize.X / 2 * Legs_distance_slider.Value / 100 - legSize.X, origin.Y - legSize.Y / 2, origin.Z + tableSize.Z / 2 * Legs_distance_slider.Value / 100 - legSize.Z), legSize));
            return model;
        }
        private void LoadTextures()
        {
            textureViewModel.AddToList(new MGTexture("dark_wood_texture"));
            textureViewModel.AddToList(new MGTexture("light_wood_texture"));
            textureViewModel.AddToList(new MGTexture("metal_texture"));
            textureViewModel.AddToList(new MGTexture("plastic_texture"));
            textureViewModel.AddToList(new MGTexture("stone_texture"));
            cbtextrue.Items.Refresh();
            cbtextrue.SelectedIndex = 0;
        }

        private void cbtextrue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.mainViewport.Children.Contains(model))
                this.mainViewport.Children.Remove(model);
            model = new ModelVisual3D();
            model.Content = Table(new Point3D(0, 0, 0), new Size3D(X_silder.Value, Y_silder.Value, Z_silder.Value));
            this.mainViewport.Children.Add(model);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
