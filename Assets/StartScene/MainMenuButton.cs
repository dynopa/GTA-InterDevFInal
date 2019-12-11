using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {
	public Button StartButton2;

	void Start () {
		Button btn2 = StartButton2.GetComponent<Button>();
		btn2.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		SceneManager.LoadScene(1);
	}
}