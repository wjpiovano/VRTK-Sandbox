using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedWall : MonoBehaviour
{
  public GameObject player;
  public ParticleSystem electricalExplosionPrefab;
  
  private AudioSource explosionAudio;

  private void Start()
  {
    explosionAudio = GetComponent<AudioSource>();
  }

  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag == player.tag)
    {
      var explosion = Instantiate(electricalExplosionPrefab, other.GetContact(0).point, Quaternion.identity);
      var explosionDuration = explosion.main.duration;
      explosionAudio.Play();
      explosion.Play();
      Destroy(explosion.gameObject, explosionDuration);
      Destroy(other.gameObject);
    }
  }
}
