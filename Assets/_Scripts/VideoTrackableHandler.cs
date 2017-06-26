/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;

namespace Vuforia
{
	/// <summary>
	/// A custom handler that implements the ITrackableEventHandler interface.
	/// </summary>
	public class VideoTrackableHandler : MyArDemoTrackableHandler,
	ITrackableEventHandler
	{	
//		public string videoName = "";

		override protected void OnTrackingFound()
		{
			base.OnTrackingFound ();
//			VideoPlaybackBehaviour[] currentVideos = FindObjectsOfType<VideoPlaybackBehaviour>();
//			foreach (VideoPlaybackBehaviour video in currentVideos) {
//				if(video.m_path.Contains (videoName))
//					video.VideoPlayer.Play (false, 0);
//			}
//			Transform transform = GetComponent<Transform>();
//			foreach (Transform child in transform) {
//
//			}
			VideoPlaybackBehaviour video = GetComponentInChildren<VideoPlaybackBehaviour> ();
			video.VideoPlayer.Play (false, 0);
		}

		override protected void OnTrackingLost()
		{
			base.OnTrackingLost ();
			VideoPlaybackBehaviour video = GetComponentInChildren<VideoPlaybackBehaviour> ();
			video.VideoPlayer.Pause ();
		}

	}
}
