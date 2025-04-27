using System.Threading;
using System.Threading.Tasks;
using GUtils.Visibility.Visibles;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Players;

namespace GUtilsUnity.Level.FailGoalPopup.UseCases
{
    public sealed class TweenClipVisible : IVisible
    {
        readonly MonoBehaviourTweenClip _showTweenClip;
        readonly MonoBehaviourTweenClip _hideTweenClip;
        readonly TweenPlayer _tweenPlayer;

        public TweenClipVisible(
            MonoBehaviourTweenClip showTweenClip,
            MonoBehaviourTweenClip hideTweenClip,
            TweenPlayer tweenPlayer)
        {
            _showTweenClip = showTweenClip;
            _hideTweenClip = hideTweenClip;
            _tweenPlayer = tweenPlayer;
        }

        public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        {
            MonoBehaviourTweenClip tweenClip = visible
                ? _showTweenClip
                : _hideTweenClip;

            return _tweenPlayer.Play(tweenClip, instantly, cancellationToken);
        }
    }
}
