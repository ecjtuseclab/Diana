var type = $.request("type");
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
    me.selectrow = function (model, event)
    {
        //console.log(model);
        var m = $ko.toJS(model);
        window.location.href = '../index.ashx?ctrl=help&action=Details&articleid=' + m.id;
       
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
        params.type = type;

        $ace.get("../index.ashx?ctrl=help&action=showTypeAll", params, function (result) {
            me.DataTable.SetPagedData(result);
        }
      );
    }
    
}
