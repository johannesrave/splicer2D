
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    private PlayerController player;
    private GameObject damageScreen;
    private TextMeshProUGUI healthText;

    protected virtual void Awake()
    {
        InitializeFields();
    }
    protected virtual void OnEnable()
    {
        RegisterEventHandlers();
    }

    protected virtual void OnDisable()
    {
        DeregisterEventHandlers();
    }

    private void Update()
    {
        healthText.text = $"{player.health} HP";
    }


    private void InitializeFields()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        damageScreen = transform.Find("DamageScreen").gameObject;
        healthText = transform.Find("Buttons").Find("Button_Test").Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    private void OnDamageTaken(GameObject o, GameObject other)
    {
        StartCoroutine(FlashScreen(damageScreen));
        
    }

    private IEnumerator FlashScreen(GameObject o)
    {
        var startTime = Time.time;
        var length = 0.4f;
        var canvasGroup = damageScreen.GetComponent<CanvasGroup>();
        while (Time.time - startTime < length)
        {
            canvasGroup.alpha = Mathf.PingPong(Time.time, length);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    private void RegisterEventHandlers()
    {
        player.DamageTaken += OnDamageTaken;
    }

    private void DeregisterEventHandlers()
    {
        player.DamageTaken -= OnDamageTaken;
        
    }
}
