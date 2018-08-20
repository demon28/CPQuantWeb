


function GetAllNumber() {

    var list = [];

    for (var i = 0; i < 9; i++) {

        for (var j = 0; j < 9; j++) {

            for (var k = 0; k < 9; k++) {

                for (var l = 0; l < 9; l++) {

                    for (var z = 0; z < 9; z++) {

                        var number = i + "," + j + "," + "," + k + "," + "," + l + "," + "," + z;
                        list.push(number);
                    }
                }
            }
        }

    }

    return list;

}


//是否是偶数
 function isOU(num) {
    if(num % 2 === 0)
     {
        return true;
     }
}

