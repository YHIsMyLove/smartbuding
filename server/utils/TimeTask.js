var later = require('later')

var sched = later.parse.text('every 5 sec')

later.setInterval(() => {
    console.log('fuck')
}, sched)

