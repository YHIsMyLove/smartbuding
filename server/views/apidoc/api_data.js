define({ "api": [
  {
    "type": "POST",
    "url": "/api/SetDoorIOInfo",
    "title": "",
    "name": "________",
    "group": "DoorIO",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "UserID",
            "description": "<p>用户登录ID.</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "DoorID",
            "description": "<p>门禁ID.</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "IOTime",
            "description": "<p>进出时间.</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "IO",
            "description": "<p>进/出.</p>"
          }
        ]
      }
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserNotFound",
            "description": "<p>code:-1 用户没有找到</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "DoorNotFound",
            "description": "<p>code:-2 门禁设备没找到</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "NoAuth",
            "description": "<p>code:-3 没有权限</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "Other",
            "description": "<p>code:-4 未知错误</p>"
          }
        ]
      }
    },
    "version": "0.0.0",
    "filename": "mongodb/business/DoorIO_Business.js",
    "groupTitle": "DoorIO"
  },
  {
    "type": "POST",
    "url": "/api/SetMeetingReaded",
    "title": "",
    "name": "__________",
    "group": "Meeting",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "MeetingID",
            "description": "<p>会议ID.</p>"
          }
        ]
      }
    },
    "version": "0.0.0",
    "filename": "mongodb/business/Meeting_Business.js",
    "groupTitle": "Meeting"
  },
  {
    "type": "POST",
    "url": "/api/CreateUser",
    "title": "",
    "name": "CreateUser",
    "group": "User",
    "description": "<p>api to create user info</p>",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "UserID",
            "description": "<p>用户登录ID.</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "UserName",
            "description": "<p>用户登录ID.</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "UserSex",
            "description": "<p>用户登录ID.</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "UserAge",
            "description": "<p>用户登录ID.</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "UserPhoneNum",
            "description": "<p>用户登录ID.</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "UserEmail",
            "description": "<p>用户登录ID.</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "UserCardID",
            "description": "<p>用户登录ID.</p>"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "OK",
            "description": "<p>保存成功</p>"
          }
        ]
      }
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "Other",
            "description": "<p>code:-4 未知错误</p>"
          }
        ]
      }
    },
    "version": "0.0.0",
    "filename": "mongodb/business/Users_Business.js",
    "groupTitle": "User"
  },
  {
    "type": "POST",
    "url": "/api/Login",
    "title": "",
    "name": "__",
    "group": "User",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "UserID",
            "description": "<p>用户登录ID.</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "UserPwd",
            "description": "<p>用户登录密码.</p>"
          }
        ]
      }
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserNotFound",
            "description": "<p>code:-1 用户没有找到</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "PassWorldErr",
            "description": "<p>code:-2 密码错误</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "NoAuth",
            "description": "<p>code:-3 没有权限</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "Other",
            "description": "<p>code:-4 未知错误</p>"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "UserID",
            "description": "<p>用户ID</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "SessionID",
            "description": "<p>用户Session</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "YSToken",
            "description": "<p>萤石的Token</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "{\n \"UserID\":\"xx\"\n \"SessionID\":\"xx\"\n \"YSToken\":\"xx\"\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "mongodb/business/Users_Business.js",
    "groupTitle": "User"
  }
] });
