--- 

1. 区域  
    areaid|areaname  
    --|--  
    01|北京
    02|上海
    03|广州
    04|深圳

2. 项目    

    areaid|projid|projname
    --|--|--
    01|01|XX项目
    02|02|xXX项目
    03|03|xxXX项目

2. 用户        
    userid|username|areaid|deptid|postid|roleids
    --|--|--|--|--|--
    01|test1|01|01|01|01,02,03
    02|test2|02|02|02|01,02
    03|test3|03|03|03|01

3. 部门
    deptid|deptname|areaid
    --|--|--
    01|01部|01
    02|02部|01
    03|01部|02

4. 职位
    postid|postname|areaid
    --|--|--
    01|部门经理|01
    02|造价师|02

5. 角色
    roleid|rolename
    --|--
    01|管理员
    02|操作员
    03|游客

6. 设备
    devid|devname|areaid|devclass
    --|--|--|--
    01|摄像头|01|c
    02|摄像头|02|c
    02|摄像头|01|c
    03|门禁|01|d
    04|门禁|01|d
    05|门禁|03|d

7. 设备权限
    >  operation 操作权限说明 共5位代表[新增,删除,查看,修改,操作] 0:false 1:true 

    authid|authdesc|roleid|devid|operation
    --|--|--|--|--
    01|管理员|01|01|00100
    01|管理员|01|02|00100
    01|管理员|01|03|00100
    01|管理员|01|04|00100
