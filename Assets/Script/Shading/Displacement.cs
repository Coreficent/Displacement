﻿namespace Coreficent.Shading
{
    using UnityEngine;

    public class Displacement : MonoBehaviour
    {
        public AnimationCurve waveform = new AnimationCurve(
            new Keyframe(0.00f, 0.50f, 0, 0),
            new Keyframe(0.05f, 1.00f, 0, 0),
            new Keyframe(0.15f, 0.10f, 0, 0),
            new Keyframe(0.25f, 0.80f, 0, 0),
            new Keyframe(0.35f, 0.30f, 0, 0),
            new Keyframe(0.45f, 0.60f, 0, 0),
            new Keyframe(0.55f, 0.40f, 0, 0),
            new Keyframe(0.65f, 0.55f, 0, 0),
            new Keyframe(0.75f, 0.46f, 0, 0),
            new Keyframe(0.85f, 0.52f, 0, 0),
            new Keyframe(0.99f, 0.50f, 0, 0)
        );

        [Range(0.01f, 1.0f)]
        public float refractionStrength = 0.5f;

        public Color reflectionColor = Color.gray;

        [Range(0.01f, 1.0f)]
        public float reflectionStrength = 0.7f;

        [Range(1.0f, 3.0f)]
        public float waveSpeed = 1.25f;

        [Range(0.0f, 2.0f)]
        public float dropInterval = 0.5f;

        [SerializeField]
        Shader shader;

        Disturbance[] droplets;
        Texture2D gradTexture;
        Material material;
        float timer;
        int dropCount;

        void UpdateShaderParameters()
        {
            var camera = GetComponent<Camera>();

            material.SetVector("_Drop1", droplets[0].MakeShaderParameter(camera.aspect));
            material.SetVector("_Drop2", droplets[1].MakeShaderParameter(camera.aspect));
            material.SetVector("_Drop3", droplets[2].MakeShaderParameter(camera.aspect));

            material.SetColor("_Reflection", reflectionColor);
            material.SetVector("_Params1", new Vector4(camera.aspect, 1, 1 / waveSpeed, 0));
            material.SetVector("_Params2", new Vector4(1, 1 / camera.aspect, refractionStrength, reflectionStrength));
        }

        void Awake()
        {
            droplets = new Disturbance[3];
            droplets[0] = new Disturbance();
            droplets[1] = new Disturbance();
            droplets[2] = new Disturbance();

            gradTexture = new Texture2D(2048, 1, TextureFormat.Alpha8, false);
            gradTexture.wrapMode = TextureWrapMode.Clamp;
            gradTexture.filterMode = FilterMode.Bilinear;
            for (var i = 0; i < gradTexture.width; i++)
            {
                var x = 1.0f / gradTexture.width * i;
                var a = waveform.Evaluate(x);
                gradTexture.SetPixel(i, 0, new Color(a, a, a, a));
            }
            gradTexture.Apply();

            material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;
            material.SetTexture("_GradTex", gradTexture);

            UpdateShaderParameters();
        }

        void Update()
        {
            if (dropInterval > 0)
            {
                timer += Time.deltaTime;
                while (timer > dropInterval)
                {
                    Emit();
                    timer -= dropInterval;
                }
            }

            foreach (var d in droplets) d.Update();

            UpdateShaderParameters();
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, material);
        }

        public void Emit()
        {
            droplets[dropCount++ % droplets.Length].Reset();
        }
    }
}