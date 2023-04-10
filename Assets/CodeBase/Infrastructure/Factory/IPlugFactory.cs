using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IPlugFactory : IService
    {
        IEnumerable<ISaveProgressReader> SaveProgressReaders { get; }
        GameObject CreatePlug();
    }
}