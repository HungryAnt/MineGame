using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntGameMine.Models
{
    public class MineGridMap
    {
        private static readonly Random random = new Random();

        private int _rowCount;
        private int _columnCount;
        private int _gridCount;
        private MineGridEntity[] _grids;

        public int RowCount
        {
            get { return _rowCount; }
        }

        public int ColumnCount
        {
            get { return _columnCount; }
        }

        public int GridCount
        {
            get { return _gridCount; }
        }

        public MineGridMap(int rowCount, int columnCount, int mineCount)
        {
            _rowCount = rowCount;
            _columnCount = columnCount;
            _gridCount = _rowCount*_columnCount;

            _grids = new MineGridEntity[_rowCount * _columnCount];
            for (int i = 0; i < GridCount; i++)
            {
                _grids[i] = new MineGridEntity();
            }

            RandomSetMines(mineCount);
        }

        void RandomSetMines(int mineCount)
        {
            int gridCount = GridCount;
            for (int i = 0; i < mineCount; i++)
            {
                int index;
                do
                {
                    index = random.Next(gridCount);
                } while (_grids[index].IsMine);
                SetMine(index);
            }
        }

        void SetMine(int index)
        {
            _grids[index].IsMine = true;
            int rowIndex = index/ColumnCount;
            int columnIndex = index%ColumnCount;

            IncreaseRelatedGridMineCount(rowIndex - 1, columnIndex - 1);
            IncreaseRelatedGridMineCount(rowIndex - 1, columnIndex);
            IncreaseRelatedGridMineCount(rowIndex - 1, columnIndex + 1);
            IncreaseRelatedGridMineCount(rowIndex, columnIndex - 1);
            IncreaseRelatedGridMineCount(rowIndex, columnIndex + 1);
            IncreaseRelatedGridMineCount(rowIndex + 1, columnIndex - 1);
            IncreaseRelatedGridMineCount(rowIndex + 1, columnIndex);
            IncreaseRelatedGridMineCount(rowIndex + 1, columnIndex + 1);
        }

        public MineGridEntity GetGridEntity(int rowIndex, int columnIndex)
        {
            return _grids[rowIndex * ColumnCount + columnIndex];
        }

        void IncreaseRelatedGridMineCount(int rowIndex, int columnIndex)
        {
            if (IsRowColumnAvailable(rowIndex, columnIndex))
            {
                GetGridEntity(rowIndex, columnIndex).MineCountArround++;
            }
        }

        bool IsRowColumnAvailable(int rowIndex, int columnIndex)
        {
            return rowIndex >= 0 && rowIndex < RowCount &&
                   columnIndex >= 0 && columnIndex < ColumnCount;
        }

        public bool IsMineGrid(int rowIndex, int columnIndex, out int mineCountArround)
        {
            int index = rowIndex*ColumnCount + columnIndex;
            mineCountArround = _grids[index].MineCountArround;
            return _grids[index].IsMine;
        }

        public void CheckGrid(int rowIndex, int columnIndex)
        {
            if (IsRowColumnAvailable(rowIndex, columnIndex))
            {
                MineGridEntity entity = GetGridEntity(rowIndex, columnIndex);
                if (entity.IsChecked)
                {
                    return;
                }
                entity.IsChecked = true;
                if (entity.IsMine)
                {
                    return;
                }
                if (entity.MineCountArround == 0)
                {
                    CheckGrid(rowIndex - 1, columnIndex - 1);
                    CheckGrid(rowIndex - 1, columnIndex);
                    CheckGrid(rowIndex - 1, columnIndex + 1);
                    CheckGrid(rowIndex, columnIndex - 1);
                    CheckGrid(rowIndex, columnIndex + 1);
                    CheckGrid(rowIndex + 1, columnIndex - 1);
                    CheckGrid(rowIndex + 1, columnIndex);
                    CheckGrid(rowIndex + 1, columnIndex + 1);
                }
            }
        }
    }
}
