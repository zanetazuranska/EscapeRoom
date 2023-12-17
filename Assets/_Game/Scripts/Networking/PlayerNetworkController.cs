using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;

public class PlayerNetworkController : NetworkBehaviour
{
    private void Awake()
    {
        NetworkManager.Singleton.SceneManager.OnLoadComplete += scene;
    }

    private void scene(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Jebany netcode laduje scene");

        throw new NotImplementedException();
    }

    private void Update()
    {

        if(IsOwner)
        {
            //Debug.Log("Owner");
        }
    }
}
