﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletable : MonoBehaviour
{
  public void DestroyMe()
  {
    Destroy(this.gameObject);
  }
}
