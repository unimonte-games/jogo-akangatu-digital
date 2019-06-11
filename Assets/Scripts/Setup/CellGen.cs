using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;

namespace Akangatu
{
    public class CellGen : MonoBehaviour, ISetup
    {
        const int CELL_COUNT = 24;
        public GameObject[] cells = new GameObject[CELL_COUNT];

        static int[] usedCells = new int[CELL_COUNT];
        static int[] freeCells = new int[CELL_COUNT];
        static int freeCellsCount = CELL_COUNT;

        void Start()
        {
            Setup();
            AfterSetup();
        }

        public void Setup()
        {
            Random.InitState(Random.Range(0, 30000));

            int free_idx = Random.Range(0, freeCellsCount);
            int cell_idx = freeCells[free_idx];

            usedCells[cell_idx] = usedCells[cell_idx] + 1;

            if (usedCells[cell_idx] == 2)
            {
                for (int i = free_idx; i < freeCellsCount-1; i++)
                    freeCells[i] = freeCells[i + 1];

                freeCellsCount = freeCellsCount - 1;
            }

            GameObject gbj = Instantiate<GameObject>(
                cells[cell_idx],
                transform.position,
                Quaternion.identity,
                transform.parent
            );
        }

        public void AfterSetup()
        {
            Destroy(gameObject);
        }

        public static void Reset()
        {
            for (uint i = 0; i < CELL_COUNT; i++)
                usedCells[i] = 0;

            for (int i = 0; i < CELL_COUNT; i++)
                freeCells[i] = i;

            freeCellsCount = CELL_COUNT;
        }
    }
}
