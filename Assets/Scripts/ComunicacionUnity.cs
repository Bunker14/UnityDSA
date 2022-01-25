using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComunicacionUnity : MonoBehaviour
{


    string Username;
    int NumeroMonedas;
    int temp;
    int monedasServidor;
    int monedasiniciales;
    int puntos;
    int vida = 100;
    string userInventoryItems;
    string item;
    string itemParaBorrar;
    string[] InventoryItems;
    int Inicio_bayafisica;
    int Inicio_tartafisica;
    int Inicio_notasfisica;
    int Inicio_vasijafisica;
    int Inicio_llaveBarfisica;
    int Inicio_llaveEETACfisica;
    int Inicio_llaveMuseofisica;
    int startroutine;



    public Inventory playerInventory;
    private PlayerInventory myInventory;
    public InventoryItem baya;
    public InventoryItem notas;
    public InventoryItem tarta;
    public InventoryItem vasija;
    public InventoryItem llaveBar;
    public InventoryItem llaveEETAC;
    public InventoryItem llaveMuseo;

    public PhysicalItem bayafisica;
    public PhysicalItem tartafisica;
    public PhysicalItem notasfisica;
    public PhysicalItem vasijafisica;
    public PhysicalItem llaveBarfisica;
    public PhysicalItem llaveEETACfisica;
    public PhysicalItem llaveMuseofisica;
    


    int tap_count;


    private static  bool m_Initialized = false;

    void Start()
    {
        myInventory.myInventory.Clear();
        GetParametersServidor();
        NumeroMonedas = playerInventory.coins;
        playerInventory.coins = monedasiniciales;
        temp = monedasiniciales;
        addToInventory();
        startroutine = startroutine + 1;
        //{
        //    myInventory.myInventory.Clear();
        //    GetParametersServidor();
        //    NumeroMonedas = playerInventory.coins;
        //    playerInventory.coins = monedasiniciales;
        //    temp = NumeroMonedas;
        //    addToInventory();
        //    startroutine = startroutine + 1;

        //}

        //NumeroMonedas = playerInventory.coins;
        //playerInventory.coins = monedasiniciales;
        ////NumeroMonedas = playerInventory.coins;
        //temp = NumeroMonedas;
        //addToInventory();
    }

    public void cerrarapp()
    {
        puntos = puntos + NumeroMonedas * 10;
        GuardarPartidaServidor();
    }

    public void GetParametersServidor()
    {
#if UNITY_ANDROID
        GetParameters();
#else
        Debug.Log("Esto no es android");
#endif
    }

    public void GiveCoinsServidor()
    {
#if UNITY_ANDROID
        UpdateCoins();
#else
        Debug.Log("Esto no es android");
#endif
    }


    public void GiveItemServidor()
    {
#if UNITY_ANDROID
        UpdateItem();
#else
        Debug.Log("Esto no es android");
#endif
    }

    public void GetItemServidor()
    {
#if UNITY_ANDROID
        GetItem();
#else
        Debug.Log("Esto no es android");
#endif
    }

    public void DeleteItemServidor()
    {
#if UNITY_ANDROID
        DeleteItem();
#else
        Debug.Log("Esto no es android");
#endif
    }

    public void GuardarPartidaServidor()
    {
#if UNITY_ANDROID
        GuardarPartida();
#else
        Debug.Log("Esto no es android");
#endif
    }

    public void GetParameters()
    {
        Debug.Log("GetParameters");
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject UnityPlayerActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject intent = UnityPlayerActivity.Call<AndroidJavaObject>("getIntent");
        bool hasExtra = intent.Call<bool>("hasExtra", "userName");
        if (hasExtra)
        {
            AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
            Username = extras.Call<string>("getString", "userName");
            Debug.Log("Username: " + Username);
        }
        bool hasExtra2 = intent.Call<bool>("hasExtra", "coins");
        if (hasExtra2)
        {
            AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
            monedasiniciales = extras.Call<int>("getInt", "coins");
            Debug.Log("monedasiniciales: " + monedasiniciales);
        }
        bool hasExtra3 = intent.Call<bool>("hasExtra", "points");
        if (hasExtra3)
        {
            AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
            puntos = extras.Call<int>("getInt", "points");
            Debug.Log("puntos: " + puntos);
        }
        bool hasExtra4 = intent.Call<bool>("hasExtra", "health");
        if (hasExtra4)
        {
            AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
            vida = extras.Call<int>("getInt", "health");
            Debug.Log("vida: " + vida);
        }
        bool hasExtra5 = intent.Call<bool>("hasExtra", "userInventory");
        if (hasExtra5)
        {
            AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
            userInventoryItems = extras.Call<string>("getString", "userInventory");
            Debug.Log("userInventoryItems: " + userInventoryItems);
            InventoryItems = userInventoryItems.Split(',');
        }
    }

    public void UpdateCoins()
    {
        Debug.Log("UpdateCoins");
        AndroidJavaClass UnityConnect = new AndroidJavaClass("com.dsa.frontendprojecte.connections.UnityConnect");
        monedasServidor = UnityConnect.CallStatic<int>("updateCoins", NumeroMonedas);
        Debug.Log("monedasServidor: " + monedasServidor);
    }

    public void UpdateItem()
    {
        Debug.Log("UpdateItem");
        AndroidJavaClass UnityConnect = new AndroidJavaClass("com.dsa.frontendprojecte.connections.UnityConnect");
        UnityConnect.CallStatic("collectItem", item);
    }

    public void GetItem()
    {
        Debug.Log("GetItem");
        AndroidJavaClass UnityConnect = new AndroidJavaClass("com.dsa.frontendprojecte.connections.UnityConnect");
        userInventoryItems = UnityConnect.CallStatic<string>("getUserInventory");
        Debug.Log("userInventoryItems: " + userInventoryItems);
        InventoryItems = userInventoryItems.Split(',');
    }

    public void DeleteItem()
    {
        Debug.Log("DeleteItem");
        AndroidJavaClass UnityConnect = new AndroidJavaClass("com.dsa.frontendprojecte.connections.UnityConnect");
        UnityConnect.CallStatic("useItem", itemParaBorrar);
    }

    public void GuardarPartida()
    {
        Debug.Log("GuardarPartida");
        AndroidJavaClass UnityConnect = new AndroidJavaClass("com.dsa.frontendprojecte.connections.UnityConnect");
        UnityConnect.CallStatic("saveGame", puntos, vida);
    }

   
    public void Update()
    {
        NumeroCoins();
        itemaded();
        StartCoroutine(ComprobarTienda());



        //void NumeroCoins()
        //{


        //    if (NumeroMonedas > temp)
        //    {                
        //        GiveCoinsServidor();
        //        temp = NumeroMonedas;
        //    }
        //    else if(monedasServidor < NumeroMonedas)
        //    {
        //        monedasServidor = playerInventory.coins;
        //    }

        //}

        //void itemaded()
        //{
        //    if (bayafisica.itemrecibido == true)
        //    {
        //        item = bayafisica.thisItem.itemName;
        //        GiveItemServidor();
        //    }
        //    if (tartafisica.itemrecibido == true)
        //    {
        //        item = bayafisica.thisItem.itemName;
        //        GiveItemServidor();
        //    }
        //    if (notasfisica.itemrecibido == true)
        //    {
        //        item = bayafisica.thisItem.itemName;
        //        GiveItemServidor();
        //    }
        //    if (basijafisica.itemrecibido == true)
        //    {
        //        item = bayafisica.thisItem.itemName;
        //        GiveItemServidor();
        //    }
        //    if (llaveBarfisica.itemrecibido == true)
        //    {
        //        item = bayafisica.thisItem.itemName;
        //        GiveItemServidor();
        //    }
        //    if (llaveEETACfisica.itemrecibido == true)
        //    {
        //        item = bayafisica.thisItem.itemName;
        //        GiveItemServidor();
        //    }
        //    if (llaveMuseofisica.itemrecibido == true)
        //    {
        //        item = bayafisica.thisItem.itemName;
        //        GiveItemServidor();
        //    }

        //}

    }

    private IEnumerator ComprobarTienda()
    {
        
        yield return new WaitForSeconds(25f);
        myInventory.myInventory.Clear();
        GetItemServidor();
        addToInventory();
    }

    public void NumeroCoins()
    {


        if (playerInventory.coins > temp)
        {
            GiveCoinsServidor();
            temp = temp + 1;
        }
        else if (monedasServidor < NumeroMonedas)
        {
            monedasServidor = playerInventory.coins;
        }

    }

    public void itemaded()
    {
        if (bayafisica.itemrecibido == true)
        {
            item = bayafisica.thisItem.itemName;
            if (bayafisica.thisItem.numberHeld > Inicio_bayafisica)
            {
                GiveItemServidor();
            }          
            bayafisica.itemrecibido = false;
        }
        if (tartafisica.itemrecibido == true)
        {
            item = tartafisica.thisItem.itemName;
            if (tartafisica.thisItem.numberHeld > Inicio_tartafisica)
            {
                GiveItemServidor();
            }
            tartafisica.itemrecibido = false;
        }
        if (notasfisica.itemrecibido == true)
        {
            item = notasfisica.thisItem.itemName;
            if (notasfisica.thisItem.numberHeld > Inicio_notasfisica)
            {
                GiveItemServidor();
            }
            notasfisica.itemrecibido = false;
        }
        if (vasijafisica.itemrecibido == true)
        {
            item = vasijafisica.thisItem.itemName;
            if (vasijafisica.thisItem.numberHeld > Inicio_vasijafisica)
            {
                GiveItemServidor();
            }
            vasijafisica.itemrecibido = false;
        }
        if (llaveBarfisica.itemrecibido == true)
        {
            item = llaveBarfisica.thisItem.itemName;
            if (bayafisica.thisItem.numberHeld > Inicio_llaveBarfisica)
            {
                GiveItemServidor();
            }
            llaveBarfisica.itemrecibido = false;
        }
        if (llaveEETACfisica.itemrecibido == true)
        {
            item = llaveEETACfisica.thisItem.itemName;
            if (llaveEETACfisica.thisItem.numberHeld > Inicio_llaveEETACfisica)
            {
                GiveItemServidor();
            }
            llaveEETACfisica.itemrecibido = false;
        }
        if (llaveMuseofisica.itemrecibido == true)
        {
            item = llaveMuseofisica.thisItem.itemName;
            if (bayafisica.thisItem.numberHeld > Inicio_llaveMuseofisica)
            {
                GiveItemServidor();
            }
            llaveMuseofisica.itemrecibido = false;
        }

    }

    public void DeletearItemInventario(string itemname)
    {
        itemParaBorrar = itemname;
        DeleteItemServidor();
    }


    

    public void addToInventory()
    {
        int i = InventoryItems.Length;
        if (InventoryItems[0] == "0")
        {           
        }
        else
        {
            bayafisica.AddItemToInventory();
            Inicio_bayafisica = 1;
        }

        int j = 1;
        while (j < i)
        {

            if (InventoryItems[j] == tarta.itemName)
            {
                tartafisica.AddItemToInventory();
                Inicio_tartafisica = 1;
                Destroy(tartafisica.gameObject);
            }
            if (InventoryItems[j] == notas.itemName)
            {
                notasfisica.AddItemToInventory();
                Inicio_notasfisica = 1;
                Destroy(notasfisica.gameObject);
            }
            if (InventoryItems[j] == vasija.itemName)
            {
                vasijafisica.AddItemToInventory();
                Inicio_vasijafisica = 1;
                Destroy(vasijafisica.gameObject);
            }
            if (InventoryItems[j] == llaveBar.itemName)
            {
                llaveBarfisica.AddItemToInventory();
                Inicio_llaveBarfisica = 1;
                Destroy(llaveBarfisica.gameObject);
            }
            if (InventoryItems[j] == llaveEETAC.itemName)
            {
                llaveEETACfisica.AddItemToInventory();
                Inicio_llaveEETACfisica = 1;
                Destroy(llaveEETACfisica.gameObject);
            }
            if (InventoryItems[j] == llaveMuseo.itemName)
            {
                llaveMuseofisica.AddItemToInventory();
                Inicio_llaveMuseofisica = 1;
                Destroy(llaveMuseofisica.gameObject);
            }
            j++;
        }
        
    }






}
