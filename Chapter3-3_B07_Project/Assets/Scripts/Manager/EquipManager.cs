using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public Equip curEquip;
	public Transform equipParant;

	private void Start()
	{
		GameManager.Instance.equipManager = this;
	}

	public void EquipNew(ItemData item)
	{
		UnEquip();
		curEquip = Instantiate(item.equipPrefab, equipParant).GetComponent<Equip>();
	}

	public void UnEquip()
	{
		if(curEquip != null)
		{
			Destroy(curEquip.gameObject);
			curEquip = null;	
		}
	}
}
