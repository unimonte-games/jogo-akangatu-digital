using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akangatu;

namespace Akangatu
{
    public class CellGen : MonoBehaviour, ISetup
    {
        public GameObject[] cellsByRarity = new GameObject[3];

        void Start()
        {
            Setup();
            AfterSetup();
        }

        public void Setup()
        {
            Rarity rarity = Rarity.Frequent;

            Random.InitState(Random.Range(0, 1000000));
            float r = Random.Range(0f, 1f);

            RarityMethods.FromF(r, out rarity);

            GameObject gbj = Instantiate<GameObject>(
                cellsByRarity[(int)rarity],
                transform.position,
                Quaternion.identity,
                transform.parent
            );

            gbj.name = string.Concat(rarity.ToString(), " Cell");
        }

        public void AfterSetup()
        {
            Destroy(gameObject);
        }

        //IEnumerator COInst()
        //{
//
        //}
    }
}
