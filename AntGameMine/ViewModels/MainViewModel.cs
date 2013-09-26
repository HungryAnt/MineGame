using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using AntGameMine.Models;

namespace AntGameMine.ViewModels
{
    public class MainViewModel
    {
        MineGameModel MineGameModel
        {
            get { return GameServices.Instance.MineGameModel; }
        }

        private HardLevel HardLevel
        {
            get { return MineGameModel.HardLevel; }
            set { MineGameModel.HardLevel = value; }
        }

        public int MineGridRowCount
        {
            get { return MineGameModel.MineGridMap.RowCount; }
        }

        public int MineGridColumnCount
        {
            get { return MineGameModel.MineGridMap.ColumnCount; }
        }

        

        private static bool TryConvertToHardLevel(string hardLevelString, out HardLevel hardLevel)
        {
            switch (hardLevelString)
            {
                case "0":
                    hardLevel = HardLevel.Easy;
                    return true;
                case "1":
                    hardLevel = HardLevel.Normal;
                    return true;
                case "2":
                    hardLevel = HardLevel.Hard;
                    return true;
                default:
                    hardLevel = HardLevel.Easy;
                    return false;
            }
        }

        private void ChangeHardLevel(string hardLevelString)
        {
            if (!string.IsNullOrEmpty(hardLevelString))
            {
                HardLevel hardLevel;
                if (TryConvertToHardLevel(hardLevelString, out hardLevel))
                {
                    HardLevel = hardLevel;
                }
            }
        }

        public void StartGame(string hardLevelString)
        {
            ChangeHardLevel(hardLevelString);

            MineGameModel.StartGame();
        }

        public void CheckGrid(int rowIndex, int columnIndex)
        {
            MineGameModel.MineGridMap.CheckGrid(rowIndex, columnIndex);
        }

        public MineGridFlag GetMineGridFlag(int rowIndex, int columnIndex, out int mineCountArround)
        {
            var entity = MineGameModel.MineGridMap.GetGridEntity(rowIndex, columnIndex);
            mineCountArround = entity.MineCountArround;

            if (!entity.IsChecked)
            {
                return MineGridFlag.Unknown;
            }
            if (entity.IsMine)
            {
                return MineGridFlag.Mine;
            }
            if (entity.IsMarked)
            {
                return MineGridFlag.Mark;
            }
            if (mineCountArround == 0)
            {
                return MineGridFlag.Null;
            }
            
            return MineGridFlag.MineCountNum;
        }


        //public bool IsMine(int rowIndex, int columnIndex, out int mineCountArround)
        //{
        //    return _mineGridMap.IsMineGrid(rowIndex, columnIndex, out mineCountArround);
        //}


    }
}
