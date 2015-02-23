namespace DataObjects
{
    // Implementing QueueBuffer - Generic classes with Inheritance
    // but with feature to get memory back
    // when buffer exceeds capacity
    public class CircularBufferInheritance<T> : QueueBuffer<T>
    {
        private int capacity;

        public CircularBufferInheritance(int capacity = 10)
        {
            this.capacity = capacity;
        }

        public override void Write(T value)
        {
            base.Write(value);
            if (queue.Count > capacity)
            {
                // Throw it away when over capacity
                queue.Dequeue();
            }
        }

        public bool IsFull { 
            get
            {
                return queue.Count == this.capacity;
            } 
        }
    }
}
