using UnityEngine;

namespace Utils.Menue {
    public class SelectedObject<T> : ScriptableObject {
        [SerializeField] private T _object;

        public T Object {
            get { 
                return _object; 
            }
            set { 
            _object = value;
            }
        }
    }
}