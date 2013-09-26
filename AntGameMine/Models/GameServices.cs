using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntGameMine.Models
{
    public class GameServices
    {
        public static GameServices _instance = new GameServices();
        private MineGameModel _mineGameModel = new MineGameModel();

        public static GameServices Instance
        {
            get { return _instance; }
        }

        public MineGameModel MineGameModel
        {
            get { return _mineGameModel; }
        }
    }
}
