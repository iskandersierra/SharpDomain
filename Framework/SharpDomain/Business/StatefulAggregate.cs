using SharpDomain.Messaging;

namespace SharpDomain.Business
{
    public abstract class StatefulAggregate : Aggregate
    {
        protected StatefulAggregate()
        {
        }

        protected override void ApplyOverride(IEvent @event)
        {

        }

        #region [ Reflection cache registration ]


        class StatefulApplyMethods
        {
            
        }

        #endregion
    }
}