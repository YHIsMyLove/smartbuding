define({ "api": [
  {
    "type": "POST",
    "url": "/api/Login?UserID",
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
    "filename": "business/Users_Business.js",
    "groupTitle": "User"
  }
] });
