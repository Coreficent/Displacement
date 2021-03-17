namespace Coreficent.Interface
{
    using Coreficent.Utility;
    using UnityEngine;

    public class InterfaceControl : ReinforcedBehavior
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