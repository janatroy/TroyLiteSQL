
function PrintItem(ID) {

    window.showModalDialog('./ProductPurchaseBill.aspx?Req=N&SID=' + ID, self, 'dialogWidth:800px;dialogHeight:530px;status:no;dialogHide:yes;unadorned:yes;');

}

function OnKeyPress(args) {
    if (args.keyCode == Sys.UI.Key.esc) {
        $find("ctl00_cplhControlPanel_tabs2_tabMaster_ModalPopupProduct").hide();
    }
}

$("#ctl00_cplhControlPanel_tabs2_tabMaster_cmdCancelProduct").live("click", function () {
    $find("ctl00_cplhControlPanel_tabs2_tabMaster_ModalPopupProduct").hide();
});


function confirm_delete() {
    if (confirm("Are you sure you want to delete the purchase bill ?") == true)
        return true;
    else
        return false;
}
function unl() {
    document.getElementById("hdDel").value = "BrowserClose";
    document.form1.submit();
}

function openNewWin(url) {
    var x = window.open(url, 'mynewwin', 'width=600,height=600,toolbar=1');
    x.focus();
}

function setDelFlag() {
    document.getElementById("delFlag").value = "1";
}

function checkDate(sender, args) {
    if (sender._selectedDate > new Date()) {
        alert("Purchase Date cannot be greater than Today!");
        sender._selectedDate = new Date();
        // set the date back to the current date
        sender._textbox.set_Value(sender._selectedDate.format(sender._format))
    }
}

function pageLoad() {
    //  get the behavior associated with the tab control
    var tabContainer = $find('ctl00_cplhControlPanel_tabs2');

    $addHandler(document, "keydown", OnKeyPress);

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