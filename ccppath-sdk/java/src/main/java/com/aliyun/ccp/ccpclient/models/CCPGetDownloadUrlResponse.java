// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.ccp.ccpclient.models;

import com.aliyun.tea.*;

/**
 * 获取download url response
 */
public class CCPGetDownloadUrlResponse extends TeaModel {
    @NameInMap("expiration")
    public String expiration;

    @NameInMap("method")
    public String method;

    @NameInMap("size")
    public Long size;

    @NameInMap("streams_url")
    public java.util.Map<String, Object> streamsUrl;

    @NameInMap("url")
    public String url;

    public static CCPGetDownloadUrlResponse build(java.util.Map<String, ?> map) throws Exception {
        CCPGetDownloadUrlResponse self = new CCPGetDownloadUrlResponse();
        return TeaModel.build(map, self);
    }

}
