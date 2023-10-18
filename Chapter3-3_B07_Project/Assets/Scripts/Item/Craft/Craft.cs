using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Craft : MonoBehaviour
{
	public CraftSlotUI[] uiSlot;
	private CraftData[] slots;
	public GameObject CraftWindow;

	[Header("Selected CraftItem")]
	private CraftData selectedCraftItem;
	private int selectedCraftItemIndex;
	public Image matarial;
	public TextMeshProUGUI CraftItemName;
	public TextMeshProUGUI CraftItemDescription;
	public TextMeshProUGUI matarialQuantity;
	public GameObject craftButton;

	private void Start()
	{
		GameManager.Instance.craft = this;
		CraftWindow.SetActive(false);
		slots = new CraftData[uiSlot.Length];

		for(int i = 0; i < slots.Length; i++)
		{
			slots[i] = new CraftData();
			uiSlot[i].index = i;
		}
		ClearCraftUI();
		CraftItemList();
	}
	
	public void AddItem(CraftData recipe)
	{
		Debug.Log("1");
		CraftData emptySlot = GetEmptySlot();
		if(emptySlot != null)
		{
			Debug.Log("add");
			emptySlot = recipe;
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

		CraftItemName.text = selectedCraftItem.Result.ItemName;
		CraftItemDescription.text = selectedCraftItem.Result.ItemDescription;

		craftButton.SetActive(true);
	}

	CraftData GetEmptySlot()
	{
		Debug.Log("4");
		for (int i = 0; i < slots.Length; i++)
		{
			Debug.Log("3");
			if (slots[i] == null)
			{
				Debug.Log("2");
				return slots[i];
			}
		}
		Debug.Log("5");
		return null;
	}

	void UpdateCraftUI()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			if (slots[i] != null)
			{
				uiSlot[i].Set(slots[i]);
			}
			else
			{
				return;
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
