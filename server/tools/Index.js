
const fs = require('fs')
const API = require('./APIBuilder')
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
var buildRoutes = (Name) => {
    fs.readFile('./template/routes.temp', 'utf8', (err, res) => {
        if (err) console.log(err)
        else {
            var result = res.replaceAll('TemplateName', Name)
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
var main = async () => {
    eachDatas('./data', (item) => {
        var ModelName = item.name
        var ModelStruct = API.GetStruct(item.value)
        console.log(ModelStruct)
        API.BuildModels(ModelName, ModelStruct)
        //API.BuildAPI(ModelName, ModelStruct.filter(i => i.must))
        //buildRoutes(ModelName)
        console.log(`${item.name} -> Build Over!`)
    })
}
main()