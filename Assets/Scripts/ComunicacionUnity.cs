using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComunicacionUnity : MonoBehaviour
{


    string Username;
    int NumeroMonedas;
    int temp = 0;
    int monedasServidor;
    int monedasiniciales;
    int puntos;
    int vida = 3;
    string userInventoryItems;
    string item;
    string itemParaBorrar;
    string[] InventoryItems;



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

    void Start()
    {
        GetParametersServidor();
        monedasiniciales = NumeroMonedas;
        NumeroMonedas = playerInventory.coins;

    }

    public void Quit()
    {
        //puntos = NumeroMonedas * 10;
        //GuardarPartidaServidor();
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
        bool hasExtra2 = intent.Call<bool>("hasExtra2", "coins");
        if (hasExtra2)
        {
            AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
            monedasiniciales = extras.Call<int>("getInt", "coins");
            Debug.Log("monedasiniciales: " + monedasiniciales);

        }
        bool hasExtra3 = intent.Call<bool>("hasExtra3", "points");
        if (hasExtra3)
        {
            AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
            puntos = extras.Call<int>("getInt", "points");
            Debug.Log("puntos: " + puntos);

        }
        bool hasExtra4 = intent.Call<bool>("hasExtra4", "health");
        if (hasExtra4)
        {
            AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
            vida = extras.Call<int>("getInt", "health");
            Debug.Log("vida: " + vida);

        }
        bool hasExtra5 = intent.Call<bool>("hasExtra5", "userInventory");
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
        AndroidJavaClass UnityConnect = new AndroidJavaClass("com.dsa.frontendprojecte.connections.UnityConnect");
        monedasServidor = UnityConnect.CallStatic<int>("updateCoins", NumeroMonedas);
        Debug.Log("monedasServidor: " + monedasServidor);
    }

    public void UpdateItem()
    {
        AndroidJavaClass UnityConnect = new AndroidJavaClass("com.dsa.frontendprojecte.connections.UnityConnect");
        UnityConnect.CallStatic("collectItem", item);
    }

    public void GetItem()
    {

        AndroidJavaClass UnityConnect = new AndroidJavaClass("com.dsa.frontendprojecte.connections.UnityConnect");
        userInventoryItems = UnityConnect.CallStatic<string>("getUserInventory");
        Debug.Log("userInventoryItems: " + userInventoryItems);

    }

    public void DeleteItem()
    {
        AndroidJavaClass UnityConnect = new AndroidJavaClass("com.dsa.frontendprojecte.connections.UnityConnect");
        UnityConnect.CallStatic("useItem", itemParaBorrar);
    }

    public void GuardarPartida()
    {
        AndroidJavaClass UnityConnect = new AndroidJavaClass("com.dsa.frontendprojecte.connections.UnityConnect");
        UnityConnect.CallStatic("saveGame", puntos, vida);
    }

    


    public void Update()
    {
        NumeroCoins();
        itemaded();
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

    public void NumeroCoins()
    {


        if (NumeroMonedas > temp)
        {
            GiveCoinsServidor();
            temp = NumeroMonedas;
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
            GiveItemServidor();
        }
        if (tartafisica.itemrecibido == true)
        {
            item = tartafisica.thisItem.itemName;
            GiveItemServidor();
        }
        if (notasfisica.itemrecibido == true)
        {
            item = notasfisica.thisItem.itemName;
            GiveItemServidor();
        }
        if (vasijafisica.itemrecibido == true)
        {
            item = vasijafisica.thisItem.itemName;
            GiveItemServidor();
        }
        if (llaveBarfisica.itemrecibido == true)
        {
            item = llaveBarfisica.thisItem.itemName;
            GiveItemServidor();
        }
        if (llaveEETACfisica.itemrecibido == true)
        {
            item = llaveEETACfisica.thisItem.itemName;
            GiveItemServidor();
        }
        if (llaveMuseofisica.itemrecibido == true)
        {
            item = llaveMuseofisica.thisItem.itemName;
            GiveItemServidor();
        }

    }

    public void DeletearItemInventario(string itemname)
    {
        itemParaBorrar = itemname;
        DeleteItemServidor();
    }


    

    public void addToInventory(string itemTienda)
    {
        int i = InventoryItems.Length;
        int j = 0;
        while (j < i)
        {
            if (InventoryItems[j] == baya.itemName)
            {
                bayafisica.AddItemToInventory();
            }
            if (InventoryItems[j] == tarta.itemName)
            {
                tartafisica.AddItemToInventory();
            }
            if (InventoryItems[j] == notas.itemName)
            {
                notasfisica.AddItemToInventory();
            }
            if (InventoryItems[j] == vasija.itemName)
            {
                vasijafisica.AddItemToInventory();
            }
            if (InventoryItems[j] == llaveBar.itemName)
            {
                llaveBarfisica.AddItemToInventory();
            }
            if (InventoryItems[j] == llaveEETAC.itemName)
            {
                llaveEETACfisica.AddItemToInventory();
            }
            if (InventoryItems[j] == llaveMuseo.itemName)
            {
                llaveMuseofisica.AddItemToInventory();
            }
            j++;
        }
        
    }






}
