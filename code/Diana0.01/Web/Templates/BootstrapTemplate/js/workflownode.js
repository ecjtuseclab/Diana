$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "../index.ashx?ctrl=workflownode&action=showAll",
        height: $(window).height() - 96,
        colModel: [
            { label: "主键", name: "id", hidden: true, key: true },
            {
                label: '所属工作流', name: 'wfid', width: 150, align: 'left',
                formatter: function (cellvalue) {
                    $.ajax({
                        url: "../index.ashx?ctrl=workflownode&action=GetSelectJson",
                        data: { keyValue: cellvalue },
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            cellvalue = data[0].text;
                        }
                    });
                    return cellvalue;
                }
            },
            { label: '节点名称', name: 'wfnodename', width: 150, align: 'left' },
            { label: '节点描述', name: 'wfnodememo', width: 150, align: 'left' },
            { label: '节点开始时间', name: 'wfnodebegintime', width: 150, align: 'left' },
            { label: '节点结束时间', name: 'wfnodeendtime', width: 150, align: 'left' },
            {
                label: '节点状态', name: 'wfnodestatus', width: 150, align: 'left',
                formatter: function (cellvalue) {
                    return cellvalue == 1 ? "启用" : "未启用";
                }
            },
            {
                label: '锁定状态', name: 'wfnodelock', width: 150, align: 'left',
                formatter: function (cellvalue) {
                    return cellvalue == 1 ? "锁定" : "未锁定";
                }
            }
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
        title: "新增节点",
        url: "../index.ashx?ctrl=workflownode&action=Form",
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
        title: "修改节点",
        url: "../index.ashx?ctrl=workflownode&action=Form&keyValue=" + keyValue,
        width: "700px",
        height: "510px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "../index.ashx?ctrl=workflownode&action=DeleteForm",
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
        title: "查看节点",
        url: "../index.ashx?ctrl=workflownode&action=Details&keyValue=" + keyValue,
        width: "700px",
        height: "510px",
        btn: null,
    });
}
function btn_copyandpaste() {
    $.deleteForm({
        url: "../index.ashx?ctrl=workflownode&action=copyAndPasteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}