using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;
using Akangatu.Setup;

namespace Akangatu.Setup
{
    public class BoardGen : MonoBehaviour, ISetup
    {
        const uint GRID_COUNT = 9;

        public float cellSize;
        public GameObject cellGbj;
        public Transform gridGroup;

        void Start()
        {
            Setup();
            AfterSetup();
        }

        public void Setup()
        {
            CellGen.Reset();

            float gridSize = cellSize * GRID_COUNT;

            GetComponent<BoxCollider>().size = new Vector3(
                gridSize,
                1f,
                gridSize
            );

            Vector3 posInst = Vector3.zero;

            float pos_x = 0;
            float pos_z = 0;


            float halfGridSize = gridSize / 2;
            float halfCellSize = cellSize / 2;

            int meio_idx = Mathf.CeilToInt(GRID_COUNT/2);


            for (int i = 0; i < GRID_COUNT; i++)
            {
                pos_z = (cellSize * i) - halfGridSize + halfCellSize;

                for (int j = 0; j < GRID_COUNT; j++)
                {
                    bool meio = (i==j) && i == meio_idx;

                    if (meio)
                        continue;

                    pos_x = (cellSize * j) - halfGridSize + halfCellSize;

                    posInst.x = pos_x;
                    posInst.z = pos_z;

                    GameObject cellGbjInst = Instantiate<GameObject>(
                        cellGbj,
                        gridGroup.position,
                        Quaternion.identity,
                        gridGroup
                    );

                    Transform cellTrInst = cellGbjInst.GetComponent<Transform>();
                    cellTrInst.localPosition = posInst;
                }
            }
        }

        public void AfterSetup()
        {
            Destroy(this);
        }

#if UNITY_EDITOR
        bool rodandoPeloEditor;

        void Awake ()
        {
            rodandoPeloEditor = true;
        }

        void OnValidate()
        {
            if (!rodandoPeloEditor)
                return;

            for (int i = gridGroup.childCount-1; i >= 0; i--)
                Destroy(gridGroup.GetChild(i).gameObject);

            Setup();
        }
#endif


    }

}
