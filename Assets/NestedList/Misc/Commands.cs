using QMVC;

public class RefreshNestedListCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        this.SendEvent<RefreshNestedListEvent>();
    }
}