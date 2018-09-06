var cart = {
    init: function () {
        cart.registerEvents();
    },

    registerEvents: function () {
        $(document).on('click','#btnRefresh', function () {
            var listProduct = $(".txtQuanlity");
            var cartList = [];

            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quanlity: $(item).val(),
                    product: {
                        ID: $(item).data('id')
                    }
                });
            })
            var temp = JSON.stringify(cartList)
            $.ajax({
                url: "/Cart/Update",
                data: { cartModel: temp  },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        window.location.href = "/cart";
                    }
                }
            });
        });

        $(document).on('click', '#btnDelete', function () {
            var id = $(".txtQuanlity");
            if (confirm ('Do you want delete this product?')) {
                $.ajax({
                    url: "/Cart/Delete",
                    data: { id: id.data("id") },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        if (response.status == true) {
                            window.location.href = "/cart";
                        }
                    }
                });
            }
                
            
        });
            
    },

}

cart.init();