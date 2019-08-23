
var k = $.request("keyValue");
var arraylist;

$(function () {

    $.ajax({
        url: "../index.ashx?ctrl=article&action=GetFormJson",
        data: { keyValue: k },
        dataType: "json",
        async: false,
        success: function (data) {
            arraylist = data;
            //$("#form1").formSerialize(data);
        }
    });

    var vm = new ViewModel();
    ko.applyBindings(vm);
});

function ViewModel() {
    var me = this;
    me.model = ko.observable(arraylist);


    me.save = function () {

        var model = me.model();
        var content = UE.getEditor('content').getContent();
        delete (model.content);
        model.content = content;
        if (!$('#form1').formValid()) {
            return false;
        }
        //提交表单数据
        $ace.post("../index.ashx?ctrl=article&action=SubmitForm", model, function (result) {
            //console.log("xxxx");
        });
    }

}
