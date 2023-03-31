using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class NotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private const string ChannelId = "notification_Channel";

    //Creates and set up the notification that will be send to the player
    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel NotificationChannel = new AndroidNotificationChannel
        {
            Id = ChannelId,
            Name = "Notification Channel",
            Description = "Some random description",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(NotificationChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy Recharged!",
            Text = "Come back to us, Your energy is recharged!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime,
        };

        AndroidNotificationCenter.SendNotification(notification, ChannelId);
    }
#endif

}
