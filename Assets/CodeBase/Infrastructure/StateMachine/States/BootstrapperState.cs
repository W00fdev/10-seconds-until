using Infrastructure.Services.InputService;
using Logic.Managers;
using Logic;
using Infrastructure.Services.FactoryService;

namespace Infrastructure.GameStateMachine.States
{
    public class BootstrapperState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly SoundHandler _soundController;
        private readonly SceneLoader _sceneLoader;
        private readonly IProjectile _enemyProjectilePrefab;
        
        private readonly AllServices _services = AllServices.Container;

        private const string InitialSceneName = "InitialScene";
        private const string NextSceneName = "Level1";

        public BootstrapperState(IStateSwitcher stateSwitcher, SoundHandler soundController, SceneLoader sceneLoader, IProjectile enemyProjectilePrefab)
        {
            _stateSwitcher = stateSwitcher;
            _soundController = soundController;
            _sceneLoader = sceneLoader;
            _enemyProjectilePrefab = enemyProjectilePrefab;
        }

        public void Enter()
        {
            RegisterServices();
            
            _sceneLoader.Load(InitialSceneName, EnterLoadLevel); 
        }

        private void EnterLoadLevel()
        {
            _stateSwitcher.Enter<LoadLevelState, string>(NextSceneName);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(new StandaloneInputService());

            // Projectile factory for enemies in statics. For player in inner scripts;
            _services.RegisterSingle<IProjectileFactoryService<IProjectile>>(new ProjectileFactory<IProjectile>(_enemyProjectilePrefab));
            _soundController.Init();
        }

    }
}
