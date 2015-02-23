namespace DataObjects
{
    using System;
    using System.CodeDom;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    using DataObjects.Abstract;

    public class QueueBuffer<T> : IBuffer<T>
    {
        protected Queue<T> queue = new Queue<T>();
 
        // This will be a base class so that other classes can implement it
        // Make it virtual - child classes may override the behaviour
        public virtual bool IsEmpty
        {
            get
            {
                return queue.Count == 0;
            }
        }

        public virtual void Write(T value)
        {
            queue.Enqueue(value);
        }

        public virtual T Read()
        {
            // For event
            var discard = queue.Dequeue();

            // ERROR here => but we would have access to value written here
            T someValue = default(T);
            OnItemDiscarded(discard, someValue);

            return discard;
        }

        private void OnItemDiscarded(T discard, T someValue)
        {
            // Check to see if anyone listening/ subscribe to event
            if (ItemDiscarded != null)
            {
                var args = new ItemDiscardedEventArgs<T>(discard, someValue);
                ItemDiscarded(this, args);
            }
        }

        // Implementation of IEnumerator<T>
        // allows foreach
        // GetEnumerator => returns object that can be used to iterate through the collection
        // Returns Generic enumerator
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in queue)
            {
                // ... other code
                // do some other stuff
                // "yield" keyword => will return each item (return then come back here and return next)
                yield return item;
            }
        }

        // Returns IEnumerator, not generic type
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        // New methods returns Enumerable of TOutput
        // ex. Strings for logger (contents of buffer)
        // NOW every IBuffer concrete type will be able return content of the buffer
        // with a specified type
        public IEnumerable<TOutput> AsEnumerableOf<TOutput>()
        {
            // Need to conver T into TOutput we want
            // So we're using TypeDescriptor to figure out what type T is
            var converter = TypeDescriptor.GetConverter(typeof(T));
            foreach (var item in queue)
            {
                var result = converter.ConvertTo(item, typeof(TOutput));
                yield return (TOutput)result;
            }
        }

        // TAKE ONE
        // Adding event when item is discarded
        // The Problem is that EventArgs makes it difficult to have your own properties sent
        // Therefor its easy to make another custom EventArgs class
        //public event EventHandler<EventArgs> ItemDiscarded;

        // TAKE TWO
        // Extending EventArgs
        public event EventHandler<ItemDiscardedEventArgs<T>> ItemDiscarded;
    }

    public class ItemDiscardedEventArgs<T> : EventArgs
    {
        public ItemDiscardedEventArgs(T discard, T newitem)
        {
            ITemDiscarded = discard;
            NewItem = newitem;
        }

        // How to make it generic? since you don't know if INT will be returned
        //public int ItemDiscarded { get; set; }
        public T ITemDiscarded { get; set; }

        public T NewItem { get; set; }
    }
}
