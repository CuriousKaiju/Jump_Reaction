using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOnPlayerMesh : MonoBehaviour
{
    [SerializeField] private AudioSource _jumpSound;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayJumpSound()
    {
        _jumpSound.Play();
    }
    
}
