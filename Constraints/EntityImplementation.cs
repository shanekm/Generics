using Constraints.Abstract;

namespace Constraints
{
    internal class EntityImplementation : IEntity
    {
        private int index;

        public EntityImplementation(int index)
        {
            this.index = index;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}