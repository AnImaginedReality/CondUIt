using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace AIR.Conduit {
    public class ServiceLoader : IServiceLoader {
        private Dictionary<Type, object> Services = new Dictionary<Type, object>();
        private const string MISSING_SERVICE_LOG = "No service was registered for {0}.";

        internal void ClearServices() {
            if (Services == null) return;
            Services.Clear();
            // Services = null;
        }

        internal void RegisterService(Type t, object service) {
            Services[t] = service;
        }

        public void RegisterService<T>(T service) {
            RegisterService(typeof(T), service);
        }

        public T GetService<T>() {
            var serviceType = typeof(T);
            if (Services.ContainsKey(serviceType))
                return (T)Services[serviceType];
            else
                throw new Exception(string.Format(MISSING_SERVICE_LOG, serviceType.Name));
        }

        internal bool CheckServiceRegistered<T>() {
            return Services.ContainsKey(typeof(T));
        }
        
    }
}