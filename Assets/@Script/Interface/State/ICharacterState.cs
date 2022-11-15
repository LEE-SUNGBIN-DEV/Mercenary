using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{
    public int StateWeight { get; }

    public void Enter(Character character);
    public void Update(Character character);
    public void Exit(Character character);
}