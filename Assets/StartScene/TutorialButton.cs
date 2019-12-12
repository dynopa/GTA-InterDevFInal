using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour {

	public Button TutButton;

	void Start () {
		Button btn3 = TutButton.GetComponent<Button>();
		btn3.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		SceneManager.LoadScene(3);
	}
}