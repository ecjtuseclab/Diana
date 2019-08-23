var table = $('#role_action_Table'),
    layfm = 'role_action_layer_fm',
    searchfm = 'role_action_search_fm',
    layContent = $('#role_action_layer'),
    toolbar = "#role_action_Toolbar",
    refreshtype = "bootstrapTable",

    btn_insert = $('#btn_insert'),
    btn_update = $('#btn_update'),
    btn_remove = $('#btn_remove'),
    btn_removeAll = $('#btn_removeAll'),
    btn_copyandpaste = $('#btn_copyandpaste'),
    btn_save = $('#btn_save'),
    btn_search = $('#btn_search'),
    btn_searchReset = $('#btn_searchReset');
var tempurl,
    refreshurl = url + '&action=showAll',
    layer_width = '700px',
    layer_height = '500px';

$(table).bootstrapTable({
    url: url + '&action=showAll', //请求后台的URL（*）
    method: 'post',                             //请求方式（*）
    toolbar: toolbar,                           //工具按钮用哪个容器
    striped: 0,                                 //是否显示行间隔色
    cache: !0,                                  //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
    pagination: !0,                             //是否显示分页（*）
    sortable: 0,                                //是否启用排序
    sortOrder: "asc",                           //排序方式
    queryParams: queryParams,                   //传递参数（*）
    sidePagination: "server",                   //分页方式：client客户端分页，server服务端分页（*）
    pageNumber: 1,                              //初始化加载第一页，默认第一页
    pageSize: 10,                               //每页的记录行数（*）
    pageList: [10, 25, 50, 100],                //可供选择的每页的行数（*）
    search: !0,                                 //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
    showRefresh: !0,                            //是否显示刷新按钮
    strictSearch: !0,                           //设置为 true启用 全匹配搜索，否则为模糊搜索
    showColumns: !0,                            //是否显示所有的列
    //minimumCountColumns: 2,                     //最少允许的列数
    clickToSelect: !0,                          //是否启用点击选中行
    maintainSelected: !0,                        //设置为 true 在点击分页按钮或搜索按钮时，将记住checkbox的选择项
    //height: 500,                              //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
    uniqueId: "id",                             //每一行的唯一标识，一般为主键列
    showToggle: !0,                             //是否显示详细视图和列表视图的切换按钮
    cardView: 0,                                //是否显示详细视图
    detailView: 0,                              //是否显示父子表
    singleSelect: 0,                            //是否单选
    showExport: true,                             //是否显示导出
    exportDataType: "basic",                    //basic', 'all', 'selected'.
    checkboxHeader: !0,
    iconSize: "outline",
    icons: {
        paginationSwitchDown: 'glyphicon-collapse-down icon-chevron-down',
        paginationSwitchUp: 'glyphicon-collapse-up icon-chevron-up',
        refresh: "glyphicon-repeat",
        toggle: "glyphicon-list-alt",
        columns: "glyphicon-list",
        detailOpen: 'glyphicon-plus icon-plus',
        detailClose: 'glyphicon-minus icon-minus'
    },
    columns: [{ checkbox: true },
           { field: 'id', title: 'id', switchable: 0, editable: 0 ,visible: !0},
            { field: 'roleid', title: 'roleid', switchable: 0, editable: !0 ,visible: !0},
            { field: 'actionid', title: 'actionid', switchable: 0, editable: !0 ,visible: !0},
            { field: 'controlername', title: 'controlername', switchable: 0, editable: !0 ,visible: !0},
            { field: 'actionscode', title: 'actionscode', switchable: 0, editable: !0 ,visible: !0},
            ],
    onEditableSave: function (field, row, oldValue, $el) {
        tempurl = url + '&action=update&id=' + row.id;
        onEditableSaveCom(tempurl,refreshurl, layfm, row);
    }
});