using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiebaNet.Analyser;
using JiebaNet.Segmenter;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test1();
            Test2();

            Console.ReadLine();
        }

        static void Test1()
        {
            var segmenter = new JiebaSegmenter();
            var segments = segmenter.Cut("我来到北京清华大学", cutAll: true);
            Console.WriteLine("【全模式】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("我来到北京清华大学");  // 默认为精确模式
            Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("他来到了网易杭研大厦");  // 默认为精确模式，同时也使用HMM模型
            Console.WriteLine("【新词识别】：{0}", string.Join("/ ", segments));

            segments = segmenter.CutForSearch("小明硕士毕业于中国科学院计算所，后在日本京都大学深造"); // 搜索引擎模式
            Console.WriteLine("【搜索引擎模式】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("结过婚的和尚未结过婚的");
            Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));
        }

        static void Test2()
        {
            JiebaSegmenter seg = new JiebaSegmenter();
            seg.AddWord("风机");
            seg.AddWord("风机1");
            seg.AddWord("风机2");
            seg.AddWord("风机3");
            seg.AddWord("启动");
            seg.AddWord("停止");
            seg.AddWord("开");
            seg.AddWord("关");
            seg.AddWord("停");
            seg.AddWord("停止");
            seg.AddWord("风");
            
            //var segments = seg.Cut("风机1启动");  // 默认为精确模式
            //Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));

            Console.WriteLine($"{string.Join("/",seg.Cut("风机1启动"))}");
            Console.WriteLine($"{string.Join("/", seg.Cut("风机1开"))}");
            Console.WriteLine($"{string.Join("/", seg.Cut("风机1关"))}");
            Console.WriteLine($"{string.Join("/", seg.Cut("风机1停"))}");
            Console.WriteLine($"{string.Join("/", seg.Cut("启动风机1"))}");
            Console.WriteLine($"{string.Join("/", seg.Cut("开风机1"))}");
            Console.WriteLine($"{string.Join("/", seg.Cut("关风机"))}");
            Console.WriteLine($"{string.Join("/", seg.Cut("停止风机1"))}");
            Console.WriteLine($"{string.Join("/", seg.Cut("开风机4"))}");
        }
    }
}
