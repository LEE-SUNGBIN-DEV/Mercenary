using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    public bool IsSpawn { get; set; }

    public void Spawn();
}
