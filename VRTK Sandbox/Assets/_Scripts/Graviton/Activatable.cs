using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
  public Animator animator;

  public void Activate()
  {
    if (animator != null)
      animator.SetTrigger("Activate");
  }

  public void Deactivate()
  {
    if (animator != null)
      animator.SetTrigger("Deactivate");
  }
}
