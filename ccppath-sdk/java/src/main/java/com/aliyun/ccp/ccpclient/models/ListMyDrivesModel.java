// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.ccp.ccpclient.models;

import com.aliyun.tea.*;

public class ListMyDrivesModel extends TeaModel {
    @NameInMap("headers")
    public java.util.Map<String, String> headers;

    @NameInMap("body")
    @Validation(required = true)
    public ListDriveResponse body;

    public static ListMyDrivesModel build(java.util.Map<String, ?> map) throws Exception {
        ListMyDrivesModel self = new ListMyDrivesModel();
        return TeaModel.build(map, self);
    }

}
