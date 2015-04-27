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

namespace Adam_Jasiolek
{
    class Texture
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
    class TextureViewModel
    {
        private List<Texture> list = new List<Texture>();
        public List<Texture> Textures { get { return list; } }
        public TextureViewModel()
        {
          list.Add(new Texture() { Name = "/Adam_Jasiolek/Resources/wood1.jpg" });
          list.Add(new Texture() { Name = "/Adam_Jasiolek/Resources/wood2.jpg" });
            
        }
    }
    /// <summary>
    /// Interaction logic for ChairModel.xaml
    /// </summary>
    public partial class AJ_ChairModel : Window , IGetFurnitureModel
    {
        private ModelVisual3D chairModel= new ModelVisual3D();
        private TextureViewModel textureViewModel;
        private double ChairLegsWidth = 0.1;
        private double ChairLegsHeight =0.4 ;
        private double ChairLegsLenght =0.1 ;
        private Point3D PositionOfSeat = new Point3D(0, 0, 0);
        public AJ_ChairModel()
        {

            InitializeComponent();
            textureViewModel = (TextureViewModel)this.DataContext;
            SliderOfRibs.ValueChanged += makeButtonClick;
            LengthSlider.ValueChanged += makeButtonClick;
            WidthSlider.ValueChanged += makeButtonClick;
            HeightSlider.ValueChanged += makeButtonClick;
            comboBoxOfTexture.SelectionChanged += comboBoxOfTexture_SelectionChanged;
           
        }
        void comboBoxOfTexture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            makeButtonClick();
        }
        public  ModelVisual3D getFurniture()
        {
            RemoveObjectFromMainViewPort();
            return chairModel;
        }
        private void RemoveObjectFromMainViewPort()
        {
            mainViewPort.Children.Remove(chairModel);
        }
        private void makeButtonClick()
        {
            mainViewPort.Children.Clear();
            ChairLegsHeight = 0.4 * HeightSlider.Value;
            ChairLegsLenght = 0.1 * LengthSlider.Value;
            ChairLegsWidth = 0.1 * WidthSlider.Value;
            chairModel.Content = ModelOfChair(new Point3D(0, 0, 0), new Size3D(WidthSlider.Value, HeightSlider.Value, LengthSlider.Value));
            this.mainViewPort.Children.Add(chairModel);
            mainViewPort.Children.Add(light);
        }
        private void makeButtonClick(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mainViewPort.Children.Clear();
            LabelOfAmount.Content = ((int)SliderOfRibs.Value).ToString();
            ChairLegsHeight = 0.4 * HeightSlider.Value;
            ChairLegsLenght = 0.1 * LengthSlider.Value;
            ChairLegsWidth = 0.1 * WidthSlider.Value;
            chairModel.Content = ModelOfChair(new Point3D(0, 0, 0), new Size3D(WidthSlider.Value, HeightSlider.Value, LengthSlider.Value));
            this.mainViewPort.Children.Add(chairModel);
            mainViewPort.Children.Add(light);
        }
        private Model3DGroup ModelOfChair(Point3D point,Size3D size){
            Model3DGroup group = new Model3DGroup() ;
            group.Children.Add(MakeChairLegs(point,size));
            group.Children.Add(MakeSeat(point,size));
            group.Children.Add(GetBack(point, size));
            return group;
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

            mesh.TextureCoordinates.Add(new Point(0, 0));
            mesh.TextureCoordinates.Add(new Point(1, 0));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(0, 1));

            ImageBrush colors_brush = new ImageBrush();
            colors_brush.ImageSource = new BitmapImage(new Uri((comboBoxOfTexture.SelectedItem as Texture).Name.Substring(1), UriKind.Relative));
            // Material material = new DiffuseMaterial(new SolidColorBrush(Colors.Red));
            // GeometryModel3D model = new GeometryModel3D(mesh, material);

            //Kafelkowanie
            // colors_brush.TileMode=TileMode.Tile;
            //colors_brush.Viewport=((Rect)new RectConverter().ConvertFromString("0,0,0.5,0.5");
            Material material = new DiffuseMaterial(colors_brush);
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }
        private Model3DGroup MakeChairLegs(Point3D point,Size3D size)
        {
            Model3DGroup model = new Model3DGroup();

            model.Children.Add(ModelOfRec(point, new Size3D(ChairLegsWidth, ChairLegsHeight, ChairLegsLenght)));
            model.Children.Add(ModelOfRec(new Point3D(point.X+size.X,point.Y,point.Z), new Size3D(ChairLegsWidth, ChairLegsHeight, ChairLegsLenght)));
            model.Children.Add(ModelOfRec(new Point3D(point.X + size.X, point.Y, point.Z-size.Z), new Size3D(ChairLegsWidth, ChairLegsHeight, ChairLegsLenght)));
            model.Children.Add(ModelOfRec(new Point3D(point.X , point.Y, point.Z-size.Z), new Size3D(ChairLegsWidth, ChairLegsHeight, ChairLegsLenght)));
            return model;
        }
        private void Slider_ValueChanged_2(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TranslateTransform3D translate = new TranslateTransform3D(SliderX.Value, SliderY.Value, SliderZ.Value);
            AxisAngleRotation3D AxisAngleRotator_rot_x = new AxisAngleRotation3D(new Vector3D(1, 0, 0), Rot_X.Value);
            AxisAngleRotation3D AxisAngleRotator_rot_y = new AxisAngleRotation3D(new Vector3D(0, 1, 0), Rot_Y.Value);
            AxisAngleRotation3D AxisAngleRotator_rot_z = new AxisAngleRotation3D(new Vector3D(0, 0, 1), Rot_Z.Value);
            Transform3DGroup Transform = new Transform3DGroup();
            Transform.Children.Add(new RotateTransform3D(AxisAngleRotator_rot_x));
            Transform.Children.Add(new RotateTransform3D(AxisAngleRotator_rot_y));
            Transform.Children.Add(new RotateTransform3D(AxisAngleRotator_rot_z));
            Transform.Children.Add(translate);
            this.mainViewPort.Camera.Transform = Transform;
            light.Transform = Transform;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Rot_X.Value = 0;
            Rot_Y.Value = 0;
            Rot_Z.Value = 0;
            SliderX.Value = 0;
            SliderY.Value = 0;
            SliderZ.Value = 0;
        }
        private  Model3DGroup ModelOfRec(Point3D origin, Size3D size)
        {
            Model3DGroup model = new Model3DGroup();
            Point3D p0 = new Point3D(origin.X - (size.X / 2), origin.Y - (size.Y / 2), origin.Z + (size.Z / 2));
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
        private Model3DGroup ModelOfPara(Point3D p0, Size3D size,double leng)
        {
            Model3DGroup model = new Model3DGroup();
            Point3D p1 = new Point3D(p0.X + size.X, p0.Y, p0.Z);
            Point3D p2 = new Point3D(p1.X, p1.Y + size.Y, p1.Z-leng);
            Point3D p3 = new Point3D(p0.X, p2.Y, p2.Z-leng);
            Point3D p4 = new Point3D(p0.X, p0.Y, p0.Z - size.Z);
            Point3D p5 = new Point3D(p1.X, p1.Y, p1.Z - size.Z);
            Point3D p6 = new Point3D(p2.X, p2.Y, p2.Z - size.Z-leng);
            Point3D p7 = new Point3D(p3.X, p3.Y, p3.Z - size.Z-leng);
            model.Children.Add(RectangleModel(p0, p1, p2, p3));
            model.Children.Add(RectangleModel(p1, p5, p6, p2));
            model.Children.Add(RectangleModel(p5, p4, p7, p6));
            model.Children.Add(RectangleModel(p4, p0, p3, p7));
            model.Children.Add(RectangleModel(p0, p4, p5, p1));
            model.Children.Add(RectangleModel(p3, p2, p6, p7));
            return model;
        }
        private Model3DGroup MakeSeat(Point3D point,Size3D size){
            Model3DGroup group =new Model3DGroup();
            group.Children.Add(ModelOfRec(new Point3D(WidthSlider.Value/2,ChairLegsHeight/2,0-(LengthSlider.Value/2)),new Size3D(2*ChairLegsWidth+WidthSlider.Value,0.06*HeightSlider.Value,LengthSlider.Value+2*ChairLegsLenght)));
            PositionOfSeat.X = 0;
            PositionOfSeat.Y = ChairLegsHeight / 2;
            PositionOfSeat.Z = 0 - LengthSlider.Value;
            return group;
         }
        private Model3DGroup GetBack(Point3D point, Size3D size)
        {
            int amount = (int)SliderOfRibs.Value;
            Model3DGroup group = new Model3DGroup();
            double widthBetween = (0.97*size.X )/((amount+1));
            Point3D p = new Point3D(PositionOfSeat.X , PositionOfSeat.Y + ((0.6*size.Y)/2), PositionOfSeat.Z + 0.1 * ChairLegsLenght);
            group.Children.Add(ModelOfRec(new Point3D(PositionOfSeat.X, PositionOfSeat.Y + (0.8 * size.Y / 2), PositionOfSeat.Z), new Size3D(ChairLegsWidth, 0.8 * size.Y, ChairLegsLenght)));
            group.Children.Add(ModelOfRec(new Point3D(PositionOfSeat.X+size.X, PositionOfSeat.Y + (0.8 * size.Y / 2), PositionOfSeat.Z), new Size3D(ChairLegsWidth, 0.8 * size.Y, ChairLegsLenght)));
            group.Children.Add(ModelOfRec(new Point3D(PositionOfSeat.X+size.X/2, PositionOfSeat.Y + (0.7 * size.Y), PositionOfSeat.Z),new Size3D(size.X-ChairLegsWidth,0.2*size.Y,ChairLegsLenght)));
            for (int i = 1; i < amount+1; i++)
            {
                group.Children.Add(ModelOfRec(new Point3D(p.X+i*widthBetween, p.Y, p.Z), new Size3D(0.8*ChairLegsWidth, 0.6 * size.Y, 0.8*ChairLegsLenght)));
            }
            return group;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            makeButtonClick();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
