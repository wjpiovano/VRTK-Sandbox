using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceptor : MonoBehaviour
{
  public Activatable activatable;
  public AudioClip activatedClip;
  public AudioClip deactivatedClip;
  public Material activeMaterial;
  public MeshRenderer mesh;

  private int _activationCount = 0;
  private Material[] activeMaterialsArray;
  private Material[] inactiveMaterialsArray;
  private AudioSource audioSource;
  private void Start()
  {
    audioSource = GetComponent<AudioSource>();

    inactiveMaterialsArray = mesh.materials;

    activeMaterialsArray = mesh.materials;
    activeMaterialsArray[1] = activeMaterial;
  }

  public void PowerUpReceptor()
  {
    activatable.Activate();

    if (activeMaterialsArray != null)
    {
      audioSource.PlayOneShot(activatedClip);
      mesh.materials = activeMaterialsArray;
    }

    _activationCount++;
  }

  public void PowerDownReceptor()
  {
    if (_activationCount == 0)
    {
      return;
    }

    _activationCount--;

    if (_activationCount == 0 && inactiveMaterialsArray != null)
    {
      activatable.Deactivate();
      audioSource.PlayOneShot(deactivatedClip);
      mesh.materials = inactiveMaterialsArray;
    }
  }
}
