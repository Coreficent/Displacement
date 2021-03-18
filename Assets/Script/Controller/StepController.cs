namespace Coreficent.Controller
{
    using UnityEngine;

    public class StepController
    {
        private readonly float frequency = 0.15f;
        private float currentTime = 0.0f;

        public void Update()
        {
            currentTime += Time.deltaTime;
        }

        public bool HasNext()
        {
            return currentTime > frequency;
        }

        public void Next()
        {
            currentTime -= frequency;
        }
    }
}
