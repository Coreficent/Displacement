namespace Coreficent.Interface
{
    using Coreficent.Main;
    using Coreficent.Shading;
    using Coreficent.Utility;

    public class WaveControl : InterfaceControl
    {
        public Main Main;

        private Wave wave = new Wave();

        public void OnSelectWave(string waveType)
        {
            DebugUtility.Bug("select wave", waveType);

            switch (waveType)
            {
                case "DeminishingSine":
                    Main.UpdateParameters(wave.DiminishingSineWave);
                    break;
                case "Linear":
                    Main.UpdateParameters(wave.LinearWave);
                    break;
                default:
                    DebugUtility.Warn("unexpected wave type");
                    break;
            }
        }
    }
}