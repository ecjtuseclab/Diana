   $(function () {
        gridList();
    })
function gridList() {
    var template1 =
   {
       "groupOp": "AND",
       "rules": [
       { "field": "resourcename", "op": "bw", "data": "用户管理" },
       { "field": "resourcetype", "op": "gt", "data": "1" }
       ]
   };
    var template2 =
    {
        "groupOp": "AND",
        "rules": [
          { "field": "resourcetype", "op": "eq", "data": "2" },
          { "field": "resourcename", "op": "le", "data": "角色管理" }
        ]
    };

    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        ExpandColumn: "description",
        url: "../index.ashx?ctrl=resource&action=showAll",
        height: $(window).height() - 126,
        colModel: [
            { label: "主键", name: "id", hidden: true, key: true },
            { label: '名称', name: 'resourcename', width: 200, align: 'left' },
            { label: '描述', name: 'description', width: 200, align: 'left' },
            { label: '路径', name: 'resourceurl', width: 200, align: 'left' },
            //{ label: '类型', name: 'resourcetype', width: 50, align: 'left' },
            {
                label: '类型', name :'resourcetype',width: 50, align: 'left',
                formatter: function(cellvalue, options, rowObject){
                    switch (cellvalue) {
                        case 1: return "菜单"; break;
                        case 2: return "工具条"; break;
                        case 3: return "按钮"; break;
                        case 4: return "脚本"; break;
                        case 5: return "文件"; break;
                    }
                }
            },
            { label: '级别', name: 'resourceleval', width: 50, align: 'left' },
            { label: "样式", name: "resourceclass", width: 100, align: 'left' },
            { label: '左侧图标', name: 'resourceleftico', width: 120, align: 'left' },
            { label: '右侧图标', name: 'resourcerightico', width: 120, align: 'left' },
            { label: '预警数量', name: 'resourcenumber', width: 50, align: 'left' },
            { label: '排序', name: 'order', width: 50, align: 'left' },
            { label: '脚本', name: 'resourceBootstrapscript', width: 200, align: 'left' }
        ],
        pager: '#pgrps3',
        sortname: 'id asc',
        //viewrecords: true,
        //rowList: [10, 20, 30],
        //rowNum: 10,

        rowNum: 5, //每页记录数  
        rowList: [5, 10, 20, 30], //每页记录数可选列表  
        viewrecords: true, //显示记录数信息，如果这里设置为false,下面的不会显示 recordtext: "第{0}到{1}条, 共{2}条记录", //默认显示为{0}-{1} 共{2}条 scroll: false, //滚动翻页，设置为true时翻页栏将不显示  

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
        title: "新增菜单",
        url: "../index.ashx?ctrl=resource&action=Form",
        width: "800px",
        height: "660px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().id;
    $.modalOpen({
        id: "Form",
        title: "修改菜单",
        url: "../index.ashx?ctrl=resource&action=Form&keyValue=" + keyValue,
        width: "800px",
        height: "660px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "../index.ashx?ctrl=resource&action=DeleteForm",
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
        title: "查看菜单",
        url: "../index.ashx?ctrl=resource&action=Details&keyValue=" + keyValue,
        width: "800px",
        height: "640px",
        btn: null,
    });
}
function btn_copyandpaste() {
    $.deleteForm({
        url: "../index.ashx?ctrl=resource&action=copyAndPasteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().id },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })

}
