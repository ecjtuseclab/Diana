
$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "../index.ashx?ctrl=propertymapping&action=showAll",
        height: $(window).height() - 126,
        colModel: [
            { label: "主键", name: "id", hidden: true, key: true },
            { label: '名称', name: 'propertyName', width: 150, align: 'left' },
            { label: '值', name: 'propertyValue', width: 150, align: 'left' },
            { label: '描述', name: 'propertyMeaning', width: 150, align: 'left' },
            { label: '上级', name: 'parentId', width: 150, align: 'left' },
            { label: '备注', name: 'remark', width: 150, align: 'left' }
        ],
        pager: "#gridPager",
        sortname: 'id asc',
        viewrecords: true
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: { keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}

function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增",
        url: "../index.ashx?ctrl=propertymapping&action=Form",
        width: "550px",
        height: "570px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Form",
        title: "修改",
        url: "../index.ashx?ctrl=propertymapping&action=Form&keyValue=" + keyValue,
        width: "550px",
        height: "570px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "../index.ashx?ctrl=propertymapping&action=DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Details",
        title: "查看",
        url: "../index.ashx?ctrl=propertymapping&action=Details&keyValue=" + keyValue,
        width: "550px",
        height: "620px",
        btn: null,
    });
}
function btn_copyandpaste() {
    $.deleteForm({
        url: "../index.ashx?ctrl=propertymapping&action=copyAndPasteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}