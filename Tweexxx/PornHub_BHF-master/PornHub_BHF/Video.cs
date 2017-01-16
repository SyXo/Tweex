using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;

namespace PornHub_BHF
{
    public class Video
    {
        public string title { get; set; }
        public string url { get; set; }
        public string default_thumb { get; set; }
        public Image Image { get; private set; }

        public void GetImage()
        {
            Image = GetImg();
        }

        private Bitmap GetImg()
        {
            Bitmap result;
            WebClient webClient = new WebClient();
            byte[] buffer = webClient.DownloadData(default_thumb);
            MemoryStream stream = new MemoryStream(buffer);
            result = new Bitmap(stream);

            return result;
        }
    }

    public class Videos
    {
        public IList<Video> videos { get; set; }
    }
}