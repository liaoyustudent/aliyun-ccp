// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.ccp.ccpclient.models;

import com.aliyun.tea.*;

/**
 * 
 */
public class GetUserAccessTokenRequest extends TeaModel {
    @NameInMap("role")
    public String role;

    @NameInMap("user_id")
    @Validation(required = true)
    public String userId;

    public static GetUserAccessTokenRequest build(java.util.Map<String, ?> map) throws Exception {
        GetUserAccessTokenRequest self = new GetUserAccessTokenRequest();
        return TeaModel.build(map, self);
    }

}
