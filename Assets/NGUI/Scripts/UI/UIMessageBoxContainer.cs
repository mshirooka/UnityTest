using UnityEngine;
using AnimationOrTween;

/// <summary>
/// Generic message box class capable of displaying variable text. Should not be used directly, but rather through some manager.
/// </summary>

[AddComponentMenu("Game/UI/Message Box Container")]
public class UIMessageBoxContainer : MonoBehaviour
{
    public UILabel title;
    public UILabel body;
    public GameObject okButton;
    public GameObject yesButton;
    public GameObject noButton;

    public delegate void Callback (bool response);

    /// <summary>
    /// Callback function that will be invoked when the message box is closed.
    /// </summary>

    public Callback callback;

    bool mIsVisible = false;

    /// <summary>
    /// Whether the message box is currently visible.
    /// </summary>

    public bool isVisible { get { return mIsVisible; } }

    /// <summary>
    /// Register the listener callbacks.
    /// </summary>

    void Awake ()
    {
        UIEventListener.Get(okButton).onClick += OnButtonYes;
        UIEventListener.Get(yesButton).onClick += OnButtonYes;
        UIEventListener.Get(noButton).onClick += OnButtonNo;
    }

    /// <summary>
    /// "Yes" or "OK" button press.
    /// </summary>

    void OnButtonYes (GameObject go)
    {
        mIsVisible = false;
        ActiveAnimation.Play(animation, null, Direction.Reverse, EnableCondition.DoNothing, DisableCondition.DisableAfterReverse);
//        if (UIDimmer.instance != null) UIDimmer.instance.SetActive(false);
        if (callback != null) callback(true);
    }

    /// <summary>
    /// "No" button press.
    /// </summary>

    void OnButtonNo (GameObject go)
    {
        mIsVisible = false;
        ActiveAnimation.Play(animation, null, Direction.Reverse, EnableCondition.DoNothing, DisableCondition.DisableAfterReverse);//
//        if (UIDimmer.instance != null) UIDimmer.instance.SetActive(false);
        if (callback != null) callback(false);
    }

    /// <summary>
    /// Show a single-button dialog box.
    /// </summary>

    public void Show (string titleText, string bodyText, string okText)
    {
        if (!mIsVisible)
        {
            NGUITools.SetActive(gameObject, true);
            NGUITools.SetActive(yesButton, false);
            NGUITools.SetActive(noButton, false);
        }

        title.text = titleText;
        body.text = bodyText;
        okButton.GetComponentInChildren<UILabel>().text = okText;
        UICamera.selectedObject = okButton;

        if (!mIsVisible)
        {
            ActiveAnimation.Play(animation, null, Direction.Forward, EnableCondition.DoNothing, DisableCondition.DisableAfterReverse);
//            if (UIDimmer.instance != null) UIDimmer.instance.SetActive(true);
            mIsVisible = true;
        }
    }

    /// <summary>
    /// Show a yes/no dialog box.
    /// </summary>

    public void Show (string titleText, string bodyText, string yesText, string noText)
    {
        mIsVisible = true;
        NGUITools.SetActive(gameObject, true);
        NGUITools.SetActive(okButton, false);

        title.text = titleText;
        body.text = bodyText;

        yesButton.GetComponentInChildren<UILabel>().text = yesText;
        noButton.GetComponentInChildren<UILabel>().text = noText;
        UICamera.selectedObject = yesButton;

        ActiveAnimation.Play(animation, null, Direction.Forward, EnableCondition.DoNothing, DisableCondition.DisableAfterReverse);

//        if (UIDimmer.instance != null) UIDimmer.instance.SetActive(true);
    }
}
