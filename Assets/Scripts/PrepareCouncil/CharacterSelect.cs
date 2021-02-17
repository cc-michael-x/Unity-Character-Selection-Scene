using System;
using Mirror;
using UnityEngine;

namespace PrepareCouncil
{
    public class CharacterSelect : NetworkBehaviour
    {
        [SerializeField] private GameObject[] characters;

        private NetworkIdentity _networkIdentity;
        
        private void Awake()
        {
            _networkIdentity = GetComponent<NetworkIdentity>();
            
        }

        public void SelectCharacter()
        {
            /*CmdSelect();*/
        }

        /*[Command]
        public void CmdSelect(NetworkConnectionToClient sender = null)
        {
            
        }*/
    }
}
