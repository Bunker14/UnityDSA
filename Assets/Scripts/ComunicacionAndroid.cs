using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoJuego : MonoBehaviour
{

#if UNITY_ANDROID

AndroidJavaClass UnityPlayer;


AndroidJavaObject currentActivity;


AndroidJavaObject intent;


#endif

    public VectorValue playerCoins;
    public FloatValue playerHealth;

}
