using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AntGameMine.ViewModels
{
    public class MineGameCommands
    {
        private static RoutedUICommand _startGame;

        static MineGameCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
            _startGame = new RoutedUICommand("StartGame", "开始游戏", typeof(MineGameCommands));
        }

        public static RoutedUICommand StartGame
        {
            get { return _startGame; }
        }
    }
}
