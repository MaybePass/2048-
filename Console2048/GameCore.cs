using System;
using System.Collections.Generic;
using System.Text;

namespace Console2048
{
    /// <summary>
    /// 游戏核心类与界面无关
    /// </summary>
    class GameCore
    {
        private int[,] map;
        private int[] mergeArray;
        public int[,] Map
        {
            get { return this.map; }
        }
        /// <summary>
        /// 地图是否发生改变
        /// </summary>
        public bool IsChange { get; set; }

        public GameCore()
        {
            map = new int[4,4];
            mergeArray = new int[4];
            emptyLocationList = new List<Location>(16);
            random = new Random();
            originalMap = new int[4,4];
            IsChange = false;
        }
        private int[,] originalMap;

        //函数入口
        public void MapMove(MoveDirection dir)
        {
            Array.Copy(map, originalMap, map.Length);
            IsChange = false;
            switch (dir)
            {
                case MoveDirection.Up:Up();
                    break;
                case MoveDirection.Down:Down();
                    break;
                case MoveDirection.Left:Left();
                    break;
                case MoveDirection.Right:Right();
                    break;
                default:
                    break;
            }
            //移动完毕
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if (map[r,c]!=originalMap[r,c])
                    {
                        IsChange = true;
                        return;
                    }
                }
            }
            //CreatNumber();

            return;
        }

        #region 数据合并
        //去零，将输入的数组中的数字移到前面
        private void RemoveZero()
        {
            int[] t = new int[mergeArray.Length];
            int q = 0;
            for (int i = 0; i < mergeArray.Length; i++)
            {
                if (mergeArray[i] != 0)
                {
                    t[q++] = mergeArray[i];
                }
            }
            for (int i = 0; i < mergeArray.Length; i++)
            {
                mergeArray[i] = t[i];
            }
        }
        //合并数据
        private void DataMerge()
        {
            RemoveZero();
            for (int i = 0; i < mergeArray.Length-1; i++)
            {
                if (mergeArray[i] != 0 && mergeArray[i]==mergeArray[i+1])
                {
                    mergeArray[i] = mergeArray[i] + mergeArray[i + 1];
                    mergeArray[i + 1] = 0;
                    RemoveZero();
                }
            }
        }
        #endregion 

        #region 数据移动
        //上移 
        private void Up()
        {
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    mergeArray[j] = map[j, i];
                }
                DataMerge();
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    map[j,i] = mergeArray[j];
                }
            }
        }
        //下移 
        private void Down()
        {
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = map.GetLength(0)-1; j >= 0; j--)
                {
                    mergeArray[map.GetLength(0)-j-1] = map[j, i];
                }
                DataMerge();
                for (int j = map.GetLength(0)-1; j >= 0; j--)
                {
                    map[j, i] = mergeArray[map.GetLength(0) - j - 1];
                }
            }
        }
        //左移 
        private void Left()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    mergeArray[j] = map[i, j];
                }
                DataMerge();
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = mergeArray[j];
                }
            }
        }
        //右移 
        private void Right()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = map.GetLength(1) - 1; j >= 0; j--)
                {
                    mergeArray[map.GetLength(1) - j - 1] = map[i, j];
                }
                DataMerge();
                for (int j = map.GetLength(1) - 1; j >= 0; j--)
                {
                    map[i, j] = mergeArray[map.GetLength(1) - j - 1];
                }
            }
        }
        #endregion 

        /*废弃，用老师的，因为要实现其他功能
        //生成一个随机的2或4，90%是2,10%是4
        /*private void CreatNumber()
        {
            Random r = new Random();
            //一个随机数，决定是2还是4
            int n = r.Next(0, 10);
            if (n == 10)
                n = 4;
            else
                n = 2;

            //一个随机数，决定往哪里放
            int temp = r.Next(0 , 15);
            for (int i = 0; i < 16; i++)
            {
                if (map[(temp / 4), (temp % 4)] != 0)
                {
                    temp=(temp >= 15) ? 0 : ++temp;
                }
                else
                {
                    map[(temp / 4), (temp % 4)] = n;
                    break;
                }
            }
        }*/

        private List<Location> emptyLocationList;
        private void CalculateEmpty()
        {
            emptyLocationList.Clear();
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if(map[r,c]==0)
                    {
                        //记录r,c
                       emptyLocationList.Add(new Location(r, c));
                    }
                }
            }
        }

        private Random random;
        public void GenerateNumber()
        {
            CalculateEmpty();
            if (emptyLocationList.Count > 0)
            {
                int randomIndex = random.Next(0, emptyLocationList.Count);
                Location loc = emptyLocationList[randomIndex];

                map[loc.RIndex, loc.CIndex] = (random.Next(0, 10) == 1) ? 4 : 2;
            }
        }
    }
}
