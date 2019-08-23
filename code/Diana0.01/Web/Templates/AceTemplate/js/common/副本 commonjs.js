$(function () {
    btn_insert.click(function () {
        tempurl = url + '&action=insert';
        openlayer(tempurl, refreshurl, layfm, table, 'New', layContent, layer_width, layer_height);
        ResetFrom(layfm, 1);
    });

    btn_update.click(function () {
        if (refreshtype == "" || refreshtype == "bootstrapTable") {
            var rowdata = table.bootstrapTable('getSelections');
            if (rowdata) {
                tempurl = url + '&action=update&id=' + getIdSelections(table);
                openlayer(tempurl, refreshurl, layfm, table, 'Edit', layContent, layer_width, layer_height);
                GetRowToFrom(layfm, rowdata);
            }
        }
        else if (refreshtype == "treegrid") {
            var rows = table.treegrid('getSelected');
            if (rows) {
                tempurl = url + '&action=update&id=' + rows.id;
                openlayer(tempurl, refreshurl, layfm, table, 'Edit', layContent, layer_width, layer_height);
                var rowdata = new Array()
                rowdata[0] = rows;
                GetRowToFrom(layfm, rowdata);
            }
        }
    });

    btn_remove.click(function () {
        tempurl = url + '&action=delete';
        removeclick(tempurl, refreshurl, table, '删除');
        //openRemoveLayer(tempurl, refreshurl, table, '删除');
    });

    btn_removeAll.click(function () {
        tempurl = url + '&action=deleteAll';
        //openRemoveAllLayer(tempurl, refreshurl, table, '清空数据');
    });

    btn_copyandpaste.click(function () {
        tempurl = url + '&action=copyAndPaste';
        //openRemoveLayer(tempurl, refreshurl, table, '复制粘贴');
    });

    btn_search.click(function () {
        tempurl = url + '&action=showAll';
        SearchFrom(tempurl, table, refreshtype);
    });

    btn_searchReset.click(function () {
        tempurl = url + '&action=showAll';
        ResetFrom(searchfm, 1);
        SearchFrom(tempurl, table, refreshtype);
    });
});

//layui layer打开方法
function openlayer(url, refreshurl, layfm, table, layTitle, layContent, layer_width, layer_height) {
    layer.open({
        title: layTitle,
        content: layContent,
        skin: 'layui-layer-molv', //样式类名
        area: [layer_width, layer_height],
        closeBtn: 1,
        shadeClose: true,
        type: 1,
        btn: ['确认', '关闭'],
        yes: function (index, layero) {
            //按钮【确认】的回调
            SaveForm(url, refreshurl, layfm, table, refreshtype);
            layer.close(index); //如果设定了yes回调，需进行手工关闭
        },
        btn2: function (index, layero) {
            //按钮【关闭】的回调
            //alert("取消");
            //return false 开启该代码可禁止点击该按钮关闭
        },
        cancel: function (index, layero) {
            //右上角关闭回调
            //            if (confirm('确定要关闭么')) { //只有当点击confirm框的确定时，该层才会关闭
            //                layer.close(index)
            //            }
            //return false 开启该代码可禁止点击该按钮关闭
        },
        moveOut: true, // 是否允许拖拽到窗口外
        success: function (elem) {
            $('.layui-layer-setwin').children().append('×');
            $('.layui-layer-btn0').addClass("btn btn-primary");
            $('.layui-layer-btn1').addClass("btn btn-danger");
        }
    });
};

function removeclick(url, refreshurl, table, layContent) {
    if (refreshtype == "" || refreshtype == "bootstrapTable") {
        var rowdata = $(table).bootstrapTable('getAllSelections');
        if (rowdata) {
            var ids = rowdata[0].id;
            for (var i = 1; i < rowdata.length; i++) {
                var ids = ids + ',' + rowdata[i].id;
            }
            openRemoveLayer(url, refreshurl, table, layContent, ids, refreshtype)
        }
    }
    else if (refreshtype == "treegrid") {
        var rowdata = table.treegrid('getSelected');
        if (rowdata) {
            var ids = rowdata[0].id;
            for (var i = 1; i < rowdata.length; i++) {
                var ids = ids + ',' + rowdata[i].id;
            }
            openRemoveLayer(url, refreshurl, table, layContent, ids, refreshtype)
        }
    }
};

function removeAllclick(url, refreshurl, table, layContent, ids, refreshtype) {
    openRemoveLayer(url, refreshurl, table, layContent, ids, refreshtype);
};

function openRemoveLayer(url, refreshurl, table, layContent, ids, refreshtype) {
    layer.alert('注：您确定要【' + layContent + '】该项数据吗？', {
        title: '系统提示',
        icon: "fa fa-exclamation-circle",
        skin: 'layui-layer-molv', //样式类名
        btn: ['确认', '取消'],
        shadeClose: true,
        success: function (elem) {
            $('.layui-layer-setwin').children().append('×');
            $('.layui-layer-btn0').addClass("btn btn-primary");
            $('.layui-layer-btn1').addClass("btn btn-danger");
        },
        yes: function (index, layero) {
            $.post(url, { ids: ids },
                function (result) {
                    ajax_callback(result, table，refreshurl, refreshtype);
                }, 'json');
            layer.close(index); //如果设定了yes回调，需进行手工关闭
        },
    });
};
//end layui layer打开方法

//将选中的rowdata数据赋值给form
function GetRowToFrom(layfm, rowdata) {
    //获得layfm的inputid
    var inputs = $("#" + layfm + " input");
    //获得编辑行rowdata的数据信息
    var strjson = JSON.stringify(rowdata);
    var objson = eval('(' + strjson + ')');

    for (var k = 0, l = inputs.length; k < l; k++) {
        var input = inputs[k];//循环所有layfm中的input
        //alert(input.id + ',' + input.name);
        for (var i = 0, M = objson.length; i < M; i++) {
            //alert(input.name + ',' + objson[i][input.name]);
            setValue(input.id, objson[i][input.name]);//找到rowdata数据中名称为input.name的数据，并且赋值到layfm中
        }
    }
};

var setValue = function (inputid, val) {
    if (val != "") {
        var htmlType = $("#" + inputid).attr("type");
        if (htmlType == "text" || htmlType == "textarea" || htmlType == "select-one" || htmlType == "hidden" || htmlType == "button") {
            $("#" + inputid).val(val);
        } else if (htmlType == "radio") {
            $("input[type=radio][id='" + inputid + "'][value='" + val + "']").attr("checked", true);
        } else if (htmlType == "checkbox") {
            var vals = val.split(",");
            for (var i = 0; i < vals.length; i++) {
                $("input[type=checkbox][id='" + inputid + "'][value='" + vals[i] + "']").attr("checked", true);
            }
        }
    }
};
//end 将选中的rowdata数据赋值给form

//清空form数据 type:清空类型
function ResetFrom(formid, type) {
    if (type == 1)
        $('#' + formid)[0].reset();
    else {
        $(':input', '#' + formid)
         .not(':button, :submit, :reset, :hidden')
         .val('')
         .removeAttr('checked')
         .removeAttr('selected');
    }
}
//end 清空form数据

//bootstrap通过getSelections获得选中数据的id
function getIdSelections(table) {
    return $.map($(table).bootstrapTable('getSelections'), function (row) {
        return row.id
    });
}
//end bootstrap通过getSelections获得选中数据的id

//提交form数据到后台
function SaveForm(url, refreshurl, layfm, table, refreshtype) {
    var form = new FormData(document.getElementById(layfm));
    url = url;
    $.ajax({
        url: url,
        type: "post",
        data: form,
        processData: false,
        contentType: false,
        success: function (result) {
            ajax_callback(result, table，refreshurl, refreshtype);
        }
    });
}
//end提交form数据到后台

function ajax_callback(result, table, refreshurl, refreshtype) {
    if (result.success == "true") {
        refreshTable(refreshurl, table, refreshtype);
    }
    else
        alert(result.msg);
}

//刷新Table
function refreshTable(refreshurl, table, refreshtype) {
    if (refreshtype == "" || refreshtype == "bootstrapTable")
        $(table).bootstrapTable('refresh', { url: refreshurl });
    else if (refreshtype == "treegrid")
        $(table).treegrid('reload');
}
//end刷新Table

//onEditableSave的具体方法
function onEditableSaveCom(url, refreshurl, layfm, row) {
    var rowdata = new Array();
    rowdata[0] = row;

    GetRowToFrom(layfm, rowdata);

    var form = new FormData(document.getElementById(layfm));

    $.ajax({
        url: url,
        type: "post",
        data: form,
        processData: false,
        contentType: false,
        success: function (result) {
            if (result.success == "true")
                $(table).bootstrapTable('refresh', { url: refreshurl });
            else
                alert(result.msg);
        }
    });
}
//end onEditableSave的具体方法

//将form数据序列化为json
function FormToJson(fm) {
    var formdataArray = $('#' + fm).serializeArray();
    var formJsonData = "{"; //处理成Json字符串格式，注意""的处理
    for (var i = 0; i < formdataArray.length; i++) {
        formJsonData = formJsonData + "\"" + formdataArray[i].name + "\"" + ":\"" + formdataArray[i].value + "\",";
    }
    formJsonData = formJsonData.substring(0, formJsonData.length - 1); //去掉最后的，
    formJsonData = formJsonData + "}";   //"{"username":"kwyss12","password":"1223"}"
    formJsonData = JSON.stringify(formJsonData);

    return formJsonData;
}
//end将form数据序列化为json

//为bootstrapTable重queryParams（传递参数）写的共用方法
function queryParams(params) {  //配置参数  
    var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的  
        pageSize: params.limit,                   //页面大小
        pageIndex: (params.offset / params.limit) + 1, //页码
        searchInfo: FormToJson(searchfm),
        //sort: params.sort,  //排序列名  
        //sortOrder: params.order//排位命令（desc，asc）  
    };
    return temp;
}
//end为bootstrapTable重queryParams（传递参数）写的共用方法