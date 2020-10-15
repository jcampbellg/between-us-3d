using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SkinRenderer : NetworkBehaviour
{
    public Renderer bodyRenderer;
	[SyncVar]
    public Skin skin;
    Skin oldSkin;

	private void Update()
	{
		if (oldSkin != skin)
		{
			oldSkin = skin;
			if (isClientOnly)
				skin.SetSkin(bodyRenderer);
		}
	}
}
