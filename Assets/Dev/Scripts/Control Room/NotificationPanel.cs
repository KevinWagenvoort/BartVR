﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour {
    //image needs to be public to be able to clone
    public Sprite image;
    public GameObject panelImage;
    public GameObject notificationMenu;
    public GameObject Timestamp;

    private Notification notification;
    private Text username;
    private Text message;
    private Image mediaPlaform;
    private GameObject imageButton;
    private Image favoriteButton;
    private Text Date;

    public void Setup(Notification notification)
    {
        SetGameObjects();
        SetComponents();
        SetupPanelInformation(notification);
    }

    private void SetGameObjects() {
        imageButton = this.transform.Find("Show Button").gameObject;
    }

    private void SetComponents() {
        username = this.transform.Find("UserName").GetComponent<Text>();
        message = this.transform.Find("Message").GetComponent<Text>();
        mediaPlaform = this.transform.Find("MediaPlatform").GetComponent<Image>();
        favoriteButton = this.transform.Find("Favorite Button").GetComponent<Image>();
        Date = this.transform.Find("Date").GetComponent<Text>();
    }

    private void SetupPanelInformation(Notification notification)
    {
        username.text = notification.Name;
        message.text = notification.Message;
        mediaPlaform.sprite = notification.PlatformLogo;
        SetImage(notification.Img);
        SetTime();

        this.notification = notification;
    }

    public void SetImage(Sprite img) {
        if(img != null) {
            this.image = img;
            imageButton.SetActive(true);
        }
    }

    public void SetTime(){
        Text date = Timestamp.transform.Find("Date").GetComponent<Text>();
        Text time = Timestamp.transform.Find("Time").GetComponent<Text>();
        Date.text = string.Format("{0} \n{1}", time.text, date.text);
    }

    public void ShowImage() {
        panelImage.GetComponent<Image>().sprite = image;
    }

    public void ToggleFavoriteButton() {
        if(notification.IsFavorite) {
            notification.IsFavorite = false;
            favoriteButton.sprite = Resources.Load<Sprite>("Notification/EmptyStar");
        }
        else {
            notification.IsFavorite = true;
            favoriteButton.sprite = Resources.Load<Sprite>("Notification/FilledStar");
        }
        notificationMenu.GetComponent<NotificationControl>().ToggleFavoritePanel(gameObject, notification);
        DeletePanel();
    }

    public void DeletePanel() {
        Debug.Log("Fav: " + notification.IsFavorite);
     
        notificationMenu.GetComponent<NotificationControl>().NotificationPanelRemoved(notification);

        Destroy(gameObject);
    }
}
