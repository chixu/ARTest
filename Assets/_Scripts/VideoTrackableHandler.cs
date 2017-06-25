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
		
		public VideoPlaybackBehaviour currentVideo;
		private void OnTrackingFound()
		{
			base.OnTrackingFound ();
			if(currentVideo)
				currentVideo.VideoPlayer.Play(false, currentVideo.VideoPlayer.GetCurrentPosition());

		}
	}
}
