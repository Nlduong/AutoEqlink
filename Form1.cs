using AutoEqlink.ACWS;
using Emgu.CV.Ocl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using static AutoEqlink.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AutoEqlink
{
    public partial class Form1 : Form
    {
        DAL.DataAccessWS dal;
        bool clickRun = false;
        bool clickRun1 = false;
        public class Odds
        {
            public string Win { get; set; }
            public string Place { get; set; }
        }
        public Form1()
        {
            InitializeComponent();
            dal = new DAL.DataAccessWS();
        }
        bool isStop = false;
        private void btnStart_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                Auto();
            });
           
            if (clickRun == false)
            {
                btnStart.Text = "Running Screenshot";
                clickRun = true;
                isStop = false;
                t.Start();
            }
            else
            {
                btnStart.Text = "Run Screenshot";
                clickRun = false;
                isStop = true;
            }
           
        }
        void Auto()
        {
            var i = 0;
            List<string> devices = ADBHelper.GetDevices();
            foreach (var device in devices)
            {
               if(i==0)
                {
                    Task t = new Task(async () =>
                    {
                        while (isStop == false)
                        {
                            int totalhosre = int.Parse(txtTotalHorse.Text);

                            ADBHelper.ScreenShoot(device, false, "front.jpg");
                            lblActivity.Invoke((MethodInvoker)(() => lblActivity.Text = "screen shot Front"));
                           
                            delay(3);
                        }
                    });

                    t.Start();
                }
                else if (i == 1)
                {
                    Task t = new Task(async () =>
                    {
                        while (isStop == false)
                        {
                            int totalhosre = int.Parse(txtTotalHorse.Text);
                            ADBHelper.ScreenShoot(device, false, "end.jpg");
                            lblActivity.Invoke((MethodInvoker)(() => lblActivity.Text = "screen shot End"));
                            delay(1);
                        }

                    });

                    t.Start();
                }
                else
                {
                    Task t = new Task(async () =>
                    {
                        while (isStop == false)
                        {
                            int totalhosre = int.Parse(txtTotalHorse.Text);

                            ADBHelper.ScreenShoot(device, false, "end_final.jpg");
                            lblActivity.Invoke((MethodInvoker)(() => lblActivity.Text = "screen shot End -1"));
                            delay(1);
                        }

                    });

                    t.Start();
                }
                    i++;
            }
        }

        void delay(int delay)
        {
            while (delay > 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                delay--;
                //lblWaitingTime.Invoke((MethodInvoker)(() => lblWaitingTime.Text = delay.ToString()));
                if (isStop)
                    break;
            }

        }
        private List<Odds> GetImageToText(string imagePath)
        {           
            try
            {
                // Crop vùng màu đỏ (tọa độ x, y, width, height)
                Rectangle cropArea = new Rectangle(1800, 250, 350, 3400);
                // 👉 bạn tự chỉnh lại cho đúng với vùng đỏ

                if(imagePath == "./InputImage/end.jpg")
                {
                    cropArea = new Rectangle(620, 320, 270, 1200);
                }
                if (imagePath == "./InputImage/end_final.jpg")
                {
                    cropArea = new Rectangle(620, 320, 270, 1200);
                }
                Bitmap src = new Bitmap(imagePath);
                Bitmap cropped = src.Clone(cropArea, src.PixelFormat);

                // Lưu ảnh crop ra file mới (PNG hoặc JPG tuỳ chọn)
                string savePath = @"InputImage\cropped1.png";
                cropped.Save(savePath);
             
                Bitmap pre = Preprocess(cropped);
                //pre.Save("./InputImage/debug_after.png"); // để xem thử ảnh đã xử lý
             
                string tessPath = @"./tessdata";  // thư mục chứa tessdata
                var ocr = new TesseractEngine(tessPath, "eng", EngineMode.Default);
                var page = ocr.Process(pre);
                string rawData = page.GetText(); 
                List<Odds> odds = new List<Odds>();

                foreach (string line in rawData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string trimmed = line.Trim();
                    if (trimmed.Equals("Scratched", StringComparison.OrdinalIgnoreCase))
                    {                     
                        string win = "SCR";
                        string place = "SCR";                       
                        odds.Add(new Odds { Win = win, Place = place });
                    }
                  


                     string[] parts = trimmed.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        if (double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double win) &&
                            double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double place))
                        {
                            odds.Add(new Odds { Win = win.ToString(), Place = place.ToString() });

                        }
                        else if (double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double win1) && parts[1] == "-")
                        {
                            odds.Add(new Odds { Win = win1.ToString(), Place = "0" });

                        }
                        else if (double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double place1) &&  parts[0] == "-")
                        {
                            odds.Add(new Odds { Win = "0", Place = place1.ToString() });

                        }
                        else if (double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double win2) && parts[1] == "=")
                        {
                            odds.Add(new Odds { Win = win2.ToString(), Place = "0" });

                        }
                    }
                    else
                    {
                        if (double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double win))
                        {
                            odds.Add(new Odds { Win = win.ToString(), Place = "0" });

                        }                        
                    }
                }
               
                return odds;
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ OCR lỗi:");
                Console.WriteLine(ex.ToString());
                return null;
            }
        }  
        bool isStopCap = false;
        private void btnCapture_Click(object sender, EventArgs e)
        {
           
            if (clickRun1 == false)
            {
                btnCapture.Text = "Run Capture";
                clickRun1 = true;
                isStopCap = false;
                Task t = new Task(() =>
                {
                    CaputerData();
                });
                t.Start();
            }
            else
            {
                btnCapture.Text = "Stop Capture";
                clickRun1 = false;
                isStopCap = true;
            }
           

        }
        void CaputerData()
        {
            try
            {
                Task t = new Task(async () =>
                {
                    while (isStopCap == false)
                    {
                        lblTotalList.Invoke((MethodInvoker)(() => lblTotalList.Text = "Read image"));
                        delay(1);
                        
                        string imageFrontPath = "./InputImage/front.jpg"; ; // ảnh gốc
                        var listFront = GetImageToText(imageFrontPath);
                       
                        List<LiveTote> _liveTote = new List<LiveTote>();
                        int totalHorse = int.Parse(txtTotalHorse.Text);
                        if (listFront.Count == 0)
                        {
                            break;
                        }
                        var merged = new List<Odds>();
                        if (chkLDPlayer3.Checked)
                        {
                            string imageEndPath = "./InputImage/end.jpg"; ; // ảnh gốc
                            var listEnd = GetImageToText(imageEndPath);                           
                            merged = MergeOdds(listFront, listEnd,  totalHorse);
                        }
                        else
                        {
                             merged = MergeOdds(listFront, new List<Odds>(), totalHorse);
                        }
                        
                        for (int i = 0; i < merged.Count; i++)
                        {
                            LiveTote lt = new LiveTote()
                            {

                                Win = (merged[i].Win == "SCR") ? "-1" : merged[i].Win,
                                RaceNo = DateTime.Now.ToString("yyMMdd") + (i <= 8 ? '0' + (i + 1).ToString() : (i + 1).ToString()),
                                Place = (merged[i].Place == "SCR") ? "-1" : merged[i].Place,
                                RaceDay = DateTime.Now.ToString("yyyyMMdd")
                            };
                            _liveTote.Add(lt);
                        }
                        List<LiveTote> listtest = _liveTote.OrderBy(i => i.RaceNo).ToList();
                        foreach (var item in listtest)
                        {
                            item.RaceBoard = txtRB2.Text;
                            item.RaceCountry = txtRC2.Text;
                            item.RaceCard = txtRC2.Text + item.RaceDay;
                            item.txday = item.RaceDay;
                            item.Win = (item.Win != "-1") ? ((float.Parse(item.Win) >= 999 || item.Win == "0") ? "999" : ParseWinPlace(item.Win)) : "-1";
                            item.Place = (item.Place != "-1") ? ((float.Parse(item.Place) >= 999 || item.Place == "0") ? "999" : ParseWinPlace(item.Place)) : "-1";
                            ///lblTotalList.Invoke((MethodInvoker)(() => lblTotalList.Text = "RaceNo: " + item.RaceNo + "  Win:" + item.Win + "-Place: " + item.Place));
                            //delay(1);
                        }
                        lblTotalList.Invoke((MethodInvoker)(() => lblTotalList.Text = "Save data to livetote"));
                        delay(2);
                        if (listtest.Count >0)
                        {
                            dal.SaveLiveToteList(listtest);
                        }
                        
                    }
                });
                t.Start();
            }
            catch ( Exception ex)
            {
                btnCapture.Text = "Stop Capture";
                clickRun1 = false;
                isStopCap = true;
            }
        }
        public static List<Odds> MergeOdds(List<Odds> list1, List<Odds> list2, int total)
        {
            var result = new List<Odds>(capacity: total);

            if (list2.Count == 0)
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    if (i < total)
                    {
                        string win = list1[i].Win;
                        string place = list1[i].Place;
                        result.Add(new Odds { Win = NormalizeNumber(win), Place = NormalizeNumber(place) });
                    }
                }

            }
            else
            {
                for (int i = 0; i < list1.Count; i++)
                {
                   
                        string win = list1[i].Win;
                        string place = list1[i].Place;
                        result.Add(new Odds { Win = NormalizeNumber(win), Place = NormalizeNumber(place) });
                }

                for (int j = 0; j < total - (list1.Count); j++)

                {
                    int index = list2.Count - (total - list1.Count) + j;
                    string win = list2[index].Win;
                    string place = list2[index].Place;
                    result.Add(new Odds { Win = NormalizeNumber(win), Place = NormalizeNumber(place) });
                }
            }

             return result;
        }

        private static bool IsScr(string s)
            => string.IsNullOrWhiteSpace(s) || s.Trim().Equals("SCR", StringComparison.OrdinalIgnoreCase);

        /// Chuẩn hoá số: sửa lỗi OCR "430" -> "4.30", "400" -> "4.00".
        private static string NormalizeNumber(string s)
        {
            if (IsScr(s)) return "SCR";
            s = s.Trim();

            // bỏ ký tự lạ, giữ số và dấu chấm
            var cleaned = "";
            foreach (char c in s)
                if (char.IsDigit(c) || c == '.') cleaned += c;

            if (string.IsNullOrEmpty(cleaned)) return "SCR";

            // nếu không có dấu chấm và dài >= 3 thì chèn dấu trước 2 số cuối
            if (!cleaned.Contains('.') && cleaned.Length >= 3)
                cleaned = cleaned.Insert(cleaned.Length - 2, ".");

            // parse lại về định dạng 0.## (tuỳ bạn đổi format)
            if (double.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
                return d.ToString("0.##", CultureInfo.InvariantCulture);

            return cleaned; // fallback
        }
        public static string ParseWinPlace(string winValue)
        {
            if (string.IsNullOrWhiteSpace(winValue) || winValue == "SCR")
                return "-1";

            if (float.TryParse(winValue, out float val))
            {
                if (val >= 999)
                    return "999";

                return ((val * 10) / 2).ToString();
            }

            // Trường hợp OCR đọc ra dấu "-" hay ký tự lạ thì trả về -1
            return "-1";
        }

        public static Bitmap Preprocess(Bitmap src)
        {
            Bitmap gray = new Bitmap(src.Width, src.Height);

            // Chuyển grayscale
            using (Graphics g = Graphics.FromImage(gray))
            {
                var colorMatrix = new System.Drawing.Imaging.ColorMatrix(
                    new float[][]
                    {
                new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                new float[] {0.59f,0.59f,0.59f,0,0},
                new float[] {0.11f,0.11f,0.11f,0,0},
                new float[] {0,0,0,1,0},
                new float[] {0,0,0,0,1}
                    });

                var attrs = new ImageAttributes();
                attrs.SetColorMatrix(colorMatrix);

                g.DrawImage(src, new Rectangle(0, 0, src.Width, src.Height),
                    0, 0, src.Width, src.Height,
                    GraphicsUnit.Pixel, attrs);
            }

            // Áp dụng threshold (biến thành đen trắng)
            for (int y = 0; y < gray.Height; y++)
            {
                for (int x = 0; x < gray.Width; x++)
                {
                    Color c = gray.GetPixel(x, y);
                    int l = (c.R + c.G + c.B) / 3;
                    if (l > 160) gray.SetPixel(x, y, Color.White);
                    else gray.SetPixel(x, y, Color.Black);
                }
            }

            return gray;
        }
    }
}
