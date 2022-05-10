using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFunction : MonoBehaviour
{
    [Header("COIN VARIABLES")]

    [SerializeField] private GameObject[] _particle;
    [SerializeField] private int _coinValue;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _rotationDirection;

    
    void Update()
    {
        CoinRotation();
    }
    private void CoinRotation()
    {
        transform.Rotate(_rotationDirection, _rotationSpeed * Time.deltaTime);
    }
    public void DestroyCoin()
    {
        foreach(GameObject particles in _particle)
        {
            Instantiate(particles, transform.position, Quaternion.Euler(0, 0, 0));
        }    
        MainGamesEvents.OnActionCollectCoin(_coinValue);
        Destroy(transform.parent.gameObject);
    }
}
