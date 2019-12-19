using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
  public Animator animator;
  public string activationTrigger = "Activate";
  public string deactivationTrigger = "Deactivate";

  public void Activate()
  {
    if (animator != null)
      animator.SetTrigger(activationTrigger);
  }

  public void Deactivate()
  {
    if (animator != null)
      animator.SetTrigger(deactivationTrigger);
  }
}
