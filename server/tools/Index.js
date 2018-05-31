
const fs = require('fs')
String.prototype.replaceAll = function (FindText, RepText) {
    regExp = new RegExp(FindText, "g");
    return this.replace(regExp, RepText);
}
var eachDatas = (path, callback) => {
    fs.readdir(path, (err2, res2) => {
        if (err2) {
            rej(err2)
        } else {
            res2.forEach(i => {
                fs.readFile(`${path}/${i}`, (err3, res3) => {
                    if (err3) {
                        rej(err3)
                    } else {
                        var item = JSON.parse(res3)
                        callback({
                            name: i.substr(0, i.indexOf('.')),
                            value: item
                        })
                    }
                })
            })
        }
    })
}
var getStruct = (item) => {
    var result = []
    Object.keys(item).forEach(i => {
        var tmpname = i
        var must = false
        var index = 0
        if ((index = i.indexOf('*')) > 0) {
            tmpname = i.substr(0, index)
            must = true
        }
        result.push({
            name: tmpname,
            type: item[i],
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
                lstStr += `\t${i.name}:${i.type},\n`
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
var buildRoutes = (Name) => {
    fs.readFile('./template/routes.temp', 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            var result = res.replaceAll('TemplateName',Name)
            fs.exists('./dist/routes', (exists) => {
                if (!exists) {
                    fs.mkdirSync('./dist/routes')
                }
                fs.writeFile(`./dist/routes/${Name}.js`, result, (err) => {
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
var main = async () => {
    eachDatas('./data', (item) => {
        var ModelName = item.name
        var ModelStruct = getStruct(item.value)
        buildModels(ModelName, ModelStruct)
        buildAPI(ModelName, ModelStruct.filter(i => i.must))
        buildRoutes(ModelName)
        console.log(`${item.name} -> Build Over!`)
    })
}
main()