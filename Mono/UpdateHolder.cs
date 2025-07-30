using UnityEngine;
using YekGames.UpdateService.Abstraction;

namespace YekGames.UpdateService.Mono
{
    public class UpdateHolder : MonoBehaviour
    {
        private IUpdateService _updateService;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void SetUpdateListener(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void Update()
        {
            _updateService.OnUpdate();
        }

        private void LateUpdate()
        {
            _updateService.OnLateUpdate();
        }

        private void FixedUpdate()
        {
            _updateService.OnFixedUpdate();
        }
    }
}