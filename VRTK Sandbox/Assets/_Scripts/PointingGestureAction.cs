using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class PointingGestureAction : BooleanAction
{
  public bool gripTrigger { get; set; }
  public bool indexTrigger { get; set; }
  public bool isGrabbing { get; set; }

  void Update()
  {
    Receive(gripTrigger && !indexTrigger && !isGrabbing);
  }
}
