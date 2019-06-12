using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;

namespace Akangatu
{
    public class GameMan : MonoBehaviour
    {
        Cell cell1;
        Cell cell2;
        uint last_cell_idx;

        public static GameMan gameManInstance;

        void Awake ()
        {
            gameManInstance = this;
        }

        public void PickCell (Transform cell)
        {
            last_cell_idx = last_cell_idx + 1;
        }
    }
}
