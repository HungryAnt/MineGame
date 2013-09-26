using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntGameMine.Models
{
    public class MineGameModel
    {
        private HardLevel _hardLevel = HardLevel.Normal;
        public HardLevel HardLevel
        {
            get { return _hardLevel; }
            set { _hardLevel = value; }
        }

        private MineGridMap _mineGridMap;
        public MineGridMap MineGridMap
        {
            get { return _mineGridMap; }
        }

        public void StartGame()
        {
            switch (HardLevel)
            {
                case HardLevel.Easy:
                    _mineGridMap = new MineGridMap(10, 10, 10);
                    break;
                case HardLevel.Normal:
                    _mineGridMap = new MineGridMap(12, 18, 30);
                    break;
                default:
                    _mineGridMap = new MineGridMap(16, 30, 100);
                    break;
            }
        }
    }
}
