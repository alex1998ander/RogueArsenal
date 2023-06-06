using System;

public class Event {
    
    private event Action Action;

    public void Trigger() {
        Action?.Invoke();
    }

    public void Subscribe(Action func) {
        Action += func;
    }

    public void Unsubscribe(Action func) {
        Action += func;
    }
    
}

public class Event<T> {
    
    private event Action<T> Action;

    public void Trigger(T t) {
        Action?.Invoke(t);
    }

    public void Subscribe(Action<T> func) {
        Action += func;
    }

    public void Unsubscribe(Action<T> func) {
        Action += func;
    }
    
}