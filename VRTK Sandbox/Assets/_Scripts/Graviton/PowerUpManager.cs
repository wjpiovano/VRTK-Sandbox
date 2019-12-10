using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
  private List<PowerUp> _powerUps;

  void Start()
  {
    _powerUps = GameObject.FindGameObjectsWithTag("PowerUp").Select(o => o.GetComponent<PowerUp>()).ToList();
  }

  void Update()
  {
    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
    {
      _powerUps.ForEach(pu => pu.TogglePowerUp(true));
    }
  }
}
