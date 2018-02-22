using UnityEngine;

namespace TwoOfUs.Player
{
    public interface IPlayerController
    {
        void Init(Creator creator);
        
        GameObject GameObject { get; }
        
        Creator Creator { get; }
    }
}