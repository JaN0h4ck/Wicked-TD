using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Menue {
    public class MenueNavigation : MonoBehaviour
    {
        [Header("Menue")]
        [SerializeField] GameObject _menue;

        public virtual void OpenMenue()
        {
            _menue.SetActive(true);
        }

        public virtual void CloseMenue()
        {
            _menue.SetActive(false);
        }
    }
}