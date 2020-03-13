using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Cettic.Utilities
{
    public static class FileManagerUtility
    {
        /// <summary>
        /// This coroutine loads the question image from streaming assets.
        /// </summary>
        /// <param name="path">Example: Application.streamingAssetsPath + "/FolderName/FileName.png",</param>
        /// <returns></returns>
        public static Sprite __LoadImageFromStreamingAsset(string path)
        {
            byte[] imgData = default;

            if (!File.Exists(path)) { Debug.LogError("Null Error: Not found this file " + path); return null; }

            imgData = File.ReadAllBytes(path);
            return __ConvertBytesImageToSprite(imgData);
        }

        public static Sprite __ConvertBytesImageToSprite(byte[] imgData)
        {
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgData); // transforma data en textura
            Vector2 pivot = Vector2.one / 2;
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f); // crear el sprite
            if (tex.width < 50) Debug.Log($"null sprite ");
            return sprite;
        }
    }
}
