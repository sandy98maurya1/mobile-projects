using Android.Content;
using Android.Graphics;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Drawing;
using Color = Xamarin.Forms.Color;

namespace AmIReady
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }
        Plugin.Media.Abstractions.MediaFile temp;
               
        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
            {
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                temp = photo;
            }
                
        }

        private void Process_Clicked(object sender, EventArgs e)
        {
            var memoryStream = new System.IO.MemoryStream();

            temp.GetStream().CopyTo(memoryStream);

            var imageBytes = memoryStream.ToArray();

            Bitmap img = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);

            int center = img.Width / 2;
            int topcenter = img.Height / 2 / 2;
            int bottomcenter = img.Height / 2 + topcenter;
            int shoescenter = img.Height - 100;

            var Tpixel = new Color(img.GetPixel(center, topcenter));
            var Bpixel = new Color(img.GetPixel(center, bottomcenter));
            var BSpixel = new Color(img.GetPixel(center, shoescenter));

            TextBox1.BackgroundColor = Color.FromRgb(Tpixel.R, Tpixel.G, Tpixel.B);
            TextBox2.BackgroundColor = Color.FromRgb(Bpixel.R, Bpixel.G, Bpixel.B);
            TextBox3.BackgroundColor = Color.FromRgb(BSpixel.R, BSpixel.G, BSpixel.B);

            //var pixel = new Color(img.GetPixel(center, topcenter));
            //var color = Color.FromRgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
        }
        
    }
}
