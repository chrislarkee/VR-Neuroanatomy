using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class MC_SwitchTexture : MonoBehaviour, IPointerClickHandler
{
	public Texture[] textures;
	public Material targetMaterial;

	RawImage image;
	int index = 0;

	void Awake()
	{
		image = this.GetComponent<RawImage>();
		ReloadTexture();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			NextTexture();
		}
		else if (eventData.button == PointerEventData.InputButton.Middle)
		{
			PrevTexture();
		}
		else if (eventData.button == PointerEventData.InputButton.Right)
		{
			PrevTexture();
		}
	}

	public void NextTexture()
	{
		index++;
		if (index >= textures.Length)
		{
			index = 0;
		}
		ReloadTexture();
	}
	public void PrevTexture()
	{
		index--;
		if (index < 0)
		{
			index = textures.Length-1;
		}
		ReloadTexture();
	}
	private void ReloadTexture()
	{
		image.texture = textures[index];
		targetMaterial.SetTexture("_MatCap", image.texture);
	}
}
