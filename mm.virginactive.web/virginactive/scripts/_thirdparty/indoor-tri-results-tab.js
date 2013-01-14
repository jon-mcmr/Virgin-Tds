var return_url = window.location.hash.replace("#","")
var valid_urls = ['http://local.virginactive', 'http://www.virginactive.co.uk', 'http://uat.virginactive.co.uk', 'http://uat2.virginactive.co.uk', 'http://uat3.virginactive.co.uk'];

if(!Array.prototype.indexOf) {
    Array.prototype.indexOf = function(needle) {
        for(var i = 0; i < this.length; i++) {
            if(this[i] === needle) {
                return i;
            }
        }
        return -1;
    };
}

Array.prototype.contains = function(obj) {
    var i = this.length;
    while (i--) {
        if (this[i] == obj) {
            return true;
        }
    }
    return false;
}

if(valid_urls.contains(return_url)){

    
//    $(window).load(function () {
        setInterval(function(){
            var windowHeight = document.documentElement.scrollHeight || document.body.scrollHeight ;
            parent.postMessage(windowHeight,return_url);
        },100)
//    });
}