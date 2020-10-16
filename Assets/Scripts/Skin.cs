using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[CreateAssetMenu(fileName = "New Skin", menuName = "Skin")]
public class Skin : ScriptableObject
{
    public string skinName;
    public Texture texture;
    public Texture logo;
    public void SetSkin(Renderer bodyRenderer)
    {
        bodyRenderer.material.SetTexture("_BaseMap", texture);
    }
}

public static class SkinSerializer
{
    public static void WriteSkin(this NetworkWriter writer, Skin skin)
    {
        writer.WriteString("Skins/" + skin.name);
    }

    public static Skin ReadSkin(this NetworkReader reader)
    {
        string skinName = reader.ReadString();
        return Resources.Load<Skin>(skinName);
    }
}