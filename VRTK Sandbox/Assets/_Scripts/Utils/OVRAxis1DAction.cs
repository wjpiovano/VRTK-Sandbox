using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class OVRAxis1DAction : FloatAction
{
  public OVRInput.Controller controller = OVRInput.Controller.Active;
  public OVRInput.Axis1D axis;

  void Update()
  {
    Receive(OVRInput.Get(axis, controller));
  }
}
