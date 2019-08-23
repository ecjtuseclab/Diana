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

    me.DeleteUrl = "../index.ashx?ctrl=user&action=DeleteForm";
    //me.DataTable = new DataTableBase(me);
    me.Dialog = new Dialog(me);        

    me.OpenRevisePasswordDialog = function () {
        var model = me.DataTable.SelectedModel()
        $ace.get("../index.ashx?ctrl=user&action=ResetPassword", model, function (result) {
            
        });
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

        $ace.get("../index.ashx?ctrl=user&action=showAll", params, function (result) {
            me.DataTable.SetPagedData(result);
        });
    }
    ///获取Content缩略字符
    //me.getFuzzyContent = function (content) {
    //    if (content == "" || content == undefined || content == null) {
    //        return content;
    //    }
    //    return (content.length > 10 ? content.substring(0, 9) + '....' : content);
    //}
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
        var userId = me.Model().id;
        //角色树的加载
        $("#permissionTree").html(null);
        $("#permissionTree").treeview({
            height: 256,
            showcheck: true,
            url: "../index.ashx?ctrl=user&action=GetPermissionTree",
            //url: "/SystemManage/User/GetPermissionTree",
            param: { userId: userId }
            });
        //组树的加载
        $("#permissionGroupTree").html(null);
        $("#permissionGroupTree").treeview({
            height: 256,
            showcheck: true,
            url: "../index.ashx?ctrl=user&action=GetPermissionGroupTree",
            param: { userId: userId },
        });
    }        
    //点击保存
    me.OnSave = function () {
        //实体
        var model = me.Model();
        if (!$('#form1').formValid()) {
            return false;
        }
        //角色树的权限
        var permissionIdArr = $("#permissionTree").getCheckedNodes();
        var permissionIds = "";
        var c = "";
        for (var i = 0; i < permissionIdArr.length; i++) {
            permissionIds += c + permissionIdArr[i];
            c = ",";
        }
        //组树的权限
        var permissionIdGroupArr = $("#permissionGroupTree").getCheckedNodes();
        var permissionGroupIds = "";
        var c = "";
        for (var i = 0; i < permissionIdGroupArr.length; i++) {
            permissionGroupIds += c + permissionIdGroupArr[i];
            c = ",";
        }
        //角色树的权限
        model.rolelistIds = permissionIds;
        model.grouplistIds = permissionGroupIds;
        //新增或修改保存
        $ace.post("../index.ashx?ctrl=user&action=SubmitForm", model, function (result) {
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


function getFuzzyContent(content) {
    if (content == "" || content == undefined || content == null) {
        return content;
    }
    return (content.length > 10 ? content.substring(0, 9) + '....' : content);
}