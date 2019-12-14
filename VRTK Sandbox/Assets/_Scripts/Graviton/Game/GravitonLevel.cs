using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Locomotion.Teleporters;
using Zinnia.Data.Type;

public class GravitonLevel : MonoBehaviour
{
  public event Action<GravitonLevel> sceneCompleted;
  public GameObject startLocation;
  public LevelEnd levelEnd;
  public GameObject contentRoot;
  public GameObject player;
  public AudioSource soundtrack;
  
  private TeleporterFacade _teleporter;
  private bool _running = false;
  private float _startingSoundtrackVolume;

  private void Awake()
  {
    //_teleporter = GameObject.FindWithTag("Teleporter").GetComponent<TeleporterFacade>();
    levelEnd.onLevelComplete += OnLevelComplete;
    _startingSoundtrackVolume = soundtrack.volume;;
  }

  public virtual void Setup()
  {
    Debug.Log("Setting up scene.");
    contentRoot.SetActive(true);
    soundtrack.Play();
    MoveToStartLocation();
    _running = true;
  }

  public virtual void TearDown()
  {
    Debug.Log("Tearing down scene.");
    contentRoot.SetActive(false);
    soundtrack.Stop();
    _running = false;
  }

  protected void MoveToStartLocation()
  {
    if (_teleporter != null)
    {
      TransformData teleportDestination = new TransformData(startLocation.transform);
      _teleporter.Teleport(teleportDestination);
    }
    else
    {
      player.transform.SetPositionAndRotation(startLocation.transform.position, startLocation.transform.rotation);
    }
  }

  private void OnLevelComplete()
  {
    if (_running && sceneCompleted != null)
    {
      sceneCompleted(this);
    }
  }

  internal void MuteAudio()
  {    
    soundtrack.volume = _startingSoundtrackVolume;
  }

  internal void ResumeAudio()
  {
    soundtrack.volume = 0.1f;
  }
}
