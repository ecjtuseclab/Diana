   $(function () {
        gridList();
    })
function gridList() {
    var template1 =
    {
        "groupOp": "AND",
        "rules": [
        { "field": "username", "op": "bw", "data": "Diana" },
        { "field": "rolelist", "op": "gt", "data": "管理员" }
        ]
    };
    var template2 =
    {
        "groupOp": "AND",
        "rules": [
          { "field": "rolelist", "op": "eq", "data": "黄金会员" },
          { "field": "username", "op": "le", "data": "Diana" }
        ]
    };

    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "../index.ashx?ctrl=user&action=showAll",
        height: $(window).height() - 128,
        colModel: [
            { label: '主键', name: 'id', hidden: true },
            { label: '用户名', name: 'username', width: 150, align: 'left' },
            //{ label: '密码', name: 'password', width: 150, align: 'left' },
            { label: '角色', name: 'rolelist', width: 200, align: 'left' },
            {
                label: '分组', name: 'grouplist', width: 200, align: 'left'
            }
        ],
        //pager: "#gridPager",
        pager: '#pgrps3',
        sortname: 'id asc',
        viewrecords: true
    });
    jQuery("#gridList").jqGrid('navGrid', '#pgrps3',
    { edit: false, add: false, del: false },
    {},
    {},
    {},
    {
        multipleSearch: true,
        multipleGroup: true,
        showQuery: true,
        // set the names of the template
        "tmplNames": ["模板一", "模板二"],
        // set the template contents
        "tmplFilters": [template1, template2]
    }
    );


    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: { keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增用户",
        url: "../index.ashx?ctrl=user&action=Form",
        width: "800px",
        height: "600px",
        btn: null,
        shade:"0",
        
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Form",
        title: "修改用户",
        url: "../index.ashx?ctrl=user&action=Formedit&keyValue=" + keyValue,
        width: "800px",
        height: "600px",
        btn: null,
        shade:"0",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "../index.ashx?ctrl=user&action=DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    var pubkey = $("#gridList").jqGridRowValue().pubkey;
    var prikey = $("#gridList").jqGridRowValue().prikey;
    $.modalOpen({
        id: "Details",
        title: "查看用户",
        url: "../index.ashx?ctrl=user&action=Details&keyValue=" + keyValue + '&pubkey=' + escape(pubkey) + '&prikey= ' + escape(prikey),
        width: "700px",
        height: "550px",
        btn: null,
    });
}
function btn_CopyAndPaste() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.copyAndPasteForm({
        url: '../index.ashx?ctrl=user&action=copyAndPaste',
        param:{keyValue:keyValue},
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}

function btn_revisepassword() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    var RealName = $("#gridList").jqGridRowValue().username;
    //var password = $("#gridList").jqGridRowValue().password;
    $.modalOpen({
        id: "RevisePassword",
        title: '是否重置密码',
        url: "../index.ashx?ctrl=user&action=RevisePassword&keyValue=" + keyValue ,
        width: "200px",
        height: "120px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        },
    });
       
}
  
    
