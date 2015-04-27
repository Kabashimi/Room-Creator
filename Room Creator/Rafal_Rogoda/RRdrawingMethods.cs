using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace rafal_rogoda
{
    class RRdrawingMethods
    {
        RRforDrawing meth = new RRforDrawing();

        public Model3DGroup DrawCover(Point3D orgin, Size3D size , bool IsOn)
        {
            Uri Light = new Uri("Rafal_Rogoda/Resources/LampshadeLIGHT.jpg", UriKind.Relative);
            Uri Darc = new Uri("Rafal_Rogoda/Resources/LampshadeDARC.jpg", UriKind.Relative);

            Point3D TrzonOrgin = new Point3D(orgin.X, orgin.Y + 0.25, orgin.Z);
            Size3D Trzon = new Size3D(size.X / 2, size.Y - size.Y / 20, size.Z / 2);

            return  groupDrawCover(TrzonOrgin, Trzon, IsOn?Light:Darc);
        }

        public Model3DGroup DrawCuboids(Point3D orgin, Size3D size)
        {
            Uri Metal = new Uri("Rafal_Rogoda/Resources/Metal.png", UriKind.Relative);

            orgin.Y = orgin.Y - 1;
            
            Model3DGroup cuboidmodel= new Model3DGroup();

            Point3D TrzonOrgin = new Point3D(orgin.X,orgin.Y+0.25 , orgin.Z);
            Size3D Trzon = new Size3D(size.X/2, size.Y-size.Y/20, size.Z/2);
            cuboidmodel.Children.Add(groupDrawCuboid(TrzonOrgin, Trzon, Metal));
            

            Point3D PodstawaOrgin = new Point3D(orgin.X, orgin.Y-size.Y/2+0.25, orgin.Z);
            Size3D Podstawa = new Size3D(size.X, size.Y/20, size.Z);
            cuboidmodel.Children.Add(groupDrawCuboid(PodstawaOrgin, Podstawa, Metal));

            return cuboidmodel;
        }


        private Model3DGroup groupDrawCuboid(Point3D orgin, Size3D size , Uri tex)
        {
            //przud laski
            Point3D A1 = new Point3D(orgin.X - size.X / 2, orgin.Y - size.Y / 2, orgin.Z - size.Z / 2);//A
            Point3D B1 = new Point3D(orgin.X + size.X / 2, orgin.Y - size.Y / 2, orgin.Z - size.Z / 2);//B
            Point3D C1 = new Point3D(orgin.X + size.X / 2, orgin.Y + size.Y / 2, orgin.Z - size.Z / 2);//C
            Point3D D1 = new Point3D(orgin.X - size.X / 2, orgin.Y + size.Y / 2, orgin.Z - size.Z / 2);//D
            //tył laski
            Point3D A2 = new Point3D(orgin.X - size.X / 2, orgin.Y - size.Y / 2, orgin.Z + size.Z / 2);//A'
            Point3D B2 = new Point3D(orgin.X + size.X / 2, orgin.Y - size.Y / 2, orgin.Z + size.Z / 2);//B'
            Point3D C2 = new Point3D(orgin.X + size.X / 2, orgin.Y + size.Y / 2, orgin.Z + size.Z / 2);//C'
            Point3D D2 = new Point3D(orgin.X - size.X / 2, orgin.Y + size.Y / 2, orgin.Z + size.Z / 2);//D'

            


            //trzon
            GeometryModel3D model1 = meth.rectangleModel(D1,C1,B1,A1 ,tex);
            GeometryModel3D model2 = meth.rectangleModel(A2,B2,C2,D2, tex);
            GeometryModel3D model3 = meth.rectangleModel(A1, B1, B2, A2, tex);
            GeometryModel3D model4 = meth.rectangleModel(C1, D1, D2, C2, tex);
            GeometryModel3D model5 = meth.rectangleModel(C1, C2, B2, B1, tex);
            GeometryModel3D model6 = meth.rectangleModel(A1, A2, D2, D1, tex);

           
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model1);//tył
            group.Children.Add(model2);//przud
            group.Children.Add(model3);//duł
            group.Children.Add(model4);//góra
            group.Children.Add(model5);//prawa
            group.Children.Add(model6);//lewa
            
            return group;

        }

        private Model3DGroup groupDrawCover(Point3D orgin, Size3D size, Uri col)
        {
            //duł klosza
            Point3D A1 = new Point3D((orgin.X) * 4,              (orgin.Y + size.Y / 5), (orgin.Z - size.Z / 2) * 4);//A
            Point3D B1 = new Point3D((orgin.X + size.X / 2) * 4, (orgin.Y + size.Y / 5), (orgin.Z - size.Z / 4) * 4);//B
            Point3D C1 = new Point3D((orgin.X + size.X / 2) * 4, (orgin.Y + size.Y / 5), (orgin.Z + size.Z / 4) * 4);//C
            Point3D D1 = new Point3D((orgin.X) * 4,              (orgin.Y + size.Y / 5), (orgin.Z + size.Z / 2) * 4);//D
            Point3D E1 = new Point3D((orgin.X - size.X / 2) * 4, (orgin.Y + size.Y / 5), (orgin.Z + size.Z / 4) * 4);//E
            Point3D F1 = new Point3D((orgin.X - size.X / 2) * 4, (orgin.Y + size.Y / 5), (orgin.Z - size.Z / 4) * 4);//F

            //góra klosza
            Point3D A2 = new Point3D((orgin.X) * 2 ,             (orgin.Y + size.Y / 2), (orgin.Z - size.Z / 2) * 2);//A'
            Point3D B2 = new Point3D((orgin.X + size.X / 2) * 2, (orgin.Y + size.Y / 2), (orgin.Z - size.Z / 4) * 2);//B'
            Point3D C2 = new Point3D((orgin.X + size.X / 2) * 2, (orgin.Y + size.Y / 2), (orgin.Z + size.Z / 4) * 2);//C'
            Point3D D2 = new Point3D((orgin.X) * 2,              (orgin.Y + size.Y / 2), (orgin.Z + size.Z / 2) * 2);//D'
            Point3D E2 = new Point3D((orgin.X - size.X / 2) * 2, (orgin.Y + size.Y / 2), (orgin.Z + size.Z / 4) * 2);//E'
            Point3D F2 = new Point3D((orgin.X - size.X / 2) * 2, (orgin.Y + size.Y / 2), (orgin.Z - size.Z / 4) * 2);//F'
            

            //GeometryModel3D model1 = meth.rectangleModel(A1, B1, B2, A2, col);
            GeometryModel3D model1 = meth.rectangleModel(A2, B2, B1, A1, col);
            GeometryModel3D model2 = meth.rectangleModel(B2, C2, C1, B1, col);
            GeometryModel3D model3 = meth.rectangleModel(C2, D2, D1, C1, col);
            GeometryModel3D model4 = meth.rectangleModel(D2, E2, E1, D1, col);
            GeometryModel3D model5 = meth.rectangleModel(E2, F2, F1, E1, col);
            GeometryModel3D model6 = meth.rectangleModel(F2, A2, A1, F1, col);
            

            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model1);//prawy tył
            group.Children.Add(model2);//prawa 
            group.Children.Add(model3);//prawy przód
            group.Children.Add(model4);//lewy przód
            group.Children.Add(model5);//lewa
            group.Children.Add(model6);//lewa tył
            
            return group;

        }

        
    }
}
