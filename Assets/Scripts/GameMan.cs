using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;
using Akangatu.CompoLibs;
using Akangatu.Types;

namespace Akangatu
{
    public class GameMan : MonoBehaviour
    {
        const uint FUNC_ID_CompareCellsAndProceed = 0;

        Cell cell1;
        Cell cell2;
        uint cell_counter = 0;
        public int playerNumber = 1;

        public static GameMan instance;

        void Awake ()
        {
            playerNumber = 1;
            instance = this;
        }

        void CancelCell(ref Cell cellToCancel)
        {
            cellToCancel = null;
            cell_counter = cell_counter - 1;
        }

        public void PickCell (Transform cell)
        {
            if (cell1 && cell == cell1.transform)
            {
                cell1.Flip();
                CancelCell(ref cell1);
            }
            else if (cell2 && cell == cell2.transform)
            {
                cell2.Flip();
                CancelCell(ref cell2);
            }
            else
            {
                cell_counter = cell_counter + 1;

                switch (cell_counter)
                {
                    case 1:
                        cell1 = cell.GetComponent<Cell>();
                        cell1.Flip();
                        break;
                    case 2:
                        cell2 = cell.GetComponent<Cell>();
                        cell2.Flip();
                        StartCoroutine(
                            WaitAndDo(1f, FUNC_ID_CompareCellsAndProceed)
                        );
                        break;
                }
            }
        }

        void CompareCellsAndProceed()
        {
            bool sameAnimals = cell1 && cell2 && cell1.cellId == cell2.cellId;

            if (sameAnimals)
            {
                cell1.GoToDeck();
                cell2.Discard();
            }

            cell_counter = 2;
            CancelCell(ref cell1);
            CancelCell(ref cell2);

            playerNumber = playerNumber == 1 ? 2 : 1;
        }

        IEnumerator WaitAndDo(float t, uint funcID)
        {
            yield return new WaitForSeconds(t);

            switch (funcID)
            {
                case FUNC_ID_CompareCellsAndProceed:
                    CompareCellsAndProceed();
                    break;
            }
        }
    }
}
