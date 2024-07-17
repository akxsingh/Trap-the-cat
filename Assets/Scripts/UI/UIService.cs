using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TrapTheCat.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIService : MonoBehaviour
{
    [SerializeField] TMP_Text textGameText; // Text field to display game-related text
    [SerializeField] Button restartButton; // Button to restart the game
    [SerializeField] GameObject gameOverPanel; // Panel to display when game is over
    [SerializeField] Button restartPanelButton; // Button on the game over panel to restart

    private EventService eventService; // Reference to EventService for event handling


    private void Start()
    {
        restartButton.onClick.AddListener(OnRestartClick);  // Registering OnRestartClick method to restartButton click event

        restartPanelButton.onClick.AddListener(OnRestartClick); // Registering OnRestartClick method to restartPanelButton click event

    }

    // Initializes UIService with EventService reference
    public void Init(EventService eventService)
    {
        this.eventService = eventService;   // Assigning provided EventService to local refe
        eventService.OnGameOver.AddListener(OnGameOver);    // Subscribing to OnGameOver event in EventService
    }

    // Method called when either restartButton or restartPanelButton is clicked
    private void OnRestartClick()
    {
        gameOverPanel.SetActive(false); // Hiding gameOverPanel
        SceneManager.LoadScene(0); // Reloading the scene(assuming scene index 0 is the main game scene)
    }

    // Sets the text to be displayed in textGameText
    public void SetGameText(string textToSet)
    {
        textGameText.text = textToSet;  // Setting the text of textGameText
    }

    // Method called when the game over event is triggered
    public void OnGameOver(bool isGameOver)
    {
        gameOverPanel.SetActive(isGameOver); // Showing or hiding gameOverPanel based on isGameOver
    }
}
