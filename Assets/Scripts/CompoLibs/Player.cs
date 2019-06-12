using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;

namespace Akangatu.CompoLibs
{
    public class Player : MonoBehaviour
    {
        Transform tr;

        void Awake()
        {
            tr = transform;
        }

        public bool IsInCenter()
        {
            return Vector3.Distance(tr.position, Vector3.zero) < 0.5f;
        }

        public bool CanMove(Vector3 target_position)
        {
            return Vector3.Distance(tr.position, target_position) < 1.6f;
        }

        public void Move(Vector3 target_position)
        {
            tr.position = target_position;
        }
    }
}
