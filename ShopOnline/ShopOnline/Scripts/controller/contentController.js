var content = {
    init: function () {
        content.registerEvents();
    },

    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var button = $(this);
            var id = button.data('id');
            $.ajax({
                url: "/Admin/Content/ChangeContentStatus/",
                data: { contentID: id },
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

content.init();