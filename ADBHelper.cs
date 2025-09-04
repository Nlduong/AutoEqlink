#region Assembly KAutoHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// G:\Project\ADPProject\KAutoHelper V2\KAutoHelper.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutoEqlink
{
    public class ADBHelper
    {
        private static string LIST_DEVICES = "adb devices";

        private static string TAP_DEVICES = "adb -s {0} shell input tap {1} {2}";

        private static string SWIPE_DEVICES = "adb -s {0} shell input swipe {1} {2} {3} {4} {5}";

        private static string KEY_DEVICES = "adb -s {0} shell input keyevent {1}";

        private static string INPUT_TEXT_DEVICES = "adb -s {0} shell input text \"{1}\"";

        private static string CAPTURE_SCREEN_TO_DEVICES = "adb -s {0} shell screencap -p \"{1}\"";

        private static string PULL_SCREEN_FROM_DEVICES = "adb -s {0} pull \"{1}\"";

        private static string REMOVE_SCREEN_FROM_DEVICES = "adb -s {0} shell rm -f \"{1}\"";

        private static string GET_SCREEN_RESOLUTION = "adb -s {0} shell dumpsys display | Find \"mCurrentDisplayRect\"";

        private const int DEFAULT_SWIPE_DURATION = 100;

        private static string ADB_FOLDER_PATH = "";

        private static string ADB_PATH = "";

        public static string SetADBFolderPath(string folderPath)
        {
            ADB_FOLDER_PATH = folderPath;
            ADB_PATH = folderPath + "\\adb.exe";
            if (!File.Exists(ADB_PATH))
            {
                return "ADB Path not Exits!!!";
            }

            return "OK";
        }

        public void SetTextFromClipboard(string deviceID, string text)
        {
            string[] array = text.Split(new string[1] { "\r\n" }, StringSplitOptions.None);
            int num = 0;
            string[] array2 = array;
            foreach (string text2 in array2)
            {
                string text3 = ExecuteCMDBat(deviceID, "adb -s " + deviceID + " shell am broadcast -a clipper.set -e text \"\\\"" + text2 + "\\\"\"");
                ExecuteCMD("adb -s " + deviceID + " shell input keyevent 279");
                num++;
                if (num < array.Length)
                {
                    Key(deviceID, ADBKeyEvent.KEYCODE_ENTER);
                }
            }
        }

        private void Note(string deviceID)
        {
            ExecuteCMD("adb -s " + deviceID + " shell am force-stop com.zing.zalo");
            string text = ExecuteCMD("adb -s " + deviceID + " shell rm -f /sdcard/Pictures/Images/*");
            text = ExecuteCMD("adb -s " + deviceID + " shell mkdir /sdcard/Pictures/Images");
            DirectoryInfo directoryInfo = new DirectoryInfo("C:\\images");
            IEnumerable<string> enumerable = from x in directoryInfo.GetFiles()
                                             select x.FullName;
            foreach (string item in enumerable)
            {
                ExecuteCMD("adb -s " + deviceID + " push " + item + " sdcard/Pictures/Images");
            }
        }

        public static string ExecuteCMD(string cmdCommand)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WorkingDirectory = ADB_FOLDER_PATH;
                processStartInfo.FileName = "cmd.exe";
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.Verb = "runas";
                process.StartInfo = processStartInfo;
                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
                return process.StandardOutput.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        public static string ExecuteCMDBat(string deviceID, string cmdCommand)
        {
            try
            {
                string text = "bat_" + deviceID + ".bat";
                File.WriteAllText(text, cmdCommand);
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WorkingDirectory = ADB_FOLDER_PATH;
                processStartInfo.FileName = text;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.Verb = "runas";
                process.StartInfo = processStartInfo;
                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
                return process.StandardOutput.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        public static List<string> GetDevices()
        {
            List<string> list = new List<string>();
            string input = ExecuteCMD("adb devices");
            string pattern = "(?<=List of devices attached)([^\\n]*\\n+)+";
            MatchCollection matchCollection = Regex.Matches(input, pattern, RegexOptions.Singleline);
            if (matchCollection.Count > 0)
            {
                string value = matchCollection[0].Groups[0].Value;
                string[] array = Regex.Split(value, "\r\n");
                string[] array2 = array;
                foreach (string text in array2)
                {
                    if (string.IsNullOrEmpty(text) || !(text != " "))
                    {
                        continue;
                    }

                    string[] array3 = text.Trim().Split(new char[1] { '\t' });
                    string text2 = array3[0];
                    string text3 = "";
                    try
                    {
                        text3 = array3[1];
                        if (text3 != "device")
                        {
                            continue;
                        }
                    }
                    catch
                    {
                    }

                    list.Add(text2.Trim());
                }
            }

            return list;
        }

        public static string GetDeviceName(string deviceID)
        {
            string result = "";
            string cmdCommand = "";
            string text = ExecuteCMD(cmdCommand);
            return result;
        }

        public static void TapByPercent(string deviceID, double x, double y, int count = 1)
        {
            Point screenResolution = GetScreenResolution(deviceID);
            int num = (int)(x * ((double)screenResolution.X * 1.0 / 100.0));
            int num2 = (int)(y * ((double)screenResolution.Y * 1.0 / 100.0));
            string text = string.Format(TAP_DEVICES, deviceID, num, num2);
            for (int i = 1; i < count; i++)
            {
                text = text + " && " + string.Format(TAP_DEVICES, deviceID, x, y);
            }

            string text2 = ExecuteCMD(text);
        }

        public static void Tap(string deviceID, int x, int y, int count = 1)
        {
            string text = string.Format(TAP_DEVICES, deviceID, x, y);
            for (int i = 1; i < count; i++)
            {
                text = text + " && " + string.Format(TAP_DEVICES, deviceID, x, y);
            }

            string text2 = ExecuteCMD(text);
        }

        public static void Key(string deviceID, ADBKeyEvent key)
        {
            string cmdCommand = string.Format(KEY_DEVICES, deviceID, key);
            string text = ExecuteCMD(cmdCommand);
        }

        public static void InputText(string deviceID, string text)
        {
            string cmdCommand = string.Format(INPUT_TEXT_DEVICES, deviceID, text.Replace(" ", "%s").Replace("&", "\\&").Replace("<", "\\<")
                .Replace(">", "\\>")
                .Replace("?", "\\?")
                .Replace(":", "\\:")
                .Replace("{", "\\{")
                .Replace("}", "\\}")
                .Replace("[", "\\[")
                .Replace("]", "\\]")
                .Replace("|", "\\|"));
            string text2 = ExecuteCMD(cmdCommand);
        }

        public static void SwipeByPercent(string deviceID, double x1, double y1, double x2, double y2, int duration = 100)
        {
            Point screenResolution = GetScreenResolution(deviceID);
            int num = (int)(x1 * ((double)screenResolution.X * 1.0 / 100.0));
            int num2 = (int)(y1 * ((double)screenResolution.Y * 1.0 / 100.0));
            int num3 = (int)(x2 * ((double)screenResolution.X * 1.0 / 100.0));
            int num4 = (int)(y2 * ((double)screenResolution.Y * 1.0 / 100.0));
            string cmdCommand = string.Format(SWIPE_DEVICES, deviceID, num, num2, num3, num4, duration);
            string text = ExecuteCMD(cmdCommand);
        }

        public static void Swipe(string deviceID, int x1, int y1, int x2, int y2, int duration = 100)
        {
            string cmdCommand = string.Format(SWIPE_DEVICES, deviceID, x1, y1, x2, y2, duration);
            string text = ExecuteCMD(cmdCommand);
        }

        public static void LongPress(string deviceID, int x, int y, int duration = 100)
        {
            string cmdCommand = string.Format(SWIPE_DEVICES, deviceID, x, y, x, y, duration);
            string text = ExecuteCMD(cmdCommand);
        }

        public static Point GetScreenResolution(string deviceID)
        {
            string cmdCommand = string.Format(GET_SCREEN_RESOLUTION, deviceID);
            string text = ExecuteCMD(cmdCommand);
            text = text.Substring(text.IndexOf("- "));
            text = text.Substring(text.IndexOf(' '), text.IndexOf(')') - text.IndexOf(' '));
            string[] array = text.Split(new char[1] { ',' });
            int x = Convert.ToInt32(array[0].Trim());
            int y = Convert.ToInt32(array[1].Trim());
            return new Point(x, y);
        }

        public static Bitmap ScreenShoot(string deviceID = null, bool isDeleteImageAfterCapture = true, string fileName = "screenShoot.png")
        {

            if (string.IsNullOrEmpty(deviceID))
            {
                List<string> devices = GetDevices();
                if (devices == null || devices.Count <= 0)
                {
                    return null;
                }

                deviceID = devices.First();
            }

            string text = deviceID;
           

            string text2 = Path.GetFileNameWithoutExtension(fileName) +  Path.GetExtension(fileName);
            if (File.Exists(text2))
            {
                try
                {
                    File.Delete(text2);
                }
                catch (Exception)
                {
                }
            }

            string text3 = Directory.GetCurrentDirectory() +  text2;
            string text4 = Directory.GetCurrentDirectory() + "\\InputImage";
            text4= text4.Replace("\\\\", "\\");
            text4 = "\"" + text4 + "\"";
            string cmdCommand = string.Format("adb -s {0} shell screencap -p \"{1}\"", deviceID, "/sdcard/" + text2);
            string cmdCommand2 = string.Format("adb -s " + deviceID + " pull /sdcard/" + text2 + " " + text4);
            string text5 = ExecuteCMD(cmdCommand);
            string text6 = ExecuteCMD(cmdCommand2);
            Bitmap result = null;
            try
            {
                Bitmap val = new Bitmap(text3);
                try
                {
                    result = new Bitmap((Image)(object)val);
                }
                finally
                {
                    ((IDisposable)val)?.Dispose();
                }
            }
            catch
            {
            }

            if (isDeleteImageAfterCapture)
            {
                try
                {
                    File.Delete(text2);
                }
                catch
                {
                }

                try
                {
                    string cmdCommand3 = string.Format("adb -s " + deviceID + " shell \"rm /sdcard/" + text2 + "\"");
                    string text7 = ExecuteCMD(cmdCommand3);
                }
                catch
                {
                }
            }

            return result;
        }

        public static void ConnectNox(int count = 1)
        {
            string text = "";
            int num = 62000;
            if (count <= 1)
            {
                text = text + "adb connect 127.0.0.1:" + (num + 1);
            }
            else
            {
                text = text + "adb connect 127.0.0.1:" + (num + 1);
                for (int i = 25; i < count + 24; i++)
                {
                    text = text + Environment.NewLine + "adb connect 127.0.0.1:" + (num + i);
                }
            }

            ExecuteCMD(text);
        }

        public static void PlanModeON(string deviceID, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                string text = "adb -s " + deviceID + " settings put global airplane_mode_on 1";
                text = text + Environment.NewLine + "adb -s " + deviceID + " am broadcast -a android.intent.action.AIRPLANE_MODE";
                ExecuteCMD(text);
            }
        }

        public static void PlanModeOFF(string deviceID, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                string text = "adb -s " + deviceID + " settings put global airplane_mode_on 0";
                text = text + Environment.NewLine + "adb -s " + deviceID + " am broadcast -a android.intent.action.AIRPLANE_MODE";
                ExecuteCMD(text);
            }
        }

        public static void Delay(double delayTime)
        {
            for (double num = 0.0; num < delayTime; num += 100.0)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(100.0));
            }
        }

        //public static Point? FindImage(string deviceID, string ImagePath, int delayPerCheck = 2000, int count = 5)
        //{
        //    //IL_008a: Unknown result type (might be due to invalid IL or missing references)
        //    //IL_0091: Expected O, but got Unknown
        //    DirectoryInfo directoryInfo = new DirectoryInfo(ImagePath);
        //    FileInfo[] files = directoryInfo.GetFiles();
        //    do
        //    {
        //        Bitmap val = null;
        //        int num = 3;
        //        do
        //        {
        //            try
        //            {
        //                val = ScreenShoot(deviceID);
        //            }
        //            catch (Exception)
        //            {
        //                num--;
        //                Delay(1000.0);
        //                continue;
        //            }

        //            break;
        //        }
        //        while (num > 0);
        //        if (val == null)
        //        {
        //            return null;
        //        }

        //        Point? result = null;
        //        FileInfo[] array = files;
        //        foreach (FileInfo fileInfo in array)
        //        {
        //            Bitmap subBitmap = (Bitmap)Image.FromFile(fileInfo.FullName);
        //            result = ImageScanOpenCV.FindOutPoint(val, subBitmap);
        //            if (result.HasValue)
        //            {
        //                break;
        //            }
        //        }

        //        if (result.HasValue)
        //        {
        //            return result;
        //        }

        //        Delay(2000.0);
        //        count--;
        //    }
        //    while (count > 0);
        //    return null;
        //}

        //public static bool FindImageAndClick(string deviceID, string ImagePath, int delayPerCheck = 2000, int count = 5)
        //{
        //    // IL_0081: Unknown result type(might be due to invalid IL or missing references)
        //    // IL_0088: Expected O, but got Unknown
        //    DirectoryInfo directoryInfo = new DirectoryInfo(ImagePath);
        //    FileInfo[] files = directoryInfo.GetFiles();
        //    do
        //    {
        //        Bitmap val = null;
        //        int num = 3;
        //        do
        //        {
        //            try
        //            {
        //                val = ScreenShoot(deviceID);
        //            }
        //            catch (Exception)
        //            {
        //                num--;
        //                Delay(1000.0);
        //                continue;
        //            }

        //            break;
        //        }
        //        while (num > 0);
        //        if (val == null)
        //        {
        //            return false;
        //        }

        //        Point? point = null;
        //        FileInfo[] array = files;
        //        foreach (FileInfo fileInfo in array)
        //        {
        //            Bitmap subBitmap = (Bitmap)Image.FromFile(fileInfo.FullName);
        //            point = ImageScanOpenCV.FindOutPoint(val, subBitmap);
        //            if (point.HasValue)
        //            {
        //                break;
        //            }
        //        }

        //        if (point.HasValue)
        //        {
        //            Tap(deviceID, point.Value.X, point.Value.Y);
        //            return true;
        //        }

        //        Delay(delayPerCheck);
        //        count--;
        //    }
        //    while (count > 0);
        //    return false;
        //}
    }
    public enum ADBKeyEvent
    {
        KEYCODE_0 = 0,
        KEYCODE_SOFT_LEFT = 1,
        KEYCODE_SOFT_RIGHT = 2,
        KEYCODE_HOME = 3,
        KEYCODE_BACK = 4,
        KEYCODE_CALL = 5,
        KEYCODE_ENDCALL = 6,
        KEYCODE_0_ = 7,
        KEYCODE_1 = 8,
        KEYCODE_2 = 9,
        KEYCODE_3 = 10,
        KEYCODE_4 = 11,
        KEYCODE_5 = 12,
        KEYCODE_6 = 13,
        KEYCODE_7 = 14,
        KEYCODE_8 = 0xF,
        KEYCODE_9 = 0x10,
        KEYCODE_STAR = 17,
        KEYCODE_POUND = 18,
        KEYCODE_DPAD_UP = 19,
        KEYCODE_DPAD_DOWN = 20,
        KEYCODE_DPAD_LEFT = 21,
        KEYCODE_DPAD_RIGHT = 22,
        KEYCODE_DPAD_CENTER = 23,
        KEYCODE_VOLUME_UP = 24,
        KEYCODE_VOLUME_DOWN = 25,
        KEYCODE_POWER = 26,
        KEYCODE_CAMERA = 27,
        KEYCODE_CLEAR = 28,
        KEYCODE_A = 29,
        KEYCODE_B = 30,
        KEYCODE_C = 0x1F,
        KEYCODE_D = 0x20,
        KEYCODE_E = 33,
        KEYCODE_F = 34,
        KEYCODE_G = 35,
        KEYCODE_H = 36,
        KEYCODE_I = 37,
        KEYCODE_J = 38,
        KEYCODE_K = 39,
        KEYCODE_L = 40,
        KEYCODE_M = 41,
        KEYCODE_N = 42,
        KEYCODE_O = 43,
        KEYCODE_P = 44,
        KEYCODE_Q = 45,
        KEYCODE_R = 46,
        KEYCODE_S = 47,
        KEYCODE_T = 48,
        KEYCODE_U = 49,
        KEYCODE_V = 50,
        KEYCODE_W = 51,
        KEYCODE_X = 52,
        KEYCODE_Y = 53,
        KEYCODE_Z = 54,
        KEYCODE_COMMA = 55,
        KEYCODE_PERIOD = 56,
        KEYCODE_ALT_LEFT = 57,
        KEYCODE_ALT_RIGHT = 58,
        KEYCODE_SHIFT_LEFT = 59,
        KEYCODE_SHIFT_RIGHT = 60,
        KEYCODE_TAB = 61,
        KEYCODE_SPACE = 62,
        KEYCODE_SYM = 0x3F,
        KEYCODE_EXPLORER = 0x40,
        KEYCODE_ENVELOPE = 65,
        KEYCODE_ENTER = 66,
        KEYCODE_DEL = 67,
        KEYCODE_GRAVE = 68,
        KEYCODE_MINUS = 69,
        KEYCODE_EQUALS = 70,
        KEYCODE_LEFT_BRACKET = 71,
        KEYCODE_RIGHT_BRACKET = 72,
        KEYCODE_BACKSLASH = 73,
        KEYCODE_SEMICOLON = 74,
        KEYCODE_APOSTROPHE = 75,
        KEYCODE_SLASH = 76,
        KEYCODE_AT = 77,
        KEYCODE_NUM = 78,
        KEYCODE_HEADSETHOOK = 79,
        KEYCODE_FOCUS = 80,
        KEYCODE_PLUS = 81,
        KEYCODE_MENU = 82,
        KEYCODE_NOTIFICATION = 83,
        KEYCODE_APP_SWITCH = 187
    }
}
