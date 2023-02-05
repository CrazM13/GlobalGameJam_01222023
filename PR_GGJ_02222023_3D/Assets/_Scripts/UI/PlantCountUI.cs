using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlantCountUI : MonoBehaviour {

	[SerializeField] TMPro.TMP_Text consumedPlants;
	[SerializeField] TMPro.TMP_Text totalPlants;



	void Update() {
		Plant_Manager plantManager = ServiceLocator.PlantManager;
		int total = plantManager.totalPlantCount();
		int consumed = total - plantManager.remainingPlantCount();

		consumedPlants.text = consumed.ToString("D3");
		totalPlants.text = total.ToString("D3");
	}
}
