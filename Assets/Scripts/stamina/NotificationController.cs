using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;
public class NotificationController : MonoBehaviour
{
    [SerializeField] AndroidNotifications androidNotifications;

    private void Start()
    {
        androidNotifications.RequestAuthorization();
        androidNotifications.RegisterNotificationChannel();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus== false)
        {
           AndroidNotificationCenter.CancelAllNotifications();
            androidNotifications.SendNotification("Money Accumulated", "Hey! You've got a lot of money saved up, come spend it improving your planet", 10);

        }
    }
}
