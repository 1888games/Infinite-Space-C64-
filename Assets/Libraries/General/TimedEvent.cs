using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 

public static class TimedEvent {


	public static Dictionary<string, long> longestTime = new Dictionary<string, long> ();
	public static Dictionary<string, long> startTimes = new Dictionary<string, long> ();
	public static Dictionary<string, long> endTimes = new Dictionary<string, long> ();
	public static Dictionary<string, bool> running = new Dictionary<string, bool> ();



	public static void Click (string name) {

		//Debug.Log (name);

		long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

		if (running.ContainsKey (name) == false) {

			longestTime.Add (name, 0);
			startTimes.Add (name, milliseconds);
			running.Add (name, true);

			return;

		}

	
		bool isRunning = running[name];
		long record = longestTime [name];

	
		if (isRunning) {

			running [name] = false;

			long elapsed = milliseconds - startTimes [name];

			//if (elapsed > record) {

				longestTime [name] = elapsed;
				Debug.Log ("Time taken: " + name + " - " + elapsed + "ms");
			//}


		}

		else {

			startTimes [name] = milliseconds;
			running [name] = true;

		}


	}


}
