namespace Coreficent.Main
{
    using Coreficent.Shading;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : ReinforcedBehavior
    {
        [Range(0.01f, 1.0f)]
        public float refractionStrength = 0.5f;

        public Color reflectionColor = Color.gray;

        [Range(0.01f, 1.0f)]
        public float reflectionStrength = 0.7f;

        [Range(0.01f, 3.0f)]
        public float waveSpeed = 1.25f;

        private float interval = 0.05f;

        [SerializeField]
        Shader shader;

        List<Disturbance> disturbances = new List<Disturbance>();
        int disturbanceCount = 64;

        Texture2D gradient;
        Material material;
        float timer;
        int count;

        Vector3 lastMousePosition;

        void UpdateShaderParameters()
        {
            var camera = GetComponent<Camera>();

            List<Vector4> buffer = new List<Vector4>();
            foreach (var disturbance in disturbances)
            {
                buffer.Add(disturbance.MakeShaderParameter(camera.aspect));
            }

            material.SetInt("_DisturbanceCount", buffer.Count);
            material.SetVectorArray("_Disturbance", buffer);

            material.SetColor("_Reflection", reflectionColor);
            material.SetVector("_Params1", new Vector4(camera.aspect, 1, 1 / waveSpeed, 0));
            material.SetVector("_Params2", new Vector4(1, 1 / camera.aspect, refractionStrength, reflectionStrength));
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
            if (interval > 0)
            {
                timer += Time.deltaTime;
                while (timer > interval)
                {
                    Emit();
                    timer -= interval;
                }
            }

            foreach (var d in disturbances) d.Update();

            UpdateShaderParameters();
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, material);
        }

        public void Emit()
        {
            if (Input.mousePosition != lastMousePosition)
            {
                disturbances[count++ % disturbances.Count].Reset();
            }
            lastMousePosition = Input.mousePosition;
        }
    }
}
