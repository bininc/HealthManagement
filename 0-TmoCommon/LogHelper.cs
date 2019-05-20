using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using TmoCommon;

public class LogHelper
{
    const string LOG_PATTERN = "[%level|%date|%appdomain->%message]%newline";
    /// <summary>
    /// 写日志锁
    /// </summary>
    private static readonly object logLock = new object();

    public static ILog logger = null;
    public static bool WriteLog(object message, Exception ex = null, Level level = Level.Info)
    {
        lock (logLock)
        {
            try
            {
                bool writeLog = ConfigHelper.GetConfigBool("WriteLog"); //读取开关
                if (!writeLog) return true;
                if (message == null || string.IsNullOrWhiteSpace(message.ToString())) return false; //不写空日志
                if (logger == null)
                {
                    PatternLayout patternLayout = new PatternLayout();
                    patternLayout.ConversionPattern = LOG_PATTERN;
                    patternLayout.ActivateOptions();

                    TraceAppender tracer = new TraceAppender();
                    tracer.Layout = patternLayout;
                    tracer.ActivateOptions();

                    RollingFileAppender roller = new RollingFileAppender();
                    roller.Layout = patternLayout;
                    roller.AppendToFile = true;
                    roller.File = "Log/";
                    roller.StaticLogFileName = false;
                    roller.DatePattern = "yyyyMMdd.LOG";
                    roller.RollingStyle = RollingFileAppender.RollingMode.Date;
                    roller.MaximumFileSize = "10MB";
                    roller.MaxSizeRollBackups = 10;
                    //roller.ImmediateFlush = true;
                    roller.Encoding = Encoding.UTF8;
                    roller.LockingModel = new FileAppender.MinimalLock();
                    roller.ActivateOptions();

                    Hierarchy hierarchy = (Hierarchy) LogManager.GetRepository();
                    hierarchy.Name = "log";
                    hierarchy.Root.AddAppender(tracer);
                    hierarchy.Root.AddAppender(roller);
                    hierarchy.Root.Level = log4net.Core.Level.All;
                    hierarchy.Configured = true;

                    logger = LogManager.GetLogger("log");
                }

                if (ex == null)
                {
                    switch (level)
                    {
                        case Level.Debug:
                            logger.Debug(message);
                            break;
                        case Level.Error:
                            logger.Error(message);
                            break;
                        case Level.Fatal:
                            logger.Fatal(message);
                            break;
                        case Level.Info:
                            logger.Info(message);
                            break;
                        case Level.Warn:
                            logger.Warn(message);
                            break;
                    }
                }
                else
                {
                    switch (level)
                    {
                        case Level.Debug:
                            logger.Debug(message, ex);
                            break;
                        case Level.Error:
                            logger.Error(message, ex);
                            break;
                        case Level.Fatal:
                            logger.Fatal(message, ex);
                            break;
                        case Level.Info:
                            logger.Info(message, ex);
                            break;
                        case Level.Warn:
                            logger.Warn(message, ex);
                            break;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                
            }
        }
    }

    public enum Level
    {
        /// <summary>
        /// 调试级别
        /// </summary>
        Debug,

        /// <summary>
        /// 错误级别
        /// </summary>
        Error,

        /// <summary>
        /// 致命错误级别
        /// </summary>
        Fatal,

        /// <summary>
        /// 一般级别
        /// </summary>
        Info,

        /// <summary>
        /// 警告级别
        /// </summary>
        Warn
    }

    public static bool WriteInfo(object message)
    {
        return WriteLog(message, null, Level.Info);
    }

    public static bool WriteDebug(object message, Exception ex = null)
    {
        return WriteLog(message, ex, Level.Debug);
    }

    public static bool WriteWarn(object message, Exception ex = null)
    {
        return WriteLog(message, ex, Level.Warn);
    }

    public static bool WriteError(Exception ex, object message = null)
    {
        if (message == null || string.IsNullOrWhiteSpace(message.ToString()))
        {
            if (ex != null)
                message = ex.Message;
        }
        return WriteLog(message, ex, Level.Error);
    }

    public static bool WriteFatal(Exception ex, object message = null)
    {
        if (message == null || string.IsNullOrWhiteSpace(message.ToString()))
        {
            if (ex != null)
                message = ex.Message;
        }
        return WriteLog(message, ex, Level.Fatal);
    }
}

