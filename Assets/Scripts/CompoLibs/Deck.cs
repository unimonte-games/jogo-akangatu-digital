using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;
using Akangatu.Types;
using Akangatu.CompoLibs;

namespace Akangatu.CompoLibs
{
    public class Deck : MonoBehaviour
    {
        public int playerNumber;
        Transform tr;

        Transform[] slots = new Transform[2];

        void Awake()
        {
            tr = GetComponent<Transform>();
            slots[0] = tr.GetChild(0);
            slots[1] = tr.GetChild(1);
        }

        public void Deposit(Transform cell_tr)
        {
            if (slots[1].childCount > 0)
                slots[1].GetChild(0).GetComponent<Cell>().Discard();

            if (slots[0].childCount > 0)
                DepositCell(slots[0].GetChild(0), 1);

            DepositCell(cell_tr, 0);
        }

        void DepositCell(Transform cell_tr, int idx)
        {
            cell_tr.SetParent(slots[idx]);
            cell_tr.localPosition = Vector3.zero;
        }

        public void DiscardDepositedCell(string slotName)
        {
            int slotIdx = slotName == "0" ? 0 : 1;
            slots[slotIdx].GetChild(0).GetComponent<Cell>().Discard();
        }
    }
}
