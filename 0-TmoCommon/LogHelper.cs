using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using TmoCommon;

public class LogHelper
{
    public static ILog Log = ConfigHelper.GetConfigBool("WriteLog") ? InnerClass._log : InnerClass._nullLog;

    static class InnerClass
    {
        static InnerClass()
        {
        }

        internal static ILog _log = CreateLog();
        internal static ILog _nullLog = new NullLog();
    }

    public static ILog CreateLog()
    {
        PatternLayout patternLayout = new PatternLayout {ConversionPattern = "[%level|%date|%message|%location]%newline"};
        patternLayout.ActivateOptions();

        PatternLayout infoPatternLayout = new PatternLayout {ConversionPattern = "[%level|%date|%message]%newline"};
        infoPatternLayout.ActivateOptions();

        RollingFileAppender roller = new RollingFileAppender
        {
            Layout = patternLayout,
            AppendToFile = true,
            File = "Log/",
            StaticLogFileName = false,
            DatePattern = "yyyyMMdd.LOG",
            RollingStyle = RollingFileAppender.RollingMode.Date,
            MaximumFileSize = "10MB",
            MaxSizeRollBackups = 10,
            Encoding = Encoding.UTF8,
            LockingModel = new FileAppender.MinimalLock()
        };
        roller.AddFilter(new LevelRangeFilter() {LevelMin = Level.Warn, LevelMax = Level.Emergency});
        roller.ActivateOptions();

        RollingFileAppender infoRoller = new RollingFileAppender
        {
            Layout = infoPatternLayout,
            AppendToFile = true,
            File = "Log/",
            StaticLogFileName = false,
            DatePattern = "yyyyMMdd.LOG",
            RollingStyle = RollingFileAppender.RollingMode.Date,
            MaximumFileSize = "10MB",
            MaxSizeRollBackups = 10,
            Encoding = Encoding.UTF8,
            LockingModel = new FileAppender.MinimalLock()
        };
        infoRoller.AddFilter(new LevelRangeFilter() {LevelMin = Level.Verbose, LevelMax = Level.Notice});
        infoRoller.ActivateOptions();

        Hierarchy hierarchy = (Hierarchy) LogManager.GetRepository();
        hierarchy.Name = "log";
        hierarchy.Root.AddAppender(roller);
        hierarchy.Root.AddAppender(infoRoller);
        hierarchy.Root.Level = Level.All;
        hierarchy.Configured = true;

        return LogManager.GetLogger("logger");
    }
}