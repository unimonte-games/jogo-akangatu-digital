using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;
using Akangatu.Types;
using Akangatu.CompoLibs;

public class BoardPointer : MonoBehaviour
{
    public Transform pointerGbj;
    public float offset;

    void Update()
    {
        const int LAYER_BOARD = (int)LayersAndTags.LayerBoard;
        const int LAYER_BOARD_MASK = 1 << (int)LayersAndTags.LayerBoard;

        const int LAYER_CELL = (int)LayersAndTags.LayerCell;
        const int LAYER_CELL_MASK = 1 << (int)LayersAndTags.LayerCell;

        const int LAYER_DECK_SLOT = (int)LayersAndTags.LayerDeckSlot;
        const int LAYER_DECK_SLOT_MASK = 1 << (int)LayersAndTags.LayerDeckSlot;

        Ray pointerRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] pointerHits = Physics.RaycastAll(
            pointerRay,
            Mathf.Infinity,
            LAYER_BOARD_MASK | LAYER_CELL_MASK | LAYER_DECK_SLOT_MASK
        );

        if (pointerHits.Length == 0)
            return;

        int board_hit_idx = -1;
        int cell_hit_idx = -1;
        int deck_slot_hit_idx = -1;

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
                case LAYER_DECK_SLOT:
                    deck_slot_hit_idx = i;
                    break;
            }
        }

        if (cell_hit_idx > -1)
        {
            Transform cell_hit_tr = pointerHits[cell_hit_idx].transform;
            pointerGbj.position = new Vector3 (cell_hit_tr.position.x, cell_hit_tr.position.y + offset, cell_hit_tr.position.z);

            if (Input.GetMouseButtonDown(0))
                GameMan.instance.PickCell(cell_hit_tr);
        }

        if (deck_slot_hit_idx > -1)
        {
            Transform deck_slot_hit_tr = pointerHits[deck_slot_hit_idx].transform;
            pointerGbj.position = deck_slot_hit_tr.position;

            if (Input.GetMouseButtonDown(0))
                GameMan.instance.UseCell(deck_slot_hit_tr);

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
