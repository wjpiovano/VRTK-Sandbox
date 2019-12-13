using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpPadMode { SurfaceNormal, Reflective, SurfaceNormalNoMass }

public class JumpPad : MonoBehaviour
{
  public float bounciness = 5;
  public ForceMode forceMode = ForceMode.Impulse;
  public JumpPadMode jumpPadMode = JumpPadMode.SurfaceNormalNoMass;

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
    if (jumpPadMode == JumpPadMode.SurfaceNormal)
    {
      var moveVector = other.contacts[0].normal * -bounciness;
      Debug.DrawRay(other.contacts[0].point, moveVector, Color.red, 5);
      rbOther.AddForce(moveVector, forceMode);
    }
    else if (jumpPadMode == JumpPadMode.Reflective)
    {
      rbOther.velocity = Vector3.zero;
      var moveVector = Vector3.Reflect(rbOther.velocity, other.contacts[0].normal) * -bounciness;
      Debug.DrawRay(other.contacts[0].point, moveVector, Color.red, 5);
      rbOther.AddForce(moveVector, forceMode);
    }
    else if (jumpPadMode == JumpPadMode.SurfaceNormalNoMass)
    {
      rbOther.velocity = Vector3.zero;
      rbOther.angularVelocity = Vector3.zero;
      var moveVector = other.GetContact(0).normal * -bounciness;
      Debug.DrawRay(other.contacts[0].point, moveVector, Color.red, 5);
      rbOther.AddForce(moveVector, forceMode);
    }
  }
}
