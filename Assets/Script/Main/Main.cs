﻿namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Shading;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : ReinforcedBehavior
    {
        [SerializeField]
        private Shader shader;

        [SerializeField]
        private Camera mainCamera;

        private readonly float waveSpeed = 0.25f;
        private readonly float reflectionStrength = 0.2f;

        private int disturbanceCount = 64;
        private int count;
        private float waveStrength = 0.5f;

        private Texture2D gradient;
        private StepController stepController = new StepController();
        private Material material;
        private List<Disturbance> disturbances = new List<Disturbance>();
        private readonly MouseController mouseController = new MouseController();

        public float WaveStrength
        {
            set { waveStrength = value; }
        }

        void UpdateShaderParameters()
        {
            List<Vector4> buffer = new List<Vector4>();
            foreach (var disturbance in disturbances)
            {
                buffer.Add(disturbance.CalculateShade(mainCamera.aspect));
            }

            material.SetInt("_DisturbanceCount", buffer.Count);
            material.SetVectorArray("_Disturbance", buffer);

            material.SetColor("_Reflection", Color.gray);
            material.SetVector("_Params1", new Vector4(mainCamera.aspect, 1, 1 / waveSpeed, 0));
            material.SetVector("_Params2", new Vector4(1, 1 / mainCamera.aspect, waveStrength, reflectionStrength));
        }

        protected override void Awake()
        {
            UpdateParameters(new Wave().DiminishingSine);
        }

        public void UpdateParameters(AnimationCurve wave)
        {
            disturbances.Clear();

            for (var i = 0; i < disturbanceCount; ++i)
            {
                disturbances.Add(new Disturbance());
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
            material.SetTexture("_GradTex", gradient);

            UpdateShaderParameters();
        }

        void Update()
        {
            stepController.Update();
            while (stepController.HasNext())
            {
                if (mouseController.MouseChanged())
                {
                    disturbances[count++ % disturbances.Count].Reset();
                    mouseController.Update();
                }
                stepController.Next();
            }

            foreach (var disturbance in disturbances)
            {
                disturbance.Update();
            }

            UpdateShaderParameters();
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, material);
        }
    }
}
