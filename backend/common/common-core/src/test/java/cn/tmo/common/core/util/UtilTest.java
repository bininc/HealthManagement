package cn.tmo.common.core.util;

import cn.hutool.json.JSONUtil;
import org.junit.jupiter.api.Test;

import java.time.LocalDateTime;

import static org.junit.jupiter.api.Assertions.*;

class UtilTest {

    @Test
    void testLocalDataTimeUtils() {
        long timestamp = LocalDateTimeUtils.datetimeToTimestamp(LocalDateTime.now(), false);
        System.out.println(timestamp);
        LocalDateTime dt = LocalDateTimeUtils.timestampToDatetime(timestamp, false);
        System.out.println(dt);

        timestamp = LocalDateTimeUtils.datetimeToTimestamp(LocalDateTime.now(), true);
        System.out.println(timestamp);
        dt = LocalDateTimeUtils.timestampToDatetime(timestamp, true);
        System.out.println(dt);
    }

    @Test
    void testR() {
        R<String> r = R.ok("message", "msg");
        System.out.println(r);

        r = R.failed("Unknown Error", -1, "Failed");
        String jsonStr = JSONUtil.toJsonStr(r);
        System.out.println(jsonStr);
    }
}