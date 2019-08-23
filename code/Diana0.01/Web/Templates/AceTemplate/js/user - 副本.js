//1.定义ViewModel
      
var EmpViewModel = function () {
    //Name=ko.observable("Lilei");
    //Profession= "软件工程师";
          
    var me = this;
    // me.EmployeeID = ko.observable("");
    me.username = ko.observable("");
    me.password = ko.observable("");
    //me.DeptName = ko.observable("");
    //me.Address = ko.observable("");
    //var EmpData = {
    //    EmployeeID: me.EmployeeID,
    //    EmployeeName: me.EmployeeName,
    //    Salary: me.Salary,
    //    DeptName: me.DeptName,
    //    Address: me.Address
    //};

    //  生成一个ObservableArray来存储返回的所有数据
    me.Users = ko.observableArray([]);
    GetUsers(); //通过ajax请求返回所有数据
    ////保存数据
    //me.save = function () {
    //    //Ajax 提交到webapi保存数据
    //    $.ajax({
    //        type: "POST",
    //        url: "/api/EmployeeApi",
    //        data: ko.toJSON(EmpData),
    //        contentType: "application/json",
    //        success: function (data) {
    //            alert("记录保存成功");
    //            me.EmployeeID(data.EmployeeID);
    //            window.location.reload();
    //            GetEmployees();
    //        },
    //        error: function () {
    //            alert("提交失败");
    //        }
    //    });
    //};

    //me.update = function () {
    //    var url = "/api/EmployeeApi/" + me.EmployeeID();
    //    $.ajax({
    //        type: "PUT",
    //        url: url,
    //        data: ko.toJSON(EmpData),
    //        contentType: "application/json",
    //        success: function (data) {
    //            alert("修改成功");
    //            GetEmployees();
    //        },
    //        error: function (error) {
    //            alert(error.status + "<!----!>" + error.statusText);
    //        }
    //    });
    //};

    ////删除操作
    //me.deleterec = function (employee) {
    //    $.ajax({
    //        type: "DELETE",
    //        url: "/api/EmployeeApi/" + employee.EmployeeID,
    //        success: function (data) {
    //            alert("Record Deleted Successfully");
    //            GetEmployees();//Refresh the Table
    //        },
    //        error: function (error) {
    //            alert(error.status + "<--and--> " + error.statusText);
    //        }
    //    });
    //};

    //获取所有Employee
    function GetUsers() {
        //var data = [{ EmployeeName: "ss6sss6s", ss: "dd", Salary: 8000 }, { EmployeeName: "sss", ss: "dd", Salary: 8000 }];

        //me.Employees(data);
        //Ajax 获取所有Employee记录
        $.ajax({
            type: "GET",
            url: "../index.ashx?ctrl=user&action=showAll",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                me.Users(data);
            },
            error: function (error) {
                alert(error.status + "<--and--> " + error.statusText);
            }
        });
    };
    ////点击右侧列表某一行左侧编辑赋值
    //me.getselectedemployee = function (employee) {
    //    me.EmployeeID(employee.EmployeeID),
    //    me.EmployeeName(employee.EmployeeName),
    //    me.Salary(employee.Salary),
    //    me.DeptName(employee.DeptName),
    //    me.Address(employee.Address)
    //};
};
//2.激活绑定
ko.applyBindings(EmpViewModel);