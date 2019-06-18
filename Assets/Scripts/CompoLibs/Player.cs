using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;

namespace Akangatu.CompoLibs
{
    public class Player : MonoBehaviour
    {
        Transform tr;
        SmoothMove sMove;

        void Awake()
        {
            tr = transform;
        }

        void Start()
        {
            sMove = GetComponent<SmoothMove>();
        }

        public bool IsInCenter()
        {
            return Vector3.Distance(sMove.targetPosition, Vector3.zero) < 0.5f;
        }

        public bool CanMove(Vector3 target_position)
        {
            return Vector3.Distance(sMove.targetPosition, target_position) < 1.6f;
        }

        public void Move(Vector3 target_position)
        {
            sMove.targetPosition = target_position;
        }
    }
}
