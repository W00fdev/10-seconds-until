using System.Collections.Generic;
using UnityEngine;
using System;

namespace Logic.Player
{
    public class PlayerFormChanger : MonoBehaviour, IPlayer
    {
        public Transform PlayerTransform 
        {
            get => transform.GetChild(0); 
        }

        private void Awake()
        {
            
        }
    }
}
