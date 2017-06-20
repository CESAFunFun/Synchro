using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearZone : MonoBehaviour
{
    private ParticleSystem _clearPar;

    private void Start()
    {
        _clearPar = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Child")
        {
            GameObject.Find("GameManager").SendMessage("Clear");
            _clearPar.Play();
        }
    }
}
