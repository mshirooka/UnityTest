using UnityEngine;

[AddComponentMenu("Game/UI/Message Box")]
public class UIMessageBox : MonoBehaviour
{
    static UIMessageBox mInst;

    public GameObject prefab;

    GameObject mChild;
    UIMessageBoxContainer mBox;

    /// <summary>
    /// Convenience function that instantiates a new message box if one was not found.
    /// </summary>

    UIMessageBoxContainer messageBox
    {
        get
        {
            if (mBox == null && prefab != null)
            {
                mChild = NGUITools.AddChild(gameObject, prefab);
                mChild.transform.localPosition = new Vector3(0f, 1400f, 0f);

                mBox = mChild.GetComponent<UIMessageBoxContainer>();

                if (mBox == null)
                {
                    Debug.LogError("No message box found", this);
                    Destroy(mChild);
                    prefab = null;
                }
            }
            return mBox;
        }
    }

    /// <summary>
    /// Set the instance.
    /// </summary>

    void Awake () { mInst = this; }

    /// <summary>
    /// Show a single-button message box.
    /// </summary>

    static public void Show (string title, string body, string ok, UIMessageBoxContainer.Callback callback)
    {
        if (mInst != null)
        {
            UIMessageBoxContainer mb = mInst.messageBox;

            if (mb != null)
            {
                mb.callback = callback;
                mb.Show(title, body, ok);
            }
        }
    }

    /// <summary>
    /// Show a yes/no message box.
    /// </summary>

    static public void Show (string title, string body, string yes, string no, UIMessageBoxContainer.Callback callback)
    {
        if (mInst != null)
        {
            UIMessageBoxContainer mb = mInst.messageBox;

            if (mb != null)
            {
                mb.callback = callback;
                mb.Show(title, body, yes, no);
            }
        }
    }
}
