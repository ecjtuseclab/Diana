$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "../index.ashx?ctrl=action&action=showAll",
        height: $(window).height() - 126,
        //pager: 'pager',
        //rowList: [10, 20, 30], //可调整每页显示的记录数   
        //rowNum: 40,
        pager: "#gridPager",
        sortname: 'id asc',
        viewrecords: true,
        colModel: [
            { label: "主键", name: "id", hidden: true, key: true },
            { label: '动作名称', name: 'actionname', width: 150, align: 'left' },
            { label: '动作编号', name: 'actioncode', width: 150, align: 'left' },
            { label: '动作所属', name: 'actionowner', width: 150, align: 'left', },
            { label: '动作类型', name: 'actiontype', width: 150, align: 'left', },
            { label: '动作地址', name: 'actionurl', width: 150, align: 'left' },
            { label: '动作参数', name: 'actionparam', width: 150, align: 'left' },
            { label: '控制器名称', name: 'controlername', width: 150, align: 'left' }
        ]
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
        title: "新增动作",
        url: "../index.ashx?ctrl=action&action=Form",
        width: "700px",
        height: "510px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Form",
        title: "修改动作",
        url: "../index.ashx?ctrl=action&action=Form&keyValue=" + keyValue,
        width: "700px",
        height: "510px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "../index.ashx?ctrl=action&action=DeleteForm",
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
        id: "Details",
        title: "查看动作",
        url: "../index.ashx?ctrl=action&action=Details&keyValue=" + keyValue,
        width: "700px",
        height: "510px",
        btn: null,
    });
}
function btn_CopyAndPaste() {
    $.deleteForm({
        url: "../index.ashx?ctrl=action&action=copyAndPasteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}