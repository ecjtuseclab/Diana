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

    me.DeleteUrl = "../index.ashx?ctrl=workflownodeaction&action=DeleteForm";
    me.Dialog = new Dialog(me);

    //获取工作流名称
    var wfList; //工作流键值关系
    $ace.get("../index.ashx?ctrl=workflownodeaction&action=GetWorkflowName", function (result) {
        wfList = result;
    })
    me.getWorkflowName = function (value) {
        var text = "";
        var len = wfList.length;
        for (var i = 0; i < len; i++) {
            if (wfList[i]["Value"] == value) {
                text = wfList[i]["Text"];
                break;
            }
        }
        return text;
    }

    //获取工作流节点名称
    var wfnodeList; //工作流键值关系
    $ace.get("../index.ashx?ctrl=workflownodeaction&action=GetWorkflownodeName", function (result) {
        wfnodeList = result;
    })
    me.getWorkflownodeName = function (value) {
        var text = "";
        var len = wfnodeList.length;
        for (var i = 0; i < len; i++) {
            if (wfnodeList[i]["Value"] == value) {
                text = wfnodeList[i]["Text"];
                break;
            }
        }
        return text;
    }

    //获取动作名称
    var actionList; //工作流键值关系
    $ace.get("../index.ashx?ctrl=workflownodeaction&action=GetActionName", function (result) {
        actionList = result;
    })
    me.getActionName = function (value) {
        var text = "";
        var len = actionList.length;
        for (var i = 0; i < len; i++) {
            if (actionList[i]["Value"] == value) {
                text = actionList[i]["Text"];
                break;
            }
        }
        return text;
    }


    //详情页
    me.DetailDialog = new DetailDialog(me);
    me.Detail = function () {
        me.DetailDialog.Open(me.DataTable.SelectedModel(), "详情");
    }
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

        $ace.get("../index.ashx?ctrl=workflownodeaction&action=showAll", params, function (result) {
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
            var bindModel = $ko.toJS(model);
            me.Model(bindModel);
        }
        else {
            me.Model({ IsEnabled: true });
        }
        //选中的动作ID
        var actionId = me.Model().id;
    }
    //点击保存
    me.OnSave = function () {
        //实体
        var model = me.Model();
        if (!$('#form1').formValid()) {
            return false;
        }
        //提交表单数据
        $ace.post("../index.ashx?ctrl=workflownodeaction&action=SubmitForm", model, function (result) {
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
            var bindModel = $ko.toJS(model);
            me.Model(bindModel);
        }
        else {
            me.Model({});
        }
    }
}
