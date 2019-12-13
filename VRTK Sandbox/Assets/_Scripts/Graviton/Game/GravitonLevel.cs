using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Locomotion.Teleporters;
using Zinnia.Data.Type;

public class GravitonLevel : MonoBehaviour
{
  public GameObject startLocation;
  public LevelEnd levelEnd;
  public GameObject contentRoot;
  public event Action<GravitonLevel> sceneCompleted;
  
  private TeleporterFacade _teleporter;

  private void Awake()
  {    
    _teleporter = GameObject.FindWithTag("Teleporter").GetComponent<TeleporterFacade>();
  }

  public virtual void Setup()
  {
    Debug.Log("Setting up scene.");
    contentRoot.SetActive(true);
    MoveToStartLocation();
  }
  public virtual void TearDown()
  {
    Debug.Log("Tearing down scene.");
    contentRoot.SetActive(false);
  }

  protected void MoveToStartLocation()
  {
    if (_teleporter != null)
    {
      TransformData teleportDestination = new TransformData(startLocation.transform);
      _teleporter.Teleport(teleportDestination);
    }
  }
}
