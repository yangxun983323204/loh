using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int Id { get; set; }

    public override string ToString()
    {
        return Id.ToString();
    }
}
