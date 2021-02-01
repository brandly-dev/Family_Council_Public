
$(document).ready(function () {
    $('#ExpandModel').modal('show');
})


$(document).on("change", "#surname", function () {
    $("#spSurname").hide();
});


function SubmitUserInfo(email, password, maritalStatus, surname, gender, age, country, city) {

    $.post("/Payment/PaidCROrder",
        data = {
            email: email,
            password: password,
            maritalStatus: maritalStatus,
            PaymentDate: paymentDate
        },

        function (data) {
            if (data.status = "success") {
                $(obj).attr("disabled", "disabled");
                $(obj).closest('tr').find('#dueDate').attr("disabled", "disabled");
                toastr.success("Supplier credit payment has been paid successfully.");
                var url = '@Url.Action("PayablePaymentFromSupplierCredit", "Payment")';
                window.location.href = url;
            }
        });

}



function ValidateEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2, 4})+$/;
    return regex.test(email);
};


$(document).on("change", "#email", function () {
    $("#spEmail").hide();
});

$(document).on("change", "#password", function () {
    $("#spPassword").hide();
});
$(document).on("change", "#confirmpassword", function () {
    $("#spConfirmPassword").hide();
});



$(document).on("change", "#surname", function () {
    $("#spSurname").hide();
});

$(document).on("change", "#age", function () {
    $("#spAge").hide();
});
$(document).on("change", "#drpCity", function () {
    $("#spCity").hide();
});


$(document).on("click", "#btnNext", function (e) {
    e.preventDefault();
    if ($("#email").val() == null || $("#email").val() == undefined || $("#email").val() == "") {
        $("#spEmail").show();
        return false;
    }
    else {

        if (!ValidateEmail($("#email").val())) {
            $("#spEmail").text("Please enter valid email");
            $("#spEmail").show();
            return false;
        }
        else {
            $("#spEmail").hide();
        }
    }

    if ($("#password").val() == null || $("#password").val() == undefined || $("#password").val() == "") {
        $("#spPassword").show();
        return false;
    }
    else {
        $("#spPassword").hide();

    }

    if ($("#confirmpassword").val() == null || $("#confirmpassword").val() == undefined || $("#confirmpassword").val() == "") {
        $("#spConfirmPassword").show();
        return false;
    }
    else {
        $("#spConfirmPassword").hide();
    }

    if ($("#password").val() === $("#confirmpassword").val()) {
        $("#spConfirmPassword").text("Required");
        $("#spConfirmPassword").hide();
    }
    else {
        $("#spConfirmPassword").text("Password and ConfirmPassword not match!");
        $("#spConfirmPassword").show();
        return false;
    }

    $('#ExpandModel').modal('hide');
    $('#ExpandModel2').modal('show');
});

$(document).on("click", "#btnNext2", function (e) {
    e.preventDefault();
    $('#ExpandModel2').modal('hide');
    $('#ExpandModel3').modal('show');
});
$(document).on("click", "#btnNext3", function (e) {
    e.preventDefault();

    if ($("#surname").val() == null || $("#surname").val() == undefined || $("#surname").val() == "") {
        $("#spSurname").show();
        return false;
    }
    else {
        $("#spSurname").hide();
    }

    if ($("#age").val() == null || $("#age").val() == undefined || $("#age").val() == "") {
        $("#spAge").show();
        return false;
    }
    else {
        $("#spAge").hide();
    }


    if ($("#drpCity").val() == null || $("#drpCity").val() == undefined || $("#drpCity").val() == "") {
        $("#spCity").show();
        return false;
    }
    else {
        $("#spCity").hide();
    }

    $("#MainForm").submit();
});



