using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntGameMine.ViewModels
{
    public enum MineGridFlag
    {
        Null,   // 空，周围没有雷
        Unknown,
        Mine,
        Mark,   // 旗帜标记
        MineCountNum    // 周围雷数
    }
}
