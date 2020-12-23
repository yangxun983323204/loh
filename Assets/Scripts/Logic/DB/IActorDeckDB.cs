using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorDeckDB
{
    Deck GetDeck(int actorId);
}
