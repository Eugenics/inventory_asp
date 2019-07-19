var _postNode;

$(function () {
    $('#treeview').jstree({
        'core': {
            'check_callback': true,
            'data': {
                'url': '/Home/Nodes',
                'data': function (node) {
                    return { 'id': node.id };
                }
            }
        }
    });
    $('#treeview').on('changed.jstree', function (e, data) {
        var i, j, r = [];
        var selectedItems = [];
        for (i = 0, j = data.selected.length; i < j; i++) {
            r.push(data.instance.get_node(data.selected));

            //Fetch the Id.
            var id = data.selected[i];
            //Remove the ParentId.
            if (id.indexOf('-') != -1) {
                id = id.split("-")[1];
            }
            //Add the Node to the JSON Array.
            selectedItems.push({
                text: data.instance.get_node(data.selected[i]).text,
                id: id,
                parent: data.node.parents[0]
            });
        }
        var header = document.getElementById("divTab");
        var tabs = header.getElementsByClassName("tab-pane");
        _postNode = selectedItems;

        for (var i = 0; i < tabs.length; i++) {
            var x = tabs[i].className.split(" ");
            for (var j = 0; j < x.length; j++) {
                if (x[j] == "active") {
                    if (tabs[i].id == "hard") {
                        getChecked(selectedItems);
                        break;
                    }
                    else { getCheckedSoft(selectedItems); }
                }
            }
        }
    }).jstree();
});

function getChecked(selectedItems) {
    $.ajax({
        url: '/Home/Index',
        type: 'POST',
        dataType: 'html',
        data: JSON.stringify(selectedItems[0]),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#hard').html(data);
            //$('#soft').html(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }
    })
}

function getCheckedSoft(selectedItems) {
    $.ajax({
        url: '/Home/Index',
        type: 'POST',
        dataType: 'html',
        data: JSON.stringify(selectedItems[0]),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            //$('#hard').html(data);
            $('#soft').html(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }
    })
}