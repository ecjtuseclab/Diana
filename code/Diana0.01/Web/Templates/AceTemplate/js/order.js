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

    me.DeleteUrl = "../index.ashx?ctrl=order&action=DeleteForm";
    me.Dialog = new Dialog(me);

    //详情页
    me.DetailDialog = new DetailDialog(me);
    me.Detail = function () {
        me.DetailDialog.Open(me.DataTable.SelectedModel(), "详情");
    }

    me.ReportFormDialog = new ReportFormDialog(me);
    me.ReportForm = function () {
        me.ReportFormDialog.Open(null,"报表");
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

        $ace.get("../index.ashx?ctrl=order&action=showAll", params, function (result) {
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
        $ace.post("../index.ashx?ctrl=order&action=SubmitForm", model, function (result) {
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
//报表对话框
function ReportFormDialog() {
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

function ReportForm() {
    var text = $("#sort").val();
    var value = $("#value").val();
    var xdata = new Array();
    var ydata = new Array();
    $("#order tbody tr").each(function () {
        xdata.push($(this).find("td").eq(text).text());
        ydata.push($(this).find("td").eq(value).text())
    })
    console.log(xdata);
    // 基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('reportform'));

    // 指定图表的配置项和数据
    var option = {
        title: {
            text: '销售报表'
        },
        tooltip: {},
        legend: {
            data: ['销售额']
        },
        xAxis: {
            data: xdata
        },
        yAxis: {},
        series: [{
            name: '销售额',
            type: 'bar',
            data: ydata
        }]
    };
    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
}
    


