﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Edit file ILog.tt instead
//     Inspired by NLog to make easier at least one addapter
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace SharpDomain.Logging
{
    using System;

    public static class ExtensionsToLog
    {

        #region [ Trace level ]
        
        public static bool GetIsTraceEnabled(this ILog logger)
        {
            return logger.GetIsEnabled(LoggingLevel.Trace);
        }

        public static void Trace<T>(this ILog logger, T value)
        {
            logger.Log<T>(LoggingLevel.Trace, value);
        }

        public static void Trace<T>(this ILog logger, IFormatProvider formatProvider, T value)
        {
            logger.Log<T>(LoggingLevel.Trace, formatProvider, value);
        }

        public static void Trace(this ILog logger, Func<string> messageGenerator)
        {
            logger.Log(LoggingLevel.Trace, messageGenerator);
        }

        public static void Trace(this ILog logger, string message, Exception exception)
        {
            logger.Log(LoggingLevel.Trace, message, exception);
        }

        public static void Trace(this ILog logger, IFormatProvider formatProvider, string message, object arg1)
        {
            logger.Log(LoggingLevel.Trace, formatProvider, message, arg1);
        }

        public static void Trace(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Trace, formatProvider, message, arg1, arg2);
        }

        public static void Trace(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Trace, formatProvider, message, arg1, arg2, arg3);
        }

        public static void Trace(this ILog logger, IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Trace, formatProvider, message, args);
        }

        public static void Trace(this ILog logger, string message)
        {
            logger.Log(LoggingLevel.Trace, message);
        }

        public static void Trace(this ILog logger, string message, object arg1)
        {
            logger.Log(LoggingLevel.Trace, message, arg1);
        }

        public static void Trace(this ILog logger, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Trace, message, arg1, arg2);
        }

        public static void Trace(this ILog logger, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Trace, message, arg1, arg2, arg3);
        }

        public static void Trace(this ILog logger, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Trace, message, args);
        }

        #endregion [ Trace level ]

        #region [ Debug level ]
        
        public static bool GetIsDebugEnabled(this ILog logger)
        {
            return logger.GetIsEnabled(LoggingLevel.Debug);
        }

        public static void Debug<T>(this ILog logger, T value)
        {
            logger.Log<T>(LoggingLevel.Debug, value);
        }

        public static void Debug<T>(this ILog logger, IFormatProvider formatProvider, T value)
        {
            logger.Log<T>(LoggingLevel.Debug, formatProvider, value);
        }

        public static void Debug(this ILog logger, Func<string> messageGenerator)
        {
            logger.Log(LoggingLevel.Debug, messageGenerator);
        }

        public static void Debug(this ILog logger, string message, Exception exception)
        {
            logger.Log(LoggingLevel.Debug, message, exception);
        }

        public static void Debug(this ILog logger, IFormatProvider formatProvider, string message, object arg1)
        {
            logger.Log(LoggingLevel.Debug, formatProvider, message, arg1);
        }

        public static void Debug(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Debug, formatProvider, message, arg1, arg2);
        }

        public static void Debug(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Debug, formatProvider, message, arg1, arg2, arg3);
        }

        public static void Debug(this ILog logger, IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Debug, formatProvider, message, args);
        }

        public static void Debug(this ILog logger, string message)
        {
            logger.Log(LoggingLevel.Debug, message);
        }

        public static void Debug(this ILog logger, string message, object arg1)
        {
            logger.Log(LoggingLevel.Debug, message, arg1);
        }

        public static void Debug(this ILog logger, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Debug, message, arg1, arg2);
        }

        public static void Debug(this ILog logger, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Debug, message, arg1, arg2, arg3);
        }

        public static void Debug(this ILog logger, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Debug, message, args);
        }

        #endregion [ Debug level ]

        #region [ Info level ]
        
        public static bool GetIsInfoEnabled(this ILog logger)
        {
            return logger.GetIsEnabled(LoggingLevel.Info);
        }

        public static void Info<T>(this ILog logger, T value)
        {
            logger.Log<T>(LoggingLevel.Info, value);
        }

        public static void Info<T>(this ILog logger, IFormatProvider formatProvider, T value)
        {
            logger.Log<T>(LoggingLevel.Info, formatProvider, value);
        }

        public static void Info(this ILog logger, Func<string> messageGenerator)
        {
            logger.Log(LoggingLevel.Info, messageGenerator);
        }

        public static void Info(this ILog logger, string message, Exception exception)
        {
            logger.Log(LoggingLevel.Info, message, exception);
        }

        public static void Info(this ILog logger, IFormatProvider formatProvider, string message, object arg1)
        {
            logger.Log(LoggingLevel.Info, formatProvider, message, arg1);
        }

        public static void Info(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Info, formatProvider, message, arg1, arg2);
        }

        public static void Info(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Info, formatProvider, message, arg1, arg2, arg3);
        }

        public static void Info(this ILog logger, IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Info, formatProvider, message, args);
        }

        public static void Info(this ILog logger, string message)
        {
            logger.Log(LoggingLevel.Info, message);
        }

        public static void Info(this ILog logger, string message, object arg1)
        {
            logger.Log(LoggingLevel.Info, message, arg1);
        }

        public static void Info(this ILog logger, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Info, message, arg1, arg2);
        }

        public static void Info(this ILog logger, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Info, message, arg1, arg2, arg3);
        }

        public static void Info(this ILog logger, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Info, message, args);
        }

        #endregion [ Info level ]

        #region [ Warn level ]
        
        public static bool GetIsWarnEnabled(this ILog logger)
        {
            return logger.GetIsEnabled(LoggingLevel.Warn);
        }

        public static void Warn<T>(this ILog logger, T value)
        {
            logger.Log<T>(LoggingLevel.Warn, value);
        }

        public static void Warn<T>(this ILog logger, IFormatProvider formatProvider, T value)
        {
            logger.Log<T>(LoggingLevel.Warn, formatProvider, value);
        }

        public static void Warn(this ILog logger, Func<string> messageGenerator)
        {
            logger.Log(LoggingLevel.Warn, messageGenerator);
        }

        public static void Warn(this ILog logger, string message, Exception exception)
        {
            logger.Log(LoggingLevel.Warn, message, exception);
        }

        public static void Warn(this ILog logger, IFormatProvider formatProvider, string message, object arg1)
        {
            logger.Log(LoggingLevel.Warn, formatProvider, message, arg1);
        }

        public static void Warn(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Warn, formatProvider, message, arg1, arg2);
        }

        public static void Warn(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Warn, formatProvider, message, arg1, arg2, arg3);
        }

        public static void Warn(this ILog logger, IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Warn, formatProvider, message, args);
        }

        public static void Warn(this ILog logger, string message)
        {
            logger.Log(LoggingLevel.Warn, message);
        }

        public static void Warn(this ILog logger, string message, object arg1)
        {
            logger.Log(LoggingLevel.Warn, message, arg1);
        }

        public static void Warn(this ILog logger, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Warn, message, arg1, arg2);
        }

        public static void Warn(this ILog logger, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Warn, message, arg1, arg2, arg3);
        }

        public static void Warn(this ILog logger, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Warn, message, args);
        }

        #endregion [ Warn level ]

        #region [ Error level ]
        
        public static bool GetIsErrorEnabled(this ILog logger)
        {
            return logger.GetIsEnabled(LoggingLevel.Error);
        }

        public static void Error<T>(this ILog logger, T value)
        {
            logger.Log<T>(LoggingLevel.Error, value);
        }

        public static void Error<T>(this ILog logger, IFormatProvider formatProvider, T value)
        {
            logger.Log<T>(LoggingLevel.Error, formatProvider, value);
        }

        public static void Error(this ILog logger, Func<string> messageGenerator)
        {
            logger.Log(LoggingLevel.Error, messageGenerator);
        }

        public static void Error(this ILog logger, string message, Exception exception)
        {
            logger.Log(LoggingLevel.Error, message, exception);
        }

        public static void Error(this ILog logger, IFormatProvider formatProvider, string message, object arg1)
        {
            logger.Log(LoggingLevel.Error, formatProvider, message, arg1);
        }

        public static void Error(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Error, formatProvider, message, arg1, arg2);
        }

        public static void Error(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Error, formatProvider, message, arg1, arg2, arg3);
        }

        public static void Error(this ILog logger, IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Error, formatProvider, message, args);
        }

        public static void Error(this ILog logger, string message)
        {
            logger.Log(LoggingLevel.Error, message);
        }

        public static void Error(this ILog logger, string message, object arg1)
        {
            logger.Log(LoggingLevel.Error, message, arg1);
        }

        public static void Error(this ILog logger, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Error, message, arg1, arg2);
        }

        public static void Error(this ILog logger, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Error, message, arg1, arg2, arg3);
        }

        public static void Error(this ILog logger, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Error, message, args);
        }

        #endregion [ Error level ]

        #region [ Fatal level ]
        
        public static bool GetIsFatalEnabled(this ILog logger)
        {
            return logger.GetIsEnabled(LoggingLevel.Fatal);
        }

        public static void Fatal<T>(this ILog logger, T value)
        {
            logger.Log<T>(LoggingLevel.Fatal, value);
        }

        public static void Fatal<T>(this ILog logger, IFormatProvider formatProvider, T value)
        {
            logger.Log<T>(LoggingLevel.Fatal, formatProvider, value);
        }

        public static void Fatal(this ILog logger, Func<string> messageGenerator)
        {
            logger.Log(LoggingLevel.Fatal, messageGenerator);
        }

        public static void Fatal(this ILog logger, string message, Exception exception)
        {
            logger.Log(LoggingLevel.Fatal, message, exception);
        }

        public static void Fatal(this ILog logger, IFormatProvider formatProvider, string message, object arg1)
        {
            logger.Log(LoggingLevel.Fatal, formatProvider, message, arg1);
        }

        public static void Fatal(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Fatal, formatProvider, message, arg1, arg2);
        }

        public static void Fatal(this ILog logger, IFormatProvider formatProvider, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Fatal, formatProvider, message, arg1, arg2, arg3);
        }

        public static void Fatal(this ILog logger, IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Fatal, formatProvider, message, args);
        }

        public static void Fatal(this ILog logger, string message)
        {
            logger.Log(LoggingLevel.Fatal, message);
        }

        public static void Fatal(this ILog logger, string message, object arg1)
        {
            logger.Log(LoggingLevel.Fatal, message, arg1);
        }

        public static void Fatal(this ILog logger, string message, object arg1, object arg2)
        {
            logger.Log(LoggingLevel.Fatal, message, arg1, arg2);
        }

        public static void Fatal(this ILog logger, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(LoggingLevel.Fatal, message, arg1, arg2, arg3);
        }

        public static void Fatal(this ILog logger, string message, params object[] args)
        {
            logger.Log(LoggingLevel.Fatal, message, args);
        }

        #endregion [ Fatal level ]

    }
}