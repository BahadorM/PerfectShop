

$(function () {
    CountShopCart();
});

function CountShopCart() {
    $.get("/Api/Shop", function (result) {
        $("#ShopCartCount").html(result);
    });
}


function AddProductCart(id) {
    alert("this is your Id" + id);
    $.get("/Api/Shop/" + id, function (result) {
        $("#ShopCartCount").html(result);
    });
}





