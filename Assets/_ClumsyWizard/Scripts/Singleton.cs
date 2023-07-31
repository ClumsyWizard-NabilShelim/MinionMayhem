using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClumsyWizard.Utilities
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
	{
		public static T Instance { get; private set; }

		protected virtual void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}

			SceneManagement.OnSceneLoadTriggered += CleanUpStaticData;
			Instance = this as T;
		}

		//Used to remove reference and clear out Delegate functions
		protected abstract void CleanUpStaticData();

        protected virtual void OnDestroy()
        {
            CleanUpStaticData();
            SceneManagement.OnSceneLoadTriggered -= CleanUpStaticData;
		}
    }

	public abstract class Persistant<T> : Singleton<T> where T : MonoBehaviour
	{
		protected override void Awake()
		{
			base.Awake();
			transform.SetParent(null);
			DontDestroyOnLoad(gameObject);
		}
	}
}