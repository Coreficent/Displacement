namespace Coreficent.Shading
{
    using UnityEngine;

    public class Ripple
    {
        public readonly AnimationCurve DiminishingSine = new AnimationCurve(
            new Keyframe(0.00f, 0.50f, 0.0f, 0.0f),
            new Keyframe(0.05f, 1.00f, 0.0f, 0.0f),
            new Keyframe(0.15f, 0.10f, 0.0f, 0.0f),
            new Keyframe(0.25f, 0.80f, 0.0f, 0.0f),
            new Keyframe(0.35f, 0.30f, 0.0f, 0.0f),
            new Keyframe(0.45f, 0.60f, 0.0f, 0.0f),
            new Keyframe(0.55f, 0.40f, 0.0f, 0.0f),
            new Keyframe(0.65f, 0.55f, 0.0f, 0.0f),
            new Keyframe(0.75f, 0.46f, 0.0f, 0.0f),
            new Keyframe(0.85f, 0.52f, 0.0f, 0.0f),
            new Keyframe(0.99f, 0.50f, 0.0f, 0.0f)
        );

        public readonly AnimationCurve Linear = new AnimationCurve(
            new Keyframe(0.0f, 0.0f, 0.0f, 0.0f),
            new Keyframe(1.0f, 1.0f, 0.0f, 0.0f)
            );

        public readonly AnimationCurve Constant = new AnimationCurve(
            new Keyframe(1.0f, 1.0f, 0.0f, 0.0f),
            new Keyframe(1.0f, 1.0f, 0.0f, 0.0f)
            );

        public readonly AnimationCurve Sine = new AnimationCurve(
            new Keyframe(0.0f, 0.50f, 0.0f, 0.0f),
            new Keyframe(0.1f, 1.00f, 0.0f, 0.0f),
            new Keyframe(0.2f, 0.50f, 0.0f, 0.0f),
            new Keyframe(0.3f, 1.00f, 0.0f, 0.0f),
            new Keyframe(0.4f, 0.50f, 0.0f, 0.0f),
            new Keyframe(0.5f, 1.00f, 0.0f, 0.0f),
            new Keyframe(0.6f, 0.50f, 0.0f, 0.0f),
            new Keyframe(0.7f, 1.00f, 0.0f, 0.0f),
            new Keyframe(0.8f, 0.50f, 0.0f, 0.0f),
            new Keyframe(0.9f, 1.00f, 0.0f, 0.0f),
            new Keyframe(1.0f, 0.50f, 0.0f, 0.0f)
        );

        public readonly AnimationCurve EaseIn = new AnimationCurve(
            new Keyframe(0.00f, 0.00f, 0.0f, 0.0f),
            new Keyframe(0.25f, 0.00f, 0.0f, 0.0f),
            new Keyframe(1.00f, 1.00f, 0.0f, 0.0f)
        );

        public readonly AnimationCurve EaseOut = new AnimationCurve(
            new Keyframe(0.00f, 0.00f, 0.0f, 0.0f),
            new Keyframe(0.75f, 1.00f, 0.0f, 0.0f),
            new Keyframe(1.00f, 1.00f, 0.0f, 0.0f)
        );

        public readonly AnimationCurve Step = new AnimationCurve(
            new Keyframe(0.00f, 0.00f, 0.0f, 0.0f),
            new Keyframe(0.24f, 0.00f, 0.0f, 0.0f),
            new Keyframe(0.25f, 1.00f, 0.0f, 0.0f),
            new Keyframe(0.74f, 1.00f, 0.0f, 0.0f),
            new Keyframe(0.75f, 0.00f, 0.0f, 0.0f),
            new Keyframe(1.00f, 0.00f, 0.0f, 0.0f)
        );

        public readonly AnimationCurve Spike = new AnimationCurve(
            new Keyframe(0.00f, 0.00f, 0.0f, 0.0f),
            new Keyframe(0.49f, 0.00f, 0.0f, 0.0f),
            new Keyframe(0.50f, 1.00f, 0.0f, 0.0f),
            new Keyframe(0.51f, 0.00f, 0.0f, 0.0f),
            new Keyframe(1.00f, 0.00f, 0.0f, 0.0f)
        );
    }
}
