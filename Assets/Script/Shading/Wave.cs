namespace Coreficent.Shading
{
    using Coreficent.Utility;
    using UnityEngine;

    public class Wave
    {
        public AnimationCurve DiminishingSineWave = new AnimationCurve(
            new Keyframe(0.00f, 0.50f, 0, 0),
            new Keyframe(0.05f, 1.00f, 0, 0),
            new Keyframe(0.15f, 0.10f, 0, 0),
            new Keyframe(0.25f, 0.80f, 0, 0),
            new Keyframe(0.35f, 0.30f, 0, 0),
            new Keyframe(0.45f, 0.60f, 0, 0),
            new Keyframe(0.55f, 0.40f, 0, 0),
            new Keyframe(0.65f, 0.55f, 0, 0),
            new Keyframe(0.75f, 0.46f, 0, 0),
            new Keyframe(0.85f, 0.52f, 0, 0),
            new Keyframe(0.99f, 0.50f, 0, 0)
        );

        public AnimationCurve LinearWave = new AnimationCurve(
            new Keyframe(0.0f, 0.0f, 0, 0),
            new Keyframe(1.0f, 1.0f, 0, 0)
            );
    }
}
