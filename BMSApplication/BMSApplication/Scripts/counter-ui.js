$(document).ready(function () {
    $('#counter-table').DataTable();

    var i = 0;
    $("#myform").hide();
    $(".actionBtn").click(function () {
        i++;
        //$("#data").prop("disabled",true);
        //var str = $("#id").val();
        var button_id = $(this).attr("id");
        console.log(button_id);
        console.log($("#data1-cost").val());
        var str = $("#" + button_id + "").text();
        var cost = $("#" + button_id + "-cost").val();
        var quantity = $("#" + button_id + "-quantity").val();
        var Id = $("#" + button_id + "-id").val();
        var Sell = $("#" + button_id + "-sell").val();
        var totalCost = $("#totalCost").val();
        var tendered = $("#tendered").val();
        
        if ($("." + button_id).length > 0) {
            var valInc = $('.' + button_id).val();

            valInc++;
                $("." + button_id).val(valInc);
                $("." + button_id + "-price").val(valInc * cost);
                totalCost = parseFloat(totalCost) + parseFloat(cost);
                $("#totalCost").val(totalCost);
                $("#change").val((parseFloat(tendered) - parseFloat(totalCost)).toFixed(2));
            

        } else {
            $(`<tr id="row${i}">`
                + "<input type=\"hidden\" name=\"id[]\" class=\"" + button_id + "-id\" value= \"" + Id + "\" />"
                + `<td>${str}</td><td><input name="quantity[]" class="span1 ${button_id}" id="${button_id}-quant" type="text" value="1" /></td>`
                + `<td><input type="text" name="sell[]" class="span1 ${button_id}-sell" id="${button_id}-sell-price" value="${Sell}" /></td>`
                + `<td><input type="text" name="cost[]" class="span1 ${button_id}-price" value="${cost}" readonly/></td>`                
                + `<td><button type="button" name="remove" id="${i}" class="${button_id} btn btn-small btn-danger btn_remove actionBtn">X</button></td>` +
                +"</tr>"              
                ).appendTo("#test");
            
            $("#totalCost").val(parseFloat(totalCost) + parseFloat(cost));
            $("#change").val(parseFloat(tendered) - (parseFloat(totalCost) + parseFloat(cost)).toFixed(2));
            
        }
        
       
        var count = $("#test tr").length;
        if (count > 0) {
            $("#myform").show();
        }

    });
    $(document).on('click', '.btn_remove', function () {
        var button_id = $(this).attr("id");
        console.log(button_id);
        var priceClass = ($(this).attr("class"));
        var price = priceClass.split(" ");
        //var price = '.data'+ button_id + '-price';
        console.log($(`.${price[0]}-price`).val());
        $(`#totalCost`).val(parseFloat($(`#totalCost`).val()) - parseFloat($(`.${price[0]}-price`).val()));
        $("#change").val(parseFloat($("#tendered").val()) - parseFloat($(`#totalCost`).val()).toFixed(2));
        $('#row' + button_id + '').remove();
        var count = $("#test tr").length;
        if (count < 1) {
            $("#myform").hide();
        }
    });

    $("#tendered").change(function () {
        var totalCost = $("#totalCost").val();
        var tendered = $("#tendered").val();
        $("#change").val((parseFloat(tendered) - parseFloat(totalCost)).toFixed(2));
    });

    //PaymentMethod Dropdown List
    $('#PaymentMethod').change(function () {
        var paymentMethod = $('#PaymentMethod').val();
        var previousPrice = $(`#totalCost`).val();

        if (paymentMethod == 'Credit Card') {
            $("#previous-price").val(previousPrice);
            $("#totalCost").val(parseFloat($("#totalCost").val()) + (parseFloat($("#totalCost").val()) * 0.06));
            $("#change").val(parseFloat($("#tendered").val()) - parseFloat($(`#totalCost`).val()).toFixed(2));
            $("#credit_invoice").prop('disabled', false);
        } else{
            $("#totalCost").val($("#previous-price").val());
            $("#change").val(parseFloat($("#tendered").val()) - parseFloat($(`#totalCost`).val()).toFixed(2));
            $("#credit_invoice").prop('disabled', true);
        }
        
    });

 

    $(document).on('focus', 'input', function () {
        var id = $(this).attr("id");
        console.log(id);
        $("td").on('change', `#${id}`, function (event) {
            //var PN = $(event.target).val();
            //var ID = event.target.id;
            if (id.indexOf("quant") !== -1) {
                var quantity = $(`#${id}`).val();
                var priceKey = id.replace('quant', 'price');
                var pricePreviousValue = $(`.${priceKey}`).val();
                
                var sellKey = id.replace('quant', 'sell');
                var price = parseFloat($(`.${sellKey}`).val()) * parseFloat(quantity);
                /*console.log(quantity);
                console.log(sellKey);
                console.log(price);*/
                $(`.${priceKey}`).val(price)
                $(`#totalCost`).val(parseFloat($(`#totalCost`).val()) - parseFloat(pricePreviousValue));
                $(`#totalCost`).val(parseFloat($(`#totalCost`).val()) + parseFloat(price));
                $("#change").val(parseFloat($("#tendered").val()) - parseFloat($(`#totalCost`).val()).toFixed(2));
            } else if (id.indexOf("sell-price") !== -1) {
                var sell = $(`#${id}`).val();
                var priceKey = id.replace('sell-price', 'price');
                var pricePreviousValue = $(`.${priceKey}`).val();
                var quantityKey = id.replace('sell-price', 'quant');
                var price = parseFloat($(`#${quantityKey}`).val()) * parseFloat(sell);
                /*console.log(quantityKey);
                console.log(sell);
                console.log(price);*/
                $(`.${priceKey}`).val(price)
                $(`#totalCost`).val(parseFloat($(`#totalCost`).val()) - parseFloat(pricePreviousValue));
                $(`#totalCost`).val(parseFloat($(`#totalCost`).val()) + parseFloat(price));
                $("#change").val(parseFloat($("#tendered").val()) - parseFloat($(`#totalCost`).val()).toFixed(2));
            }
            
        });
    });




   
    
});

