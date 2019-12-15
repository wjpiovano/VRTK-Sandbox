using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
  public GameObject player;

  private ParticleSystem particleExplosion;
  private List<MeshRenderer> meshRenderers;
  private AudioSource pickupSound;

  private void Start()
  {
    particleExplosion = GetComponent<ParticleSystem>();
    meshRenderers = GetComponentsInChildren<MeshRenderer>().ToList();
    pickupSound = GetComponent<AudioSource>();    

    #if UNITY_EDITOR
    InvokeRepeating("DebugToggleOn", 5, 5);
    #endif
  }

  private void DebugToggleOn()
  {
    TogglePowerUp(true);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == player.tag && gameObject.activeSelf)
    {
      pickupSound.Play();
      meshRenderers.ForEach(mr => mr.enabled = false);
      particleExplosion.Play();
      Invoke("DisablePowerUp", particleExplosion.main.duration);
    }
  }

  private void DisablePowerUp()
  {
    gameObject.SetActive(false);
  }

  public void TogglePowerUp(bool turnOn)
  {
    gameObject.SetActive(turnOn);
    meshRenderers.ForEach(mr => mr.enabled = turnOn);
  }
}
