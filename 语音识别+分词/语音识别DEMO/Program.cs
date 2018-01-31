using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 语音识别DEMO
{
    class Program
    {
        static void Main(string[] args)
        {
            //App ID  10663392
            //API Key oKyF9Oz9GK8RKUZgR8Gf86uk
            //Secret Key DBYeFMm5r8Vwaj6toInoUT4KFE6GXt1a

            // 设置APPID/AK/SK
            var APP_ID = "10663392";
            var API_KEY = "oKyF9Oz9GK8RKUZgR8Gf86uk";
            var SECRET_KEY = "DBYeFMm5r8Vwaj6toInoUT4KFE6GXt1a";

            var client = new Baidu.Aip.Speech.Asr(API_KEY, SECRET_KEY);
            //var data = File.ReadAllBytes("16k.pcm");
            //var result = client.Recognize(data, "pcm", 16000);
            //var data = File.ReadAllBytes("16k.wav");
            //var result = client.Recognize(data, "wav", 16000);
            //Console.Write(result);

            string path = "16k.wav";
            string format = "wav";
            M4a2pcm(path, "wav","test.wav");
            var data = File.ReadAllBytes("test.wav");
            var result = client.Recognize(data, format, 16000);
            Console.Write(result);


            Console.ReadLine();
        }

        /// <summary>
        /// 转码为pcm
        /// </summary>
        /// <param name="path"></param>
        static void M4a2pcm(string path, string format, string newFileName)
        {
            string Command = " -i \"" + path + "\" -vn  -f wav -c:a pcm_s16le -ar 16000 -ac 1 " + newFileName;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"tool\ffmpeg.exe";
            p.StartInfo.Arguments = Command;
            p.StartInfo.WorkingDirectory = "bin/tools/";

            #region 方法一  
            //p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序 启动 线程  
            //p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用 StandardOutput是捕获不到任何消息的...  
            //p.StartInfo.CreateNoWindow = false;//不创建进程窗口  
            //p.Start();//启动线程  
            //p.BeginErrorReadLine();//开始异步读取  
            //p.WaitForExit();//等待完成  
            //p.Close();//关闭进程  
            //p.Dispose();//释放资源  
            #endregion

            #region 方法二  
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.Start();//启动线程  
            p.WaitForExit();//等待完成  
            p.Close();//关闭进程  
            p.Dispose();//释放资源  
            #endregion
        }

    }
}
