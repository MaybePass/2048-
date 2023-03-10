using System;

namespace Console2048
{
    class Program
    {
        static void Main(string[] args)
        {
            GameCore g = new GameCore();
            g.GenerateNumber();
            g.GenerateNumber();
            DrowMap(g);

            while (true)
            {
                KeyDown(g);
                if (g.IsChange == true)
                {
                    g.GenerateNumber();
                    DrowMap(g);
                }
            }
        }

        private static void DrowMap(GameCore g)
        {
            Console.Clear();
            for (int i = 0; i < g.Map.GetLength(0); i++)
            {
                for (int j = 0; j < g.Map.GetLength(1); j++)
                {
                    Console.Write("{0,3}", g.Map[i, j]);
                }
                Console.Write("\n");
            }
            Console.WriteLine("往哪个方向移动(上下左右),输入end结束");
        }

        private static void KeyDown(GameCore g)
        {

            string dir = Console.ReadLine();
            switch (dir)
            {
                case "上":
                    g.MapMove(MoveDirection.Up);
                    break;
                case "下":
                    g.MapMove(MoveDirection.Down);
                    break;
                case "左":
                    g.MapMove(MoveDirection.Left);
                    break;
                case "右":
                    g.MapMove(MoveDirection.Right);
                    break;
                case "end":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("你输错了");
                    break;
            }
        }
    }
}
