using System;
using System.Collections.Generic;

namespace SharpDomain.EventSourcing
{
    [ContextSet(typeof(Guid), ProcessorContextKeys.ResourceId)]
    public class GuidResourceIdProcessingStep : MessageProcessingStep
    {
        public GuidResourceIdProcessingStep(
            IProcessorContext context, 
            [ContextGet(ProcessorContextKeys.MessageParameters)] IDictionary<string, string> parameters)
        {
            var resourceId = parameters[ProcessorContextKeys.ResourceId];
            var guid = Guid.Parse(resourceId);
            context.Set(guid, ProcessorContextKeys.ResourceId);
        }
    }
    
    [ContextSet(typeof(string), ProcessorContextKeys.ResourceId)]
    public class StringResourceIdProcessingStep : MessageProcessingStep
    {
        public StringResourceIdProcessingStep(
            IProcessorContext context, 
            [ContextGet(ProcessorContextKeys.MessageParameters)] IDictionary<string, string> parameters)
        {
            var resourceId = parameters[ProcessorContextKeys.ResourceId];
            context.Set(resourceId, ProcessorContextKeys.ResourceId);
        }
    }

    [ContextSet(typeof(Int32), ProcessorContextKeys.ResourceId)]
    public class Int32ResourceIdProcessingStep : MessageProcessingStep
    {
        public Int32ResourceIdProcessingStep(
            IProcessorContext context, 
            [ContextGet(ProcessorContextKeys.MessageParameters)] IDictionary<string, string> parameters)
        {
            var resourceId = parameters[ProcessorContextKeys.ResourceId];
            var id = int.Parse(resourceId);
            context.Set(id, ProcessorContextKeys.ResourceId);
        }
    }
}