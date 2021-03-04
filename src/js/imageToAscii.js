const jimp = require('jimp')
const fs = require('fs')
const path = require('path')

module.exports.imageToAscii = (imagePath, outputPath) => {
    fs.readFile('chars.json', (err, chars) => {
        if (err) throw err;
        
        ProcessImage(JSON.parse(chars).chars)
    })
    
    
    function ProcessImage(chars) {
        var output = ''
        jimp.read(imagePath, (err, img) => {
            if (err) throw err;
            var _y = 0;
            var time = Date.now()
            img.scan(0, 0, img.getWidth(), img.getHeight(), function(x, y, idx) {
                var red = this.bitmap.data[idx + 0];
                var green = this.bitmap.data[idx + 1];
                var blue = this.bitmap.data[idx + 2];
    
                var brightness = Math.round((chars.length-1) / 255 * (0.2126*red + 0.7152*green + 0.0722*blue))
                
                if (_y != y) {
                    _y = y;
                    output += '\n'
                }
                
                output += chars[brightness]
            })
            fs.writeFileSync(path.join(outputPath, ("output-" + time + ".txt")), output)
        })
    }
}

