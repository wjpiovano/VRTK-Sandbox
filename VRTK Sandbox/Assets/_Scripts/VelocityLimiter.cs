using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityLimiter : MonoBehaviour
{
  public float maxVelocity = 100;

  private Rigidbody rigidBody;

  void Start()
  {
    rigidBody = GetComponent<Rigidbody>();
  }

  void FixedUpdate()
  {
    rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);
  }
}
