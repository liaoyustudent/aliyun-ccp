// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.ccp.ccpclient.models;

import com.aliyun.tea.*;

/**
 * 获取download url response
 */
public class OSSGetDownloadUrlResponse extends TeaModel {
    @NameInMap("expiration")
    public String expiration;

    @NameInMap("method")
    public String method;

    @NameInMap("url")
    public String url;

    public static OSSGetDownloadUrlResponse build(java.util.Map<String, ?> map) throws Exception {
        OSSGetDownloadUrlResponse self = new OSSGetDownloadUrlResponse();
        return TeaModel.build(map, self);
    }

}
