using System;
using SharpDomain.Properties;

namespace SharpDomain.EventSourcing
{
    public abstract class MessageProcessingStep : IMessageProcessingStep
    {
        protected MessageProcessingStep()
        {
            State = ProcessingStepState.Open;
        }

        public ProcessingStepState State { get; private set; }
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            if (IsDisposed) return;
            try
            {
                if (State == ProcessingStepState.Open)
                    Commit();
                DoDispose();
            }
            finally
            {
                IsDisposed = true;
                GC.SuppressFinalize(this);
            }
        }

        public void Commit()
        {
            VerifyState(ProcessingStepState.Open);
            try
            {
                DoCommit();
                State = ProcessingStepState.Committed;
            }
            catch (Exception ex)
            {
                State = ProcessingStepState.Exception;
            }

        }

        public void Rollback()
        {
            VerifyState(ProcessingStepState.Open);
            try
            {
                DoRollback();
                State = ProcessingStepState.RolledBack;
            }
            catch (Exception ex)
            {
                State = ProcessingStepState.Exception;
            }
        }

        protected virtual void DoCommit() { }

        protected virtual void DoRollback() { }

        protected virtual void DoDispose() { }

        private void VerifyState(ProcessingStepState expectedState)
        {
            if (expectedState != State)
                throw new InvalidOperationException(string.Format(Resources.UnexpectedProcessingStepState, expectedState, State));
        }
    }
}