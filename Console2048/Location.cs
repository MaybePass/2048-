using System;
using System.Collections.Generic;
using System.Text;

namespace Console2048
{
    struct Location
    {
        /// <summary>
        /// 行索引
        /// </summary>
        public int RIndex { get; set; }
        /// <summary>
        /// 列索引
        /// </summary>
        public int CIndex { get; set; }

        public Location(int rIndex, int cIndex) : this()
        {
            this.RIndex = rIndex;
            this.CIndex = cIndex;
        }
    }
}
