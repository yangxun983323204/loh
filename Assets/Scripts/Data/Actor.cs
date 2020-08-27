using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewActor", menuName = "LOH/Actor")]
public class Actor : ScriptableObject
{
    public ActorData Data;
    public ActorRenderData RenderData;
    [System.NonSerialized]
    public List<Effect> Effects;
    [System.NonSerialized]
    public ActorRenderer Renderer;
    public void SetRenderer(ActorRenderer renderer)
    {
        Renderer = renderer;
        Renderer.Target = this;
    }
}