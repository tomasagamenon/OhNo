using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShader : MonoBehaviour
{
   public enum PixelScreenMode { resize, scale}

    [System.Serializable]
    public struct ScreenSize
    {
        public int width;
        public int height;
    }

    [Header("Screen scaling settings")]
    public PixelScreenMode mode;
    public ScreenSize targetScreenSize = new ScreenSize { width = 256, height = 144 };
    public uint screenScaleFactor = 1;

    private Camera _renderCamera;
    private RenderTexture _renderTexture;
    private int screenWidth, screenHeight;

    [Header("Display")]
    public RawImage display;

    public void Init()
    {
        if (!_renderCamera) _renderCamera = GetComponent<Camera>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        if (screenScaleFactor < 1) screenScaleFactor = 1;
        if (targetScreenSize.width < 1) targetScreenSize.width = 1;
        if (targetScreenSize.height < 1) targetScreenSize.height = 1;

        int width = mode == PixelScreenMode.resize ? (int)targetScreenSize.width : screenWidth / (int)screenScaleFactor;
        int height = mode == PixelScreenMode.resize ? (int)targetScreenSize.width : screenHeight / (int)screenScaleFactor;

        _renderTexture = new RenderTexture(width, height, 24)
        {
            filterMode = FilterMode.Point,
            antiAliasing = 1
        };

        _renderCamera.targetTexture = _renderTexture;

        display.texture = _renderTexture;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (CheckScreenResize()) Init();
    }

    public bool CheckScreenResize()
    {
        return Screen.width != screenWidth || Screen.height != screenHeight;
    }

}
