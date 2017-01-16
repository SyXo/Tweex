using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PornHub_BHF
{
    public partial class FormMain : Form
    {
        private PornSites.PornHub pornHub;
        private static List<Video> videos;
        private string selectedTitle;

        public FormMain()
        {
            InitializeComponent();
            selectedTitle = null;

            pornHub = new PornSites.PornHub(@"http://www.pornhub.com", "PornHub");
            Task.Factory.StartNew(InitializeCombo);
        }

        private void InitializeCombo()
        {            
            pornHub.GetCategories().categories
                .ToList()
                .ForEach(x => comboBoxCat.Invoke(new Action(() => comboBoxCat.Items.Add(x.category))));

            comboBoxCat.Invoke(new Action(() => comboBoxCat.SelectedIndex = 0));
        }

        private Bitmap GetImg(string imageLink)
        {
            Bitmap result;
            WebClient webClient = new WebClient();
            byte[] buffer = webClient.DownloadData(imageLink);
            MemoryStream stream = new MemoryStream(buffer);
            result = new Bitmap(stream);

            return result;
        }

        private void GetTitles()
        {
            listBox.Invoke(new Action(() => listBox.Items.Clear()));
            videos
                .ForEach(x =>
                {
                    Task.Factory.StartNew(() => x.GetImage());
                    listBox.Invoke(new Action(() => listBox.Items.Add(x.title)));
                });
        }

        private void GetVideo()
        {            
            var res = videos.First(x => x.title == selectedTitle);
            pictureBoxPreview.Invoke(new Action(() => pictureBoxPreview.Image = res.Image));
            textBoxLink.Invoke(new Action(() => textBoxLink.Text = res.url));
        }

        private void comboBoxCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            videos = pornHub.GetVideos(comboBoxCat.Text).videos.ToList();
            Task.Factory.StartNew(GetTitles);
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTitle = listBox.SelectedItem.ToString();
            Task.Factory.StartNew(GetVideo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(textBoxLink.Text);
        }
    }
}
