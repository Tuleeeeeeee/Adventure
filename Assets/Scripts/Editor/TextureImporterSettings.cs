
using UnityEditor;
using UnityEngine;

public class TextureImporterSettings : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;

        textureImporter.textureType = TextureImporterType.Sprite;

        textureImporter.spritePixelsPerUnit = 16f;

        textureImporter.spritePivot = new Vector2(0.5f, 0);

        textureImporter.isReadable = false;

        textureImporter.mipmapEnabled = false;


        textureImporter.compressionQuality = 0;

    }
}
