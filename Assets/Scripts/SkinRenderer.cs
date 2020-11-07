using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SkinRenderer : NetworkBehaviour
{
    public Renderer bodyRenderer;
	public Material ghostMaterial;
	[SyncVar]
    public Skin skin;
    Skin oldSkin;
	Material bodyMaterial;

	private void Start()
	{
		bodyMaterial = bodyRenderer.material;
	}

	private void Update()
	{
		if (oldSkin != skin)
		{
			oldSkin = skin;
			if (skin && isClientOnly)
				skin.SetSkin(bodyRenderer);
		}
	}

	public void GhostUp()
	{
		bodyRenderer.material = ghostMaterial;
		skin.SetSkin(bodyRenderer);
	}

	public void Reset()
	{
		bodyRenderer.material = bodyMaterial;
		skin.SetSkin(bodyRenderer);
	}
}