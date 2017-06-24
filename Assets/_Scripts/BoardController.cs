/*============================================================================== 
 * Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

/// <summary>
/// This class manages the content displayed on top of cloud reco targets in this sample
/// </summary>
public class BoardController : MonoBehaviour
{
	#region PUBLIC_VARIABLES
	public RawImage LoadingSpinnerBackground;
	public RawImage LoadingSpinnerImage;
	public Button CancelButton;
	public GameObject BoardUI;

	/// <summary>
	/// The root gameobject that serves as an augmentation for the image targets created by search results
	/// </summary>
	public GameObject AugmentationObject;

	/// <summary>
	/// the URL the JSON data should be fetched from
	/// </summary>
	public string JsonServerUrl;
	#endregion //PUBLIC_VARIABLES


	#region PRIVATE_MEMBERS
	private bool mIsShowingBookData = false;
	private bool mIsLoadingBookData = false;
	private bool mIsLoadingBookThumb = false;
	private WWW mJsonBookInfo;
	private WWW mBookThumb;
	private bool mIsBookThumbRequested = false;
	private bool mIsShowingMenu = false;
	private bool mIsShowingBoard = false;
	#endregion //PRIVATE_MEMBERS


	#region MONOBEHAVIOUR_METHODS
	void Start ()
	{

		//HideObject();

		SetBoardUIVisible(false);
	}

	void Update () 
	{
		if (!mIsShowingBoard) 
		{
			if (Input.GetMouseButtonUp (0)) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit, 1000.0f)) {
					GameObject hitObject = hit.collider.gameObject;
					if (hitObject != null && hitObject.name == "BoardAugmentationObject") {

						CubeBehavior cube = hitObject.GetComponent<CubeBehavior>();
						if (cube) {
							cube.OnUserClicked();
						}
					}
				}
			}
		}


		/*
		if ( mIsShowingBookData )
		{
			if (Input.GetMouseButtonUp(0))
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast (ray, out hit, 1000.0f)) {
					GameObject hitObject = hit.collider.gameObject;
					if (hitObject != null && hitObject.name == "BookInformation" )                        
					{
						if (mBookData != null && mIsShowingMenu == false)
						{
							Application.OpenURL(mBookData.BookDetailUrl);
						}
					}
				}
			}
		}

		if (mIsLoadingBookThumb)
		{
			LoadBookThumb();
		}
		*/
		// Show/hide loading progress spinner if we are loading book data or thumb
		//SetLoadingSpinnerVisibile (mIsLoadingBookData || mIsLoadingBookThumb);

		// Show cancel button if the Cloud Reco is not enabled, otherwise hide it
		//SetCancelButtonVisible(mCloudRecoBehaviour.CloudRecoInitialized && !mCloudRecoBehaviour.CloudRecoEnabled);

		//SetLoadingSpinnerVisibile (true);
		//SetBoardUIVisible (true);
	}
	#endregion //MONOBEHAVIOUR_METHODS


	#region PUBLIC_METHODS
	public void OnCancel()
	{
		//mCloudRecoBehaviour.CloudRecoEnabled = true;
		//TargetDeleted();

		SetBoardUIVisible(false);
	}

	//load some information from board target
	//and then display it
	public void ShowBoardTarget(){
		
		this.SetBoardUIVisible (true);
	}

	/// <summary>
	/// Method called from the CloudRecoEventHandler
	/// when a new target is created
	/// </summary>
	public void TargetCreated(string targetMetadata)
	{
		// Initialize the showing book data variable
		mIsShowingBookData = true;
		mIsLoadingBookData = true;

		mIsBookThumbRequested = false;

		// Loads the JSON Book Data
		StartCoroutine( LoadJSONBookData(targetMetadata) );
	}

	/// <summary>
	/// Method called when the Close button is pressed
	/// to clean the target Data
	/// </summary>
	public void TargetDeleted()
	{
		// Initialize the showing book data variable
		mIsShowingBookData = false;
		mIsLoadingBookData = false;
		mIsLoadingBookThumb = false;
	}
		

	/// <summary>
	/// Hides the augmentation object
	/// </summary>
	public void HideObject()
	{
		Renderer[] rendererComponents = AugmentationObject.GetComponentsInChildren<Renderer>();
		Collider[] colliderComponents = AugmentationObject.GetComponentsInChildren<Collider>();

		// Enable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = false;
		}

		// Enable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = false;
		}
	}

	/// <summary>
	/// Method to let the ContentManager know if the CloudReco
	/// SampleMenu is being displayed
	/// </summary>
	public void SetIsShowingMenu(bool isShowing)
	{
		mIsShowingMenu = isShowing;
	}
	#endregion //PUBLIC_METHODS


	#region PRIVATE_METHODS
	private void SetLoadingSpinnerVisibile(bool visible)
	{
		if (LoadingSpinnerBackground == null ||
			LoadingSpinnerImage == null) 
			return;

		if (LoadingSpinnerBackground.enabled != visible)
			LoadingSpinnerBackground.enabled = visible;

		if (LoadingSpinnerImage.enabled != visible)
			LoadingSpinnerImage.enabled = visible;

		if (visible)
		{
			LoadingSpinnerImage.rectTransform.Rotate(Vector3.forward, 90.0f * Time.deltaTime);
		}
	}

	private void SetBoardUIVisible(bool visible)
	{
		if (this.BoardUI == null) return;

		this.BoardUI.SetActive (visible);
		mIsShowingBoard = visible;
	}
		
	/// <summary>
	/// Fetches the JSON data from a server
	/// </summary>
	private IEnumerator LoadJSONBookData(string jsonBookUrl)
	{
		// Gets the full book json url
		string fullBookURL = JsonServerUrl + jsonBookUrl;

		// Gets the json book info from the url
		mJsonBookInfo = new WWW(fullBookURL);
		yield return mJsonBookInfo;

		// Loading done
		mIsLoadingBookData = false;

		if (mJsonBookInfo.error == null)
		{
			// Parses the json Object
			//JSONParser parser = new JSONParser();

			//BookData bookData = parser.ParseString(mJsonBookInfo.text);
			//mBookData = bookData;

			// Updates the BookData info in the augmented object
			//mBookInformationParser.UpdateBookData(bookData);

			mIsLoadingBookThumb = true;
		} 
		else
		{
			Debug.LogError("Error downloading json");
		}
	}

	/// <summary>
	/// Loads the texture for the book thumbnail
	/// </summary>
	private void LoadBookThumb()
	{
		if (!mIsBookThumbRequested )            
		{
			//if (mBookData != null )
			{
			//	mBookThumb = new WWW(mBookData.BookThumbUrl);
			//	mIsBookThumbRequested = true;
			}
		}

		if (mBookThumb.progress >=1)
		{
			//if(mBookThumb.error == null && mBookData != null)
			{
			//	mBookInformationParser.UpdateBookThumb(mBookThumb.texture);
			//	mIsLoadingBookThumb = false;
			//	ShowObject();
			}
		}
	}

	/// <summary>
	/// Shows the augmentation object
	/// </summary>
	private void ShowObject()
	{
		Renderer[] rendererComponents = AugmentationObject.GetComponentsInChildren<Renderer>();
		Collider[] colliderComponents = AugmentationObject.GetComponentsInChildren<Collider>();

		// Enable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = true;
		}

		// Enable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = true;
		}
	}
	#endregion //PRIVATE_METHODS
}
