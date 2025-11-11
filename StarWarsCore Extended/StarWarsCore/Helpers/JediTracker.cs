using System;
using System.Collections.Generic;
using StarWarsCore.Models;

namespace StarWarsCore.Helpers
{
    public class JediTracker : IObservable<JediKnight>
    {
        public JediTracker()
        {
            observers = new List<IObserver<JediKnight>>();
        }

        private List<IObserver<JediKnight>> observers;

        public IDisposable Subscribe(IObserver<JediKnight> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<JediKnight>> _observers;
            private IObserver<JediKnight> _observer;

            public Unsubscriber(List<IObserver<JediKnight>> observers, IObserver<JediKnight> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void TrackJedi(JediKnight myWarrior)
        {
            foreach (var observer in observers)
            {
                if (myWarrior.FightLog == null)
                    observer.OnError(new JediKnightUnknownException());
                else
                    observer.OnNext(myWarrior);
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }
    }

    public class JediKnightUnknownException : Exception
    {
        internal JediKnightUnknownException()
        { }
    }

}