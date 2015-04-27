using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace hubert_drogosz
{
    static class HDModelHelpers
    {
        public static Model3DGroup RectangleModel(Point3D p0, Point3D p1, Point3D p2, Point3D p3, BitmapImage texture)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);
            for (int i = 0; i < 3; i++)
                mesh.TriangleIndices.Add(i);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            mesh.TextureCoordinates.Add(new Point(0, 0));
            mesh.TextureCoordinates.Add(new Point(1, 0));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(0, 1));

            ImageBrush colorsBrush = new ImageBrush();
            colorsBrush.ImageSource = texture;
            colorsBrush.TileMode = TileMode.Tile;
            colorsBrush.Viewport = ((Rect)new RectConverter().ConvertFromString("0,0,0.5,0.5"));

            Material material = new DiffuseMaterial(colorsBrush);
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }
        public static Model3DGroup RectangleModel(Point3D p0, Point3D p1, Point3D p2, Point3D p3, Brush texture)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);
            for (int i = 0; i < 3; i++)
                mesh.TriangleIndices.Add(i);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            mesh.TextureCoordinates.Add(new Point(0, 0));
            mesh.TextureCoordinates.Add(new Point(1, 0));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(0, 1));


            Material material = new DiffuseMaterial(texture);
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }
        public static Model3DGroup TriangleModel(Point3D p0, Point3D p1, Point3D p2, Brush texture)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            for (int i = 0; i < 3; i++)
                mesh.TriangleIndices.Add(i);

            Material material = new DiffuseMaterial(texture);
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }
        public static Model3DGroup CubeModel(Point3D center, Size3D size, BitmapImage texture)
        {
            Point3D a = new Point3D(center.X - (size.X / 2), center.Y - (size.Y / 2), center.Z + (size.Z / 2));
            Point3D b = new Point3D(a.X + size.X, a.Y, a.Z);
            Point3D c = new Point3D(a.X + size.X, a.Y + size.Y, a.Z);
            Point3D d = new Point3D(a.X, a.Y + size.Y, a.Z);

            Point3D ap = new Point3D(a.X, a.Y, a.Z - size.Z);
            Point3D bp = new Point3D(b.X, b.Y, b.Z - size.Z);
            Point3D cp = new Point3D(c.X, c.Y, c.Z - size.Z);
            Point3D dp = new Point3D(d.X, d.Y, d.Z - size.Z);

            Model3DGroup group = new Model3DGroup();

            group.Children.Add(RectangleModel(a, b, c, d, texture));
            group.Children.Add(RectangleModel(b, bp, cp, c, texture));
            group.Children.Add(RectangleModel(ap, dp, cp, bp, texture));
            group.Children.Add(RectangleModel(a, d, dp, ap, texture));
            group.Children.Add(RectangleModel(a, ap, bp, b, texture));
            group.Children.Add(RectangleModel(d, c, cp, dp, texture));
            return group;
        }
        public static Model3DGroup CubeModel(Point3D center, Size3D size, Brush texture)
        {
            Point3D a = new Point3D(center.X - (size.X / 2), center.Y - (size.Y / 2), center.Z + (size.Z / 2));
            Point3D b = new Point3D(a.X + size.X, a.Y, a.Z);
            Point3D c = new Point3D(a.X + size.X, a.Y + size.Y, a.Z);
            Point3D d = new Point3D(a.X, a.Y + size.Y, a.Z);

            Point3D ap = new Point3D(a.X, a.Y, a.Z - size.Z);
            Point3D bp = new Point3D(b.X, b.Y, b.Z - size.Z);
            Point3D cp = new Point3D(c.X, c.Y, c.Z - size.Z);
            Point3D dp = new Point3D(d.X, d.Y, d.Z - size.Z);

            Model3DGroup group = new Model3DGroup();

            group.Children.Add(RectangleModel(a, b, c, d, texture));
            group.Children.Add(RectangleModel(b, bp, cp, c, texture));
            group.Children.Add(RectangleModel(ap, dp, cp, bp, texture));
            group.Children.Add(RectangleModel(a, d, dp, ap, texture));
            group.Children.Add(RectangleModel(a, ap, bp, b, texture));
            group.Children.Add(RectangleModel(d, c, cp, dp, texture));
            return group;
        }
        public static Model3DGroup MemModel(Point3D center, Size3D size, Brush texture)
        {
            Point3D a = new Point3D(center.X - (size.X / 2), center.Y - (size.Y / 2), center.Z + (size.Z / 2));
            Point3D b = new Point3D(a.X + size.X, a.Y, a.Z);
            Point3D c = new Point3D(a.X + size.X, a.Y + size.Y, a.Z);
            Point3D d = new Point3D(a.X, a.Y + size.Y, a.Z);

            Point3D ap = new Point3D(a.X, a.Y, a.Z - size.Z);
            Point3D bp = new Point3D(b.X, b.Y, b.Z - size.Z);
            Point3D cp = new Point3D(c.X, c.Y, c.Z - size.Z);
            Point3D dp = new Point3D(d.X, d.Y, d.Z - size.Z);

            Model3DGroup group = new Model3DGroup();

            group.Children.Add(TriangleModel(a, b, center, texture));
            group.Children.Add(TriangleModel(b, c, center, texture));
            group.Children.Add(TriangleModel(c, d, center, texture));
            group.Children.Add(TriangleModel(d, a, center, texture));

            group.Children.Add(RectangleModel(b, bp, cp, c, texture));
            group.Children.Add(RectangleModel(ap, dp, cp, bp, texture));
            group.Children.Add(RectangleModel(a, d, dp, ap, texture));
            group.Children.Add(RectangleModel(a, ap, bp, b, texture));
            group.Children.Add(RectangleModel(d, c, cp, dp, texture));
            return group;
        }
    }
}
