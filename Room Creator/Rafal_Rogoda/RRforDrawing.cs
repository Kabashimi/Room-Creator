using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace rafal_rogoda
{
    public class RRforDrawing
    {

        /// <summary>
        /// projektuje jedną ścianę
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        internal GeometryModel3D rectangleModel(
            Point3D p0,
            Point3D p1,
            Point3D p2,
            Point3D p3,
            Uri texture)
        {
            //new Uri("Resources/goldentexture.jpg", UriKind.Relative)
            

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

            //mesh.Material material = new DiffuseMaterial(new SolidColorBrush(color));

            mesh.TextureCoordinates.Add(new Point(0, 0));
            mesh.TextureCoordinates.Add(new Point(1, 0));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(0, 1));

            ImageBrush color_brush = new ImageBrush();
            color_brush.ImageSource = new BitmapImage(texture);
            DiffuseMaterial material = new DiffuseMaterial(color_brush);

            GeometryModel3D model = new GeometryModel3D(mesh, material);

            return model;
        }

        internal GeometryModel3D hexagonModel(Point3D p0orgin, Point3D p1, Point3D p2, Point3D p3, Point3D p4, Point3D p5 ,Point3D p6, Color color)
        {

            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0orgin);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);
            mesh.Positions.Add(p4);
            mesh.Positions.Add(p5);
            mesh.Positions.Add(p6);

            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(0);
            

            

            Material material = new DiffuseMaterial(new SolidColorBrush(color));

            mesh.TextureCoordinates.Add(new Point(0, 0));
            mesh.TextureCoordinates.Add(new Point(1, 0));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(0, 1));

            //ImageBrush color_brush = new ImageBrush();
            //color_brush.ImageSource = new BitmapImage(new Uri("Resources/goldentexture.jpg", UriKind.Relative));
            //DiffuseMaterial material = new DiffuseMaterial(color_brush);

            GeometryModel3D model = new GeometryModel3D(mesh, material);

            return model;
        }
    }
}
