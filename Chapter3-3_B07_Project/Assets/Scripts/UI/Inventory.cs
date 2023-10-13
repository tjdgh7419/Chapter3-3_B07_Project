using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSlot
{
	public ItemData item;
	public int quantity;
}
public class Inventory : MonoBehaviour
{
	public ItemSlotUI[] uiSlot;
	public ItemSlot[] slots;
	public GameObject inventoryWindow;

	[Header("Selected Item")]
	private ItemSlot selectedItem;
	private int selectedItemIndex;
	public TextMeshProUGUI selectedItemName;
	public TextMeshProUGUI selectedItemDescription;
	public TextMeshProUGUI selectedItemTypeTitle;
	public TextMeshProUGUI selectedItemTypeDescription;
	public TextMeshProUGUI selectedItemStatTitle;
	public TextMeshProUGUI selectedItemStatDescription;
	public GameObject equipButton;
	public GameObject useButton;
	public GameObject dropButton;
	public GameObject unEquipButton;

	private int curEquipIndex;
	private Dictionary<ItemData, int> ItemTotalCount;

	private void Awake()
	{
		ItemTotalCount = new Dictionary<ItemData, int>();
	}

	private void Start()
	{
		GameManager.Instance.inventory = this;
		inventoryWindow.SetActive(false);
		slots = new ItemSlot[uiSlot.Length];

		for (int i = 0; i < slots.Length; i++)
		{
			slots[i] = new ItemSlot();
			uiSlot[i].index = i;
			uiSlot[i].Clear();
		}
		ClearSelectedItemWindow();
	}

	public void Toggle()
	{
		if (inventoryWindow.activeInHierarchy)
		{
			inventoryWindow.SetActive(false);
		}
		else
		{
			inventoryWindow.SetActive(true);
		}
	}

	public void OnInventoryButton(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			Toggle();
		}
	}

	public void AddItem(ItemData item)
	{
		if (item.canStack)
		{
			ItemSlot slotInfo = GetItemStack(item);
			if (slotInfo != null)
			{
				slotInfo.quantity++;
				UpdateUI();
				return;
			}
		}

		ItemSlot emptySlotInfo = GetEmptySlot();

		if (emptySlotInfo != null)
		{
			emptySlotInfo.item = item;
			emptySlotInfo.quantity = 1;
			UpdateUI();
			return;
		}
		return;
	}

	private void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++)
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

	private ItemSlot GetItemStack(ItemData item)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
			{
				return slots[i];
			}
		}
		return null;
	}

	private ItemSlot GetEmptySlot()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == null)
			{
				return slots[i];
			}
		}
		return null;
	}

	public void SelectItem(int index)
	{
		if (slots[index].item == null)
		{
			return;
		}

		selectedItem = slots[index];
		selectedItemIndex = index;

		switch (selectedItem.item.type)
		{
			case ItemType.Consumable:
				{
					selectedItemTypeDescription.text = "소모품";
					break;
				}
			case ItemType.Equipable:
				{
					selectedItemTypeDescription.text = "장비";
					break;
				}
		}
		selectedItemName.text = selectedItem.item.ItemName;
		selectedItemDescription.text = selectedItem.item.ItemDescription;
		selectedItemStatDescription.text = selectedItem.item.AttackPower.ToString();
		selectedItemStatTitle.gameObject.SetActive(true);
		selectedItemTypeTitle.gameObject.SetActive(true);
		useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
		equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uiSlot[index].equipped);
		equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uiSlot[index].equipped);
		dropButton.SetActive(selectedItem.item.type != ItemType.Equipable);
	}

	public void OnUseButton()
	{
		if (selectedItem.item.type == ItemType.Consumable)
		{
			for (int i = 0; i < selectedItem.item.consumableDatas.Length; i++)
			{
				switch (selectedItem.item.consumableDatas[i].type)
				{
					case ConsumableType.HP:
						break; // 플레이어 컨디션 매니저가 생겼을 때
					case ConsumableType.MP:
						break;
				}
			}
		}
		RemoveSelectedItem();
	}

	public void OnDropButton()
	{
		RemoveSelectedItem();
	}

	public void OnEquipButton()
	{
		if (uiSlot[curEquipIndex].equipped)
		{
			UnEquip(curEquipIndex);
		}

		uiSlot[selectedItemIndex].equipped = true;

		GameManager.Instance.equipManager.EquipNew(selectedItem.item);
		UpdateUI();
		SelectItem(selectedItemIndex);
	}

	public void UnEquipButton()
	{
		UnEquip(selectedItemIndex);
	}

	private void RemoveSelectedItem()
	{
		selectedItem.quantity--;

		if (selectedItem.quantity <= 0)
		{
			if (uiSlot[selectedItemIndex].equipped)
			{
				UnEquip(selectedItemIndex);
			}
			selectedItem.item = null;
			ClearSelectedItemWindow();
		}
		UpdateUI();
	}

	private void UnEquip(int index)
	{
		uiSlot[index].equipped = false;
		GameManager.Instance.equipManager.UnEquip();
		UpdateUI();

		if (selectedItemIndex == index)
		{
			SelectItem(index);
		}
	}

	private void ClearSelectedItemWindow()
	{
		selectedItem = null;
		selectedItemName.text = string.Empty;
		selectedItemDescription.text = string.Empty;
		selectedItemStatDescription.text = string.Empty;
		selectedItemStatTitle.text = string.Empty;
		selectedItemTypeDescription.text = string.Empty;
		selectedItemTypeTitle.text = string.Empty;

		equipButton.SetActive(false);
		dropButton.SetActive(false);
		unEquipButton.SetActive(false);
		useButton.SetActive(false);
	}
}
