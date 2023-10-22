using Base.Interfaces;
using UnityEngine;

namespace Base.Data
{
    public abstract class BaseConfig<TData> : ScriptableObject where TData: IConfigData
    {
        [SerializeField] private TData _data;

        public TData Data => _data;
    }
}