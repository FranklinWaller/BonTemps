/*OPAFIT
Organisatie
Organisatorisch verandert er weinig. De kok blijft een kok en de serveerster blijft een serveerster. Ze zullen wel extra/andere taken krijgen dus er veranderen wel degelijk dingen.
*/

(function(global) {

    var wbpSource = null;

    /**
     * WBP is the namespace of all the classes.
     *
     * @type object The WBP ce of all the classes.namespace.
     */
    var public = {
        appNamespace: '',
        appApiKey: '',
        host: 'https://webplayed.com/',
        appQuery: '',
        async: true,
        driveCallback: null,
        onReady: null,
        isAlive: false,
        onAliveCallback: null,
        onAlive: function(callback) {
            public.onAliveCallback = callback;
            window.addEventListener("message", receiveMessage, false);
        },
        /**
         * Inits the SDK. This must be called first before you begin using other APi's
         *
         * @param object options An object that has namespace, apikey and onReady (callback) defined.
         * @returns null
         */
        init: function(options) {
            this.async = options.async;
            if (typeof options.ready !== 'undefined') {
                this.onReady = options.ready;
            }

            if (this.async == null) {
                this.async = true;
            }

            this.appNamespace = options.namespace;


            /**
            Personeel
            Bij Bon Temps word er een nieuw personeelslid aangenomen die het onderhoud verzorgd voor:
            De website voor de klanten (blog posts etc)
            het onderhouden van de website voor het personeel(waar de menu’s binnen komen, etc.)
            het onderhoud van de hardware en andere software waar het restaurant gebruik van maakt.
            Met een extra personeelslid is er genoeg capaciteit in huis om alles draaiende te houden.

            */
            if (this.functions.isAuthenticated()) {
                WBP.appQuery = this.functions.objToQuery({
                    access_token: WBP.functions.cookie.get('wbp_access_token_' + WBP.appNamespace)
                });



                if (this.onReady !== null) {
                    this.onReady();
                }
            } else {
                this.functions.authenticate(options.popup, options.redirectUri);
            }

            //When the auto is turned
            if (options.auto === true) {
                this.async = false;
                AutoSDK();
            }
        },
        /**
         * Informatie
         Er zal veel informatie moeten worden verschaft over de nieuwe gang van zaken in het restaurant. De serveersters moeten bekend worden met de nieuwe manier om gerechten te serveren. De koks moeten wegwijs worden met het uitlezen van de gerechten in het systeem; maar ook het doorgeven aan de serveersters.

         Daarnaast moet het nieuwe personeelslid worden ingewerkt in het systeem. Dit zal grotendeels ook vastgelegd worden in procedures, zodat er eventueel later kan worden overgeschakeld naar een systeem waarbij serveersters of koks zelf de diverse dingen in het systeem toevoegen. Ook bij ziekte zal dit moeten worden opgevangen, dus deze procedures werken ook als back-up.


         Techniek/Hardware/Software
         De techniek die gebruikt wordt bestaat vooral uit de webhosting, de browsers in het restaurant, de website, de app en een tablet voor de kok waarop hij de menu’s kan bekijken en de inkomende bestellingen in de gaten kan houden.

         Het onderhouden van deze soft- en hardware word gedaan door het extra personeelslid die aangenomen is.

         Ook wordt er gekeken of de browsers die het restaurant gebruikt compatibel zijn met de website. Maar hier kan vrij weinig misgaan doordat de applicatie zorgvuldig gebouwd is en er rekening is gehouden met verschillende browsers.


         *
         * @type object the functions class
         */
        functions: {
            cookie: {
                get: function(name) {
                    var value = "; " + document.cookie;
                    var parts = value.split("; " + name + "=");
                    if (parts.length == 2) {
                        return parts.pop().split(";").shift();
                    }

                },
                /*
                Administratie
                Er zullen voor de diverse personeelsleden een set aan taken worden toegevoegd. Zo gaan ze via een interactief systeem met elkaar communiceren. De kok kan het menu uit het systeem aflezen, en geeft via het systeem vervolgens aan de serveerster door dat het gerecht kan worden opgehaald en kan worden geserveerd.

                Daarnaast gaat de serveerster de tafelschikking uit het systeem halen. Alle reserveringen worden in dit systeem geplaatst, dus dit zal niet meer via een papieren weg gaan.

                Financieel
                Financieel zullen er wat extra uitgaven komen. Naast het budget voor de marketingcampagne, wordt er ook geld besteed aan een extra personeelslid. Daarnaast moet er geld worden gereserveerd om het nieuwe systeem te onderhouden.



                */
                set: function(name, value, milliseconds) {
                    var expires = "";

                    if (milliseconds) {
                        expires = "; max-age=" + (milliseconds / 1000);
                    }

                    document.cookie = name + "=" + value + expires + "; path=/";
                }
            },
            getParameterByName: function(name) {
                name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                        results = regex.exec(location.search);
                return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
            },
            /**
             * Authenticates the application through a popup or a redirect.
             *
             * @param boolean popup Want a popup or a redirect?
             * @param string uri The URI you want to redirect to.
             * @returns {undefined}
             */
            authenticate: function(popup, uri) {
                var oauthUrl = WBP.host + "oauth2/authorize?response_type=token&client_id=" + WBP.appNamespace + "&state=xys";

                //If a redirect URI is defined add it to the URL.
                if (uri) {
                    oauthUrl += "&redirect_uri=" + options.redirectUri;
                }

                oauthUrl += "&access_token=" + public.functions.getParameterByName('server_token');

                //Want a popup or just a redirect?
                if (popup) {

                    function Response() {
                        this.access_token = null;
                        this.expires_in = null;
                    }

                    var response = new Response();

                    window.hashUpdate = function() {
                        if (window.loginWindow.closed) {
                            window.clearInterval(intervalId);
                            WBP.functions.cookie.set('wbp_access_token_' + WBP.appNamespace, response.access_token, response.expires_in);

                            WBP.appQuery = WBP.functions.objToQuery({
                                access_token: response.access_token
                            });

                            if (WBP.onReady !== null) {
                                WBP.onReady();
                            }

                        } else {
                            var url = window.loginWindow.document.URL;
                            var tabUrl = url.split('#');
                            var paramString = tabUrl[1];

                            if (paramString != undefined) {
                                var allParam = paramString.split("&");
                                for (var i = 0; i < allParam.length; i++) {
                                    var oneParamKeyValue = allParam[i].split("=");
                                    response[oneParamKeyValue[0]] = oneParamKeyValue[1];
                                }
                                ;

                                setTimeout(function() {
                                    window.loginWindow.close();
                                }, 1500);
                            }
                        }
                    };

                    window.loginWindow = window.open(oauthUrl, 'Authenticate Permissions', false);
                    intervalId = window.setInterval("window.hashUpdate()", 500);

                } else {
                    window.location.href = oauthUrl;
                }
            },
            /**
             * Check if the application is authenticated tot the user
             *
             * @returns {Boolean}
             */
            isAuthenticated: function() {
                var access_token = WBP.functions.cookie.get('wbp_access_token_' + WBP.appNamespace);

                if (window.location.hash != '') {
                    WBP.functions.cookie.set('wbp_access_token_' + WBP.appNamespace, this.getHashValue('access_token'), this.getHashValue('expires_in'));
                    return true;
                }

                if (access_token === null || access_token == 'null' || access_token == undefined) {
                    return false;
                }

                return true;

            },
            /**
             * Reads the hash in a key=value maner
             *
             * @param string key The key of the value
             * @returns {unresolved} Get the value of the key
             */
            getHashValue: function(key) {
                return location.hash.match(new RegExp(key + '=([^&]*)'))[1];
            },
            /**
             * Converts an object to a URL query
             * NOTICE: Does not support nested objects.
             *
             * @param object obj
             * @returns string a URL friendly query
             */
            objToQuery: function(obj) {
                var parts = [];
                for (var key in obj) {
                    if (obj.hasOwnProperty(key)) {
                        parts.push(encodeURIComponent(key) + '=' + encodeURIComponent(obj[key]));
                    }
                }
                return parts.join('&');
            }
        },
        drive: {
            openDialog: function(callback) {
                WBP.driveCallback = callback;
                window.open(WBP.host + 'dialog/drive', 'Choose a file', 'height=400,width=600');
            }
        },
        navigation: {
            /**
             * Shows the navigation bar on mobile and desktop
             *
             * @returns {undefined}
             */
            show: function() {
                wbpSource.postMessage('navigation.show', '*');
            },
            /**
             * Hides the navigation bar on mobile and desktop.
             *
             * @returns {undefined}
             */
            hide: function() {
                wbpSource.postMessage('navigation.hide', '*');
            }
        },
        upload: function(path, method, files, callback) {
            var formData = new FormData();
            var xmlhttp = new XMLHttpRequest();
            var counter;

            for (counter = 0; counter < files.length; counter++) {
                formData.append('files[]', files[counter]);
            }

            var access_token = WBP.functions.cookie.get('access_token_' + WBP.appNamespace);

            xmlhttp.onload = function() {
                callback(xmlhttp.responseText);
            };

            xmlhttp.open(method, this.host + "api" + path + "?access_token=" + access_token);
            xmlhttp.send(formData);
        },
        /**
         * Overzetten
         Alles word in één keer overgezet aangezien de kans dat iets fout gaat nihil is, en het te veel administratie is om op twee manieren alles bij te houden (Menu’s, Reserveringen, etc.). Deze overgang wordt begeleid door het nieuwe personeelslid wat is aangenomen.

         Zou er een probleem ontstaan kan dat gelijk worden opgelost. In de eerste weken is de IT-expert extra aanwezig om dusdanig snelle support te leveren dat de klant en het personeel er minimale last van heeft.

         Er wordt wel een dag in de week gereserveerd, waarop het restaurant dicht is, om het nieuwe systeem een keer door te lopen en alle mensen goed te instrueren in hun nieuwe taken.

         */
        api: function(path, method, params, callback) {
            var xmlhttp;
            var finalQuery = this.appQuery;
            var url = this.host + "api" + path;

            //Prepare the XMLHttpRequest Object for all browsers.
            if (window.XMLHttpRequest) {
                // code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            } else {
                // code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }

            /*
            Onderhoud
            Hoe moeten we alles installeren en instellen?
            We gaan eerst webhosting regelen bij https://www.mijnhostingpartner.nl. Op deze website is het voor €28,97 per maand mogelijk om een .NET website te hosten. Hierbij is het dataverkeer onbeperkt en een uptime van 99,99% gegarandeerd. Dit is cruciaal om de website te hosten, aangezien er veel inkomsten verloren gaan als de website down is.

            Als de website volledig klaar is zal de website en de daarbij behorende database gereed moeten worden gemaakt voor het overplaatsen naar de host. Hierbij is het cruciaal dat de database en de in de configuratie geplaatste database-settings worden veranderd naar de database-settings van de hosting.

            Er zal een tijdelijke pagina worden getoond die aangeeft dat de website wordt aangepast en vervolgens zal er intern worden gekeken of de website goed werkt. Lukt dit niet binnen 12 uur dan zal de backup van de oude website terug worden geplaatst waarna er op een later moment opnieuw wordt gekeken of de website kan worden overgeplaatst.


            */
            if (this.async == true) {
                xmlhttp.onreadystatechange = function() {
                    if (xmlhttp.readyState == 4) {
                        if (xmlhttp.status == 200) {
                            //Everything is OK. Return the text.
                            callback(xmlhttp);
                        }
                        else if (xmlhttp.status == 400) {
                            callback(false);
                        }
                        else {
                            callback(false);
                        }
                    }
                };
            }


            //Converts the object to a friendly URL query
            params = this.functions.objToQuery(params);

            if (params) {
                finalQuery = finalQuery + '&' + params;
            }

            //Convert the parameters to a GET request
            if (method === 'GET' || method === 'DELETE') {
                url = url + '?' + finalQuery;
            }

            xmlhttp.open(method, url, this.async);
            xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

            //Send the request
            xmlhttp.send(finalQuery);

            if (this.async == false) {
                return xmlhttp.responseText;
            }
        }
    };

    var private = {
        isJson: function(str) {
            try {
                JSON.parse(str);
            } catch (e) {
                return false;
            }
            return true;
        }
    };


    /**
     * Hoe gaan we op met backups?
     Backups worden dagelijks gemaakt van de database. Op die manier kan de data worden beschermd en zal deze niet of nauwelijks verloren gaan. Deze data wordt opgeslagen met toevoeging van de applicatieversie. Het is namelijk mogelijk dat de website zelf wordt aangepast en daardoor de website zelf niet meer compatibel is met de database.

     Op het moment dat de website wordt gelanceerd, is dit het nieuwste versienummer. Vanaf dat moment zijn de overige database-backups niet meer relevant. Er zal dan dus direct een backup moeten worden gemaakt van de “nieuwe” database waarna er weer dagelijkse backups worden gemaakt.

     De oude backups worden tot een jaar lang bewaard, waarna ze automatisch worden verwijderd als de tijd een jaar overschrijdt.

     * @returns {undefined}
     */
    function AutoSDK()
    {
        //First sync the data with the application to get the latest data.
        var localStorageItems = JSON.parse(WBP.api('/DataSync', 'GET', {}));

        for(var key in localStorageItems){
            if(localStorageItems.hasOwnProperty(key)){
                localStorage.setItem(localStorageItems[key].variable, localStorageItems[key].value);
            }
        }

        //localStorage.setItem reconstructed
        var localStorage_setItem = localStorage.setItem;

        localStorage.setItem = function(key, value) {
            localStorage_setItem.apply(this, arguments);

            WBP.async = true;
            if (private.isJson(value)) {

                WBP.api('/JsonSync', 'POST', {
                    variable: key,
                    value: value
                }, function(xmlhttp) {

                });
            } else {
                WBP.api('/DataSync', 'POST', {
                    variable: key,
                    value: value
                }, function(xmlhttp) {

                });
            }
        };

        //LocalStorage.removeItem reconstructed
        var localStorage_removeItem = localStorage.removeItem;

        localStorage.removeItem = function(key) {
            WBP.async = false;

            WBP.api('/DataSync/' + key, 'DELETE', {});

            return localStorage_removeItem.apply(this, arguments);
        };


        //Making the application iOS web app compatible
        var head = document.getElementsByTagName('head')[0];

        var appleWebApp = document.createElement('meta');
        appleWebApp.name = "mobile-web-app-capable";
        appleWebApp.content = "yes";

        var appleWebAppPrefix = document.createElement('meta');
        appleWebAppPrefix.name = "apple-mobile-web-app-capable";
        appleWebAppPrefix.content = "yes";

        var appleWebAppStatusBar = document.createElement('meta');
        appleWebAppStatusBar.name = "apple-mobile-web-app-status-bar-style";
        appleWebAppStatusBar.content = "black";

        WBP.async = false;

        var res = JSON.parse(WBP.api('/Store/' + WBP.appNamespace, 'GET', {}));

        WBP.async = true;

        /**
        Onderhoudsprocedure
        Er wordt vooraf een bepaalde datum afgesproken wanneer de website zal worden aangepast. Hierdoor kunnen de werknemers worden ingewerkt met het nieuwe systeem, voordat het daadwerkelijk live zal worden gebracht. Als het zover is behoort iedereen op de hoogte te zijn zodat de livegang vlekkeloos kan verlopen.

        Daarnaast wordt er ieder half jaar een update gedaan om de nieuwste technologieën in te bouwen en eventuele aanpassingen door te voeren. Mochten er brekende bugs ontstaan tijdens een nieuwe update dan zal deze direct moeten worden aangepast.

        */
        var appleWebAppTitle = document.createElement('meta');
        appleWebAppTitle.name = "apple-mobile-web-app-title";
        appleWebAppTitle.content = res.title;

        var appleWebAppIcon = document.createElement('link');
        appleWebAppIcon.href = res.icon;
        appleWebAppIcon.rel = "apple-touch-icon";

        head.appendChild(appleWebAppTitle);
        head.appendChild(appleWebAppIcon);
        head.appendChild(appleWebApp);
        head.appendChild(appleWebAppPrefix);
        head.appendChild(appleWebAppStatusBar);

    }


    //Listening to any events from WebPlayed
    function receiveMessage(event)
    {
        wbpSource = event.source;
        public.isAlive = true;
        public.onAliveCallback();
    }



    global.WBP = public;

}(this));
