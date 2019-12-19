using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  public GameObject ballPrefab;
  public GameObject spawnLocation;

  public void Start()
  {
    #if UNITY_EDITOR
    //InvokeRepeating("Spawn", 3, 5);
    #endif
  }

  private void Update()
  {
    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
    {
      Spawn();
    }
  }

  public void Spawn()
  {
    var ballInstance = Instantiate(ballPrefab, spawnLocation.transform.position, Quaternion.identity);
    Destroy(ballInstance, 20);
  }
}
