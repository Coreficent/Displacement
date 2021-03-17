namespace Coreficent.Interface
{
    using Coreficent.Main;
    using Coreficent.Utility;
    using UnityEngine;


    public class InterfaceControl : MonoBehaviour
    {

        public void Render()
        {
            DebugUtility.Bug("render called");
        }

        public void SelectPreset(int index)
        {
            DebugUtility.Bug("select", index);
        }

        public void Slide()
        {

        }
    }
}