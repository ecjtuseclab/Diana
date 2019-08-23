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

    me.DeleteUrl = "../index.ashx?ctrl=article&action=DeleteForm";
    me.Dialog = new Dialog(me);

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

        $ace.get("../index.ashx?ctrl=article&action=showAll", params, function (result) {
            me.DataTable.SetPagedData(result);
        }
      );
    }
}
function Dialog(vm) {
    var me = this;
    DialogBase.call(me);
    me.OnOpen = function () {

        me.IsShow(false);
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

        $.ajax({
            url: "../index.ashx?ctrl=article&action=Form",
            async: false,
            success: function (data) {
                location.href = '../index.ashx?ctrl=article&action=Form&keyValue=' + actionId;
            }
        });

        //var params=null;
        //param.keyValue = actionId;
        
        //$ace.get("../index.ashx?ctrl=article&action=GetFormJson", params, function (result) {
        //    me.DataTable.SetPagedData(result);
        //});
    }
    //点击保存
    me.OnSave = function () {
        //实体
        var model = me.Model();
        if (!$('#form1').formValid()) {
            return false;
        }
        //提交表单数据
        $ace.post("../index.ashx?ctrl=article&action=SubmitForm", model, function (result) {
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
        //选中的动作ID
        var actionId = me.Model().id;
        $.ajax({
            url: "../index.ashx?ctrl=article&action=Details",
            async: false,
            success: function (data) {
                location.href = '../index.ashx?ctrl=article&action=Details&keyValue=' + actionId;
            }
        });
    }
}
