using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;
using Akangatu.Types;

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

            tr.position =
                GameObject
                .Find(DeckJX)
                .transform
                .position;

            Destroy(GetComponent<BoxCollider>());
        }

        public void Discard()
        {
            if (itsFlipped)
                Flip();

            Transform discard_tr = GameObject.Find("Discard").transform;

            tr.position = discard_tr.position;

            discard_tr.position = new Vector3(
                Mathf.Sin(discard_tr.position.z*2)/2f - 6.3f,
                0f,
                discard_tr.position.z - 0.2f
            );

            Destroy(GetComponent<BoxCollider>());
        }
    }
}
