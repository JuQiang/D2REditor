using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace D2REditorLauncher
{
    public partial class MainForm : Form
    {
        private long cur = -1, total = 1;
        public string CacheFolder, Host;
        private string resfile, downloadInfo;
        private double Version = 0.0;
        private SolidBrush textBrush;
        private delegate void DownloadProgressHandler(object sender, DownloadProgressEventArgs e);
        private delegate void TotalSizeHandler(object sender, TotalSizeEventArgs e);

        private event DownloadProgressHandler DownloadProgress;
        private event TotalSizeHandler TotalSize;

        private void MainForm_Load(object sender, EventArgs e)
        {
            Init();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.Paint += MainForm_Paint;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public void Init()
        {
            CacheFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\D2REditor";
            if (!Directory.Exists(CacheFolder)) Directory.CreateDirectory(CacheFolder);
            if (!Directory.Exists(CacheFolder + "\\backup")) Directory.CreateDirectory(CacheFolder + "\\backup");
            if (!Directory.Exists(CacheFolder + "\\update")) Directory.CreateDirectory(CacheFolder + "\\update");

            textBrush = new SolidBrush(Color.FromArgb(199, 179, 119));

            var lines = File.ReadAllLines(Environment.CurrentDirectory + "\\settings.txt");
            foreach (var line in lines)
            {
                if (line.StartsWith("host=")) { this.Host = line.Substring(5); }
                if (line.StartsWith("version=")) { this.Version = Convert.ToDouble(line.Substring(8)); }
            }

            resfile = this.CacheFolder + @"\resources.zip";
            downloadInfo = this.CacheFolder + "\\downloadinfo.txt";
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (cur < 0) return;
            long index = (cur - 1) * 10 / total;

            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            var fname = String.Format(@"{0}\Cain\{1:d4}.bmp", this.CacheFolder, index);
            g.DrawImage(Image.FromFile(fname), new Rectangle(0, 0, btnStart.Width, btnStart.Height));

            using (var format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;

                var s = String.Format("首次使用\r\n处理图片: {0}/{1}", cur, total);
                var sf = g.MeasureString(s, this.Font);
                g.DrawString(s, this.Font, this.textBrush, new RectangleF(0, this.Height - 120, btnStart.Width, sf.Height), format);
            }

            e.Graphics.DrawImage(bmp, 0, 0);
        }

        public bool CheckUpdate(ref List<string> files)
        {
            bool need = true;

            var url = String.Format("{0}/ui/updateinfo.txt", this.Host);
            using (WebClient wc = new WebClient())
            {
                var updateInfo = wc.DownloadString(url);
                var lines = updateInfo.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                //只要服务器端的版本号和launcher的版本号不一致，认为就需要更新。
                //第一行默认就是版本号，以下是具体的文件。
                if (lines.Length == 0)
                {
                    need = false;
                }
                else if (Convert.ToDouble(lines[0]) == this.Version)
                {
                    need = false;
                }
                else
                {
                    this.Version = Convert.ToDouble(lines[0]);
                    for (int i = 1; i < lines.Length; i++) files.Add(lines[i].Trim());
                }
            }

            return need;
        }

        public bool IsDownloadCompleted()
        {
            long totalSize = 0;
            if (File.Exists(this.downloadInfo)) totalSize = Convert.ToInt64(File.ReadAllText(this.downloadInfo));

            return (File.Exists(resfile) && (new FileInfo(resfile)).Length == totalSize);
        }

        private void DownloadResourcesForFirstTimeStart()
        {
            var remote = String.Format("{0}/ui/resources.zip", this.Host);

            var request = WebRequest.Create(remote) as HttpWebRequest;
            request.Method = "GET";

            btnStart.Text = "连接中……";
            Application.DoEvents();

            this.DownloadProgress += MainForm_DownloadProgress;
            this.TotalSize += MainForm_TotalSize;
            while (!Download(resfile, remote))
            {
                btnStart.Text = "网络不佳，重试中……";
                Application.DoEvents();
            }

            btnStart.Text = "解压缩中……";
            Application.DoEvents();
            ZipFile.ExtractToDirectory(resfile, this.CacheFolder);

            Application.DoEvents();
            List<string> allfiles = new List<string>();
            GetAllSprites(this.CacheFolder, ref allfiles);
            Application.DoEvents();

            btnStart.Visible = false;

            var cain = this.CacheFolder + @"\Cain";

            cur = 1;
            total = allfiles.Count;

            foreach (var file in allfiles)
            {
                Application.DoEvents();
                var newfile = file.Replace(".sprite", ".png");
                if (!File.Exists(newfile))
                {
                    Sprite2Png(file);
                }

                ++cur;
                this.Invalidate();
            }
        }

        private void MainForm_TotalSize(object sender, TotalSizeEventArgs e)
        {
            File.WriteAllText(this.downloadInfo, e.TotalSize.ToString());
        }

        private void MainForm_DownloadProgress(object sender, DownloadProgressEventArgs e)
        {
            btnStart.Text = String.Format("下载中，{0}%", e.Progress);
            Application.DoEvents();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            if (!IsDownloadCompleted()) DownloadResourcesForFirstTimeStart();

            string updateInfo = String.Empty;
            List<string> files = new List<string>();

            if (CheckUpdate(ref files))
            {
                //var url = String.Format("{0}/ui/app.zip", this.Host);
                //var appzip = this.CacheFolder + "\\app.zip";
                //if (File.Exists(appzip)) File.Delete(appzip);

                //using (WebClient wc = new WebClient())
                //{
                //    wc.DownloadFile(url, appzip);
                //}

                //if (Directory.Exists(this.CacheFolder + "\\update"))
                //{
                //    Directory.Delete(this.CacheFolder + "\\update", true);
                //    Directory.CreateDirectory(this.CacheFolder + "\\update");
                //}
                //ZipFile.ExtractToDirectory(appzip, this.CacheFolder + "\\update");

                foreach (var file in files)
                {
                    using (WebClient wc = new WebClient())
                    {
                        wc.DownloadFile(String.Format("{0}/ui/{1}", this.Host, file), this.CacheFolder + "\\" + file);
                    }
                    //File.Copy(this.CacheFolder + "\\update\\" + file, this.CacheFolder + "\\" + file, true);
                }

                List<string> settings = new List<string>();
                settings.Add("host=" + this.Host);
                settings.Add("version=" + Convert.ToString(this.Version));

                File.WriteAllLines(Environment.CurrentDirectory + "\\settings.txt", settings.ToArray());
            }

            QuitNow();
        }

        public void QuitNow()
        {
            this.Visible = false;
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.FileName = this.CacheFolder + "\\D2REditor.exe";
            psi.WorkingDirectory = this.CacheFolder;
            psi.Arguments = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();

            Process p = new Process();
            p.StartInfo = psi;
            p.Start();

            p.WaitForExit(-1);

            this.Close();
        }

        private void GetAllSprites(string path, ref List<string> allfiles)
        {
            var dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                GetAllSprites(dir, ref allfiles);
            }

            var files = Directory.GetFiles(path, "*.sprite");
            foreach (string file in files)
            {
                allfiles.Add(file);
            }
        }

        private Bitmap Sprite2Png(string fileName)
        {
            Bitmap bmp = null;

            var fname = fileName.Replace(".sprite", ".png");
            int hash = fileName.GetHashCode();

            if (!File.Exists(fname))
            {
                var bytes = File.ReadAllBytes(fileName);
                int x, y;
                var version = BitConverter.ToUInt16(bytes, 4);
                var width = BitConverter.ToInt32(bytes, 8);
                var height = BitConverter.ToInt32(bytes, 0xC);
                bmp = new Bitmap(width, height);

                if (version == 31)
                {   // regular RGBA
                    for (x = 0; x < height; x++)
                    {
                        for (y = 0; y < width; y++)
                        {
                            var baseVal = 0x28 + x * 4 * width + y * 4;
                            bmp.SetPixel(y, x, Color.FromArgb(bytes[baseVal + 3], bytes[baseVal + 0], bytes[baseVal + 1], bytes[baseVal + 2]));
                        }
                    }
                }
                else if (version == 61)
                {   // DXT
                    var tempBytes = new byte[width * height * 4];
                    Dxt.DxtDecoder.DecompressDXT5(bytes, width, height, tempBytes);
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            var baseVal = (y * width) + (x * 4);
                            bmp.SetPixel(x, y, Color.FromArgb(tempBytes[baseVal + 3], tempBytes[baseVal], tempBytes[baseVal + 1], tempBytes[baseVal + 2]));
                        }
                    }
                }

                bmp.Save(fname);
            }

            return bmp;
        }

        public bool Download(string localFile, string remoteFile)
        {
            bool succ = false;
            long start = 0;
            FileStream fs = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream stream = null;
            long totalSize = 0;

            if (File.Exists(localFile))
            {
                fs = File.OpenWrite(localFile);
                start = fs.Length;
                fs.Seek(start, SeekOrigin.Begin);
            }
            else
            {
                fs = new FileStream(localFile, FileMode.Create);
                start = 0;
            }



            byte[] buf = new byte[1 << 20];
            int size = 0;
            long downloadedSize = start;

            try
            {
                request = WebRequest.Create(remoteFile) as HttpWebRequest;
                request.Timeout = 3000;
                request.AddRange(start);

                response = request.GetResponse() as HttpWebResponse;
                totalSize = response.ContentLength + start;
                if (this.TotalSize != null) TotalSize(this, new TotalSizeEventArgs(totalSize));

                stream = response.GetResponseStream();
                stream.ReadTimeout = 1000;
                stream.WriteTimeout = 1000;

                size = stream.Read(buf, 0, buf.Length);
                while (downloadedSize < totalSize)
                {
                    downloadedSize += size;
                    fs.Write(buf, 0, size);
                    if (this.DownloadProgress != null) DownloadProgress(this, new DownloadProgressEventArgs((int)(downloadedSize / (totalSize / 100))));
                    //Console.WriteLine("Downloading " + downloadedSize.ToString());

                    size = stream.Read(buf, 0, buf.Length);
                }

                succ = true;
            }
            catch
            {
                succ = false;
            }
            finally
            {
                if (fs != null) fs.Close();
                if (stream != null) stream.Close();
                if (response != null) response.Close();
            }

            return succ;
        }
    }

    public class DownloadProgressEventArgs : EventArgs
    {
        public DownloadProgressEventArgs(long progress)
        {
            this.Progress = progress;
        }
        public long Progress;
    }

    public class TotalSizeEventArgs : EventArgs
    {
        public TotalSizeEventArgs(long totalSize)
        {
            this.TotalSize = totalSize;
        }
        public long TotalSize;
    }
}
