using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.EventSourcing;

namespace SharpDomain.ReadModel
{
    public abstract class AuditableDao
    {
        public string CreatedByUserId { get; set; }
        public string ModifiedByUserId { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }

    public static class ExtensionsToAuditableDao
    {
        public static TDao AsNewAuditable<TDao>(this TDao dao, IProcessorContext context)
            where TDao : AuditableDao
        {
            var userId = context.Get<string>(ProcessorContextKeys.UserId);
            var utcTime = context.Get<DateTime>(ProcessorContextKeys.UTCTime);

            dao.CreatedByUserId = userId;
            dao.CreationDate = utcTime;

            return dao.AsModifiedAuditable(context);
        }

        public static TDao AsModifiedAuditable<TDao>(this TDao dao, IProcessorContext context)
            where TDao : AuditableDao
        {
            var userId = context.Get<string>(ProcessorContextKeys.UserId);
            var utcTime = context.Get<DateTime>(ProcessorContextKeys.UTCTime);

            dao.ModifiedByUserId = userId;
            dao.ModificationDate = utcTime;

            return dao;
        }
    }
}
