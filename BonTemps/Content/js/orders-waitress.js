(function (global) {
    var audio = document.querySelector('audio');
    var cards = $('#cards');

    var loadedMenus = [];

    var waitress = {
        setOrderAsDone: function (id) {
            var confirmed = true;

            if (confirmed) {
                $.ajax({
                    url: 'http://localhost:7454/Api/Orders?Id=' + id + '&status=3',
                    data: {},
                    method: 'PUT',
                    success: function (res) {
                        console.log('Removed item ' + id);
                        $('[data-id=' + id + ']').parent().remove();
                    },
                    error: function (res) {
                        console.log(res);
                        alert('Could not remove ' + id);
                    }
                });

            }
        }
    };

    $(document).on("click", '.waitress-done', function () {
        var id = $(this).data('id');
        console.log(id);
        waitress.setOrderAsDone(id);
    });

    //Simulate an order
    setInterval(function () {

        $.ajax({
            url: 'http://localhost:7454/Api/Orders?status=2',
            data: {},
            method: 'GET',
            success: function (res) {
                var newItem = '';

                $.each(res, function (index, value) {
                    if (loadedMenus.indexOf(value.Id) > -1) {
                        return true;
                    } else {
                        loadedMenus.push(value.Id);

                        var id = value.Id;
                        var title = value.Menu.Name;
                        var table = value.Reservation.Seats[0].Name;
                        var img = value.Menu.Image;

                        var currentTime = new Date();
                        var hours = currentTime.getHours();
                        var minutes = currentTime.getMinutes();
                        var seconds = currentTime.getSeconds();

                        if (minutes < 10) {
                            minutes = "0" + minutes;
                        }
                        if (seconds < 10) {
                            seconds = "0" + seconds;
                        }

                        var time = hours + ':' + minutes + ':' + seconds;

                        var newCard = '<div class="card" style="display:none;">';
                        newCard += '<h2>' + title + '</h2>';
                        newCard += '<div class="img" style="background-image:url(\'' + img + '\')"></div>';
                        newCard += '<span class="time">' + table + '</span>';
                        newCard += '<span class="time">Tijd van aankomst: ' + time + '</span>';
                        newCard += '<button class="waitress-done btn btn-default" data-id="' + id + '">Voltooid</button>';
                        newCard = $(newCard);

                        cards.append(newCard);
                        newCard.slideDown();
                        audio.play();

                    }

                    console.log(value);
                });
            },
            error: function () {

            }
        });
        /*
        var title = 'Bruine bonen';
        var img = 'http://2.bp.blogspot.com/-EtHIA7UKXAg/T_YJ-KUBFrI/AAAAAAAAApM/NVfbnlq31lY/s1600/008+(4).JPG';
        var table = 'Tafel 2';
        var time = '10:50';
        
        var newCard = '<div class="card" style="display:none;">';
        newCard += '<h2>' + title + '</h2>';
        newCard += '<div class="img" style="background-image:url(\'' + img + '\')"></div>';
        newCard += '<span class="time">' + table + '</span>';
        newCard += '<span class="time">Tijd van aankomst: ' + time + '</span>';

        newCard = $(newCard);

        cards.prepend(newCard);
        newCard.slideDown();
        audio.play();*/

    }, 2000);


}(this));