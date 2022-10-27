using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{
    public int StateWeight { get; }

    public void Enter(RCharacter character);
    public void Update(RCharacter character);
    public void Exit(RCharacter character);
}