namespace Coreficent.Interface
{
    using Coreficent.Main;
    using Coreficent.Shading;
    using Coreficent.Utility;
    using UnityEngine.UI;

    public class WaveControl : ReinforcedBehavior
    {
        public Main Main;

        public Slider WaveStrength;

        private readonly Wave wave = new Wave();

        public void OnSelectWave(string waveType)
        {
            DebugUtility.Bug("select wave", waveType);

            switch (waveType)
            {
                case "DeminishingSine":
                    Main.UpdateParameters(wave.DiminishingSine);
                    break;
                case "Linear":
                    Main.UpdateParameters(wave.Linear);
                    break;
                case "Constant":
                    Main.UpdateParameters(wave.Constant);
                    break;
                case "Sine":
                    Main.UpdateParameters(wave.Sine);
                    break;
                case "EaseIn":
                    Main.UpdateParameters(wave.EaseIn);
                    break;
                case "EaseOut":
                    Main.UpdateParameters(wave.EaseOut);
                    break;
                case "Step":
                    Main.UpdateParameters(wave.Step);
                    break;
                case "Spike":
                    Main.UpdateParameters(wave.Spike);
                    break;
                default:
                    DebugUtility.Warn("unexpected wave type");
                    break;
            }
        }
        
        public void OnSlide()
        {
            Main.WaveStrength = WaveStrength.value;
        }
    }
}