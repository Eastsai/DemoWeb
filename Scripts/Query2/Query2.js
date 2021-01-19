$().ready(function () {
    
    $('#queryBtn').click(function () {         
        QueryListData()
    })

    $('#editBtn').click(function () {     
        DisplayLoading()
           
        var Sno = $('#Sno').val()
        var Name = $('#Name').val()
        var Age = $('#Age').val()
        var Account = $('#Account').val()
        var Password = $('#Password').val()
        var Address = $('#Address').val()
        var City = $('#City').val()
        var Mail = $('#Mail').val()

        var datas = {
            Sno:Sno,
            Name:Name,
            Age : Age,
            Account : Account,
            Password : Password,
            Address : Address,
            City : City,
            Mail : Mail
        }                
                
        $.ajax({
            url: "/Query2/Query2Edit",            
            type: "post",            
            data: datas,                     
            success: function (result) {
                HideLoading();
                if (result == "OK") {
                    alert('更新成功')
                } else {
                    alert('更新失敗')
                }                
            },            
            error: function (XMLHttpRequest, textstatus) {
                HideLoading()
                alert("系統發生問題，請稍候再試")
            }
        })
    })    

    function DisplayLoading() {
        //use jquery plugs : blockUI
        //use custom css : #Loading div in DemoWebHelper                
        $.blockUI({
            message: $('#Loading'),
            css: { backgroundColor: 'none', color: 'none', border: 'none' }
        })
    }

    function HideLoading() {
        $.unblockUI()
    }
})

//查詢listdata，並顯示partial view
function QueryListData() {
    data = {
        Sno: $('#Sno').val(),
        Account: $('#Account').val(),
        City: $('#City').val(),
        RM: Math.floor(Math.random() * 100 + 1)
    };
    $.ajax({
        url: "/Query2/Query2List",        
        type : "get",        
        data: data,        
        success: function (result) {            
            if (result == null) {
                alert("系統發生問題，請稍候再試");
            } else {                                
                $('#_Query2ListDiv').html(result)
                $('#_Query2ListDiv').show()                
            }            
        }, error: function () {
            alert("系統發生問題，請稍候再試")
        }
    })
}

function delCust(Sno) {
    if (confirm("請確定是否刪除資料")) {
        $.ajax({
            url: "/Query2/Query2Delete",
            method: "POST",            
            data: {Sno:Sno},
            success: function (result) {
                if (result == "OK") {
                    alert("刪除成功")
                } else {
                    alert("刪除失敗")
                }               
                QueryListData()
            }
        })     
    } else {
        return false;
    }
}



