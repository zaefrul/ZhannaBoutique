$(document).ready(function () {
    $('select').select2({
        allowClear: true
    });
    $(document).on("click", "#btnAddItem", function () {
        $('select').select2("destroy");
        $("#original").clone().removeAttr("id").appendTo($("#invoiceitem"));
        $('select').select2();
    });
    $(document).on("click", "#removeitem", function () {
        $(this).parent().parent().remove();
        $("#total").change();
    });
    $(document).on("change", "#varietyselect", function () {
        var slctbtn = $(this);
        $.get(
            "../api/avarieties",
            {
                id: slctbtn.val()
            },
            function (data) {
                slctbtn.parent().parent().find("#cost").val(data.Product.Cost);
                $(".invoiceitemform").change();
            }).fail(function () {
                alert("Failed to get Product data")
            });
    });
    $(document).on("change", ".invoiceitemform", function () {
        var changedom = $(this);
        var cost = changedom.parent().parent().find("#cost").val();
        var qty = changedom.parent().parent().find("#qty").val();
        changedom.parent().parent().find("#total").val(cost * qty);
        $("#total").change();
    });
    $(document).on("change", "#total", function () {
        var total = 0;
        $('input[name="total[]"]').each(function () {
            total += parseFloat(this.value);
        });
        $("#grandtotal").val(total);
    });
    //Validation
    $("#InvoiceForm").submit(function (event) {

    });
});