using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button0_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            var textBoxResults = textBox1;

            textBoxResults.Text = "Doing other thinds while waiting for that slow dude...\r\n";
            textBoxResults.Text = ".......";
            textBoxResults.Text += SlowDude();

            (sender as Button).Enabled = true;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            var textBoxResults = textBox1;

            var slowTask = Task<string>.Factory.StartNew(() => SlowDude());
            textBoxResults.Text = "Doing other thinds while waiting for that slow dude...\r\n";
            var res = await slowTask;
            textBoxResults.Text =".......";
            textBoxResults.Text += res;

            (sender as Button).Enabled = true;

        }

        private string SlowDude()
        {
            Thread.Sleep(4000);
            return "Ta-dam! Here I am!\r\n";
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            var textBoxResults = textBox1;

            textBoxResults.Text = "Doing other thinds while waiting for that slow dude...\r\n";
            var res= SlowDude2();
            textBoxResults.Text = ".......";
            textBoxResults.Text += await res;

            (sender as Button).Enabled = true;

        }

        async Task<string> SlowDude2()
        {
            return await Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(4000);
                return "Ta-dam! Here I am!\r\n";
            });

            //Task<string> task = Task<string>.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(2000);
            //    return "Ta-dam! Here I am!\r\n";
            //});
            //return await task;

        }

        private async void buttonSleep_Click(object sender, EventArgs e) {

            (sender as Button).Enabled = false;
            var textBoxResults = textBox1;

            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            /**************/
            //await Sleep(1);
            //await Sleep(1);
            //await Sleep(1);

            //比上面快
            var s1 = Sleep(1);
            var s2 = Sleep(1);
            var s3 = Sleep(1);
            await s1;
            await s2;
            await s3;
            /**************/
            sw.Stop();
            string result = sw.Elapsed.TotalMilliseconds.ToString();
            textBoxResults.Text = result;

            (sender as Button).Enabled = true;
        }

        async Task Sleep(int sec)
        {
            //會另開thread
            //await Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(sec * 1000);
            //});

            //沒有另開thread
            await Task.Delay(sec * 1000);
        }


        private void buttonContent1_Click(object sender, EventArgs e) { 
            (sender as Button).Enabled = false;
            var textBoxResults = textBox1;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            ContentManagement service = new ContentManagement();
            var content = service.GetContent();
            var count = service.GetCount();
            var name = service.GetName();
            watch.Stop();
            textBoxResults.Text = watch.ElapsedMilliseconds.ToString();

            (sender as Button).Enabled = true;
        }

        private async void buttonContent2_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            var textBoxResults = textBox1;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            ContentManagement service = new ContentManagement();
            var contentTask = service.GetContentAsync();
            var countTask = service.GetCountAsync();
            var nameTask = service.GetNameAsync();

            var content = await contentTask;
            var count = await countTask;
            var name = await nameTask;
            watch.Stop();
            textBoxResults.Text = watch.ElapsedMilliseconds.ToString();

            (sender as Button).Enabled = true;
        }
    }



    public class ContentManagement
    {
        public string GetContent()
        {
            Thread.Sleep(2000);
            return "content";
        }

        public int GetCount()
        {
            Thread.Sleep(5000);
            return 4;
        }

        public string GetName()
        {
            Thread.Sleep(3000);
            return "Matthew";
        }
        public async Task<string> GetContentAsync()
        {
            await Task.Delay(2000);
            return "content";
        }

        public async Task<int> GetCountAsync()
        {
            await Task.Delay(5000);
            return 4;
        }

        public async Task<string> GetNameAsync()
        {
            await Task.Delay(3000);
            return "Matthew";
        }
    }
}
