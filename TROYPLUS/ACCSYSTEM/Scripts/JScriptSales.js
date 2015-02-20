

        /*@cc_on@*/
        /*@if (@_win32 && @_jscript_version>=5)

        function window.confirm(str) {
            execScript('n = msgbox("' + str + '","4132")', "vbscript");
            return (n == 6);
        }

        @end@*/

        $("#ctl00_cplhControlPanel_tabs2_tabMaster_cmdCancelProduct").live("click", function () {
            $find("ctl00_cplhControlPanel_tabs2_tabMaster_ModalPopupProduct").hide();
        });

        function pageLoad() {

            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_tabs2');

            $addHandler(document, "keydown", OnKeyPress);
            
            //if($('#ctl00_cplhControlPanel_tabs2_tabMaster_cmdCancelProduct') != null)
            //$addHandler($('#ctl00_cplhControlPanel_tabs2_tabMaster_cmdCancelProduct'), "onclick", OnProductCancel);
            //var popup = $find('ctl00_cplhControlPanel_tabs2_tabMaster_ModalPopupExtender1'); 
            //popup.add_shown(SetzIndex); 

            if (tabContainer != null) {
                //  get all of the tabs from the container
                var tabs = tabContainer.get_tabs();

                //  loop through each of the tabs and attach a handler to
                //  the tab header's mouseover event
                for (var i = 0; i < tabs.length; i++) {
                    var tab = tabs[i];

                    $addHandler(
                tab.get_headerTab(),
                'mouseover',
                Function.createDelegate(tab, function () {
                    tabContainer.set_activeTab(this);
                }
            ));
                }
            }
        }

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Bill Date cannot be greater than Today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

        function ShowSalesPopUp() {
            $("#ctl00_uProcess1").attr("display", "inline");
            $find("ctl00_cplhControlPanel_ModalPopupSales").show();
        }

        function OnKeyPress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                $find("ctl00_cplhControlPanel_tabs2_tabMaster_ModalPopupProduct").hide();

            }
        }

        function OnProductCancel() {
            $find("ctl00_cplhControlPanel_tabs2_tabMaster_ModalPopupProduct").hide();
        }

        function ActivateTab(index, control) {

             var tabContainer = $find('ctl00_cplhControlPanel_tabs2');

             if (tabContainer != null) {
                 //  get all of the tabs from the container
                 var tabs = tabContainer.get_tabs();

                 //  loop through each of the tabs and attach a handler to
                 //  the tab header's mouseover event
                 var tab = tabs[index];
                 tabContainer.set_activeTab(tab);
                 control.focus();
             }

        }

        function OnBankselectedChange(val) {

            if (val == "1")
                $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard1").focus();
            else if (val == "2")
                $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard2").focus();
            else if (val == "3")
                $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard3").focus();
        }

        function ValidateMultiPayment() {

            if ($("#ctl00_cplhControlPanel_tabs2_tabMaster_drpPaymode").val() == "4") {

                if (
                (($("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank1").val() == "0") && ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount1").val() == "")) &&
                (($("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank1").val() == "0") && ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount2").val() == "")) &&
                (($("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank1").val() == "0") && ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount3").val() == "")) &&
                ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount1").val() == "")
                )
                {
                    alert('Please enter the Customer Payment details and try again.');
                    ActivateTab(1, $("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank1"));
                    return window.event.returnValue = false;
                }


                if (($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount1").val() != "") || ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard1").val() != "")) {
                    if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank1").val() == "0") {
                        alert("Bank is Mandatory");
                        ActivateTab(1, $("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank1"));
                        return window.event.returnValue = false;
                    }
                }

                if (($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount2").val() != "") || ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard2").val() != "")) {
                    if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank2").val() == "0") {
                        alert("Bank is Mandatory");
                        ActivateTab(1, $("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank2"));
                        return window.event.returnValue = false;
                    }
                }

                if (($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount3").val() != "") || ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard3").val() != "")) {
                    if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank3").val() == "0") {
                        alert("Bank is Mandatory");
                        ActivateTab(1, $("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank3"));
                        return window.event.returnValue = false;
                    }
                }

                if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank1").val() != "0") {

                    if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount1").val() == "" || $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount1").val() == "0") {

                        alert("Payment Amount is mandatory and should be Greater than Zero");
                        ActivateTab(1, $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount1"));
                        return window.event.returnValue = false;
                    }

                    if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard1").val() == "") {

                        alert("Card/Cheque for Customer Payment is mandatory.");
                        ActivateTab(1, $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard1"));
                        return window.event.returnValue = false;
                    }

                }

                if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank2").val() != "0") {

                    if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount2").val() == "" && $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount2").val() == "0") {
                        alert("Payment Amount is mandatory and should be Greater than Zero");
                        ActivateTab(1, $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount2"));
                        return window.event.returnValue = false;
                    }

                    if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard2").val() == "") {

                        alert("Card/Cheque for Customer Payment is mandatory");
                        ActivateTab(1, $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard2"));
                        return window.event.returnValue = false;
                    }
                }

                if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_ddBank3").val() != "0") {

                    if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount3").val() == "" && $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount3").val() == "0") {
                        alert("Payment Amount is mandatory and should be Greater than Zero");
                        ActivateTab(1, $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtAmount3"));
                        return window.event.returnValue = false;
                    }

                    if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard3").val() == "") {

                        alert("Card/Cheque for Customer Payment is mandatory");
                        ActivateTab(1, $("#ctl00_cplhControlPanel_tabs2_TabPanel1_txtCCard3"));
                        return window.event.returnValue = false;
                    }
                }

            }

            return true;

        }

        function ConfirmSMS() {


            if (Page_IsValid) {
                var confSMS = document.getElementById('ctl00_cplhControlPanel_hdSMS').value;

                var confSMSRequired = document.getElementById('ctl00_cplhControlPanel_hdSMSRequired').value;

                var txtMobile = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_txtCustPh');

                var custCLimit = parseFloat(document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCustCreditLimit').value);

                var custCredit = parseFloat(document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdBalance').value);

                var allowSales = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdAllowSales').value;
                
                var creditEXD = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCREDITEXD').value;

                var totalSalesVal = parseFloat(document.getElementById('ctl00_cplhControlPanel_tabs2_TabPanel2_lblNet').innerHTML);

                var ddPaymode = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_drpPaymode');

                var ipayMode = ddPaymode.options[ddPaymode.selectedIndex].value;

                if (ipayMode == "4") {

                    if (!ValidateMultiPayment()) {
                        return false;
                    }

                    var sTotal = 0.0;
                    var rTotal = 0.0;

                    if ($("#ctl00_cplhControlPanel_tabs2_TabPanel1_lblReceivedTotal").text() != "")
                        rTotal = parseFloat($("#ctl00_cplhControlPanel_tabs2_TabPanel1_lblReceivedTotal").text());

                    if ($("#ctl00_cplhControlPanel_tabs2_tabMaster_lblNet").text() != "")
                        sTotal = parseFloat($("#ctl00_cplhControlPanel_tabs2_TabPanel2_lblNet").text());

                    if (rTotal < sTotal) {

                        var diffAmount = sTotal - rTotal;
                        var r = confirm("Warning: Total Customer Payment is less than Total Sales Amount. The Difference Amount of " + diffAmount + " will be treated as Credit Amount. Do you wish to continue?");
                        
                        if (r == false) {
                            return window.event.returnValue = false;
                        }
                    }

                }

                if (ipayMode == "3") {

                    if ((custCredit + totalSalesVal) > custCLimit) {

                        if (creditEXD == "YES") {

                            if (allowSales == "YES") {

                                var rv2 = confirm("Warning: Customer is above Credit Limit of " + custCLimit + " and within Days Limit. Do you wish to continue?");

                                if (rv2 == true) {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "YES";
                                    //return window.event.returnValue = true;
                                }
                                else {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "NO";
                                    return window.event.returnValue = false;
                                }
                            }
                            else {

                                var rv2 = confirm("Warning: Customer is above Credit Limit of " + custCLimit + " and has Exceeded Days Limit. Do you still wish to continue?");

                                if (rv2 == true) {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "YES";
                                    //return window.event.returnValue = true;
                                }
                                else {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "NO";
                                    return window.event.returnValue = false;
                                }
                            }

                        }
                        else {
                            alert("Customer has exceeded the Credit Limit of " + custCLimit + ". No Further sales are allowed");
                            return window.event.returnValue = false;
                        }

                    }
                    else {

                        if (allowSales == "NO") {

                            if (creditEXD == "YES") {

                                var rv3 = confirm("Warning: Customer has Exceeded his Credit Limit Days however with in Credit Limit of " + custCLimit + ". Do you still wish to continue?");

                                if (rv3 == true) {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "YES";
                                    //return window.event.returnValue = true;
                                }
                                else {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "NO";
                                    return window.event.returnValue = false;
                                }

                            }
                            else {
                                alert("Customer has exceeded the Credit Limit Days. No Further sales are allowed");
                                return window.event.returnValue = false;
                            }
                        }
                        
                    }
                    
                }

                if (txtMobile.value != "") {
                    if (confSMSRequired == "YES") {
                        var rv = confirm("Do you want to send SMS to Customer?");

                        if (rv == true) {
                            document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "YES";
                            return true;
                        }
                        else {
                            document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "NO";
                            return true;
                        }
                    }
                }
            }
        }

        function SetSMSValues(v, m, f) {
            if (v == true) {
                document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "YES";
                return true;
            }
            else {
                document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "NO";
                return true;
            }

        }

        function SetSMSValuesUpdate(v, m, f) {
            if (v == true) {
                document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "YES";
            }
            else {
                document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "NO";
            }

        }

        function ShowBillRegPrompt(v, m, f) {
            if (v == true) {
                return window.event.returnValue = true;
            }
            else {
                return window.event.returnValue = false;
            }

        }

        function ConfirmSMSUpdate() {

            if (Page_IsValid) {

                var confSMS = document.getElementById('ctl00_cplhControlPanel_hdSMS').value;

                var confSMSRequired = document.getElementById('ctl00_cplhControlPanel_hdSMSRequired').value;

                var txtMobile = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_txtCustPh');

                var custCLimit = parseFloat(document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCustCreditLimit').value);

                var prevMode = parseFloat(document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdPrevMode').value);

                var custCredit = parseFloat(document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdBalance').value);

                var allowSales = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdAllowSales').value;

                var creditEXD = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCREDITEXD').value;

                var prevSalesTotal = parseFloat(document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdPrevSalesTotal').value);

                var totalSalesVal = parseFloat(document.getElementById('ctl00_cplhControlPanel_tabs2_TabPanel2_lblNet').innerHTML);

                var ddPaymode = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_drpPaymode');

                var ipayMode = ddPaymode.options[ddPaymode.selectedIndex].value;

                if (ipayMode == "3") {

                    var currTotal = 0;

                    if (prevMode == "3") {
                        currTotal = custCredit + (totalSalesVal - prevSalesTotal);
                    }
                    else {
                        currTotal = custCredit + totalSalesVal;
                    }

                    if (currTotal > custCLimit) {

                        if (creditEXD == "YES") {

                            if (allowSales == "YES") {

                                var rv2 = confirm("Warning: Customer is above Credit Limit of " + custCLimit + " and within Days Limit. Do you wish to continue?");

                                if (rv2 == true) {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "YES";
                                    //return window.event.returnValue = true;
                                }
                                else {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "NO";
                                    return window.event.returnValue = false;
                                }
                            }
                            else {

                                var rv2 = confirm("Warning: Customer is above Credit Limit of " + custCLimit + " and has Exceeded Days Limit. Do you still wish to continue?");

                                if (rv2 == true) {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "YES";
                                    //return window.event.returnValue = true;
                                }
                                else {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "NO";
                                    return window.event.returnValue = false;
                                }

                            }
                        }
                        else {

                            alert("Customer has exceeded the Credit Limit of " + custCLimit + ". No Further sales are allowed");
                            return window.event.returnValue = false;

                        }

                    }
                    else {

                        if (allowSales == "NO") {

                            if (creditEXD == "YES") {

                                var rv3 = confirm("Warning: Customer has Exceeded his Credit Limit Days however with in Credit Limit of " + custCLimit + ". Do you still wish to continue?");

                                if (rv3 == true) {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "YES";
                                    //return window.event.returnValue = true;
                                }
                                else {
                                    document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hdCreditSMS').value = "NO";
                                    return window.event.returnValue = false;
                                }

                            }
                            else {
                                alert("Customer has exceeded the Credit Limit Days. No Further sales are allowed");
                                return window.event.returnValue = false;
                            }
                        }

                    }
                }

                if (txtMobile.value != "") {
                    if (confSMSRequired == "YES") {

                        var rv = confirm("Do you want to send SMS to Customer?");

                        if (rv == true) {
                            document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "YES";
                        }
                        else {
                            document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "NO";
                        }
                    }
                }

            }
        }

        function PrintItem(ID) {
            window.showModalDialog('./PrintProductSalesBill.aspx?Req=N&SID=' + ID, self, 'dialogWidth:800px;dialogHeight:530px;status:no;dialogHide:yes;unadorned:no;');
        }

        function Mobile_Validator() {
            var txtMobile = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_txtCustPh').value;

            if (txtMobile.length > 0) {
                if (txtMobile.length != 10) {
                    alert("Customer Mobile Number should be minimum of 10 digits.");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }

                if (txtMobile.charAt(0) == "0") {
                    alert("Customer Mobile should not start with Zero. Please remove Zero and try again.");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else {
                Page_IsValid = true;
            }
        }

        function SetzIndex(sender, args) {
            sender._container.style.zIndex = 9990001;
        }

        ShowModalPopup('ctl00_cplhControlPanel_tabs2_tabMaster_pnlPopup', 9990001);

        function ShowModalPopup(modalPopupId, zIndex) {
            try {
                if (modalPopupId == null)
                    return;

                var modalPopupBehavior = $find(modalPopupId);
                if (modalPopupBehavior == null)
                    return;
                zIndex = typeof (zIndex) != 'undefined' ? zIndex : null;
                if (zIndex != null) {
                    modalPopupBehavior._backgroundElement.style.zIndex = zIndex;
                    modalPopupBehavior._foregroundElement.style.zIndex = zIndex + 1;
                }
                modalPopupBehavior.show();
            }
            catch (ex) {
                alert('Exception in ShowModalPopup: ' + ex.message);
            }
        }

