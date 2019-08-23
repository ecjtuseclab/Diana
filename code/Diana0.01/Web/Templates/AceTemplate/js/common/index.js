function logout() { 
    $.ajax({
        url: '../index.ashx?ctrl=login&action=logout',
        //dataType: "text",
        //data: user,
        type: "POST",
        success: function (result) {
            if (result.success) {
                window.location.href = "index.ashx";
            }
        },
        beforeSend: function () {
            //$('#loginBtn').css("disable","none");   //点击登陆后先禁用登录按钮，防止连击
        },
        complete: function () {
            //$('#loginBtn').css("disable", "");
        }
    });
}