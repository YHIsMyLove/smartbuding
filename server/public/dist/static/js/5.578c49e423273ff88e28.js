webpackJsonp([5,10],{352:function(t,e,a){"use strict";function r(t){return t&&t.__esModule?t:{default:t}}Object.defineProperty(e,"__esModule",{value:!0});var s=a(6),n=r(s),o=a(10),i=(r(o),a(9)),l=r(i),c=a(5),u=r(c),d=a(7);e.default={computed:(0,n.default)({},(0,d.mapGetters)(["getProj"]),{CurrentProj:function(){return this.$store.state.Proj}}),created:function(){this.getUserData()},watch:{CurrentProj:function(t){this.curProjID=t.ProjID,this.getUserData()}},data:function(){return{curCityID:-1,curProjID:-1,isEdit:!1,isEditText:"查看模式",default_active:"0",prov_city_options:[],proj_data:[],tableUserData:[],formInline:{user:""},listLoading:!1,currentPage:0,tableDataLength:0,currentPageSize:0,curentDeptID:-1}},methods:{changeUserProj:function(t){var e=this;console.log(t._id);var a={UserID:t._id,ProjID:e.curProjID,insertOrDel:t.UserInProj?"insert":"del"};u.default.post("/api/InsertOrDelUserProj",a).then(function(a){a.data.success?e.$message({message:"["+t.UserName+"]设置成功",type:"success"}):console.log(a.data)}).catch(function(t){return console.log(t)})},formatSex:function(t,e){return 1==t.UserSex?"男":0==t.UserSex?"女":"未知"},changeEdit:function(){return 0!=this.isEdit||this.curProjID?(this.isEdit=!this.isEdit,this.isEditText=this.isEdit?"编辑模式":"查看模式",void this.getUserData()):void this.$message({message:"请先选择项目",type:"error"})},getUserData:function(){if(this.getProj&&(this.curProjID=this.getProj.ProjID),this.curProjID==-1)return void this.$message({message:"请先选择项目",type:"error"});var t=this,e="/api/GetUserByProjID",a={limit:t.$data.currentPageSize,page:t.$data.currentPage,ProjID:t.curProjID,isEdit:t.isEdit};t.listLoading=!0,l.default.start(),u.default.get(e,{params:a}).then(function(e){e.data.success?(t.$data.tableUserData=e.data.data,t.$data.tableDataLength=e.data.meta.count,t.listLoading=!1,l.default.done()):(t.$data.tableUserData=[],t.$data.tableDataLength=0,t.listLoading=!1,l.default.done())}).catch(function(t){return console.log(t)})},handleEdit:function(t){},handleDel:function(t){},handleSizeChange:function(){},handleCurrentChange:function(){}}}},415:function(t,e,a){e=t.exports=a(21)(),e.push([t.id,"","",{version:3,sources:[],names:[],mappings:"",file:"UserProjManager.vue",sourceRoot:"webpack://"}])},506:function(t,e,a){var r=a(415);"string"==typeof r&&(r=[[t.id,r,""]]);a(22)(r,{});r.locals&&(t.exports=r.locals)},533:function(t,e,a){a(506);var r=a(3)(a(352),a(563),"data-v-4eb9bb8c",null);t.exports=r.exports},563:function(t,e){t.exports={render:function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",[a("el-tabs",{staticStyle:{width:"100%"}},[a("el-tab-pane",{attrs:{label:"用户项目管理"}},[a("section",[a("el-col",{staticClass:"toolbar",attrs:{span:24}},[a("el-form",{staticClass:"demo-form-inline",attrs:{inline:!0,model:t.formInline}},[a("el-form-item",{attrs:{label:"人员搜索"}},[a("el-input",{attrs:{placeholder:"工号/姓名"}})],1),t._v(" "),a("el-form-item",[a("el-button",{attrs:{round:""},on:{click:t.getUserData}},[t._v("查询")])],1),t._v(" "),a("el-form-item",[a("el-button",{attrs:{type:"primary",round:""},on:{click:t.changeEdit}},[t._v(t._s(t.isEditText))])],1)],1)],1),t._v(" "),a("el-col",{attrs:{span:24}},[a("el-table",{directives:[{name:"loading",rawName:"v-loading",value:t.listLoading,expression:"listLoading"}],staticStyle:{width:"100%",height:"100%"},attrs:{border:"",fit:"",stripe:"",data:t.tableUserData,"highlight-current-row":""}},[a("el-table-column",{attrs:{type:"index",label:"编号",width:"85"}}),t._v(" "),a("el-table-column",{attrs:{prop:"UserID",label:"账号"}}),t._v(" "),a("el-table-column",{attrs:{prop:"UserName",label:"姓名"}}),t._v(" "),a("el-table-column",{attrs:{prop:"UserSex",label:"性别",formatter:t.formatSex}}),t._v(" "),a("el-table-column",{attrs:{prop:"UserAge",label:"年龄"}}),t._v(" "),a("el-table-column",{attrs:{prop:"UserPhoneNum",label:"手机号"}}),t._v(" "),t.isEdit?a("el-table-column",{attrs:{label:"成员",width:"100"},scopedSlots:t._u([{key:"default",fn:function(e){return[a("el-switch",{on:{change:function(a){t.changeUserProj(e.row)}},model:{value:e.row.UserInProj,callback:function(a){t.$set(e.row,"UserInProj",a)},expression:"scope.row.UserInProj"}})]}}])}):t._e()],1),t._v(" "),a("el-col",{staticClass:"toolbar",staticStyle:{"padding-bottom":"10px"},attrs:{span:24}},[a("el-pagination",{staticStyle:{float:"right"},attrs:{"current-page":t.currentPage,"page-sizes":[10,20,30,40],"page-size":t.currentPageSize,layout:"total, sizes, prev, pager, next, jumper",total:t.tableDataLength},on:{"size-change":t.handleSizeChange,"current-change":t.handleCurrentChange}})],1)],1)],1)])],1)],1)},staticRenderFns:[]}}});
//# sourceMappingURL=5.578c49e423273ff88e28.js.map