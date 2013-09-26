using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AntGameMine.Views
{
    class MainViewGridCache
    {
        List<UserControl> _controls = new List<UserControl>();

        public UserControl[] GetGridArray(int gridCount)
        {
            if (_controls.Count < gridCount)
            {
                int needGridCount = gridCount - _controls.Count;
                for (int i = 0; i < needGridCount; i++)
                {
                    _controls.Add(new UserControl());
                }
            }
            return _controls.GetRange(0, gridCount).ToArray();
        }
    }
}
