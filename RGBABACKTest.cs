using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Red, Blue ����ȭ �׽�Ʈ ����ȭ�� �κ� ���� Unity���� ����
public class RGBABACKTest : MonoBehaviour
{
    void Start()
    {
        // ù ��° �̹��� ���� (������ ���� ���)
        Color reds = new Color32(255,0,0,0);
        Texture2D redBackground = CreateColoredTexture(100, 100, reds);
        SaveTextureToFile(redBackground, "red_background.png");

        // �� ��° �̹��� ���� (�Ķ��� ���� ���)
        Color blues = new Color32(0, 0, 255, 0);
        Texture2D blueBackground = CreateColoredTexture(100, 100, blues);
        SaveTextureToFile(blueBackground, "blue_background.png");
    }

    // �������� ä���� �ؽ�ó ����
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

    // �ؽ�ó�� ���Ϸ� ����
    void SaveTextureToFile(Texture2D texture, string filename)
    {
        byte[] bytes = texture.EncodeToPNG();
        string filePath = Path.Combine(Application.persistentDataPath, filename);
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Saved texture to: " + filePath);
    }
}
