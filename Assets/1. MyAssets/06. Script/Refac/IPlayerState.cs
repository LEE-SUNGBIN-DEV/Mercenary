using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    public void Enter(RPlayer player);
    public void Update(RPlayer player);
    public void Exit(RPlayer player);
}
