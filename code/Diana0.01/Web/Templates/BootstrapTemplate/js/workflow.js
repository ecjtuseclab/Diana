$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $("#gridList").dataGrid({
        url: "../index.ashx?ctrl=workflow&action=showAll",
        height: $(window).height() - 126,
        pager: "#gridPager",
        sortname: 'id asc',
        viewrecords: true,
        colModel: [
            { label: "主键", name: "id", hidden: true, key: true },
            { label: '工作流名称', name: 'wfname', width: 100, align: 'left' },
            { label: '启用时间', name: 'wfbegintime', width: 150, align: 'left' },
            { label: '有效终止时间', name: 'wfstoptime', width: 150, align: 'left' },
            { label: '工作流作用表', name: 'wfownertable', width: 100, align: 'left' },
            {
                label: '工作流状态', name: 'wfstatus', width: 100, align: 'left',
                formatter: function (cellvalue) {
                    return cellvalue == 1 ? "启用" : "未启用";
                }
            },
            {
                label: ' 锁定状态 ', name: 'wflock', width: 100, align: 'left',
                formatter: function (cellvalue) {
                    return cellvalue == 1 ? "锁定" : "未锁定";
                }
            },
            { label: '业务表工作流字段', name: 'wffieldname', width: 100, align: 'left' },
        ],
        pager: '#gridPager',
        viewrecords: true,
        sortorder: "desc",
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
        title: "新增工作流",
        url: "../index.ashx?ctrl=workflow&action=Form",
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
        title: "修改工作流",
        url: "../index.ashx?ctrl=workflow&action=Form&keyValue=" + keyValue,
        width: "700px",
        height: "510px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "../index.ashx?ctrl=workflow&action=DeleteForm",
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
        title: "查看工作流",
        url: "../index.ashx?ctrl=workflow&action=Details&keyValue=" + keyValue,
        width: "700px",
        height: "510px",
        btn: null,
    });
}
function btn_copyandpaste() {
    $.deleteForm({
        url: "../index.ashx?ctrl=workflow&action=copyAndPasteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}

function btn_processdesign() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    window.location.href = "Index.ashx?ctrl=workflow&action=WorkflowVisualization&wfid=" + keyValue;
}