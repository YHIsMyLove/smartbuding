webpackJsonp([0,9],{154:function(e,t,i){e.exports={default:i(433),__esModule:!0}},420:function(e,t,i){"use strict";function a(e){return e&&e.__esModule?e:{default:e}}Object.defineProperty(t,"__esModule",{value:!0});var l=i(154),n=a(l),s=i(24),o=(a(s),i(22)),r=a(o),c=i(21),u=a(c),d=i(2),p=(a(d),i(782)),f=a(p);t.default={data:function(){return{activeCollapseNames:"1",activeName:"BusinessManager",formInline:{user:""},tableData:[],tableDataLength:0,listLoading:!1,currentPage:1,currentPageSize:10,options:[{value:"String",label:"字符类型"},{value:"Number",label:"数值类型"},{value:"Date",label:"日期类型"}],options_Fields:[{key:"none",value:"空"}],currentTabelName:"",EditLine:[{Title:"",Desc:"",Type:"String",Link:"空"}],BusinessTitle:"",BusinessTitleDesc:"",currentField:"",currentFieldNames:[]}},created:function(){this.getSysFieldList(),this.getAllFieldList()},methods:{handleDel:function(e){var t=this;this.$confirm("确认删除该记录吗?","提示",{type:"warning"}).then(function(){t.listLoading=!0,r.default.start(),u.default.post("/api/SysField/delete",{id:e._id}).then(function(e){e.data.success?(t.$message({message:"删除成功",type:"success"}),t.listLoading=!1,r.default.done(),t.getSysFieldList()):t.$message({message:"删除失败",type:"error"})})}).catch(function(){})},handleAdd:function(){this.activeName="BusinessInfo",this.BusinessTitle="",this.BusinessTitleDesc=""},getSysFieldList:function(){try{var e=this,t={limit:e.$data.currentPageSize,page:e.$data.currentPage};u.default.get("/api/SysField/",{params:t}).then(function(t){e.$data.tableData=t.data.data,e.$data.tableDataLength=t.data.meta.count}),u.default.get("/api/SysField/AllFieldName").then(function(t){e.$data.currentFieldNames=t.data.data})}catch(e){console.log(e)}},getAllFieldList:function(){try{u.default.get("/api/SysField/AllFieldName").then(function(e){var t=this;this.currentFieldNames=e.data.data,this.options_Fields=[{key:"none",value:"空"}],e.data.data.forEach(function(e){t.options_Fields.push({key:e.SysTabName,value:e.SysTabName})})}.bind(this))}catch(e){console.log(e)}},handleSizeChange:function(e){this.$data.currentPageSize=e,this.getSysFieldList()},handleCurrentChange:function(e){this.$data.currentPage=e,this.getSysFieldList()},handleEdit:function(e){var t=this;try{this.activeName="BusinessInfo",this.BusinessTitle=e.SysTabName,this.BusinessTitleDesc=e.SysTabDesc,this.currentField=e;var i=JSON.parse(e.SysFieldInfo);this.EditLine.splice(0,this.EditLine.length),(0,n.default)(i).forEach(function(e){t.EditLine.push({Title:e,Desc:i[e].desc,Type:i[e].type,Link:i[e].link})})}catch(e){console.log(e)}},onSubmit:function(){var e=this;if(""===this.BusinessTitle)return void this.$message({message:"请输入业务信息名称",type:"error"});if(this.EditLine.filter(function(e){return""===e.Title}).length>0)return void this.$message({message:"请输入业务信息标题",type:"error"});var t="{";this.EditLine.forEach(function(e){t+='"'+e.Title+'":{"type":"'+e.Type+'","desc":"'+e.Desc+'","link":"'+e.Link+'"},'}),t=t.substr(0,t.length-1)+"}",u.default.post("/api/SysField/create",{SysTabName:this.BusinessTitle,SysTabDesc:this.BusinessTitleDesc,SysFieldInfo:t}).then(function(t,i){return i?void e.$message({message:"保存失败："+i,type:"error"}):void e.$message({message:"保存成功",type:"success"})}),this.activeName="BusinessManager",this.getSysFieldList()},onAddLine:function(){this.EditLine.filter(function(e){return""===e.Title}).length>0?this.$message({message:"请输入业务信息标题",type:"error"}):this.EditLine.push({Title:"",Type:"String"})},onCancel:function(){this.activeName="BusinessManager",this.EditLine.splice(1,this.EditLine.length-1)},handleClick:function(){},onDelLine:function(e){1!==this.EditLine.length&&this.EditLine.splice(e,1)},newCustomTable:function(){try{this.activeName="BusinessInfo",this.activeCollapseNames="1",this.BusinessTitle="",this.BusinessTitleDesc="",this.EditLine=[{Title:"",Desc:"",Type:"String",Link:"空"}]}catch(e){console.log(e)}}},components:{SysFieldManager_Child:f.default}}},421:function(e,t,i){"use strict";function a(e){return e&&e.__esModule?e:{default:e}}Object.defineProperty(t,"__esModule",{value:!0});var l=i(154),n=a(l),s=i(24),o=(a(s),i(22)),r=(a(o),i(21)),c=a(r),u=i(2);a(u);t.default={data:function(){return{EditLine:[{Title:"",Desc:"",Type:"String",Link:"",Value:"",ItemLinks:[]}],listLoading:!1,editFormVisible:!1,tableData:[],tableDataLength:0,list:[],loading:!1}},props:["Field","TabelName","TableDesc"],methods:{getFieldInfo:function(){var e=this;try{if(!this.Field)return;var t=JSON.parse(this.Field.SysFieldInfo);this.EditLine.splice(0,this.EditLine.length),(0,n.default)(t).forEach(function(i){var a={Title:i,Desc:t[i].desc,Type:t[i].type,Link:t[i].link};if(e.EditLine.push(a),"空"!==t[i].link)switch(t[i].link){case"用户表":break;default:c.default.get("/api/SysTable?SysFieldID="+t[i].link).then(function(e){a.ItemLinks=e.data.data,console.log(a)}.bind(a))}})}catch(e){console.log(e)}},getTabelData:function(){var e=this,t={limit:e.$data.currentPageSize,page:e.$data.currentPage,SysFieldID:e.TabelName};c.default.get("/api/SysTable/",{params:t}).then(function(t){e.$data.tableData=t.data.data,e.$data.tableDataLength=t.data.meta.count})},handleEdit:function(e){},handleDel:function(e){},newLine:function(){this.editFormVisible=!0},cancelNewline:function(){this.editFormVisible=!1},postNewline:function(){var e=this,t='{"SysFieldID":"'+this.TabelName+'",',i=0;this.EditLine.forEach(function(e){t+='"item'+i+'":"'+e.Value+'",',i++}),t=t.substr(0,t.length-1)+"}",console.log("保存"+t),c.default.post("/api/SysTable/create",JSON.parse(t)).then(function(t,i){return i?void e.$message({message:"保存失败："+i,type:"error"}):t.data.success?void e.$message({message:"保存成功",type:"success"}):void e.$message({message:"保存失败："+t.data.msg,type:"error"})}),this.editFormVisible=!1,this.getTabelData(),this.getFieldInfo()}},watch:{Field:function(e,t){this.getFieldInfo()},TabelName:function(e,t){this.getTabelData()}}}},433:function(e,t,i){i(458),e.exports=i(39).Object.keys},451:function(e,t,i){var a=i(67),l=i(39),n=i(56);e.exports=function(e,t){var i=(l.Object||{})[e]||Object[e],s={};s[e]=t(i),a(a.S+a.F*n(function(){i(1)}),"Object",s)}},458:function(e,t,i){var a=i(113),l=i(69);i(451)("keys",function(){return function(e){return l(a(e))}})},466:function(e,t,i){t=e.exports=i(31)(),t.push([e.id,".toolbar .el-form-item[data-v-00560db5]{margin-bottom:10px}.toolbar[data-v-00560db5]{background:#fff;padding-top:10px}","",{version:3,sources:["/./public/src/components/nav1/SysFieldManager.vue"],names:[],mappings:"AACA,wCAAwC,kBAAkB,CACzD,AACD,0BAA0B,gBAAgB,gBAAgB,CACzD",file:"SysFieldManager.vue",sourcesContent:["\n.toolbar .el-form-item[data-v-00560db5]{margin-bottom:10px\n}\n.toolbar[data-v-00560db5]{background:#fff;padding-top:10px\n}\n"],sourceRoot:"webpack://"}])},475:function(e,t,i){t=e.exports=i(31)(),t.push([e.id,"","",{version:3,sources:[],names:[],mappings:"",file:"SysFieldManager_Child.vue",sourceRoot:"webpack://"}])},751:function(e,t,i){var a=i(466);"string"==typeof a&&(a=[[e.id,a,""]]);i(33)(a,{});a.locals&&(e.exports=a.locals)},764:function(e,t,i){var a=i(475);"string"==typeof a&&(a=[[e.id,a,""]]);i(33)(a,{});a.locals&&(e.exports=a.locals)},781:function(e,t,i){i(751);var a=i(19)(i(420),i(784),"data-v-00560db5",null);e.exports=a.exports},782:function(e,t,i){i(764);var a=i(19)(i(421),i(797),"data-v-f9ccb51c",null);e.exports=a.exports},784:function(e,t){e.exports={render:function(){var e=this,t=e.$createElement,i=e._self._c||t;return i("el-tabs",{staticStyle:{width:"100%"},on:{"tab-click":e.handleClick},model:{value:e.activeName,callback:function(t){e.activeName=t},expression:"activeName"}},[i("el-tab-pane",{attrs:{name:"BusinessManager",label:"自定义表管理"}},[i("section",[i("el-col",{staticClass:"toolbar",attrs:{span:24}},[i("el-form",{staticClass:"demo-form-inline",attrs:{inline:!0,model:e.formInline}},[i("el-form-item",[i("el-input",{attrs:{placeholder:"表名"},model:{value:e.formInline.user,callback:function(t){e.$set(e.formInline,"user",t)},expression:"formInline.user"}})],1),e._v(" "),i("el-form-item",[i("el-button",{on:{click:e.getSysFieldList}},[e._v("查询")]),e._v(" "),i("el-button",{on:{click:e.newCustomTable}},[e._v("新增自定义表")])],1)],1)],1),e._v(" "),[i("el-table",{directives:[{name:"loading",rawName:"v-loading",value:e.listLoading,expression:"listLoading"}],staticStyle:{width:"100%",height:"100%"},attrs:{border:"",fit:"",stripe:"",data:e.tableData,"highlight-current-row":""}},[i("el-table-column",{attrs:{label:"编号",type:"index",width:"85"}}),e._v(" "),i("el-table-column",{attrs:{prop:"SysTabName",label:"表名"}}),e._v(" "),i("el-table-column",{attrs:{prop:"SysTabDesc",label:"描述"}}),e._v(" "),i("el-table-column",{attrs:{prop:"SysFieldInfo",label:"自定义字段"}}),e._v(" "),i("el-table-column",{attrs:{label:"操作",width:"150"},scopedSlots:e._u([{key:"default",fn:function(t){return[i("el-button",{attrs:{type:"text",size:"small"},on:{click:function(i){e.handleEdit(t.row)}}},[e._v("编辑")]),e._v(" "),i("el-button",{attrs:{type:"text",size:"small"},on:{click:function(i){e.handleDel(t.row)}}},[e._v("删除")])]}}])})],1)],e._v(" "),i("el-col",{staticClass:"toolbar",staticStyle:{"padding-bottom":"10px"},attrs:{span:24}},[i("el-pagination",{staticStyle:{float:"right"},attrs:{"current-page":e.currentPage,"page-sizes":[10,20,30,40],"page-size":e.currentPageSize,layout:"total, sizes, prev, pager, next, jumper",total:e.tableDataLength},on:{"size-change":e.handleSizeChange,"current-change":e.handleCurrentChange}})],1)],2)]),e._v(" "),i("el-tab-pane",{attrs:{name:"BusinessInfo",label:"自定义字段管理"}},[i("el-collapse",{model:{value:e.activeCollapseNames,callback:function(t){e.activeCollapseNames=t},expression:"activeCollapseNames"}},[i("el-collapse-item",{attrs:{title:"表结构管理",name:"1"}},[i("el-form",{attrs:{"label-width":"80px"}},[i("el-form-item",{attrs:{label:"表名称"}},[i("el-input",{model:{value:e.BusinessTitle,callback:function(t){e.BusinessTitle=t},expression:"BusinessTitle"}})],1),e._v(" "),i("el-form-item",{attrs:{label:"表描述"}},[i("el-input",{model:{value:e.BusinessTitleDesc,callback:function(t){e.BusinessTitleDesc=t},expression:"BusinessTitleDesc"}})],1),e._v(" "),e._l(e.EditLine,function(t,a){return i("el-form-item",{key:a,attrs:{label:"字段信息"}},[i("el-col",{attrs:{span:7}},[i("el-input",{attrs:{placeholder:"字段名称"},model:{value:t.Title,callback:function(i){e.$set(t,"Title",i)},expression:"item.Title"}})],1),e._v(" "),i("el-col",{attrs:{span:7}},[i("el-input",{attrs:{placeholder:"字段描述"},model:{value:t.Desc,callback:function(i){e.$set(t,"Desc",i)},expression:"item.Desc"}})],1),e._v(" "),i("el-col",{attrs:{span:4}},[i("el-select",{attrs:{placeholder:"关联字段"},model:{value:t.Link,callback:function(i){e.$set(t,"Link",i)},expression:"item.Link"}},e._l(e.options_Fields,function(e){return i("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})}))],1),e._v(" "),i("el-col",{attrs:{span:4}},[i("el-select",{attrs:{placeholder:"字段类型"},model:{value:t.Type,callback:function(i){e.$set(t,"Type",i)},expression:"item.Type"}},e._l(e.options,function(e){return i("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})}))],1),e._v(" "),i("el-col",{attrs:{span:2}},[i("el-button",{attrs:{type:"danger"},on:{click:function(t){e.onDelLine(a)}}},[e._v("删除")])],1)],1)}),e._v(" "),i("el-form-item",[i("el-button",{attrs:{type:"primary"},on:{click:e.onAddLine}},[e._v("新增行")]),e._v(" "),i("el-button",{attrs:{type:"primary"},on:{click:e.onSubmit}},[e._v("提交")]),e._v(" "),i("el-button",{on:{click:e.onCancel}},[e._v("取消")])],1)],2)],1),e._v(" "),i("el-collapse-item",{attrs:{title:"表内容管理",name:"2"}},[i("SysFieldManager_Child",{attrs:{TabelName:e.BusinessTitle,TableDesc:e.BusinessTitleDesc,Field:e.currentField}})],1)],1)],1)],1)},staticRenderFns:[]}},797:function(e,t){e.exports={render:function(){var e=this,t=e.$createElement,i=e._self._c||t;return i("el-col",{staticClass:"toolbar",attrs:{span:24}},[i("el-form",{staticClass:"demo-form-inline"},[i("el-button",{attrs:{type:"primary"},on:{click:e.newLine}},[e._v("新增行")]),e._v(" "),i("el-button",{attrs:{type:"primary"},on:{click:e.getTabelData}},[e._v("刷新")])],1),e._v(" "),i("el-tabs",{staticStyle:{width:"100%"}},[i("el-table",{directives:[{name:"loading",rawName:"v-loading",value:e.listLoading,expression:"listLoading"}],staticStyle:{width:"100%",height:"100%"},attrs:{border:"",fit:"",stripe:"",data:e.tableData,"highlight-current-row":""}},[e._l(e.EditLine,function(e,t){return i("div",{key:t,staticStyle:{width:"100%",height:"100%"}},[i("el-table-column",{attrs:{prop:e.Title,label:e.Title}})],1)}),e._v(" "),i("el-table-column",{attrs:{label:"操作",width:"150"},scopedSlots:e._u([{key:"default",fn:function(t){return[i("el-button",{attrs:{type:"text",size:"small"},on:{click:function(i){e.handleEdit(t.row)}}},[e._v("编辑")]),e._v(" "),i("el-button",{attrs:{type:"text",size:"small"},on:{click:function(i){e.handleDel(t.row)}}},[e._v("删除")])]}}])})],2)],1),e._v(" "),i("el-dialog",{attrs:{title:"新增行"},model:{value:e.editFormVisible,callback:function(t){e.editFormVisible=t},expression:"editFormVisible"}},[e._l(e.EditLine,function(t,a){return i("div",{key:a},[i("el-col",{attrs:{span:4}},[e._v("\n                "+e._s(t.Title)+"\n            ")]),e._v(" "),"空"==t.Link?i("el-col",{attrs:{span:20}},[i("el-input",{model:{value:t.Value,callback:function(i){e.$set(t,"Value",i)},expression:"item.Value"}})],1):i("el-col",{attrs:{span:20}},[i("el-select",{staticStyle:{width:"100%"},attrs:{filterable:"",placeholder:"请输入关键词",loading:e.loading},model:{value:t.Value,callback:function(i){e.$set(t,"Value",i)},expression:"item.Value"}},e._l(t.ItemLinks,function(t){return i("el-option",{key:t._id,attrs:{label:t._id,value:t._id}},e._l(Object.keys(t),function(a,l){return i("div",{key:l},["SysFieldID"!=a&&"_id"!=a&&l%2==0?i("span",{staticStyle:{float:"left"}},[e._v(e._s(t[a]))]):"SysFieldID"!=a&&"_id"!=a&&l%2!=0?i("span",{staticStyle:{float:"right",color:"#8492a6"}},[e._v(e._s(t[a])+"\n                                "),l!=Object.keys(t).length-1?i("span",[e._v("  |  ")]):e._e()]):e._e()])}))}))],1),e._v("\n             \n        ")],1)}),e._v(" "),i("div",{staticClass:"dialog-footer",attrs:{slot:"footer"},slot:"footer"},[i("el-button",{on:{click:e.cancelNewline}},[e._v("取消")]),e._v(" "),i("el-button",{attrs:{type:"primary"},on:{click:e.postNewline}},[e._v("提交")])],1)],2)],1)},staticRenderFns:[]}}});
//# sourceMappingURL=0.8812252f64f530ed7f11.js.map