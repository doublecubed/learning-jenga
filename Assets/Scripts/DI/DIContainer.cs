// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Barebones dependency injection container.

using System;
using System.Collections.Generic;
using UnityEngine;
using JengaGame.Data;

namespace JengaGame.DI
{
    public class DIContainer : MonoBehaviour
    {
        #region REFERENCES

        private DataSorter _dataSorter;
		
        #endregion
		
        #region VARIABLES
		
        private Dictionary<Type, object> _dependencies = new Dictionary<Type, object>();

        #endregion
		
        #region MONOBEHAVIOUR
		
        private void Awake()
        {
            _dataSorter = FindObjectOfType<DataSorter>();
            Register<IDataProvider>(_dataSorter);
        }
		
        #endregion
		
        #region METHODS

        public void Register<T>(T implementation)
        {
            _dependencies[typeof(T)] = implementation;
        }

        public T Resolve<T>()
        {
            object implementation;
            if (_dependencies.TryGetValue(typeof(T), out implementation))
            {
                return (T)implementation;
            }

            throw new Exception($"Dependency of type {typeof(T)} is not registered.");
        }
		
        #endregion
    }
}