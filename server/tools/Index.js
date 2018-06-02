
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
                        var names = i.substr(0, i.indexOf('.')).split('-')
                        callback({
                            name: names[1],
                            cname: names[0],
                            value: item
                        })
                    }
                })
            })
        }
    })
}

var main = async () => {
    eachDatas('./data', (item) => {
        var ModelName = item.name
        var ModelCName = item.cname
        var ModelStruct = API.GetStruct(item.value)
        //console.log(ModelStruct)
        API.BuildModels(ModelName, ModelStruct)
        API.BuildElementTemp(ModelName, ModelStruct, ModelCName)
        API.BuildAPI(ModelName, ModelStruct.filter(i => i.must))
        API.BuildRoutes(ModelName, ModelCName)
        console.log(`${item.name} -> Build Over!`)
    })
}
main()