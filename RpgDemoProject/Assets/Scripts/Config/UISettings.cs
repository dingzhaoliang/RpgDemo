using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New UISettings", menuName = "ScriptableObject/UISettings")]
public class UISettings : ScriptableObject
{
    public RenderMode renderMode;

    public bool orthographic;

    public CanvasScaler.ScaleMode uiScaleMode;

    public Vector2 referenceResolution;

    public CanvasScaler.ScreenMatchMode screenMathMode;

    [Range(0.0f, 1.0f)]
    public float matchWidthOrHeight;

    public List<UILayer> layers;
}

[Serializable]
public class UILayer
{
    public string name;
    public bool subCanvas = false;
}
