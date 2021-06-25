using UnityEngine;
using MLAPI;

public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera lobbycamera;
    public void setIP(string Ipaddress)
    {
        print("Ipaddress");
        GameObject.Find("NetworkManager").GetComponent<MLAPI.Transports.UNET.UNetTransport>().ConnectAddress = Ipaddress;
    }
    public void StartServer()
    {
        lobbycamera.gameObject.SetActive(false);
        NetworkManager.Singleton.StartHost();
        print("Server");
    }
    public void StartClient()
    {
        lobbycamera.gameObject.SetActive(false);
        NetworkManager.Singleton.StartClient();
        print("Client");
    }
    static void SubmitNewPosition()
    {
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
        {
            if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkedclient))
            {
                var player = networkedclient.PlayerObject.GetComponent("Body");

            }
        }
    }
    void Start()
    {

    }
}


