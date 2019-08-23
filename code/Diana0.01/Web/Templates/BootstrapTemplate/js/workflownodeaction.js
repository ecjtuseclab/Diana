$(function () {
        gridList();
    })
    function gridList() {
        var $gridList = $("#gridList");
        $gridList.dataGrid({
            url: "../index.ashx?ctrl=workflownodeaction&action=showAll",
            height: $(window).height() - 126,
            colModel: [
                { label: "主键", name: "id", hidden: true, key: true },
                {
                    label: '工作流', name: 'wfid', width: 150, align: 'left',
                    formatter: function (cellvalue) {
                        $.ajax({
                            url: "../index.ashx?ctrl=workflownodeaction&action=GetSelectJson",
                            data: { keyValue: cellvalue },
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                cellvalue = data[0].text;
                            }
                        });
                        return cellvalue;
                    }
                },
                { label: '节点动作名称', name: 'nodeactionname', width: 150, align: 'left' },
                {
                    label: '当前节点', name: 'currentnodeid', width: 150, align: 'left',
                    formatter: function (cellvalue) {
                        $.ajax({
                            url: "../index.ashx?ctrl=workflownodeaction&action=GetSelectNodeJson",
                            data: { keyValue: cellvalue },
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                cellvalue = data[0].text;
                            }
                        });
                        return cellvalue;
                    }
                },
                {
                    label: '下一个节点', name: 'nextnodeid', width: 150, align: 'left',
                    formatter: function (cellvalue) {
                        $.ajax({
                            url: "../index.ashx?ctrl=workflownodeaction&action=GetSelectNodeJson",
                            data: { keyValue: cellvalue },
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                cellvalue = data[0].text;
                            }
                        });
                        return cellvalue;
                    }
                },
                {
                    label: '动作状态', name: 'nastatus', width: 150, align: 'left',
                    formatter: function (cellvalue) {
                        return cellvalue == 1 ? "启用" : "未启用";
                    }
                },
                { label: '开始时间', name: 'begintime', width: 150, align: 'left' },
                { label: ' 结束时间 ', name: 'endtime', width: 150, align: 'left' },
                {
                    label: '锁定状态', name: 'nalock', width: 150, align: 'left',
                    formatter: function (cellvalue) {
                        return cellvalue == 1 ? "锁定" : "未锁定";
                    }
                },
                {
                    label: '动作类型', name: 'nodetype', width: 150, align: 'left',
                    formatter: function (cellvalue) {
                        switch (cellvalue) {
                            case 1:
                                cellvalue = "未定义1";
                                break;
                            case 2:
                                cellvalue = "未定义2";
                                break;
                            default:
                                cellvalue = "未定义";
                                break;
                        }
                        return cellvalue;
                    }
                }

            ],
            pager: "#gridPager",
            sortname: 'id asc',
            viewrecords: true
        });
        $("#btn_search").click(function () {
            $gridList.jqGrid('setGridParam', {
                postData: { keyword: $("#txt_keyword").val() },
            }).trigger('reloadGrid');
        });
    }
    function btn_add() {
        $.modalOpen({
            id: "Form",
            title: "新增节点动作",
            url: "../index.ashx?ctrl=workflownodeaction&action=Form",
            width: "700px",
            height: "510px",
            callBack: function (iframeId) {
                top.frames[iframeId].submitForm();
            }
        });
    }
    function btn_edit() {
        var keyValue = $("#gridList").jqGridRowValue().id;
        $.modalOpen({
            id: "Form",
            title: "修改节点动作",
            url: "../index.ashx?ctrl=workflownodeaction&action=Form&keyValue=" + keyValue,
            width: "700px",
            height: "510px",
            callBack: function (iframeId) {
                top.frames[iframeId].submitForm();
            }
        });
    }
    function btn_delete() {
        $.deleteForm({
            url: "../index.ashx?ctrl=workflownodeaction&action=DeleteForm",
            param: { keyValue: $("#gridList").jqGridRowValue().id },
            success: function () {
                $.currentWindow().$("#gridList").trigger("reloadGrid");
            }
        })
    }
    function btn_details() {
        var keyValue = $("#gridList").jqGridRowValue().id;
        $.modalOpen({
            id: "Details",
            title: "查看节点动作",
            url: "../index.ashx?ctrl=workflownodeaction&action=Details&keyValue=" + keyValue,
            width: "700px",
            height: "510px",
            btn: null,
        });
    }
    function btn_copyandpaste() {
        $.deleteForm({
            url: "../index.ashx?ctrl=workflownodeaction&action=copyAndPasteForm",
            param: { keyValue: $("#gridList").jqGridRowValue().id },
            success: function () {
                $.currentWindow().$("#gridList").trigger("reloadGrid");
            }
        })
    }