var productCatagory = {
    init: function () {
        productCatagory.registerEvents();
    },

    registerEvents: function () {
        $('.btn-active1').off('click').on('click', function (e) {
            e.preventDefault();
            var button = $(this);
            var id = button.data('id');
            $.ajax({
                url: "/Admin/Product/ChangeProductIncludeVAT/",
                data: { productID: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        button.text('Yes');
                    }
                    else {
                        button.text('No');
                    }
                }
            });
        });

        $('.btn-active2').off('click').on('click', function (e) {
            e.preventDefault();
            var button = $(this);
            var id = button.data('id');
            $.ajax({
                url: "/Admin/Product/ChangeProductStatus/",
                data: { productID: id },
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
    },
}

productCatagory.init();