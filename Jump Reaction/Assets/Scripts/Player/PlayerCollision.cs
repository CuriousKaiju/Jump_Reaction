using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("TAGS")]

    [SerializeField] private string _coinTag;
    //[SerializeField] private AudioSource _StarSound;  

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(_coinTag))
        {
            //_StarSound.Play();
            other.gameObject.GetComponent<CoinFunction>().DestroyCoin();
        }
    }
}
