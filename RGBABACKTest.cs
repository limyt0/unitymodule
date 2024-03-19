using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Red, Blue 투명화 테스트 투명화한 부분 색이 Unity에도 보임
public class RGBABACKTest : MonoBehaviour
{
    void Start()
    {
        // 첫 번째 이미지 생성 (빨간색 투명 배경)
        Color reds = new Color32(255,0,0,0);
        Texture2D redBackground = CreateColoredTexture(100, 100, reds);
        SaveTextureToFile(redBackground, "red_background.png");

        // 두 번째 이미지 생성 (파란색 투명 배경)
        Color blues = new Color32(0, 0, 255, 0);
        Texture2D blueBackground = CreateColoredTexture(100, 100, blues);
        SaveTextureToFile(blueBackground, "blue_background.png");
    }

    // 색상으로 채워진 텍스처 생성
    Texture2D CreateColoredTexture(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }
        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }

    // 텍스처를 파일로 저장
    void SaveTextureToFile(Texture2D texture, string filename)
    {
        byte[] bytes = texture.EncodeToPNG();
        string filePath = Path.Combine(Application.persistentDataPath, filename);
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Saved texture to: " + filePath);
    }
}
