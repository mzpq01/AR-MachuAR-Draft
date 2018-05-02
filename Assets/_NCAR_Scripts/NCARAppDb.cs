using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace noclew{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class NCARappDB
	{

		public static GameObject[] sceneModels;
		public static string[] sceneModelNames;

		//not used in the current version
		public static GameObject[] targets;
		//targets as game objects
		public static string[] targetNames;
		//Name of the target objects
		public static MethodInfo[] conditions;
		//Methodsinfo of condition list specified in ARTargetCondition

		public static string[] conditionNames;
		//List of condition function names;
		public static Func<NCARTrackableEventHandler, NCARTrackableEventHandler, float, float, bool>[] conditionDeligates;
		//TargetCondition Deletages

		static NCARappDB ()
		{
			Debug.Log ("-->DB inited");
			Rebuild ();
			//TrackSceneChange ();
		}

		/// <summary>
		/// rebuild ar database
		/// </summary>
		public static void Rebuild ()
		{
            //if in playmode, do not update.
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
				return;
#endif
            Debug.Log("-->DB Refreshed");
			//get all scene models and their names
			sceneModels = GameObject.FindGameObjectsWithTag ("NCAR_model");
			sceneModelNames = NCARappDB.sceneModels.Select (p => p.name).ToArray ();



			//get all the target object
			targets = NcHelpers.FindAllARTargets ();

			//get all the target names
			targetNames = NCARappDB.targets.Select (p => p.gameObject.name).ToArray ();

			//get all the conditions as methodInfo
			conditions = typeof(ARTargetCondition).GetMethods (BindingFlags.Public | BindingFlags.Static);

			//get all the condition function names
			conditionNames = NCARappDB.conditions.Select (p => p.Name).ToArray (); 

			//get all the conditiondeligates
			conditionDeligates = NCARappDB.conditions.Select (p => (Func<NCARTrackableEventHandler, NCARTrackableEventHandler, float, float, bool>) System.Delegate.CreateDelegate (typeof(Func<NCARTrackableEventHandler, NCARTrackableEventHandler, float, float, bool>), null, p)).ToArray (); 
		}


		static void RebuidModelDB ()
		{
			Debug.Log ("-->S changed");
		}

		public static GameObject GetTargetByName( string name){
			int idx = System.Array.IndexOf (NCARappDB.targetNames, name);
			return NCARappDB.targets [idx];
		}

		public static GameObject GetModelByName( string name){
			int idx = System.Array.IndexOf (NCARappDB.sceneModelNames, name);
			return NCARappDB.sceneModels [idx];
		}
	}
}