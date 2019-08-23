$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        ExpandColumn: "layers",
        url: "../index.ashx?ctrl=area&action=showAll",
        height: $(window).height() - 126,
        colModel: [
            { label: "主键", name: "id", hidden: true, key: true },
            { label: '名称', name: 'fullname', width: 200, align: 'center' },
            { label: '级别', name: 'layers', width: 200, align: 'center' },
            { label: '编号', name: 'encode', width: 200, align: 'center' }
        ],
        pager: "#gridPager",
        sortname: 'id asc',
        viewrecords: true,
        rowList: [10, 20, 50, 100, 200],
        rowNum: 20
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
        title: "新增区域",
        url: "../index.ashx?ctrl=area&action=Form",
        width: "450px",
        height: "340px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Form",
        title: "修改区域",
        url: "../index.ashx?ctrl=area&action=Form&keyValue=" + keyValue,
        width: "450px",
        height: "340px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "../index.ashx?ctrl=area&action=DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Form",
        title: "查看区域",
        url: "../index.ashx?ctrl=area&action=Details&keyValue=" + keyValue,
        width: "450px",
        height: "340px",
        btn:null,
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}