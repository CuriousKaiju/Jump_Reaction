using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShot : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPos;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Text _bulletsCounter;
    [SerializeField] private int _shotsCount;

    [SerializeField] private bool _isItButtonForRotation;

    [SerializeField] private PlatformForGun[] _pltaformWithGun;
    

    public void Shot()
    {
        if(!_isItButtonForRotation)
        {
            _bulletSpawnPos.gameObject.GetComponent<Animator>().SetTrigger("Shot");
            Destroy(Instantiate(_bulletPrefab, _bulletSpawnPos.position, _bulletSpawnPos.rotation), 3);
        }
        else
        {
            foreach(PlatformForGun platform in _pltaformWithGun)
            {
                platform.SetRotationParams();
            }
        }
    }
}
