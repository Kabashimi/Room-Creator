using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace hubert_drogosz
{
    class HDSpeaker : ModelVisual3D
    {
        private ModelVisual3D speaker;
        private ModelVisual3D block;
        private DispatcherTimer dTimer;
        private Size3D size;
        private Random r = new Random();

        public HDSpeaker(Point3D center, Size3D size,Brush speakerTexture, BitmapImage blockTexture)
        {
            this.size = size;
            block = new ModelVisual3D();
            speaker = new ModelVisual3D();
            block.Content = HDModelHelpers.CubeModel(center, size, new BitmapImage(new Uri("Hubert_Drogosz/Resources/drewno.png", UriKind.Relative)));
            speaker.Content = HDModelHelpers.MemModel(new Point3D(center.X, center.Y + size.Y / 4, center.Z + size.Z / 2)
                , new Size3D(size.X / 2, size.Y / 4, size.Z / 4)
                , new SolidColorBrush(Colors.DarkGray));

            speaker.Transform = new TranslateTransform3D();
            dTimer = new DispatcherTimer();
            dTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dTimer.Tick += dTimer_Tick;
            dTimer.Start();

            this.Children.Add(speaker);
            this.Children.Add(block);
        }
        ~HDSpeaker()
        {
                dTimer.Stop();
        }
        private void dTimer_Tick(object sender, EventArgs e)
        {
            MoveIt(speaker, size.Z / 8 * r.NextDouble());
        }

        private void MoveIt(ModelVisual3D mv3dgl, double to)
        {

            TranslateTransform3D t = mv3dgl.Transform as TranslateTransform3D;
            DoubleAnimation da = new DoubleAnimation(to,
                new Duration(TimeSpan.FromMilliseconds(100)));
            da.DecelerationRatio = 1;
            t.BeginAnimation(TranslateTransform3D.OffsetZProperty, da);
        }

       
    }
}
