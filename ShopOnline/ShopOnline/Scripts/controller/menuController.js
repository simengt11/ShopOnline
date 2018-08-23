var menu = {
    init: function () {
        menu.registerEvents();
    },

    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var button = $(this);
            var id = button.data('id');
            $.ajax({
                url: "/Admin/Menu/ChangeMenuStatus/",
                data: { menuID: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        button.text('Active');
                    }
                    else {
                        button.text('Lock');
                    }
                }
            });
        });
    }
}

menu.init();