
$(document).ready(function () {

    var $count = 0;
    var $allocatedTotalMark = 0;
    var $selectedIteration = "";
    var $outstandingTotal = 0;

    ///*************************************************************
    //Feeding the selected users informtions to the central grid
    //**************************************************************
    $('#ev1 li a').click(function () {
        $("#ev1 li").removeClass('Clicked');
        $(this).parent().addClass('Clicked');

        var $selectedUserName = $("#ev1 li.Clicked a").html();

        $.ajax({
            url: "/Students_module/GetStudent",
            data: { selectedUser: $selectedUserName },
            type: "POST",
            dataType: "json",
            success: function (data) {

                $("#innerImg").attr("src", data.student.Avatar);
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


    ///**********************************************
    //Adding current logged in user to his own group
    //***********************************************
    $('#btnCreate').click(function () {
        $("#btnCreate").removeClass('Clicked');
        $(this).parent().addClass('Clicked');

        var $UserId = $("#myReg").html();

        var $moduleId = $("#sessionModule").html();

        $.ajax({
            url: "/Students_module/FillMe",

            data: { loggedUserID: $UserId, currentModule: $moduleId },
            type: "POST",
            dataType: "json",
            success: function (data) {
                if (data == 2) {
                    alert("You already have a group and you can't create a new one!");
                }
                else {
                    alert("You're created a group successfully now add members to your group!!!");
                }
            },
            error: function () {
                alert("Failed in inserting loged in user to the table");
            }
        });

        $("#btnCreate").prop("disabled", true);
    });




    //***********************************************
    //Adding members to my group
    //***********************************************

    $("#addGrp").click(function () {

        var $reg = $("#lReg").html();
        console.log("leg issssssssss" + $reg);
        var $module = $("#sessionModule").html();
        var $leaderReg = $("#myReg").html();
        var $leadersGroup = "";

        if ($reg.length > 3) {
            $.ajax({
                url: "/Students_module/addToGroup",
                data: { studentId: $reg, module: $module, leaderID: $leaderReg },
                type: "POST",
                dataType: "json",
                success: function (data) {

                    $leadersGroup = data.requestedGroupNo;
                    console.log("return value issss " + data.returnValue);

                    if (data.returnValue == 1) {
                        // alert($reg + " has been successfully added to your group!!!");

                        //start in innerAjax1
                        $.ajax({
                            url: "/Students_module/FillTable",
                            data: { studentId: $reg },
                            type: "POST",
                            dataType: "json",
                            success: function (data) {
                                var currentStatus = "not clear";
                                if (data.acceptanceStatus.status == 0) {
                                    currentStatus = "Pending";
                                }
                                else if (data.acceptanceStatus.status == 1) {
                                    currentStatus = "Accepted";
                                }
                                else {
                                    currentStatus = "Rejected";
                                }
                                $("<tr><td><img id='imgIcon' src='" + (data.student.Avatar) + "'/></td><td>" + data.student.Name + "</td><td>" + currentStatus + "</td></tr>").appendTo("#Grptable");


                            },
                            error: function () {
                                alert("Failed to fill the table !");
                            }
                        });//End of innerAjax1

                        //start in innerAjax2
                        $.ajax({
                            url: "/Students_module/UpdateNotifications",
                            data: { newMemberId: $reg, group: $leadersGroup },
                            type: "POST",
                            dataType: "json",
                            success: function (data) {
                                if (data == 1) {
                                    alert("Member " + $reg + " has added to your group and notifications been send");
                                }
                                else {
                                    alert("Couldn't send the group request");
                                }
                            },
                            error: function () {
                                alert("Failed to update in to notification table");
                            }
                        });// End of inner Ajax2
                    }

                        ///if student didnt aded to the group
                    else if (data.returnValue == 3) {
                        alert("Please create a group before you add members!!");
                    }

                    else if (data.returnValue == 4) {
                        alert("This member is already in your group, Please insert a new member");
                    }

                    else if (data.returnValue == 2) {
                        alert(" You can't add more members to the group!!!");
                    }
                },
                error: function () {
                    alert("Error occure while adding members to your group");
                }
            });


        }
        else {
            alert("Please select a student to add to the group!");
        }

    });




    //********************************************************************************************
    //Marking Scheme



    //***********************************************
    //Adding criterias to the Table-add criteria button click
    //***********************************************

    $("#btnAddEachCriteria").click(function () {

        var $markOld = $("#lblWeight").text();
        var numOld = parseInt($markOld);
        console.log("markkkkkkkkkkkkkkk old " + numOld);

        var $markNew = $("#txtWeight").val();
        var numNew = parseInt($markNew);
        console.log("markkkkkkkkkkkkkkk newwww " + numNew);

        var $criteria = $("#txtarea").val();

        if ((numNew % 5) > 0) {
            alert("Please enter a divisible of 5! ");
        } else {
            $outstandingTotal = numOld + numNew;

            if ($outstandingTotal > $allocatedTotalMark) {
                alert("Invalid weight inserted, you are exeecing the total marks allocated!");
            }
            else if ($outstandingTotal < $allocatedTotalMark) {

                $count = $count + 1;
                $("#lblWeight").text($outstandingTotal);
                $("<tr><td>" + $count + "</td><td>" + $criteria + "</td><td>" + numNew + "</td><td id='linkData'><a id='linkEdit'>Edit</a> | <a id='linkDelete'>Delete</a></td></tr>").appendTo("#temporyTable1");
            }
            else {
                $count = $count + 1;
                $("#lblWeight").text($outstandingTotal);
                $("<tr><td>" + $count + "</td><td>" + $criteria + "</td><td>" + numNew + "</td><td id='linkData'><a id='linkEdit'>Edit</a>|<a id='linkDelete'>Delete</a></td></tr>").appendTo("#temporyTable1");
                alert("Criteria total reached to " + $allocatedTotalMark + "!");
            }
        }

        $("#txtarea").val(" ");
        $("#txtWeight").val(" ");

    });

    //***********************************************
    //AddNew Criteria big button click
    //***********************************************


    $('#btnCreateShema').click(function () {

        $("#divNewSchema").show();

        $.ajax({
            //url: "",
            //data: {},
            //type: "POST",
            //dataType: "json",

            success: function (data) {

            },
            error: function () {
                alert("failed to load criteria page");
            }
        });


    });



    //***********************************************
    //Enter criteria button click
    //***********************************************


    $('#btnAddCriteria').click(function () {

        $("#divInsertCriteria").show();

        $selectedIteration = $("#iterationMode option:selected").text();
        console.log("seleterdddddddddddddd " + $selectedIteration);

        $.ajax({
            url: "/MarkingSchemes/findTotalIterationMark",
            data: { typeId: $selectedIteration },
            type: "POST",
            dataType: "json",

            success: function (data) {
                $allocatedTotalMark = data.MarkValue;
                console.log("total alloocated markkkkkkkk     " + $allocatedTotalMark);

            },
            error: function () {
                alert("failed grab overall mark");
            }
        });


    });

    //***********************************************
    //Edit link click
    //***********************************************


    $(document).on("click", "#linkEdit", function () {

        console.log("i cllicked linkkkkkkkkkkkkkkk");


        //$.ajax({
        //    url: "/MarkingSchemes/findTotalIterationMark",
        //    data: { typeId: $selectedIteration },
        //    type: "POST",
        //    dataType: "json",

        //    success: function (data) {
        //        $allocatedTotalMark = data.MarkValue;
        //        console.log("total alloocated markkkkkkkk     " + $allocatedTotalMark);

        //    },
        //    error: function () {
        //        alert("failed grab overall mark");
        //    }
        //});


    });








    //final close

});






