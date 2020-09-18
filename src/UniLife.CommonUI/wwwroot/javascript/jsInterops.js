window.jsInterops = {
    setCookie: function (cookie) {
        try {
            document.cookie = cookie;
        }
        catch (err) {
            console.log(err.message);
        }
        
    },
    removeCookie: function (cookieName) {
        try {
            console.log(cookieName + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;');
            console.log(document.cookie);
            document.cookie = cookieName + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
            console.log(document.cookie);
        }
        catch (err) {
            console.log(err.message);
        }
        
    },
    eraseCookie: function (cookieName) {
        try {
            document.cookie = cookieName + '=; Max-Age=-99999999;';
        }
        catch (err) {
            console.log(err.message);
        }
        
    },
    deleteCookieFromAllPaths: function (cookieName)
    {
        try {
            var path = window.location.pathname;
            var paths = ['/'], pathLength = 1, nextSlashPosition;
            while ((nextSlashPosition = path.indexOf('/', pathLength)) != -1) {
                pathLength = nextSlashPosition + 1;
                paths.push(path.substr(0, pathLength));
            }

            for (let path of paths)
                document.cookie = `${cookieName}=; path=${path}; expires=Thu, 01 Jan 1970 00:00:01 GMT;`;

            console.log("deleteCookieFromAllPaths geldi");

            window.location = " "; // TO REFRESH THE PAGE
        }
        catch (err) {
            console.log(err.message);
        }
    },
    deleteCookieSon: function () {
        //Üstteki olmazsa buna gelecen denemedik daha!
        var cookies = document.cookie.split("; ");
        for (var c = 0; c < cookies.length; c++) {
            var d = window.location.hostname.split(".");
            while (d.length > 0) {
                var cookieBase = encodeURIComponent(cookies[c].split(";")[0].split("=")[0]) + '=; expires=Thu, 01-Jan-1970 00:00:01 GMT; domain=' + d.join('.') + ' ;path=';
                var p = location.pathname.split('/');
                document.cookie = cookieBase + '/';
                while (p.length > 0) {
                    document.cookie = cookieBase + p.join('/');
                    p.pop();
                };
                d.shift();
            }
        }
    },
    belgePrint: function (adres) {
        console.log(adres);
        var myWindow = window.open(adres, '_blank');
        myWindow.print();
    },
    logla: function (mesaj) {
        console.log(mesaj);
    },
    refreshPage: function () {
        window.location = " "; // TO REFRESH THE PAGE
    }
}