using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    void Update()
    {
        transform.Translate(-transform.forward * _bulletSpeed * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Flower"))
        {
            other.GetComponent<Flower>().DestroyTheFlower();
            BulletDestroy();
        }
    }
    private void BulletDestroy()
    {
        Destroy(gameObject);
    }
}
