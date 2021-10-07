using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class UIFadeTest : MonoBehaviour
{

    public float fadeSpeed = 1.0f;

    public CanvasGroup canvasGroup;

    private float alpha = 1.0f;

    private static UIFadeTest instance;

    public static UIFadeTest Instance
    {

        get
        {

            return instance;

        }

    }

    // Use this for initialization

    void Start()
    {

        instance = this;

        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();

    }

    // Update is called once per frame

    void Update()
    {

        if (alpha != canvasGroup.alpha)
        {

            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, alpha, fadeSpeed * Time.deltaTime);

            if (Mathf.Abs(canvasGroup.alpha - alpha) < 0.05f)
            {

                canvasGroup.alpha = alpha;

            }

        }

    }

    public void UIShow()
    {

        alpha = 1.0f;

        canvasGroup.blocksRaycasts = true;

    }

    public void UIHide()
    {

        alpha = 0.0f;

        canvasGroup.blocksRaycasts = false;

    }

}