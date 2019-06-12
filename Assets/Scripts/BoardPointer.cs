using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;
using Akangatu.Types;

public class BoardPointer : MonoBehaviour
{
    public Transform pointerGbj;

    void Update()
    {
        const int LAYER_BOARD = (int)LayersAndTags.LayerBoard;
        const int LAYER_BOARD_MASK = 1 << (int)LayersAndTags.LayerBoard;

        const int LAYER_CELL = (int)LayersAndTags.LayerCell;
        const int LAYER_CELL_MASK = 1 << (int)LayersAndTags.LayerCell;

        Ray pointerRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] pointerHits = Physics.RaycastAll(
            pointerRay,
            Mathf.Infinity,
            LAYER_BOARD_MASK | LAYER_CELL_MASK
        );

        if (pointerHits.Length == 0)
            return;

        int board_hit_idx = -1;
        int cell_hit_idx = -1;

        for (int i = 0; i < pointerHits.Length; i++)
        {
            switch (pointerHits[i].transform.gameObject.layer)
            {
                case LAYER_BOARD:
                    board_hit_idx = i;
                    break;
                case LAYER_CELL:
                    cell_hit_idx = i;
                    break;
            }
        }

        if (cell_hit_idx > -1)
        {
            pointerGbj.position = pointerHits[cell_hit_idx].transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                pointerHits[cell_hit_idx]
                    .transform
                    .Find("cedula")
                    .GetComponent<Animator>()
                    .SetTrigger("ClickCell");
            }
        }


#if UNITY_EDITOR
        if (board_hit_idx > -1)
            Debug.DrawLine(pointerRay.origin, pointerHits[board_hit_idx].point);

        if (cell_hit_idx > -1)
            Debug.DrawLine(
                pointerHits[cell_hit_idx].transform.position,
                pointerHits[cell_hit_idx].transform.position + Vector3.up * 2
            );
#endif
    }
}
