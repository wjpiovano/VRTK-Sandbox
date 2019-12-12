using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
  public float bounciness = 5;
  public ForceMode forceMode;
  private Rigidbody rb;
  private AudioSource bounceClip;

  private void Start()
  {
    rb = GetComponent<Rigidbody>();
    bounceClip = GetComponentInChildren<AudioSource>();
  }

  private void OnCollisionEnter(Collision other)
  {
    bounceClip.Play();

    var rbOther = other.rigidbody;
    //rb.AddForce(transform.forward * 100f);
    //rb.AddForce(Vector3.Reflect(transform.position, other.GetContact(0).normal) * 5, ForceMode.Impulse);
    //rb.AddForce(Vector3.up * -100f, ForceMode.Impulse);
    var moveVector = other.contacts[0].normal * -bounciness;
    Debug.DrawRay(other.contacts[0].point, moveVector, Color.red, 5);
    //Debug.DrawRay(transform.position, Vector3.up * 3, Color.red, 5);
    rbOther.AddForce(moveVector, forceMode);
    //transform.SetPositionAndRotation(new Vector3(0,0,0), Quaternion.identity);
  }
}
