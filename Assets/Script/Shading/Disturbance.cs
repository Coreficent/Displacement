namespace Coreficent.Shading
{
    using UnityEngine;

    public class Disturbance
    {
        private Vector2 position = new Vector2();
        private float time = 1000.0f;

        public void Reset()
        {
            Vector3 mousePos = Input.mousePosition;
            position = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);
            time = 0;
        }

        public void Update()
        {
            time += Time.deltaTime;
        }

        public Vector4 CalculateShade(float aspect)
        {
            return new Vector4(position.x * aspect, position.y, time, 0.0f);
        }
    }
}
