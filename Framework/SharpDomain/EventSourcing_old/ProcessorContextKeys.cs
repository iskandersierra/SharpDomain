namespace SharpDomain.EventSourcing
{
    public static class ProcessorContextKeys
    {
        public const string Default = "";
        /// <summary>
        /// For sending resource ids from commands to events
        /// </summary>
        public const string ResourceId = "ResourceId";
        /// <summary>
        /// For sending user ids
        /// </summary>
        public const string UserId = "UserId";
        /// <summary>
        /// For sending user names
        /// </summary>
        public const string UserName = "UserName";
        /// <summary>
        /// For sending command ids
        /// </summary>
        public const string CommandId = "CommandId";
        /// <summary>
        /// For sending command names
        /// </summary>
        public const string CommandName = "CommandName";
        /// <summary>
        /// For sending environment ids
        /// </summary>
        public const string EnvironmentId = "EnvironmentId";
        /// <summary>
        /// For sending local time
        /// </summary>
        public const string LocalTime = "LocalTime";
        /// <summary>
        /// For sending UTC time
        /// </summary>
        public const string UTCTime = "UTCTime";
        /// <summary>
        /// For general-purpose parameters
        /// </summary>
        public const string MessageParameters = "MessageParameters";
    }
}