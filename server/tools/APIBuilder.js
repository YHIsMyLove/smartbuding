<<<<<<< HEAD

const fs = require('fs')
const config = require('./config')
const vuexdata = require('./template/vuex')

/**
 * 规则 带*为必须值,-为隐藏,$为不可写
 * value 由 title,type,default,format组成 空格分割
 */
var getStruct = (item) => {
    var result = []
    Object.keys(item).forEach(i => {
        var tmpname = i
        var index = 0
        //是否必须
        var must = false
        if ((index = i.indexOf('*')) > 0) {
            tmpname = i.substr(0, index)
            must = true
        }
        var hide = false
        if ((index = i.indexOf('-')) > 0) {
            hide = true
        }
        var disabled = false
        if ((index = i.indexOf('$')) > 0) {
            disabled = true
        }
        var img = false
        if ((index = i.indexOf('#')) > 0) {
            img = true
        }

        var values = item[i].split(' ')
        result.push({
            name: tmpname,
            title: values[0],
            type: values[1],
            default: values[2],
            format: values[3],
            must: must,
            hide: hide,
            disabled: disabled,
            img: img
        })
    })
    return result
}

var buildElementTemp = (Name, lst, CName = 'APIBuilder生成') => {
    fs.readFile(config.ElementTemp, 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            //1. TemplateDataColumn
            var tmpDataColumnStr = '\n\t\t\t\t\t\t\t\t'
            lst.forEach(i => {
                if (!i.hide) {
                    if (i.img) {
                        tmpDataColumnStr += `<el-table-column width='64' label="${i.title}" >
                        <template slot-scope="scope">
                            <img :src="scope.row.${i.name}" style="width:100%;height:100%;">
                        </template>
                    </el-table-column>\n\t\t\t\t\t\t\t\t`
                    } else {
                        tmpDataColumnStr += `<el-table-column prop="${i.name}" label="${i.title}" >
                    </el-table-column>\n\t\t\t\t\t\t\t\t`
                    }
                }
            });
            //2. TemplateDataDialog
            var tmpDataDialogStr = "\n\t\t\t\t\t\t\t\t\t\t\t\t"
            lst.forEach(i => {
                var disabledStr = i.disabled ? ' disabled ' : ''
                if (i.img) {                    
                    tmpDataDialogStr += `<el-form-item label="${i.title}" prop="${i.name}">
                    \t\t<UploadImage :width=64 v-model="CurrentData.data.${i.name}" />
                    </el-form-item>`
                } else {
                    if (!i.hide)
                        switch (i.type) {
                            case "Number":
                            case "String":
                                tmpDataDialogStr += `<el-form-item label="${i.title}" prop="${i.name}">
                            \t\t<el-input ${disabledStr} v-model="CurrentData.data.${i.name}" auto-complete="off"></el-input>
                            </el-form-item>`
                                break;
                            case "Date":
                                tmpDataDialogStr += `<el-form-item label="${i.title}" prop="${i.name}">
                            \t\t<el-date-picker ${disabledStr}  style="width:100%" placeholder="选择日期时间" 
                            \t\ttype="datetime" v-model="CurrentData.data.${i.name}"></el-date-picker>
                            </el-form-item>`
                                break;
                        }
                }
            })
            //3. lst 包含 ProjID等字段自动引入Vuex
            var ImportVuex = false
            var TempVuexImportStr = ''
            var TempVuexComputedStr = ''
            if (lst.filter(i => i.name == 'ProjID').length > 0) {
                ImportVuex = true
                TempVuexImportStr = vuexdata.import
                TempVuexComputedStr = vuexdata.computedProj
            }
            //4. TemplateDialogEditData
            var tmpdialogEditDataStr = '\n\t\t\t\t'
            lst.forEach(i => {
                if (i.name != 'ProjID')
                    tmpdialogEditDataStr += `${i.name} : row.${i.name},\n\t\t\t\t`
                else {
                    tmpdialogEditDataStr += `${i.name} : this.getProj.ProjID,\n\t\t\t\t`
                }
            })
            //5. TemplateDialogNewData
            var tmpdialogNewDataStr = '\n\t\t\t\t'
            lst.forEach(i => {
                if (i.name != 'ProjID') {
                    var _default = i.type == 'String' || i.type == 'Number' ? i.default : '';
                    tmpdialogNewDataStr += `${i.name} : '${_default}',\n\t\t\t\t`
                }
                else {
                    tmpdialogNewDataStr += `${i.name} : this.getProj.ProjID,\n\t\t\t\t`
                }
            })

            var result = res.replaceAll('TemplateName', Name)
                .replaceAll('TemplateDataColumn', tmpDataColumnStr)
                .replaceAll('TemplateDataDialog', tmpDataDialogStr)
                .replaceAll('TemplateDialogEditData', tmpdialogEditDataStr)
                .replaceAll('TemplateDialogNewData', tmpdialogNewDataStr)
                .replaceAll('APIBuilder生成', CName)
                .replaceAll('TempVuexImport', TempVuexImportStr)
                .replaceAll('TempVuexComputed', TempVuexComputedStr)
            fs.exists(config.ElementTempDist, (exists) => {
                if (!exists) {
                    fs.mkdirSync(config.ElementTempDist)
                }
                fs.writeFile(`${config.ElementTempDist}/${Name}Element.vue`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}

var buildModels = (Name, Lst) => {
    fs.readFile(config.ModelsTemp, 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            var lstStr = ''
            Lst.forEach(i => {
                if (i.default) {
                    var tmpdefault = i.type == 'String' ? `'${i.default}'` : `${i.default}`
                    lstStr += `\t${i.name}:{ type: ${i.type} , default: ${tmpdefault} },\n`
                } else {
                    lstStr += `\t${i.name}:${i.type},\n`
                }
            })
            var result = res.replaceAll('TemplateName', Name)
                .replaceAll("TemplateLST", lstStr)
            fs.exists(config.ModelsDist, (exists) => {
                if (!exists) {
                    fs.mkdirSync(config.ModelsDist)
                }
                fs.writeFile(`${config.ModelsDist}/${Name}Model.js`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}

var buildAPI = (Name, Lst) => {
    fs.readFile(config.BusinessTemp, 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            fs.exists(config.BusinessDist, (exists) => {
                if (!exists) {
                    fs.mkdirSync(config.BusinessDist)
                }
                var mustCheckStr = ''
                Lst.forEach(i => {
                    mustCheckStr += `if(!req.body.${i.name}){
\t\treturn res.send(msg.genFailedMsg('err-> ${i.name} is not found'))\n\t}\n\t`
                })
                var result = res.replaceAll('TemplateName', Name)
                    .replaceAll('TemplateMustCheck', mustCheckStr)
                fs.writeFile(`${config.BusinessDist}/${Name}Business.js`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}

var buildRoutes = (Name, CName) => {
    fs.readFile(config.RoutesTemp, 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            var result = res.replaceAll('TemplateName', Name)
                .replaceAll('TemplateCName', CName)
            fs.exists(config.RoutesDist, (exists) => {
                if (!exists) {
                    fs.mkdirSync(config.RoutesDist)
                }
                fs.writeFile(`${config.RoutesDist}/${Name}.builder`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}

exports.GetStruct = getStruct
exports.BuildAPI = buildAPI
exports.BuildModels = buildModels
exports.BuildElementTemp = buildElementTemp
=======

const fs = require('fs')
const config = require('./config')
const vuexdata = require('./template/vuex')

/**
 * 规则 带*为必须值,-为隐藏,$为不可写
 * value 由 title,type,default,format组成 空格分割
 */
var getStruct = (item) => {
    var result = []
    Object.keys(item).forEach(i => {
        var tmpname = i
        var index = 0
        //是否必须
        var must = false
        if ((index = i.indexOf('*')) > 0) {
            tmpname = i.substr(0, index)
            must = true
        }
        var hide = false
        if ((index = i.indexOf('-')) > 0) {
            hide = true
        }
        var disabled = false
        if ((index = i.indexOf('$')) > 0) {
            disabled = true
        }

        var values = item[i].split(' ')
        result.push({
            name: tmpname,
            title: values[0],
            type: values[1],
            default: values[2],
            format: values[3],
            must: must,
            hide: hide,
            disabled: disabled
        })
    })
    return result
}

var buildElementTemp = (Name, lst, CName = 'APIBuilder生成') => {
    fs.readFile(config.ElementTemp, 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            //1. TemplateDataColumn
            var tmpDataColumnStr = '\n\t\t\t\t\t\t\t\t'
            lst.forEach(i => {
                if (!i.hide)
                    tmpDataColumnStr += `<el-table-column prop="${i.name}" label="${i.title}" >
                </el-table-column>\n\t\t\t\t\t\t\t\t`
            });
            //2. TemplateDataDialog
            var tmpDataDialogStr = "\n\t\t\t\t\t\t\t\t\t\t\t\t"
            lst.forEach(i => {
                var disabledStr = i.disabled ? ' disabled ' : ''
                if (!i.hide)
                    switch (i.type) {
                        case "Number":
                        case "String":
                            tmpDataDialogStr += `<el-form-item label="${i.title}" prop="${i.name}">
                        \t\t<el-input ${disabledStr} v-model="CurrentData.data.${i.name}" auto-complete="off"></el-input>
                        </el-form-item>`
                            break;
                        case "Date":
                            tmpDataDialogStr += `<el-form-item label="${i.title}" prop="${i.name}">
                        \t\t<el-date-picker ${disabledStr}  style="width:100%" placeholder="选择日期时间" 
                        \t\ttype="datetime" v-model="CurrentData.data.${i.name}"></el-date-picker>
                        </el-form-item>`
                            break;
                    }
            })
            //3. lst 包含 ProjID等字段自动引入Vuex
            var ImportVuex = false
            var TempVuexImportStr = ''
            var TempVuexComputedStr = ''
            if (lst.filter(i => i.name == 'ProjID').length > 0) {
                ImportVuex = true
                TempVuexImportStr = vuexdata.import
                TempVuexComputedStr = vuexdata.computedProj
            }
            //4. TemplateDialogEditData
            var tmpdialogEditDataStr = '\n\t\t\t\t'
            lst.forEach(i => {
                if (i.name != 'ProjID')
                    tmpdialogEditDataStr += `${i.name} : row.${i.name},\n\t\t\t\t`
                else {
                    tmpdialogEditDataStr += `${i.name} : this.getProj.ProjID,\n\t\t\t\t`
                }
            })
            //5. TemplateDialogNewData
            var tmpdialogNewDataStr = '\n\t\t\t\t'
            lst.forEach(i => {
                if (i.name != 'ProjID') {
                    var _default = i.type == 'String' || i.type == 'Number' ? i.default : '';
                    tmpdialogNewDataStr += `${i.name} : '${_default}',\n\t\t\t\t`
                }
                else {
                    tmpdialogNewDataStr += `${i.name} : this.getProj.ProjID,\n\t\t\t\t`
                }
            })

            var result = res.replaceAll('TemplateName', Name)
                .replaceAll('TemplateDataColumn', tmpDataColumnStr)
                .replaceAll('TemplateDataDialog', tmpDataDialogStr)
                .replaceAll('TemplateDialogEditData', tmpdialogEditDataStr)
                .replaceAll('TemplateDialogNewData', tmpdialogNewDataStr)
                .replaceAll('APIBuilder生成', CName)
                .replaceAll('TempVuexImport', TempVuexImportStr)
                .replaceAll('TempVuexComputed', TempVuexComputedStr)
            fs.exists(config.ElementTempDist, (exists) => {
                if (!exists) {
                    fs.mkdirSync(config.ElementTempDist)
                }
                fs.writeFile(`${config.ElementTempDist}/${Name}Element.vue`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}

var buildModels = (Name, Lst) => {
    fs.readFile(config.ModelsTemp, 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            var lstStr = ''
            Lst.forEach(i => {
                if (i.default) {
                    var tmpdefault = i.type == 'String' ? `'${i.default}'` : `${i.default}`
                    lstStr += `\t${i.name}:{ type: ${i.type} , default: ${tmpdefault} },\n`
                } else {
                    lstStr += `\t${i.name}:${i.type},\n`
                }
            })
            var result = res.replaceAll('TemplateName', Name)
                .replaceAll("TemplateLST", lstStr)
            fs.exists(config.ModelsDist, (exists) => {
                if (!exists) {
                    fs.mkdirSync(config.ModelsDist)
                }
                fs.writeFile(`${config.ModelsDist}/${Name}Model.js`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}

var buildAPI = (Name, Lst) => {
    fs.readFile(config.BusinessTemp, 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            fs.exists(config.BusinessDist, (exists) => {
                if (!exists) {
                    fs.mkdirSync(config.BusinessDist)
                }
                var mustCheckStr = ''
                Lst.forEach(i => {
                    mustCheckStr += `if(!req.body.${i.name}){
\t\treturn res.send(msg.genFailedMsg('err-> ${i.name} is not found'))\n\t}\n\t`
                })
                var result = res.replaceAll('TemplateName', Name)
                    .replaceAll('TemplateMustCheck', mustCheckStr)
                fs.writeFile(`${config.BusinessDist}/${Name}Business.js`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}

var buildRoutes = (Name, CName) => {
    fs.readFile(config.RoutesTemp, 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            var result = res.replaceAll('TemplateName', Name)
                .replaceAll('TemplateCName', CName)
            fs.exists(config.RoutesDist, (exists) => {
                if (!exists) {
                    fs.mkdirSync(config.RoutesDist)
                }
                fs.writeFile(`${config.RoutesDist}/${Name}.builder`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}

exports.GetStruct = getStruct
exports.BuildAPI = buildAPI
exports.BuildModels = buildModels
exports.BuildElementTemp = buildElementTemp
>>>>>>> 72bef902ba622ae0f489c5b3a7807f336d63243e
exports.BuildRoutes = buildRoutes