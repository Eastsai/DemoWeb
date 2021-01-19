$().ready(function () {   
    $('#loginbtn').click(function () {
        var act = $('#act').val()
        var pwd = $('#pwd').val() 

        if (act.length == 0 || pwd.length == 0) {
            alert("帳號密碼輸入不完全")
        } else {
            $.ajax({
                url: "/Home/Login",
                type: "post",
                data: {
                    account: act,
                    password: pwd
                },
                success: function (result) {
                    if (result == "OK") {
                        window.location.href = "/Home/Index1"
                    } else {
                        alert(result)
                    }
                },
                error: function () {
                    alert("系統發生問題，請稍候再試")
                }
            })
        }
    })
});




