namespace Coreficent.Interface
{
    using Coreficent.Main;
    using Coreficent.Shading;
    using Coreficent.Utility;
    using UnityEngine.UI;

    public class RippleControl : ReinforcedBehavior
    {
        public Main Main;
        public Slider WaveStrength;

        private readonly Ripple wave = new Ripple();

        public void OnSelectWave(string waveType)
        {
            switch (waveType)
            {
                case "DeminishingSine":
                    Main.UpdateShader(wave.DiminishingSine);
                    break;
                case "Linear":
                    Main.UpdateShader(wave.Linear);
                    break;
                case "Constant":
                    Main.UpdateShader(wave.Constant);
                    break;
                case "Sine":
                    Main.UpdateShader(wave.Sine);
                    break;
                case "EaseIn":
                    Main.UpdateShader(wave.EaseIn);
                    break;
                case "EaseOut":
                    Main.UpdateShader(wave.EaseOut);
                    break;
                case "Step":
                    Main.UpdateShader(wave.Step);
                    break;
                case "Spike":
                    Main.UpdateShader(wave.Spike);
                    break;
                default:
                    Test.Warn("unexpected wave type");
                    break;
            }
        }

        public void OnSlide()
        {
            Main.WaveStrength = WaveStrength.value;
        }
    }
}