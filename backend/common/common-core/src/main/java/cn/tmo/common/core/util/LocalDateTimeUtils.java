package cn.tmo.common.core.util;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeFormatterBuilder;
import java.time.temporal.ChronoField;

/**
 * LocalDateTime时间工具
 */
public class LocalDateTimeUtils {

    public static final String YYYY = "yyyy";
    public static final String YYYYMM = "yyyyMM";
    public static final String YYYYMMDD = "yyyyMMdd";
    public static final String YYYYMMDDHH = "yyyyMMddHH";
    public static final String YYYYMMDDHHMM = "yyyyMMddHHmm";
    public static final String YYYYMMDDHHMMSS = "yyyyMMddHHmmss";
    public static final String YYYY_MM = "yyyy-MM";
    public static final String YYYY_MM_DD = "yyyy-MM-dd";
    public static final String YYYY_MM_DD_HH = "yyyy-MM-dd HH";
    public static final String YYYY_MM_DD_HH_MM = "yyyy-MM-dd HH:mm";
    public static final String YYYY_MM_DD_HH_MM_SS = "yyyy-MM-dd HH:mm:ss";

    private static final String BASE_TIME_FORMAT = "[yyyyMMddHHmmss][yyyyMMddHHmm][yyyyMMddHH][yyyyMMdd][yyyyMM][yyyy][[-][/][.]MM][[-][/][.]dd][ ][HH][[:][.]mm][[:][.]ss][[:][.]SSS]";

    private static DateTimeFormatter getDateTimeFormatterByPattern(String pattern) {
        return new DateTimeFormatterBuilder()
                .appendPattern(pattern)
                .parseDefaulting(ChronoField.YEAR_OF_ERA, LocalDateTime.now().getYear())
                .parseDefaulting(ChronoField.MONTH_OF_YEAR, LocalDateTime.now().getMonthValue())
                .parseDefaulting(ChronoField.DAY_OF_MONTH, 1)
                .parseDefaulting(ChronoField.HOUR_OF_DAY, 0)
                .parseDefaulting(ChronoField.MINUTE_OF_HOUR, 0)
                .parseDefaulting(ChronoField.SECOND_OF_MINUTE, 0)
                .parseDefaulting(ChronoField.NANO_OF_SECOND, 0)
                .toFormatter();
    }

    /**
     * 【推荐】解析常用时间字符串，支持,并不局限于以下形式：
     * [yyyy][yyyy-MM][yyyy-MM-dd][yyyy-MM-dd HH][yyyy-MM-dd HH:mm][yyyy-MM-dd HH:mm:ss][yyyy-MM-dd HH:mm:ss:SSS]
     * [yyyy][yyyy/MM][yyyy/MM/dd][yyyy/MM/dd HH][yyyy/MM/dd HH:mm][yyyy/MM/dd HH:mm:ss][yyyy/MM/dd HH:mm:ss:SSS]
     * [yyyy][yyyy.MM][yyyy.MM.dd][yyyy.MM.dd HH][yyyy.MM.dd HH.mm][yyyy.MM.dd HH.mm.ss][yyyy.MM.dd HH.mm.ss.SSS]
     * [yyyy][yyyyMM][yyyyMMdd][yyyyMMddHH][yyyyMMddHHmm][yyyyMMddHHmmss]
     * [MM-dd]
     * 不支持yyyyMMddHHmmssSSS，因为本身DateTimeFormatter.ofPattern("yyyyMMddHHmmssSSS")就不支持这个形式
     *
     * @param timeString 时间字符串
     * @return
     */
    public static LocalDateTime parse(String timeString) {
        return LocalDateTime.parse(timeString, getDateTimeFormatterByPattern(BASE_TIME_FORMAT));
    }

    /**
     * 根据传进来的pattern返回LocalDateTime，自动补齐
     *
     * @param timeString 时间字符串
     * @param pattern    时间格式
     * @return
     */
    public static LocalDateTime parseByPattern(String timeString, String pattern) {
        return LocalDateTime.parse(timeString, getDateTimeFormatterByPattern(pattern));
    }

    /**
     * 将timestamp转为LocalDateTime
     *
     * @param timestamp 时间戳
     * @param isMilli   是否精确到毫秒
     * @return
     */
    public static LocalDateTime timestampToDatetime(long timestamp, boolean isMilli) {
        Instant instant = isMilli ? Instant.ofEpochMilli(timestamp) : Instant.ofEpochSecond(timestamp);
        return LocalDateTime.ofInstant(instant, ZoneId.systemDefault());
    }

    /**
     * 将LocalDateTime转为timestamp
     * @param dateTime
     * @param isMilli
     * @return
     */
    public static long datetimeToTimestamp(LocalDateTime dateTime, boolean isMilli) {
        ZoneId zone = ZoneId.systemDefault();
        if (isMilli)
            return dateTime.atZone(zone).toInstant().toEpochMilli();
        return dateTime.atZone(zone).toEpochSecond();
    }
}
