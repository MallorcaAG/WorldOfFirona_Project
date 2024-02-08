using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    [SerializeField] private GameObject cam;

    public override void OnNetworkSpawn()
    {
        cam.SetActive(IsOwner);
    }

}
