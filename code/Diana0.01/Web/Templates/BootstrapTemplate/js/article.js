
$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "../index.ashx?ctrl=article&action=showAll",
        height: $(window).height() - 126,
        colModel: [
            { label: "主键", name: "id", hidden: true, key: true },
            { label: '标题', name: 'title', width: 150, align: 'left' },
            {
                label: '类型', name: 'type', width: 150, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    switch (cellvalue) {
                        case 1: return "平台介绍"; break;
                        case 2: return "使用说明"; break;
                    }
                }
            },
            { label: '修改时间', name: 'edittime', width: 150, align: 'left' }
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
    window.location.href = '../index.ashx?ctrl=article&action=Form';
}
//function btn_add() {
//    $.modalOpen({
//        id: "Form",
//        title: "新增分组",
//        url: "../index.ashx?ctrl=article&action=Form",
//        width: "550px",
//        height: "570px",
//        callBack: function (iframeId) {
//            top.frames[iframeId].submitForm();
//        }
//    });
//}

function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.ajax({
        url: "../index.ashx?ctrl=article&action=Form",
        async: false,
        success: function (data) {
            location.href = '../index.ashx?ctrl=article&action=Form&keyValue=' + keyValue ;
        }
    });
}
//function btn_edit() {
//    var keyValue = $("#gridList").jqGridRowValue().id;
//    $.modalOpen({
//        id: "Form",
//        title: "修改",
//        url: "../index.ashx?ctrl=article&action=Form&keyValue=" + keyValue,
//        width: "550px",
//        height: "100%",
//        callBack: function (iframeId) {
//            top.frames[iframeId].submitForm();
//        }
//    });
//}
function btn_delete() {
    $.deleteForm({
        url: "../index.ashx?ctrl=article&action=DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
//function btn_details() {
//    var keyValue = $("#gridList").jqGridRowValue().id;
//    $.modalOpen({
//        id: "Details",
//        title: "详情",
//        url: "../index.ashx?ctrl=article&action=Details&keyValue=" + keyValue,
//        width: "550px",
//        height: "620px",
//        btn: null,
//    });
//}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    window.location.href = '../index.ashx?ctrl=article&action=Details&keyValue=' + keyValue;
}
//function btn_copyandpaste() {
//    $.deleteForm({
//        url: "../index.ashx?ctrl=article&action=copyAndPasteForm",
//        param: { keyValue: $("#gridList").jqGridRowValue().id },
//        success: function () {
//            $.currentWindow().$("#gridList").trigger("reloadGrid");
//        }
//    })
//}