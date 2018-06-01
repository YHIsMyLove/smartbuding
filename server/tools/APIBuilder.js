
const fs = require('fs')
var buildElementTemp = (Name, lst) => {
    fs.readFile('./template/element.temp', 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            //1. TemplateDataColumn
            var tmpDataColumnStr = ''
            lst.forEach(i => {
                tmpDataColumnStr += `                        <el-table-column prop="${i.name}" label="${i.title}" >
                </el-table-column>`
            });
            //2. TemplateDataDialog
            var tmpDataDialogStr = ""
            lst.forEach(i => {
                switch (i.type) {
                    case "String":
                        tmpDataDialogStr += ``
                        break;
                    case "Date":
                        tmpDataDialogStr += `                        <el-form-item label="${i.title}" prop="${i.name}">
                    <el-date-picker :disabled="true" style="width:100%" placeholder="选择日期时间" type="datetime" v-model="CurrentData.data.${i.name}"></el-date-picker>
                </el-form-item>`
                        break;
                    case "Number":
                        tmpDataDialogStr += ``
                        break;
                }
            })
            //3. TemplateDialogEditData
            //4. TemplateDialogNewData
            var result = res.replaceAll('TemplateName', Name)
            fs.exists('./dist/element', (exists) => {
                if (!exists) {
                    fs.mkdirSync('./dist/element')
                }
                fs.writeFile(`./dist/element/${Name}Element.js`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}


/**
 * 规则 带*为必须值,value 由 title,type,default,format组成 空格分割
 */
var getStruct = (item) => {
    var result = []
    Object.keys(item).forEach(i => {
        var tmpname = i
        var must = false
        var index = 0
        //是否必须
        if ((index = i.indexOf('*')) > 0) {
            tmpname = i.substr(0, index)
            must = true
        }
        var values = item[i].split(' ')
        result.push({
            name: tmpname,
            title: values[0],
            type: values[1],
            default: values[2],
            format: values[3],
            must: must
        })
    })
    return result
}


var buildModels = (Name, Lst) => {
    fs.readFile('./template/models.temp', 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            var lstStr = ''
            Lst.forEach(i => {
                if (i.default) {
                    var tmpdefault = i.type == 'String' ? `'${i.default}'` : `${i.default}`
                    lstStr += `\t${i.name}:{ type: ${i.type} , default: ${tmpdefault} }`
                } else {
                    lstStr += `\t${i.name}:${i.type},\n`
                }
            })
            var result = res.replaceAll('TemplateName', Name)
                .replaceAll("TemplateLST", lstStr)
            fs.exists('./dist/models', (exists) => {
                if (!exists) {
                    fs.mkdirSync('./dist/models')
                }
                fs.writeFile(`./dist/models/${Name}Model.js`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}

var buildAPI = (Name, Lst) => {
    fs.readFile('./template/api.temp', 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            fs.exists('./dist/business', (exists) => {
                if (!exists) {
                    fs.mkdirSync('./dist/business')
                }

                var mustCheckStr = ''
                Lst.forEach(i => {
                    mustCheckStr +=
                        `if(!req.body.${i.name}){
    \treturn res.send(msg.genFailedMsg('err-> ${i.name} is not found'))
\t}`
                })
                var result = res.replaceAll('TemplateName', Name)
                    .replaceAll('TemplateMustCheck', mustCheckStr)
                fs.writeFile(`./dist/business/${Name}Business.js`, result, (err) => {
                    if (err) console.log('写入失败')
                })
            })
        }
    })
}

exports.BuildAPI = buildAPI
exports.BuildModels = buildModels
exports.GetStruct = getStruct