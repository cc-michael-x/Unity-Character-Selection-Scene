using System.Collections.Generic;
using Mirror;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class PlayFabLogin : MonoBehaviour
    {
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passwordInputField;

        public static string SessionTicket;

        public Configuration configuration;
        public NetworkManager networkManager;
        public TelepathyTransport telepathyTransport;
        public ApathyTransport apathyTransport;

        public void CreateAccount()
        {
            PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
            {
                Username = usernameInputField.text,
                Email = emailInputField.text,
                Password = passwordInputField.text
            }, result =>
            {
                SessionTicket = result.SessionTicket;
                SignIn();
            }, error => { Debug.LogError(error.GenerateErrorReport()); });
        }

        public void SignIn()
        {
            PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
            {
                Username = usernameInputField.text,
                Password = passwordInputField.text
            }, result =>
            {
                SessionTicket = result.SessionTicket;
                OnPlayFabLoginSuccess(result);
                // start fade out of scene
                MenuSceneManager.Instance.EnterPrepareCouncilScene();
            }, error => { Debug.LogError(error.GenerateErrorReport()); });
        }

        private void OnPlayFabLoginSuccess(LoginResult response)
        {
            Debug.Log(response.ToString());
            if (configuration.ipAddress == "")
            {
                //We need to grab an IP and Port from a server based on the buildId. Copy this and add it to your Configuration.
                RequestMultiplayerServer();
            }
            else
            {
                ConnectRemoteClient();
            }
        }

        private void RequestMultiplayerServer()
        {
            Debug.Log("[ClientStartUp].RequestMultiplayerServer");
            RequestMultiplayerServerRequest requestData = new RequestMultiplayerServerRequest();
            requestData.BuildId = configuration.buildId;
            requestData.SessionId = System.Guid.NewGuid().ToString();
            requestData.PreferredRegions = new List<AzureRegion>() {AzureRegion.EastUs};
            PlayFabMultiplayerAPI.RequestMultiplayerServer(requestData, OnRequestMultiplayerServer,
                OnRequestMultiplayerServerError);
        }

        private void OnRequestMultiplayerServer(RequestMultiplayerServerResponse response)
        {
            Debug.Log(response.ToString());
            ConnectRemoteClient(response);
        }

        private void ConnectRemoteClient(RequestMultiplayerServerResponse response = null)
        {
            if (response == null)
            {
                networkManager.networkAddress = configuration.ipAddress;
                telepathyTransport.port = configuration.port;
                apathyTransport.port = configuration.port;
            }
            else
            {
                Debug.Log("**** ADD THIS TO YOUR CONFIGURATION **** -- IP: " + response.IPV4Address + " Port: " +
                          (ushort) response.Ports[0].Num);
                networkManager.networkAddress = response.IPV4Address;
                telepathyTransport.port = (ushort) response.Ports[0].Num;
                apathyTransport.port = (ushort) response.Ports[0].Num;
            }

            networkManager.StartClient();

            SoundManager.Instance.ButtonClickSound();
            // start fade out of scene
            MenuSceneManager.Instance.EnterPrepareCouncilScene();
        }

        private void OnRequestMultiplayerServerError(PlayFabError error)
        {
            Debug.Log(error.ErrorDetails);
        }
    }
}