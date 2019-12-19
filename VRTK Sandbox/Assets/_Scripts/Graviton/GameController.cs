using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
  public GameObject gBallPrefab;
  public GameObject victoryScreen;
  public List<GravitonLevel> levels;
  public int startingLevel = 0;

  private TrailRenderer _gBallTrailRenderer;
  private GravitonLevel _activeLevel;
  private bool _gamePaused = false;

  private void Start()
  {
    _gBallTrailRenderer = gBallPrefab.GetComponentInChildren<TrailRenderer>();

    for (int i = 0; i < levels.Count - 1; i++)
    {
      var levelIndex = i;
      levels[levelIndex].sceneCompleted += scene => LevelComplete(scene, levels[levelIndex+1]);
    }

    _activeLevel = levels[startingLevel];//.FirstOrDefault();
    _activeLevel?.Setup();
  }

  private GravitonLevel _finishedLevel;
  private GravitonLevel _nextLevel;
  private void LevelComplete(GravitonLevel finishedLevel, GravitonLevel nextLevel)
  {
    victoryScreen.SetActive(true);
    _finishedLevel = finishedLevel;
    _nextLevel = nextLevel;
  }

  public void TransitionLevel()  
  {
    victoryScreen.SetActive(false);
    _finishedLevel.TearDown();
    _nextLevel.Setup();
    _activeLevel = _nextLevel;
  }

  void Update()
  {
    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
    {
      if (_gamePaused)
        Pause();
      else
        Resume();
    }
    else if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
    {
      ToggleBallTrail();
    }

    #if UNITY_EDITOR
    // if (Input.GetKeyDown("space"))
    //     {
    //         ToggleBallTrail();
    //     }
    // if (Input.GetKeyDown("p"))
    // {
    //   if (_gamePaused)
    //     Pause();
    //   else
    //     Resume();
    // }
    #endif
  }

  private void Pause()
  {
    Time.timeScale = 1;
    _activeLevel.MuteAudio();
    _gamePaused = false;
  }

  private void Resume()
  {
    Time.timeScale = 0;
    _activeLevel.ResumeAudio();
    _gamePaused = true;
  }

  private void ToggleBallTrail()
  {
    _gBallTrailRenderer.enabled = !_gBallTrailRenderer.enabled;
  }
}
