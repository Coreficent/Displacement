namespace Coreficent.Shading
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Disturbance
    {
        Vector2 position;
        float time;

        public Disturbance()
        {
            time = 1000;
        }

        public void Reset()
        {
            position = new Vector2(Random.value, Random.value);
            time = 0;
        }

        public void Update()
        {
            time += Time.deltaTime;
        }

        public Vector4 MakeShaderParameter(float aspect)
        {
            return new Vector4(position.x * aspect, position.y, time, 0);
        }
    }
}
