$(function () {
    gridList();
})

function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "../index.ashx?ctrl=role&action=showAll",
        height: $(window).height() - 128,
        colModel: [
            { label: '主键', name: 'id', hidden: true },
            { label: '角色名', name: 'rolename', width: 150, align: 'left' },
            { label: '角色编号', name: 'rolecode', width: 200, align: 'left' },
            {
                label: '角色有效时间', name: 'roleexpiretime', width: 200, align: 'left'
            }
        ],
        pager: '#gridPager',
        sortname: 'id asc',
        viewrecords: true
    });
}

$("#btn_search").click(function () {
    $("#gridList").jqGrid('setGridParam', {
        postData: { keyword: $("#txt_keyword").val() },
    }).trigger('reloadGrid');
})

function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增角色",
        url: "../index.ashx?ctrl=role&action=Form",
        width: "800px",
        height: "600px",
        shade: "0",
        btn: null,
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Form",
        title: "修改角色",
        url: "../index.ashx?ctrl=role&action=Form&keyValue=" + keyValue,
        width: "800px",
        height: "600px",
        shade: "0",
        btn: null,
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Details",
        title: "查看角色",
        url: "../index.ashx?ctrl=role&action=Details&keyValue=" + keyValue,
        width: "550px",
        height: "620px",
        btn: null,
    });
}

function btn_CopyAndPaste() {
    $.deleteForm({
        url: "../index.ashx?ctrl=role&action=copyAndPasteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}

function btn_delete() {
    $.deleteForm({
        url: "../index.ashx?ctrl=role&action=DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
