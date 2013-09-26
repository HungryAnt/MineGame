using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntGameMine.Models
{
    public class MineGridEntity
    {
        public bool IsMine { get; set; }
        public bool IsMarked { get; set; }  // 是否被插旗标记
        public bool IsChecked { get; set; } // 是否探测过
        public int MineCountArround { get; set; }   // 周围地雷数量
    }
}
