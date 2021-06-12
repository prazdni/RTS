using Zenject;

namespace InputSystem.UI.Presenter
{
    public class PresenterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputPresenter>().AsSingle();
        }
    }
}