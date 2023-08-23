using GameControl;

public static class SingletoneGameController
{
    public static AbstractGameController _singletone;
}
/// <summary>
/// Need check isExist "T"
/// </summary>
/// <typeparam name="T"></typeparam>
public static class Singltone<T>
    where T : class
{
    // For solve you can write get for singletone variable, that will check existing
    public static T singletone { get; private set; }
    public static void SetSingltone(T ex)
    {
        if(singletone != null)
        {
            return;
        }
        else
        {
            singletone = ex;
        }
    }
}
