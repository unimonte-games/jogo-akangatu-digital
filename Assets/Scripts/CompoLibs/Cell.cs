using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;
using Akangatu.Types;
using Akangatu.CompoLibs;

namespace Akangatu.CompoLibs
{
    public class Cell : MonoBehaviour
    {
        public Rarity rarity;
        public CellID cellId;
        bool itsFlipped;

        Animator cell_animator;
        Transform cedula;
        Transform tr;

        void Awake ()
        {
            tr = GetComponent<Transform>();
            cedula = tr.Find("cedula");
            cell_animator = cedula.GetComponent<Animator>();
        }

        public void Flip()
        {
            itsFlipped = !itsFlipped;
            cell_animator.SetTrigger("ClickCell");
        }

        public void GoToDeck()
        {
            if (!itsFlipped)
                Flip();

            string DeckJX = string.Concat(
                "DeckJ",
                GameMan.instance.playerNumber
            );

            GameObject
                .Find(DeckJX)
                .GetComponent<Deck>()
                .Deposit(tr);

            Destroy(GetComponent<BoxCollider>());
        }

        public void Discard()
        {
            if (itsFlipped)
                Flip();

            Transform discard_point_tr = GameObject.Find("DiscardPoint").transform;
            tr.SetParent(GameObject.Find("Discard").transform);
            tr.GetComponent<SmoothMove>().targetPosition =
                discard_point_tr.localPosition;

            discard_point_tr.position = new Vector3(
                Mathf.Sin(discard_point_tr.position.z*2)/2f - 6.3f,
                discard_point_tr.position.y + 0.05f,
                discard_point_tr.position.z - 0.2f
            );

            Destroy(GetComponent<BoxCollider>());
        }
    }
}
