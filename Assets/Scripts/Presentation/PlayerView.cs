﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Actor _actor;
    private CardPlayer _player;
    private HandLayout _handView;

    private void Awake()
    {
        _player = gameObject.GetComponent<CardPlayer>();
        _handView = gameObject.GetComponentInChildren<HandLayout>();
    }

    public void Init(Actor actor)
    {
        _actor = actor;
    }
}
