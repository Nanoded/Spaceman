using CryoDI;

public class DataContainer : UnityStarter
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    protected override void SetupContainer(CryoContainer container)
    {
        container.RegisterSceneObject<ShipController>(LifeTime.Global);
        container.RegisterSceneObject<GridGenerator>(LifeTime.Global);
    }
}
