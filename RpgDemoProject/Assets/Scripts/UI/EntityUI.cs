using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RpgDemo
{
	public class EntityUI : MonoBehaviour
	{
		public Image hpFillImage;
		public Text hpText;
		public Text nameText;
		public void SetName(string name)
		{
			nameText.text = name;
		}
		public void SetHp(string hp)
		{
			nameText.text = hp;
		}
		public void SetHpFill(float fillAmount)
		{
			hpFillImage.fillAmount = fillAmount;
		}
	}
}