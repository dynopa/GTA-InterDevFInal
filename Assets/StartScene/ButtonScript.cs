using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {
	public Button StartButton;

	void Start () {
		Button btn = StartButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		SceneManager.LoadScene(0);
	}
}