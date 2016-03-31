$(document).ready(function () {

    $("#calNumGrps").click(function () {

      //  document.getElementById('lblq').style.display = "none";

        var div = document.getElementById('div_q');
        div.style.visibility = 'visible';

        var $onetime = $("#timeonegrp").val();
        var $stime = $("#startTime").val();
        var $etime = $("#endTime").val();
        var $intervl = $("#txtinterval").val();

        var $timepergrp = parseInt($onetime) + parseInt($intervl)

        if ($onetime == "" || $stime == "" || $etime == "") {
            alert("Enter Valid Values!");
        }

        var $shr = $stime.substr(0, 2);
        var $smin = $stime.substr(3, 2);

        var $ehr = $etime.substr(0, 2);
        var $emin = $etime.substr(3, 2);

      //  alert($emin)
        //alert($smin)

        if ($shr > $ehr) {
            alert("Check Your time values!")       
        }
        else {

            if ($emin < $smin) {
                alert("Wede Hariiiii")
                var $difMin = (parseInt($emin) + (60)) - parseInt($smin);            
                var $difHr = (parseInt($ehr) - 1) - parseInt($shr);               
                var $totMins = $difMin + ($difHr * 60);
                var $grps = $totMins / parseInt($timepergrp);
                var $stringgrps = $grps.toString();
              //  alert($grps);
               // alert($stringgrps);            
                if ($stringgrps.includes(".")) {
                    var $round = $stringgrps.substring(0, $stringgrps.indexOf("."));
                    document.getElementById('groups').value = $round.toString();
                } else {
                    document.getElementById('groups').value = $stringgrps;
                    
                }
                
            }
            else {
                var $difMin = parseInt($emin) - parseInt($smin);
                var $difHr = parseInt($ehr) - parseInt($shr);
                var $totMins = $difMin + ($difHr * 60);
                var $grps = $totMins / parseInt($timepergrp);

                var $stringgrps = $grps.toString();

                if ($stringgrps.includes(".")) {
                    var $round = $stringgrps.substring(0, $stringgrps.indexOf("."));
                    document.getElementById('groups').value = $round.toString();
                } else {
                    document.getElementById('groups').value = $stringgrps;
                }             
            }
        }     

    });


    $("#btnYes").click(function () {
        var div2 = document.getElementById('div_yes');
        div2.style.visibility = 'visible';

    });

    $("#btnNo").click(function () {

        var $grps = $("#groups").val();

        var div2 = document.getElementById('div_yes');
        div2.style.visibility = 'visible';

        document.getElementById('finalgrp').disabled = true;

        document.getElementById('finalgrp').value = $grps;
      //  var txtbx = document.getElementById('finalgrp');
       // txtbx.removeAttribute("disabled");
    });
   
    $("#btnCreateSched").click(function () {
    //    var $grps = $("#groups").val();
        document.getElementById('startTimeCol').value="11.30";
        alert("aaaaaaaaaa");

    });

});