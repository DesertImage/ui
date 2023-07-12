using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DesertImage.UI
{
    [Serializable]
    public struct BlackScreenSettings
    {
        public Color Color;
        public float Alpha;
        [FormerlySerializedAs("AlphaDuration")] public float AlphaAnimationDuration;
    }
}