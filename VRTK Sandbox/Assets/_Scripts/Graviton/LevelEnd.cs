using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zinnia.Data.Type;
using VRTK.Prefabs.Locomotion.Teleporters;

public class LevelEnd : MonoBehaviour
{
  public event Action onLevelComplete;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      if (onLevelComplete != null)
        onLevelComplete();
    }
  }
}