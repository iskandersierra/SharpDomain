namespace SharpDomain.Messaging
{
    public abstract class Command : ICommand
    {
        
    }

    public abstract class Command<TId> : Command, ICommand<TId>
    {
        private readonly TId _id;

        protected Command(TId id)
        {
            _id = id;
        }

        public TId Id
        {
            get { return _id; }
        }
    }
}