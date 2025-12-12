using QMVC;

public class NestedList : Architecture<NestedList>
{
    protected override void Init()
    {

        RegisterSystem(new PoolSystem());
        RegisterUtility(new ItemBaseFactory());

    }
}
