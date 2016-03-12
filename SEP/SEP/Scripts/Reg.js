//$(document).ready(function () {
//    $("#we").click(function () {
//        $("#Ak_para").hide();
       
//    });
//});
//$(document).on("pageload",function(){
//    alert("pageload event fired - the external page has been successfully loaded and inserted into the DOM!");
//});
//
//$(document).ready(function () {
    
//    $("#ais_1").click(function(){
//        console.log("Aiss");
//    swal({
//        title: "Do you want to delete it?",
//        text: "Please check Information before Submiting!",
//        type: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#DD6B55",
//        confirmButtonText: "Save",
//        cancelButtonText: "Cancel",
//        closeOnConfirm: false,
//        closeOnCancel: false

//    },function(isConfirm)
//    {
//        if (isConfirm)
//        {
            
//             Html.ActionLink("Delete","Delete","Students1");


//        } else
//        {
//            swal("Cancelled", "You have Cancelled Form Submission!", "error");
//             Html.ActionLink("Back to List", "Index")
    
//        }
//    });


//    });

//});
        
       

//

//$(document).ready(function () {
    
//    //$("#ak_del").click(function () {
//        swal({  
//                            title: "Do you want to save it?",  
//                            text: "Please check Information before Submiting!",  
//                            type: "warning",  
//                            showCancelButton: true,  
//                            confirmButtonColor: "#DD6B55",  
//                            confirmButtonText: "Save",  
//                            cancelButtonText: "Cancel",  
//                            closeOnConfirm: true,  
//                            closeOnCancel: false 
//        },  
//       function(isConfirm)  
//      {  
//                   if (isConfirm)  
//                   {
                       
                       

//                   } else   
//                    {  
//                       swal("Cancelled", "You have Cancelled Form Submission!", "error");
//                       console.log("Aissss");
//                       setTimeout(300);
//                       window.location.href = "/Students1/Index";
                       
                      
//                    }  
//                });



//    //});


//});

//$(document).ready(function () {
//    $('[data-toggle="tooltip"]').tooltip();
//});

$(document).ready(function () {

    $("#sh_1").click(function (e) {
        e.preventDefault();
        $("#link12").show(300);
        $(this).hide();

    });
        
 });

$(document).ready(function () {


    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
});

$(document).ready(function () {
    console.log("Hanuu BAbA");
    $("#we1").click(function () {

        $("#Ak_para").hide();

        $.ajax({
            url: '/Dropdwn/Not',
            data :"",
            type: "POST",
            dataType: "json",
            success: function (data) {
                var text =data.data.length;
                console.log(text);
                $("#NotiNo").text(text);
                $("#NotiNo1").text(text);

                for (var i = 0 ; i < text ; i++) {
                    var p = data.data[i]+" ";
                    var t = p.split("Student");
                    console.log(t[0]);

                      $("<li><a><span class='photo'><img alt='avatar' src='asasas.jpg'></span> <span class='subject'> <span class='from'>"+t[0]+"</span><span class='time'>Just now</span><span class='message'>" + t[1] + "</span></a></li>").appendTo('ul.ak_slide1');
                //  $("<li> <a> <div class='task-info'><div class='desc'>" + data.data[i] + "</div></div></a></li>")
                //  .appendTo('ul.ak_slide1');
                }
                
               
            },
            error: function () {
                alert("Failed");

            }

        });
        // load....
        $.ajax({
            url: '/Dropdwn/NotLec',
            data: "",
            type: "POST",
            dataType: "json",
            success: function (data) {
                var text = data.data.length;
                console.log(text);
                for (var i = 0 ; i < text ; i++) {
                    $("<li id = 'ak2'> <a><span id = 'sp2'>" + data.data[i] + "</span><br/><input type='button' value ='Confirm' id='cnf1'/><input type='button' value ='Decline' id='cnf1'/></a></li>")
                   .appendTo('ul.ak_slide2');
                }


            },
            error: function () {
                alert("Failed");

            }

        });

    });


});


$(document).ready(function () {
    $(document).on('click', '#cnf1', function () {
        $("#Ak_para").show();
        $("#cnf1").addClass("clicked");
        var t = $(".clicked").attr("value");
        $("#sp2").addClass("clicked2");
        var t2 = $(".clicked2").html();
        var p = t2.split(" ");
        console.log(t + "Ayesh"+p[0]);
        if (t == "Confirm") {
            $.ajax({
                url: '/Dropdwn/AddLec?Name='+t2,
                data: "",
                type: "POST",
                dataType: "json",
                success: function () {
                    alert("Success");
                    $(".clicked").parent().remove();

                },
                error: function () {
                    alert("Failed");

                }

            });

        } else if (t == "Decline") {
            $.ajax({
                url: '/Dropdwn/RemoveLec?Name='+p[0],
                data: "",
                type: "POST",
                dataType: "json",
                success:function(){
                    alert("Success");

                },
                error: function () {
                    alert("Failed");

                }

            });
        }

    });
   });