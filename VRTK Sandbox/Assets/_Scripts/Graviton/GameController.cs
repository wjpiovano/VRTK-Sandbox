using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
  public GameObject gBallPrefab;
  public AudioSource soundtrack;
  public List<GravitonLevel> levels;

  private TrailRenderer _gBallTrailRenderer;
  private bool _timePaused = false;
  private float _startingSoundtrackVolume;  

  private void Start()
  {
    _gBallTrailRenderer = gBallPrefab.GetComponent<TrailRenderer>();
    _startingSoundtrackVolume = soundtrack.volume;

    for (int i = 0; i < levels.Count - 1; i++)
    {
      levels[i].sceneCompleted += scene => TransitionLevel(scene, levels[i+1]);
    }
  }

  private void TransitionLevel(GravitonLevel finishedScene, GravitonLevel nextScene)  
  {
    finishedScene.TearDown();
    nextScene.Setup();
  }

  void Update()
  {
    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
    {
      if (_timePaused)
        Pause();
      else
        Resume();
    }
    else if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
    {
      ToggleBallTrail();
    }

    #if UNITY_EDITOR
    if (Input.GetKeyDown("space"))
        {
            ToggleBallTrail();
        }
    if (Input.GetKeyDown("p"))
    {
      if (_timePaused)
        Pause();
      else
        Resume();
    }
    #endif
  }

  private void Pause()
  {
    Time.timeScale = 1;
    soundtrack.volume = _startingSoundtrackVolume;
    _timePaused = false;
  }

  private void Resume()
  {
    Time.timeScale = 0;
    soundtrack.volume = 0.1f;
    _timePaused = true;
  }

  private void ToggleBallTrail()
  {
    _gBallTrailRenderer.enabled = !_gBallTrailRenderer.enabled;
  }
}
