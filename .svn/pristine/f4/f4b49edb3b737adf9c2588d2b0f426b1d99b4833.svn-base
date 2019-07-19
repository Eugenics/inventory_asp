$(document).ready(
    function () {     
        var region_id = $('#DepartmentRegionId');        

        region_id.on('change', function (e) {
            if (region_id.val().length > 0) {
                update_department_by_region(region_id, 0);                
            } else {
                $('#DepartmentParentId').empty();                
            }
        })
        
    })

function update_department_by_region(region_id) {
    $.ajax({
        url: window.location.origin + '/api/departmentApi/' + region_id.val(),
        data: {},
        success: function (response) {
            var new_options = response;
            //console.log(new_options);
            $('#DepartmentParentId').empty();
            $.each(new_options, function (key, value) {
                $('#DepartmentParentId').append($('<option>', {
                    value: value.Value
                }).text(value.Text));                
            });    
        }
    });
}