using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot
{
	public CraftData item;
	public int idx;
}
public class Craft : MonoBehaviour
{
	public CraftSlotUI[] uiSlot;
	private CraftSlot[] slots;
	public GameObject CraftWindow;

	[Header("Selected CraftItem")]
	private CraftSlot selectedCraftItem;
	private int selectedCraftItemIndex;
	public Image matarial;
	public TextMeshProUGUI CraftItemName;
	public TextMeshProUGUI CraftItemDescription;
	public TextMeshProUGUI matarialQuantity;
	public GameObject craftButton;

	private void Start()
	{
		GameManager.Instance.craft = this;
		
		slots = new CraftSlot[uiSlot.Length];

		for(int i = 0; i < slots.Length; i++)
		{
			slots[i] = new CraftSlot();
			uiSlot[i].index = i;
			uiSlot[i].Clear();
		}
		ClearCraftUI();
		CraftItemList();
	}
	
	public void AddItem(CraftData recipe)
	{
		CraftSlot emptySlot = GetEmptySlot();
		if(emptySlot != null)
		{
			emptySlot.item = recipe;
			emptySlot.idx = 1;
			UpdateCraftUI();
			return;
		}
	}

	public void SelectCraftItem(int index)
	{
		if (slots[index] == null)
		{
			return;
		}
		selectedCraftItem = slots[index];
		selectedCraftItemIndex = index;

		matarial.gameObject.SetActive(true);
		matarial.sprite = selectedCraftItem.item.resources[0].icon;
		matarialQuantity.text = selectedCraftItem.item.resourceCount[0].ToString();
		CraftItemName.text = selectedCraftItem.item.Result.ItemName;
		CraftItemDescription.text = selectedCraftItem.item.Result.ItemDescription;

		craftButton.SetActive(true);
	}

	private CraftSlot GetEmptySlot()
	{
		Debug.Log("4");
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == null)
			{
				return slots[i];
			}
		}
		return null;
	}

	void UpdateCraftUI()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item != null)
			{
				uiSlot[i].Set(slots[i]);
			}
			else
			{
				uiSlot[i].Clear();
			}
		}
	}

	private void ClearCraftUI()
	{
		CraftItemName.text = string.Empty;
		CraftItemDescription.text = string.Empty;
		matarial.gameObject.SetActive(false);
		matarialQuantity.text = string.Empty;

		craftButton.SetActive (false);
	}

	private void CraftItemList()
	{
		AddItem(GameManager.Instance.craftManager.swordCraft);
	}
}
