namespace InputSystem.UI.Model
{
    public class ChomperPrefabInstaller : CommandExecutorsInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<float>().WithId("Distance").FromInstance(1.0f);
            Container.Bind<float>().WithId("AttackSpeed").FromInstance(0.3f);
            Container.Bind<float>().WithId("AttackDamage").FromInstance(300.0f);
        }
    }
}