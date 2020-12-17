package cn.tmo.common.core.util;

import org.springframework.web.multipart.MultipartFile;

import java.io.*;

import cn.hutool.core.lang.UUID;

/**
 * File工具类
 */
public class FileUtils {

    /**
     * 将MultipartFile转换为File
     *
     * @param mulFile
     * @return
     * @throws IOException
     */
    public static File multipartFileToFile(MultipartFile mulFile) throws IOException {
        InputStream ins = mulFile.getInputStream();
        String filename = mulFile.getOriginalFilename();
        String prefix = getFileNameWithoutExt(filename) + UUID.fastUUID();
        String suffix = "." + getFileNameExt(filename);
        File toFile = File.createTempFile(prefix, suffix);
        OutputStream os = new FileOutputStream(toFile);
        int bytesRead = 0;
        byte[] buffer = new byte[8192];
        while ((bytesRead = ins.read(buffer, 0, buffer.length)) != -1) {
            os.write(buffer, 0, bytesRead);
        }
        os.close();
        ins.close();
        return toFile;
    }

    /**
     * 获取文件扩展名
     *
     * @param filename 文件名
     * @return
     */
    public static String getFileNameExt(String filename) {
        if (null != filename && filename.length() > 0) {
            int dotIndex = filename.indexOf('.');
            if (dotIndex > -1 && dotIndex < (filename.length() - 1)) {
                return filename.substring(dotIndex + 1);
            }
        }
        return filename;
    }

    /**
     * 获取不带扩展名的文件名
     *
     * @param filename 文件名
     * @return
     */
    public static String getFileNameWithoutExt(String filename) {
        if (null != filename && filename.length() > 0) {
            int dotIndex = filename.indexOf('.');
            if (dotIndex > -1 && dotIndex < filename.length()) {
                return filename.substring(0, dotIndex);
            }
        }
        return filename;
    }
}
