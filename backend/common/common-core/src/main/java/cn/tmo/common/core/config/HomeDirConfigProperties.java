package cn.tmo.common.core.config;

import lombok.Data;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.cloud.context.config.annotation.RefreshScope;
import org.springframework.context.annotation.Configuration;

/**
 * 文件存放目录配置
 */
@Data
@RefreshScope
@Configuration
@ConfigurationProperties(prefix = "home-dir")
public class HomeDirConfigProperties {
    private String windows = "windows";
    private String linux = "linux";
}
