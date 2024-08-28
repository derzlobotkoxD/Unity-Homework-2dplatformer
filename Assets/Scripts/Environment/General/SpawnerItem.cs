using System;

public class SpawnerItem : Spawner<Item>
{
    protected override void ActivateInstance(Item item)
    {
        item.Deleted += ReleaseProps;
        base.ActivateInstance(item);
    }

    private void ReleaseProps(Props props)
    {
        props.Deleted -= ReleaseProps;
        base.ReleaseInstance((Item)props);
    }
}