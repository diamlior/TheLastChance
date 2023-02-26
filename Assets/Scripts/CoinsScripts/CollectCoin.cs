using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Equals("Ball"))
        {
            coinFX.Play();
            StaticData.addCoins(1);
            this.gameObject.SetActive(false);
        }
    }
}

