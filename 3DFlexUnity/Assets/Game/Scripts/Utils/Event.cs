using System;

namespace Game.Scripts.Utils
{
    public delegate void Event<T>(T eventArgs) where T : EventArgs;
}