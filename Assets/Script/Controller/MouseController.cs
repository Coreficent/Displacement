namespace Coreficent.Controller
{
    using UnityEngine;

    /*
     * determines the mouse position
     */

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
