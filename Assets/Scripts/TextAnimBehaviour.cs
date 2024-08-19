using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimBehaviour : MonoBehaviour
{
	public List<TextElements> text_anim_elements;

	[System.Serializable]
	public class TextElements
	{
		public string name;
		public Transform transform;
		public GameObject prefab_to_instantiate;
	}

	public void ScoreAddText(float score_to_add)
	{
		//change text
		text_anim_elements[0].prefab_to_instantiate.GetComponent<TMP_Text>().text = "+" + score_to_add.ToString();

		//instantiate object
		GameObject exhale_text = Instantiate(text_anim_elements[0].prefab_to_instantiate, text_anim_elements[0].transform);
		//destroy after 1.5 sec
		Destroy(exhale_text, 1.5f);
		Destroy(gameObject, 2.0f);
	}
}
