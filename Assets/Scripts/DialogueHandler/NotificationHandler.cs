using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationHandler : MonoBehaviour
{
    public GameObject notificationUIGroup;
    public float moveUpToY = -197;

    public NotificationElement notificationElementForDebugging;

    private Text notificationText;
    private Image portrait;
    private float defaultHiddenPositionY;
    private RectTransform rectTransform;

    public NotificationElement NotificationElement { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        //Only for debugging
        NotificationElement = notificationElementForDebugging;

        notificationText = notificationUIGroup.GetComponentInChildren<Text>();
        portrait = notificationUIGroup.GetComponentInChildren<Image>();
        rectTransform = notificationUIGroup.GetComponent<RectTransform>();

        defaultHiddenPositionY = rectTransform.anchoredPosition.y;

        //PopUpNotification(NotificationElement);
    }

    public void PopUpNotification(NotificationElement ne)
    {
        if (ne != null)
        {
            NotificationElement = ne;
            notificationText.enabled = true;
            portrait.enabled = true;

            //portrait.sprite = NotificationElement.speakerPic;
            //notificationText.text = NotificationElement.dialogueTextEnglish
            StartCoroutine("MoveNotifiactionUp");
        }
        else
        {
            Debug.LogError("The notification element is null or empty");
        }
    }

    private IEnumerator MoveNotifiactionUp()
    {
        while(rectTransform.anchoredPosition.y < moveUpToY)
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 1);
            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine("WaitUntilPlayerRead");
    }

    private IEnumerator WaitUntilPlayerRead()
    {
        yield return new WaitForSeconds(5);

        StartCoroutine("MoveNotifiactionDown");
    }

    private IEnumerator MoveNotifiactionDown()
    {
        while (rectTransform.anchoredPosition.y > defaultHiddenPositionY)
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - 1);
            yield return new WaitForSeconds(0.01f);
        }

        notificationText.enabled = false;
        portrait.enabled = false;
    }
}
