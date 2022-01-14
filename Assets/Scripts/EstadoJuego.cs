using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComunicacionAndroid : MonoBehaviour
{

#if UNITY_ANDROID

AndroidJavaClass UnityPlayer;


AndroidJavaObject currentActivity;


AndroidJavaObject intent;


#endif

    public FloatValue playerCoins;
    public FloatValue playerHealth;


}
