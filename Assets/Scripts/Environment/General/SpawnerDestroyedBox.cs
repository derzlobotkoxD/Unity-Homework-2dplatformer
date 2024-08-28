public class SpawnerDestroyedBox : Spawner<DestroyedBox>
{
    protected override void ActivateInstance(DestroyedBox destroyedBox) 
    {
        destroyedBox.Deleted += ReleaseProps;
        base.ActivateInstance(destroyedBox);
    }

    private  void ReleaseProps(Props props)
    {
        props.Deleted -= ReleaseProps;
        base.ReleaseInstance((DestroyedBox)props);
    }
}