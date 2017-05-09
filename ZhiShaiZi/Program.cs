using System;
using System.Collections.Generic;
using System.Text;

namespace ZhiShaiZi
{
    class Program
    {
        // 计算 连续3次是小时，第4次也是小的概率
        static void Main(string[] args)
        {
            Random random = new Random();

            //假如投了16次
            int round = 4 * 4000;
            round = 10000;

            Console.WriteLine("输入投的次数");
            round = int.Parse(Console.ReadLine());

            List<Group> list = new List<Group>();

            //记录下投的结果来
            for (int i = 0; i < round; i++)
            {
                Group group = new Group(random);
                Console.WriteLine(string.Format("第{5}次投骰：{0} {1} {2} 和：{3} {4}", group.Num1, group.Num2, group.Num3, group.Sum().ToString().PadRight(2), group.IsMin() ? "小" : "大", (i + 1).ToString().PadRight(5)));
                list.Add(group);
            }

            // 连着三次都是小的情况出现了多少次
            int count = 0;
            // 连着三次都是小且第四次也是小的情况出现了多少次
            int minCount = 0;

            //从第一次开始看
            for (int i = 0; i < list.Count && i + 3 < list.Count; i++)
            {
                //如果当前投的是小，且后面两次也算小
                if (list[i].IsMin() && list[i + 1].IsMin() && list[i + 2].IsMin())
                {
                    count++;
                    if (list[i + 3].IsMin())
                    {
                        minCount++;
                    }
                }
            }

            Console.WriteLine("连着三次都是小的情况出现了多少次 ： {0}", count);
            Console.WriteLine("连着三次都是小且第四次也是小的情况出现了多少次 ： {0}", minCount);
            Console.WriteLine("三次是小，第四次也是小的概率：{0}%", ((double)minCount / count) * 100);

            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }
    }

    //假如投一次是3个骰子，算为一个组
    public class Group
    {
        public int Num1 { get; set; }
        public int Num2 { get; set; }
        public int Num3 { get; set; }

        public Group(Random random)
        {
            Num1 = random.Next(1, 7);
            Num2 = random.Next(1, 7);
            Num3 = random.Next(1, 7);
        }

        //是否是小
        public bool IsMin()
        {
            //3个一样的 庄家赢 不算小
            if (Num1 == Num2 && Num1 == Num3)
                return false;
            //如果是 小于等于10的 算小
            return Num1 + Num2 + Num3 <= 10;
        }

        public int Sum()
        {
            return Num1 + Num2 + Num3;
        }
    }
}
