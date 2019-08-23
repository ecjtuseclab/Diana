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

    me.DeleteUrl = "../index.ashx?ctrl=role&action=DeleteForm";
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

        $ace.get("../index.ashx?ctrl=role&action=showAll", params, function (result) {
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
        initControl();
        //选中的用户ID
        var roleId = me.Model().id;
        //资源权限树的加载
        $("#resourcepermissionTree").html(null);
        $("#resourcepermissionTree").treeview({
            height: 256,
            showcheck: true,
            url: "../index.ashx?ctrl=role&action=GetPermissionTree",
            param: { roleId: roleId }
        });
        //动作权限树的加载
        $("#actionpermissionTree").html(null);
        $("#actionpermissionTree").treeview({
            height: 256,
            showcheck: true,
            url: "../index.ashx?ctrl=role&action=ActionAuthorityTree",
            param: { roleId: roleId },
        });
    }
    //点击保存
    me.OnSave = function () {
        //实体
        var model = me.Model();
        if (!$('#form1').formValid()) {
            return false;
        }
        //资源树的权限
        var resourcepermissionIdArr = $("#resourcepermissionTree").getCheckedNodes();
        var resourcepermissionIds = "";
        var c = "";
        for (var i = 0; i < resourcepermissionIdArr.length; i++) {
            resourcepermissionIds += c + resourcepermissionIdArr[i];
            c = ",";
        }
        //动作树的权限
        var actionpermissionArr = $("#actionpermissionTree").getCheckedNodes();
        var actionpermissionIds = "";
        var c = "";
        for (var i = 0; i < actionpermissionArr.length; i++) {
            actionpermissionIds += c + actionpermissionArr[i];
            c = ",";
        }
        model.resourcepermissionIds = resourcepermissionIds;
        model.actionpermissionIds = actionpermissionIds;
        //提交表单数据
        $ace.post("../index.ashx?ctrl=role&action=SubmitForm", model, function (result) {
            $ace.msg(result.Msg);
            me.Close();
            vm.LoadModels();
        });
    }
    //控制上一步，下一步，按钮的显隐性
    function initControl() {
        var w = $('#wizard').wizard();
        w.on('change', function (e, data) {
            var $finish = $("#btn_finish");
            var $next = $("#btn_next");
            if (data.direction == "next") {
                switch (data.step) {
                    case 2://第二步之后将完成按钮显示，下一步隐藏
                        if (!$('#form1').formValid()) {
                            return false;
                        }
                        $finish.show();
                        $next.hide();
                        break;
                    default:
                        break;
                }
            }
            else {
                $finish.hide();
                $next.show();
            }
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
