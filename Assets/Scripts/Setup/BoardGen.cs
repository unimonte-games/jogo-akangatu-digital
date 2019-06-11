using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;

namespace Akangatu
{

    public class BoardGen : MonoBehaviour, ISetup
    {
        public float cellSize;
        [Header("Must be an odd number")]
        public uint gridCount;
        public GameObject cellGbj;
        public Transform gridGroup;

#if UNITY_EDITOR
        bool rodandoPeloEditor;
#endif

        void Start()
        {
#if UNITY_EDITOR
            rodandoPeloEditor = true;
#endif
            Setup();
            AfterSetup();
        }

        public void Setup()
        {
            Vector3 posInst = Vector3.zero;

            float pos_x = 0;
            float pos_z = 0;

            float gridSize = cellSize * gridCount;

            float halfGridSize = gridSize / 2;
            float halfCellSize = cellSize / 2;

            int meio_idx = Mathf.CeilToInt(gridCount/2);

            for (int i = 0; i < gridCount; i++)
            {
                pos_z = (cellSize * i) - halfGridSize + halfCellSize;

                for (int j = 0; j < gridCount; j++)
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
