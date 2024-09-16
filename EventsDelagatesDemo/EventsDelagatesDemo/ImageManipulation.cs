using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelagatesDemo
{
    internal class ImageManipulation
    {
        // Manipulate method (bitmap)
        public class ImageEventArgs : EventArgs
        {
            public Bitmap bmp { get; set; }
        }
        public void Manipulate(Bitmap bmp)
        {
            Color theColor = new Color();

            for(int i=0;i<bmp.Width; i++)
            {
                for(int j=0;j<bmp.Height; j++)
                {
                    // Get the color of the pixel at (i,j)
                    theColor =bmp.GetPixel(i,j);

                    // Change the color at that pixel; zero out the blue
                    Color newColor = Color.FromArgb(theColor.R, theColor.G, 0); //파란색=0

                    // Set the new color of that pixel
                    bmp.SetPixel(i,j,newColor);
                }
            }

            // Call the method to publish the event
            OnImageFinished(bmp);
        }

        // Define the EventHandler Delegate
        public event EventHandler<ImageEventArgs> ImageFinished;

        // Add Event Method
        protected virtual void OnImageFinished(Bitmap bmp)
        {
            ImageFinished?.Invoke(this, new ImageEventArgs() { bmp = bmp });
        }
    }
}
