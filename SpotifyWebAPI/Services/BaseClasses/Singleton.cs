using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebAPI.Services.BaseClasses
{
    public abstract class Singleton<T> where T : new()
    {
    private static readonly Lazy<T> Lazy =
        new Lazy<T>(() => new T());

    public static T Instance => Lazy.Value;

    }
}
