(function (global, $) {

    var online = navigator.onLine;
    var connection = $('.internet-connection');

    if (online) {
        connection.text('Online');
        connection.addClass('online');
    } else {
        connection.text('Offline');
        connection.removeClass('online');
    }

    $(document).on("click", '.order-done', function () {
        var id = $(this).data('id');
        console.log(id);
        order.setOrderAsDone(id);
    });


    var order = {
        getOrders: function () {
            
        },

        setOrderAsDone: function (id) {
            var confirmed = true;

            if (confirmed) {
                $.ajax({
                    url: 'http://localhost:7454/Api/Orders?Id=' + id + '&status=2',
                    data: {},
                    method: 'PUT',
                    success: function (res) {
                        console.log('Removed item ' + id);
                        $('[data-id=' + id + ']').remove();
                    },
                    error: function (res){ 
                        console.log(res);
                        alert('Could not remove ' + id);
                    }
                });
                
            }
        }
    };

    var counter = 8;
    var loadedMenus = [];

    setInterval(function () {

        $.ajax({
            url: 'http://localhost:7454/Api/Orders',
            data: {},
            method: 'GET',
            success: function (res) {

                var newItem = '';

                $.each(res, function (index, value) {

                    if (loadedMenus.indexOf(value.Id) > -1) {
                        return true;
                    } else {
                        loadedMenus.push(value.Id);

                        console.log(loadedMenus);

                        var id = value.Id;
                        var title = value.Menu.Name;
                        var tafel = value.Reservation.Seats[0].Name;
                        var description = value.Menu.Description;
                        var img = value.Menu.Image;

                        newItem += '<h3 data-id=' + id + '>' + title + '<div>' + tafel + '</div></h3>';
                        newItem += '<div data-id=' + id + '>';
                        newItem += '<div class="left-order">';
                        newItem += '<img src="' + img + '" width="128" height="128">';
                        newItem += '</div>';
                        newItem += '<div class="right-order">';
                        newItem += '<h4>Descriptie</h4>';
                        newItem += '<p>' + description + '</p>';
                        newItem += '<button class="order-done btn btn-default" data-id="' + id + '">Voltooid</button>';
                        newItem += '</div></div>';

                        $('#accordion').append(newItem);
                        $('[data-id=' + id + ']').hide().fadeIn();
                        $('#accordion').accordion("refresh");
                        newItem = '';
                    }

                    
                });                
            },

            error: function (res) {
                console.log(res);
            }
        });
        
        
        
        
    }, 1000);

}(this, jQuery));