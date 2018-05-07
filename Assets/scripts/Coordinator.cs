using System;

public class Coordinator {
    private static Coordinator coordinator;
    private Action OnNext;

    public static Coordinator GetInstance()
    {
        if (coordinator == null) coordinator = new Coordinator();
        return coordinator;
    }

    public void ChangeForNext()
    {
        //OnNext();
    }

    public void SetOnNext(Action OnNext)
    {
        this.OnNext = OnNext;
    }
}
