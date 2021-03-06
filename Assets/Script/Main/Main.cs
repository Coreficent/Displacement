namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Shading;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    /*
     * the main entry of the program
     */

    public class Main : ReinforcedBehavior
    {
        [SerializeField]
        private Shader shader;

        [SerializeField]
        private Camera mainCamera;

        private List<Disturbance> disturbances = new List<Disturbance>();
        private readonly StepController stepController = new StepController();
        private readonly MouseController mouseController = new MouseController();
        private readonly int disturbanceCount = 64;
        private int disturbanceIndex = 0;
        private float waveStrength = 0.5f;

        private Texture2D gradient;
        private Material material;

        public float WaveStrength
        {
            set { waveStrength = value; }
        }

        protected virtual void Start()
        {
            UpdateShader(new Ripple().DiminishingSine);
        }

        protected void Update()
        {
            while (stepController.HasNext())
            {
                if (mouseController.MouseChanged())
                {
                    disturbances[disturbanceIndex++ % disturbances.Count].Reset();
                }
                stepController.Next();
            }

            foreach (var disturbance in disturbances)
            {
                disturbance.Update();
            }

            stepController.Update();
            mouseController.Update();
            UploadToShader();
        }

        protected void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, material);
        }

        public void UpdateShader(AnimationCurve wave)
        {
            disturbances.Clear();

            for (var i = 0; i < disturbanceCount; ++i)
            {
                disturbances.Add(new Disturbance(mainCamera.aspect));
            }

            gradient = new Texture2D(2048, 1, TextureFormat.Alpha8, false);
            gradient.wrapMode = TextureWrapMode.Clamp;
            gradient.filterMode = FilterMode.Bilinear;

            for (var i = 0; i < gradient.width; ++i)
            {
                float positionX = 1.0f / gradient.width * i;
                float positionY = wave.Evaluate(positionX);
                gradient.SetPixel(i, 0, new Color(positionY, positionY, positionY, positionY));
            }

            gradient.Apply();

            material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;
            material.SetTexture("_GradientTex", gradient);

            UploadToShader();
        }

        private void UploadToShader()
        {
            List<Vector4> buffer = new List<Vector4>();

            foreach (var disturbance in disturbances)
            {
                buffer.Add(disturbance.CalculateShade());
            }

            material.SetInt("_DisturbanceCount", buffer.Count);
            material.SetVectorArray("_Disturbances", buffer);

            material.SetFloat("_Aspect", mainCamera.aspect);
            material.SetFloat("_Strength", waveStrength);
        }
    }
}
