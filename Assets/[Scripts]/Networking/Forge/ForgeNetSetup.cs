using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.SimpleJSON;

public class ForgeNetSetup : DreamboundNetBehavior
{
    [SerializeField] int maxPlayers = 20;
    JSONNode _NETData;

    [Header("Master Server Settings")]
    public string _NETID = "SERV_DEV: ";
    public string _NETName = "GOOGLYEYES";
    public string _NETType = "MMO";
    public string _NETMode = "NULL";
    public string _NETComment = "Dev comment...";
    public bool useElo = false;
    public int eloRequired = 0;

    [SerializeField] bool useTCP;

    [SerializeField] NetworkManager mgr = null;

    ushort _NETMasterPort = 15940;
    public ushort _NETPort = 15937;
    string _NETMasterIP = "127.0.0.1";
    public string _NETIP = "127.0.0.1";

    private NetWorker server;
    private string natServerHost = string.Empty;
    public bool DontChangeSceneOnConnect = false;
    public bool getLocalNetworkConnections = false;
    private bool _matchmaking = false;
    [SerializeField] bool connectUsingMatchmaking;
    public int myElo = 0;


    private void Start()
    {
        _NETID = _NETID + Random.Range(0, 1000).ToString();
        mgr = GetComponent<NetworkManager>();

        if (mgr == null)
        {
            Debug.LogWarning("A network manager was not provided, generating a new one instead");
            mgr = gameObject.AddComponent<NetworkManager>();
        }
    }

    public void OnHostServerClicked()
    {
        if (useTCP)
        {
            server = new TCPServer(maxPlayers);
            ((TCPServer)server).Connect();
        }
        else
        {
            server = new UDPServer(maxPlayers);

            if (natServerHost.Trim().Length == 0)
                ((UDPServer)server).Connect(_NETIP, _NETPort);
        }

        server.playerTimeout += (player, sender) =>
        {
            Debug.Log("Player " + player.NetworkId + " timed out");
        };

        Connected(server);
    }

    public void Connected(NetWorker networker)
    {
        if (!networker.IsBound)
        {
            Debug.LogError("NetWorker failed to bind");
            return;
        }


        // If we are using the master server we need to get the registration data
        JSONNode masterServerData = null;
        if (!string.IsNullOrEmpty(_NETMasterIP))
        {
            masterServerData = mgr.MasterServerRegisterData(networker, _NETID, _NETID + "" + _NETName, _NETType, _NETMode, _NETComment, useElo, eloRequired);
        }

        mgr.Initialize(networker, _NETMasterIP, _NETMasterPort, masterServerData);

        if (networker is IServer)
        {
            if (!DontChangeSceneOnConnect)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            }
            else
                NetworkObject.Flush(networker); //Called because we are already in the correct scene!
        }
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        Debug.Log("Switching scene");
        NetworkManager.Instance.InstantiatePlayer();
    }

    public void Connect()
    {
        if (connectUsingMatchmaking)
        {
            ConnectToMatchmaking();
            return;
        }

        //ushort port;
        //if (!ushort.TryParse(portNumber.text, out port))
        //{
        //    Debug.LogError("The supplied port number is not within the allowed range 0-" + ushort.MaxValue);
        //    return;
        //}

        NetWorker client;

        if (useTCP)
        {
            client = new TCPClient();
            ((TCPClient)client).Connect(_NETIP, _NETPort);
        }
        else
        {
            client = new UDPClient();
            if (natServerHost.Trim().Length == 0)
                ((UDPClient)client).Connect(_NETIP, _NETPort);

        }

        Connected(client);
    }

    public void ConnectToMatchmaking()
    {
        if (_matchmaking)
            return;

        _matchmaking = true;

        if (mgr == null)
            throw new System.Exception("A network manager was not provided, this is required for the tons of fancy stuff");

        mgr.MatchmakingServersFromMasterServer(_NETIP, _NETPort, myElo, (response) =>
        {
            _matchmaking = false;
            Debug.LogFormat("Matching Server(s) count[{0}]", response.serverResponse.Count);

            //TODO: YOUR OWN MATCHMAKING EXTRA LOGIC HERE!
            // I just make it randomly pick a server... you can do whatever you please!
            if (response != null && response.serverResponse.Count > 0)
            {
                MasterServerResponse.Server server = response.serverResponse[Random.Range(0, response.serverResponse.Count)];
                //TCPClient client = new TCPClient();
                UDPClient client = new UDPClient();
                client.Connect(server.Address, server.Port);
                Connected(client);
            }
        });
    }

    private void OnApplicationQuit()
    {
        if (getLocalNetworkConnections)
            NetWorker.EndSession();

        if (server != null) server.Disconnect(true);
    }

}