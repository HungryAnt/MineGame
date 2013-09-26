using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AntGameMine.ViewModels;

namespace AntGameMine.Views
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class WindowMain : Window
    {
        const double MINE_GRID_WIDTH = 24;
        MainViewModel MainViewModel { get; set; }

        private MainViewGridCache _gridCache = new MainViewGridCache();
        private Dictionary<int, SolidColorBrush> _numBrush;

        public WindowMain()
        {
            InitializeComponent();

            MainViewModel = new MainViewModel();

            StartGame(null);
        }

        private SolidColorBrush GetNumForeGroundBrush(int num)
        {
            if (_numBrush == null)
            {
                _numBrush = new Dictionary<int, SolidColorBrush>();
                _numBrush.Add(1, new SolidColorBrush(Colors.CornflowerBlue));
                _numBrush.Add(2, new SolidColorBrush(Colors.ForestGreen));
                _numBrush.Add(3, new SolidColorBrush(Colors.Brown));
                _numBrush.Add(4, new SolidColorBrush(Colors.Blue));
                _numBrush.Add(5, new SolidColorBrush(Colors.DarkRed));
                _numBrush.Add(6, new SolidColorBrush(Colors.DarkSlateGray));
                _numBrush.Add(7, new SolidColorBrush(Colors.DarkCyan));
                _numBrush.Add(8, new SolidColorBrush(Colors.OrangeRed));
            }
            return _numBrush[num];
        }

        private void StartGame(string hardLevelString)
        {
            MainViewModel.StartGame(hardLevelString);

            canvasGamePanel.Children.Clear();

            int rowCount = MainViewModel.MineGridRowCount;
            int columnCount = MainViewModel.MineGridColumnCount;

            canvasGamePanel.Width = (MINE_GRID_WIDTH + 1) * columnCount - 1;
            canvasGamePanel.Height = (MINE_GRID_WIDTH + 1) * rowCount - 1;

            UserControl[] controls = _gridCache.GetGridArray(rowCount * columnCount);

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < columnCount; col++)
                {
                    UserControl controlGrid = controls[row * columnCount + col];
                    controlGrid.Width = MINE_GRID_WIDTH;
                    controlGrid.Height = MINE_GRID_WIDTH;
                    controlGrid.Tag = new[] { row, col };
                    controlGrid.Background = Brushes.Brown;
                    controlGrid.ContentTemplate = DataTemplateUnknownGrid;
                    controlGrid.Content = null;

                    controlGrid.SetValue(Canvas.LeftProperty, (MINE_GRID_WIDTH + 1) * col);
                    controlGrid.SetValue(Canvas.TopProperty, (MINE_GRID_WIDTH + 1) * row);
                    canvasGamePanel.Children.Add(controlGrid);
                }
            }
        }

        private void StartGameCommand_Excute(object sender, ExecutedRoutedEventArgs e)
        {
            string hardLevelString = e.Parameter != null ? e.Parameter.ToString() : null;
            StartGame(hardLevelString);
        }

        private DataTemplate DataTemplateUnknownGrid
        {
            get { return FindResource("UnknownGrid") as DataTemplate; }
        }
        private DataTemplate DataTemplateBlankGrid
        {
            get { return FindResource("BlankGrid") as DataTemplate; }
        }
        private DataTemplate DataTemplateMineGrid
        {
            get { return FindResource("MineGrid") as DataTemplate; }
        }

        //private DataTemplate DataTemplateNullGrid
        //{
        //    get { return FindResource("NullGrid") as DataTemplate; }
        //}

        private DataTemplate DataTemplateNumGrid
        {
            get { return FindResource("NumGrid") as DataTemplate; }
        }

        private void CanvasChildren_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UserControl controlGrid = e.Source as UserControl;
            int row, col;
            if (TryGetMineGridControlLocationIndex(controlGrid, out row, out col))
            {
                MainViewModel.CheckGrid(row, col);
                UpdateAllMineGrid();
            }
        }

        static bool TryGetMineGridControlLocationIndex(UserControl controlGrid, out int row, out int col)
        {
            if (controlGrid != null)
            {
                int[] arr = (int[])controlGrid.Tag;
                if (arr != null && arr.Length == 2)
                {
                    row = arr[0];
                    col = arr[1];
                    return true;
                }
            }
            row = col = 0;
            return false;
        }

        private void UpdateAllMineGrid()
        {
            foreach (UserControl controlGrid in canvasGamePanel.Children)
            {
                int row, col;
                if (TryGetMineGridControlLocationIndex(controlGrid, out row, out col))
                {
                    int mineCountArround;
                    MineGridFlag mineGridFlag = MainViewModel.GetMineGridFlag(row, col, out mineCountArround);

                    switch (mineGridFlag)
                    {
                        case MineGridFlag.Null:
                            //if (controlGrid.ContentTemplate != DataTemplateBlankGrid)
                            //{
                                controlGrid.ContentTemplate = DataTemplateBlankGrid;//DataTemplateNullGrid;
                            //}
                            break;
                        case MineGridFlag.Unknown:
                            //if (controlGrid.ContentTemplate != DataTemplateUnknownGrid)
                            //{
                                controlGrid.ContentTemplate = DataTemplateUnknownGrid;
                            //}
                            break;
                        case MineGridFlag.Mine:
                            controlGrid.ContentTemplate = DataTemplateMineGrid;
                            break;
                        case MineGridFlag.Mark:
                            controlGrid.ContentTemplate = DataTemplateUnknownGrid;//DataTemplateNullGrid;
                            break;
                        case MineGridFlag.MineCountNum:
                            controlGrid.ContentTemplate = DataTemplateNumGrid;//DataTemplateNumGrid;
                            controlGrid.Content = mineCountArround;
                            controlGrid.Foreground = GetNumForeGroundBrush(mineCountArround);
                            controlGrid.FontSize = 16;
                            break;
                    }
                }
                
            }
        }
    }
}
