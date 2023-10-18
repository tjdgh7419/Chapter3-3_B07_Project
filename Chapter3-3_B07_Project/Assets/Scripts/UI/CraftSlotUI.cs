using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlotUI : MonoBehaviour
{
	public Button button;
	public Image icon;
	private CraftData curSlot;

	public int index;

	public void Set(CraftData slot)
	{
		curSlot = slot;
		icon.gameObject.SetActive(true);
		icon.sprite = slot.Image;
	}

	public void OnCraftItemClick()
	{

	}
}
