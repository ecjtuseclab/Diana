var _vm;
$(function () {
    var vm = new ViewModel();
    _vm = vm;
    ko.applyBindings(vm);
    vm.Init();
});

function ViewModel() {
    var me = this;
    ViewModelBase.call(me);
    vmExtend.call(me);//读取所有数据

    me.DeleteUrl = "../index.ashx?ctrl=resource&action=DeleteForm";
    me.Dialog = new Dialog(me);

    //详情页
    me.DetailDialog = new DetailDialog(me);
    me.Detail = function () {
        me.DetailDialog.Open(me.DataTable.SelectedModel(), "详情");
    }

    //类型
    me.FormatOpenTarget = function (val) {
        if (val == "1") {
            return "菜单";
        } else if (val == "2") {
            return "工具条";
        } else if (val == "3") {
            return "按钮";
        } else if (val == "4") {
            return "脚本";
        } else if (val == "5") {
            return "文件";
        }
        return val;
    }

    //资源上级显示文本值
    var optionList; //资源id与名称的对应关系
    $ace.get("../index.ashx?ctrl=resource&action=GetResourceListSelectOption", function (result) {
        optionList = result;
    })
    me.getResourceName = function (value)
    {
        var text = "";
        var len = optionList.length;
        for (var i = 0; i < len; i++) {
            if (optionList[i]["Value"] == value) {
                text = optionList[i]["Text"];
                break;
            }
        }
        return text;
    }

    //资源上级下拉列表
    $ace.get("../index.ashx?ctrl=resource&action=GetTreeSelectJson", function (result) {
        me.Menus = result;
    })
}
function vmExtend() {
    var me = this;

    me.Init = function () {
        me.LoadModels();
    }

    me.LoadModels = function (pageIndex) {
        me.DataTable.SelectedModel(null);
        var params = me.SearchModel();
        params.pageIndex = pageIndex || 1;
        params.pageSize = 15;

        $ace.get("../index.ashx?ctrl=resource&action=showAll", params, function (result) {
            me.DataTable.SetPagedData(result);
        }
      );
    }
}

function Dialog(vm) {
    var me = this;
    DialogBase.call(me);
    me.OnOpen = function () {
        var model = me.EditModel();
        if (model) {
            var dataModel = model.Data;
            var bindModel = $ko.toJS(dataModel);
            me.Model(bindModel);
        }
        else {
            me.Model({ IsEnabled: true });
        }
    }
    //点击保存
    me.OnSave = function () {
        //实体
        var model = me.Model();
        if (!$('#form1').formValid()) {
            return false;
        }
        //提交表单数据
        $ace.post("../index.ashx?ctrl=resource&action=SubmitForm", model, function (result) {
            $ace.msg(result.Msg);
            me.Close();
            vm.LoadModels();
        });
    }
}

//详情页对话框设置
function DetailDialog() {
    var me = this;
    DialogBase.call(me);

    me.OnOpen = function () {
        //将“保存”按钮隐藏
        me.ShowSaveButton(false);
        var model = me.EditModel();
        if (model) {
            var dataModel = model.Data;
            var bindModel = $ko.toJS(dataModel);
            me.Model(bindModel);
        }
        else {
            me.EditModel(null);
            me.Model({});
        }
    }
}


function expandChildren(ele) {

    $ele = $(ele);

    var $tr = $ele.parent().parent();
    var id = $tr.attr("id");

    var selector = "tr[parent-id='" + id + "']";
    var $children = $tr.siblings(selector);

    if ($ele.hasClass("glyphicon-triangle-bottom")) {
        $children.hide();
        $ele.removeClass("glyphicon-triangle-bottom");
        $ele.addClass("glyphicon-triangle-right");
    }
    else {
        $children.show();
        $ele.removeClass("glyphicon-triangle-right");
        $ele.addClass("glyphicon-triangle-bottom");
    }

    for (var i = 0; i < $children.length; i++) {
        var $child = $($children[i]);

        var $iconNodes = $child.find(".glyphicon");
        if ($iconNodes.length > 0) {
            expandChildren($iconNodes[0]);
        }
    }
}
function appendRetract(level) {
    var s = "";
    for (var i = 0; i < level * 4; i++) {
        s += "&nbsp;";
    }
    return s;
}
