using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [Header("FLOWER DETAILS")]

    [SerializeField] private GameObject _diePaticles;
    [SerializeField] private AudioSource _cutTheFlower;
    private Animator _flowerAnimator;

    void Start()
    {
        _flowerAnimator = GetComponent<Animator>();
    }
    public void PlayAttackAnim()
    {
        _flowerAnimator.SetTrigger("attack");
    }
    public void FinishAteAnim()
    {
        MainGamesEvents.OnActionPlayerLose();
    }
    public void DestroyTheFlower()
    {
        _cutTheFlower.Play();
        GetComponent<Collider>().enabled = false;
        Instantiate(_diePaticles, transform.position, Quaternion.identity);
        _flowerAnimator.SetTrigger("Die");
    }
    public void DestroyAfterAnim()
    {
        Destroy(gameObject);
    }
}
