using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class NotificationHandler : MonoBehaviour
{
    public GameObject notificationUIGroup;
    public float moveUpToY = -197;

    public NotificationElement defaultNotificationElement;

    private Text notificationText;
    private Image portrait;
    private float defaultHiddenPositionY;
    private RectTransform rectTransform;
    private VariableEventSystem variableEventSystem;
    private string language;

    public NotificationElement NotificationElement { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        NotificationElement = defaultNotificationElement;

        variableEventSystem = GameObject.FindGameObjectWithTag("VariableEventSystem").GetComponent<VariableEventSystem>();

        notificationText = notificationUIGroup.GetComponentInChildren<Text>();
        portrait = notificationUIGroup.GetComponentInChildren<Image>();
        rectTransform = notificationUIGroup.GetComponent<RectTransform>();

        defaultHiddenPositionY = rectTransform.anchoredPosition.y;

        //Only for Debugging:
        //PopUpNotification(NotificationElement);
    }

    private void SetLanguage()
    {
        if (variableEventSystem.FindEventInStrings("Language") != new Event<string>())
        {
            language = variableEventSystem.FindEventInStrings("Language").EventVariable;
        }
        else
        {
            Debug.LogError("Variable Event System => Language, not found. Reverting to default language (english)");
            language = "english";
        }
    }

    public void PopUpNotification(NotificationElement ne)
    {
        SetLanguage();

        if (ne != null)
        {
            NotificationElement = ne;
            notificationText.enabled = true;
            portrait.enabled = true;

            portrait.sprite = NotificationElement.speakerPic;

            if (language == "german")
            {
                notificationText.text = NotificationElement.dialogueTextGerman;
            }
            else
            {
                notificationText.text = NotificationElement.dialogueTextEnglish;
            }

            StartCoroutine("MoveNotifiactionUp");
        }
        else
        {
            Debug.LogError("The notification element is null or empty");
            PopUpNotification(defaultNotificationElement);
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
