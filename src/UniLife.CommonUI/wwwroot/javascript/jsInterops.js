window.jsInterops = {
    setCookie: function (cookie) {
        document.cookie = cookie;
    },
    removeCookie: function (cookieName) {
        console.log(cookieName + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;');
        console.log(document.cookie);
        document.cookie = cookieName + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
        console.log(document.cookie);
    },
    eraseCookie: function (cookieName) {
        document.cookie = cookieName + '=; Max-Age=-99999999;';
    },
    delete_cookie: function (cookieName) {
        if (get_cookie(name)) {
            document.cookie = cookieName + "=" +
                ((window.location.pathname) ? ";path=" + window.location.pathname : "") +
                ((window.location.hostname) ? ";domain=" + window.location.hostname : "") +
                ";expires=Thu, 01 Jan 1970 00:00:01 GMT";
        }
    },
    deleteCookieFromAllPaths: function (cookieName)
    {
        var path = window.location.pathname;
        var paths = ['/'], pathLength = 1, nextSlashPosition;
        while ((nextSlashPosition = path.indexOf('/', pathLength)) != -1) {
            pathLength = nextSlashPosition + 1;
            paths.push(path.substr(0, pathLength));
        }

        for (let path of paths)
            document.cookie = `${cookieName}=; path=${path}; expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
    },
    deleteCookieFromWithoutName: function () {
        //Üstteki olmazsa buna gelecen denemedik daha!
        var cookies = document.cookie.split(";");
        for (var i = 0; i < cookies.length; i++)
            eraseCookie(cookies[i].split("=")[0]);
    }
}