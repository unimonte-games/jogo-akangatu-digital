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
        int[] energies = new int[2] {0, 0};

        public GameMode gameMode = GameMode.Memory;

        public static GameMan instance;

        void Awake ()
        {
            playerNumber = 1;
            instance = this;
        }

        void NextTurn()
        {
            playerNumber = playerNumber == 1 ? 2 : 1;
        }

        void Victory()
        {
            Debug.Log("VICTORY OF " + playerNumber.ToString() + "!!");
        }

        public void MovePiece(Transform free_cell_tr)
        {
            if (gameMode != GameMode.Moving)
                return;

            string player_gbj_name = string.Concat("Player#", playerNumber);
            Transform player_tr = GameObject.Find(player_gbj_name).transform;
            Player player = player_tr.GetComponent<Player>();

            if (player.CanMove(free_cell_tr.position))
            {
                player.Move(free_cell_tr.position);

                if (player.IsInCenter())
                {
                    Victory();
                    Time.timeScale = 0f;
                    return;
                }

                energies[playerNumber-1] = energies[playerNumber-1] - 1;

                if (energies[playerNumber-1] == 0)
                {
                    gameMode = GameMode.Memory;
                    NextTurn();
                }
            }
        }

        public void UseCell(Transform deck_slot_tr)
        {
            Deck deck = deck_slot_tr.parent.GetComponent<Deck>();

            if (deck.playerNumber != playerNumber)
                return;

            Cell cell = deck.GetCell(deck_slot_tr.name);

            energies[playerNumber-1] =
                energies[playerNumber-1] + ((int)cell.rarity) + 1;

            cell.Discard();
            gameMode = GameMode.Moving;
        }

        void CancelCell(ref Cell cellToCancel)
        {
            cellToCancel = null;
            cell_counter = cell_counter - 1;
        }

        public void PickCell (Transform cell)
        {
            if (!(cell1 && cell == cell1.transform || cell2 && cell == cell2.transform))
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
            else
            {
                cell1.Flip();
                cell2.Flip();
            }

            cell_counter = 2;
            CancelCell(ref cell1);
            CancelCell(ref cell2);

            NextTurn();
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
