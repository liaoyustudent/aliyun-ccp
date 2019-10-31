// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.ccp.ossclient.models;

import com.aliyun.tea.*;

public class BaseCompleteFileRequest extends TeaModel {
    @NameInMap("drive_id")
    @Validation(pattern = "[0-9]+", maxLength = 0)
    public String driveId;

    @NameInMap("part_info_list")
    @Validation(pattern = "", maxLength = 0)
    public UploadPartInfo[] partInfoList;

    @NameInMap("upload_id")
    @Validation(pattern = "", maxLength = 0)
    public String uploadId;

}

