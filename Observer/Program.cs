using System;
using System.Collections.Generic;
using System.Threading;

namespace Observer
{

    public interface IObserver
    {
        // Receive update from subject.
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        // Attach an observer to the subject.
        void Attach(IObserver observer);

        // Detach an observer from the subject.
        void Detach(IObserver observer);

        // Notify all observers about an event.
        void Notify();
    }

    public class Subject : ISubject
    {
        public int State { get; set; } = -0;

        private List<IObserver> observers = new List<IObserver>();

        // The subscription management methods.
        public void Attach(IObserver observer)
        {
            System.Console.WriteLine("Subject: Attached an observer.");
            this.observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            System.Console.WriteLine("Subject: Detached an observer.");
            this.observers.Remove(observer);
        }

        // Trigger an update in each subscriber.
        public void Notify()
        {
            Console.WriteLine("Subject: Notifying observers...");
            observers.ForEach(x => x.Update(this));
        }

        // Usually, the subscription logic is only a fraction of what a Subject
        // can really do. Subjects commonly hold some important business logic,
        // that triggers a notification method whenever something important is
        // about to happen (or after it).
        public void SomeBusinessLogic()
        {
            Console.WriteLine("\nSubject: I'm doing something important.");
            this.State = new Random().Next(0, 10);

            Thread.Sleep(15);

            Console.WriteLine("Subject: My state has just changed to: " + this.State);
            this.Notify();
        }
    }

    public class ConcreteObserverA : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State > 3)
            {
                System.Console.WriteLine("ConcreteObserverA: Reacted to the event.");
            }
        }
    }

    public class ConcreteObserverB : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
            {
                System.Console.WriteLine("ConcreteObserverB: Reacted to the event.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();
        }
    }
}
