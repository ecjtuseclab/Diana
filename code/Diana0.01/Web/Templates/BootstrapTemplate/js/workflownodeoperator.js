$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "../index.ashx?ctrl=workflownodeoperator&action=showAll",
        height: $(window).height() - 126,
        colModel: [
            { label: "主键", name: "id", hidden: true, key: true },
            {
                label: '节点动作', name: 'nodeactionid', width: 150, align: 'left',
                formatter: function (cellvalue) {
                    $.ajax({
                        url: "../index.ashx?ctrl=workflownodeoperator&action=GetSelectNaJson",
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
            {
                label: '操作者', name: 'operatorid', width: 150, align: 'left',
                formatter: function (cellvalue) {
                    $.ajax({
                        url: "../index.ashx?ctrl=workflownodeoperator&action=GetSelectUserJson",
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
            {
                label: '操作者类型', name: 'operatortype', width: 150, align: 'left',
                formatter: function (cellvalue) {
                    switch (cellvalue) {
                        case 1:
                            cellvalue = "角色";
                            break;
                        case 2:
                            cellvalue = "用户";
                            break;
                        default:
                            cellvalue = "其它";
                            break;
                    }
                    return cellvalue;
                }
            },
            { label: '开始时间', name: 'begintime', width: 150, align: 'left' },
            { label: '结束时间', name: 'endtime', width: 150, align: 'left' },
            {
                label: '操作状态', name: 'operatorstatus', width: 150, align: 'left',
                formatter: function (cellvalue) {
                    return cellvalue == 1 ? "启用" : "未启用";
                }
            },
            {
                label: '操作锁定状态', name: 'operatorlock', width: 150, align: 'left',
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
        title: "新增工作流操作者",
        url: "../index.ashx?ctrl=workflownodeoperator&action=Form",
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
        title: "修改工作流操作者",
        url: "../index.ashx?ctrl=workflownodeoperator&action=Form&keyValue=" + keyValue,
        width: "700px",
        height: "510px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "../index.ashx?ctrl=workflownodeoperator&action=DeleteForm",
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
        title: "查看工作流操作者",
        url: "../index.ashx?ctrl=workflownodeoperator&action=Details&keyValue=" + keyValue,
        width: "700px",
        height: "510px",
        btn: null,
    });
}
function btn_copyandpaste() {
    $.deleteForm({
        url: "../index.ashx?ctrl=workflownodeoperator&action=copyAndPasteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}