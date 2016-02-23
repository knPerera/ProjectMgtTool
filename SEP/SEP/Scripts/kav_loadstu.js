console.log("sasasas");
$(document).ready(function () {
    console.log("Starttttttttt");
    var $count = 0;
    var $Reg = "IT14121578";



    $('#ev1 li a').click(function () {
        $("#ev1 li").removeClass('Clicked');
        $(this).parent().addClass('Clicked');

        var $t = $("#ev1 li.Clicked a").html();
        console.log($t);

        $.ajax({
            url: "/Students_module/GetStudent?id=" + $t,
            data: "",
            type: "POST",
            dataType: "json",
            success: function (data) {
                //var js=jQuery.parseJSON(data);

                // var obj = JSON.parse(data);
                $("#kkkk").attr("src", data.student.Avatar);
                $("#tname").html(data.student.Name);
                $("#lReg").html(data.student.RegistrationNo);
                $("#tgpa").html(data.student.CGPA);
                $("#tphone").html(data.student.ContactNo);
                $("#temail").html(data.student.Email);
            },
            error: function () {
                alert("Failed");
            }
        });
    });

    //Adding current user to the group
    //**********************************

    $('#creategrp').click(function () {
        $("#creategrp").removeClass('Clicked');
        $(this).parent().addClass('Clicked');

        console.log("Kaveeshaaaa");

        var $lable = $("#myReg").html();
        console.log($lable);

        $.ajax({
            url: "/Students_module/FillMe?reg=" + $lable,
            data: "",
            type: "POST",
            dataType: "json",
            success: function (data) {
                if (data != 0) {
                    $("creategrp").visible;
                    $(this).hide;
                }
            },
            error: function () {
                alert("Failed in inserting loged in user to the table");
            }
        });
    });




    //***************
    $("#addGrp").click(function () {

        var $reg = $("#lReg").html();
        console.log($reg);

        $.ajax({
            url: "/Students_module/addToGroup?sreg=" + $reg,
            data: "",
            type: "POST",
            dataType: "json",
            //    success: function (data) {

            //        if (data == 0) {
            //            alert("You can't add more than 4 members!!!");
            //            $("#btnAddGrp").disabled;
            //            $("#txtstuCount").html($StuCount + "");
            //        }
            //        else {
            //            var $StuCount = 1 + data;
            //            $("#txtstuCount").html($StuCount + "");
            //        }
            //       // //console.log(data);
            //       // //alert("success " + data);
            //       // var $StuCount = 1+data;
            //       //// alert("success " + $StuCount);
            //       // if ($StuCount > 4)
            //       // {
            //       //     alert("You can't add more than 4 members!!!")
            //       // }
            //       // else {
            //       //   //  alert("kakaakak");
            //       //     $("#txtstuCount").html($StuCount + "");
            //       // }

            //    },
            //    error: function () {
            //        alert("Failed");
            //    }
            //});


        });

        //Ajax function to get the selected student in to the group



    });

});