using GameControl;

public static class SingletoneGameController
{
    public static GameController _singletone;
}
public static class Singltone<T>
{
    public static T singletone;
    public static void SetGameController(T ex)
    {
        singletone = ex;
    }
}
