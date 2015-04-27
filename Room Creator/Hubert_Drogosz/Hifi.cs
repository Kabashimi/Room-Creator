using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace hubert_drogosz
{
    class HDHifi: ModelVisual3D
    {
        private ModelVisual3D block;
        private MediaPlayer mPlayer;
        private HDSpeaker leftSpeaker;
        private HDSpeaker rightSpeaker;

        public HDHifi(Point3D center, Size3D size, Brush blockTexture, Brush speakerTexture, BitmapImage speakerBlockTexture)
        {
            block = new ModelVisual3D();
            block.Content = HDModelHelpers.CubeModel(center, size,blockTexture);
            
            center.Offset(-size.X * 1.5, 0, 0);
            leftSpeaker = new HDSpeaker(center, size, speakerTexture, speakerBlockTexture);

            center.Offset(3* size.X, 0, 0);
            rightSpeaker = new HDSpeaker(center, size, speakerTexture, speakerBlockTexture);

            mPlayer = new MediaPlayer();
            this.Children.Add(block);
            this.Children.Add(leftSpeaker);
            this.Children.Add(rightSpeaker);

        }

        

        public void StartPlaying(Uri source)
        {
            mPlayer.Stop();
            mPlayer.Open(source);
            mPlayer.Play();
        }

        public void SetVolume(double newVal)
        {
            mPlayer.Volume = newVal;
        }
    }
}
