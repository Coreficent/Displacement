namespace Coreficent.Controller
{
    using UnityEngine;

    public class MouseController
    {
        private Vector3 lastMousePosition = new Vector3();

        public bool MouseChanged()
        {
            return Input.mousePosition != lastMousePosition;
        }

        public void Update()
        {
            lastMousePosition = Input.mousePosition;
        }
    }
}
