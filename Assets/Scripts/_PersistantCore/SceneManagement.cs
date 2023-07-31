using System.Collections;
using UnityEngine;
using ClumsyWizard.Utilities;
using UnityEngine.SceneManagement;
using System;

public class SceneManagement : Persistant<SceneManagement>
{
	private Animator animator;

    /// <summary>
    ///This wont be cleared when a new scene is loaded
    /// </summary>
    public static Action OnSceneLoadTriggeredCore;  
	public static Action OnSceneLoadTriggered;

    /// <summary>
    ///This wont be cleared when a new scene is loaded
    /// </summary>
	public static Action OnSceneLoadedCore; 
	public static Action OnSceneLoaded;

	public bool IsLastBattle => SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex).name.Contains("Level_1");

	protected override void Awake()
	{
		base.Awake();
		animator = GetComponent<Animator>();
	}

	public void Reload()
	{
		StartCoroutine(LoadScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex).name));
	}

	/// <param name="sceneName">If sceneName is empty, the next scene will be loaded from the scene build index</param>
	public void Load(string sceneName)
	{
		StartCoroutine(LoadScene(sceneName));
	}

	private IEnumerator LoadScene(string sceneName)
	{
		OnSceneLoadTriggeredCore?.Invoke();
		OnSceneLoadTriggered?.Invoke();
		OnSceneLoadTriggered = null;

		string sceneToLoad = string.Empty;

		if (sceneName == "")
		{
			int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
			sceneToLoad = SceneManager.GetSceneByBuildIndex(buildIndex).name;
			Debug.Log(sceneToLoad);
		}
		else
		{
			sceneToLoad = sceneName;
		}

		animator.SetTrigger("Transition");
		yield return new WaitForSeconds(1.0f);

		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

		operation.completed += (AsyncOperation op) =>
		{
			animator.SetBool("Transition", false);
			OnSceneLoadedCore?.Invoke();
			OnSceneLoaded?.Invoke();
			OnSceneLoaded = null;
		};
	}

    protected override void CleanUpStaticData(){}
}