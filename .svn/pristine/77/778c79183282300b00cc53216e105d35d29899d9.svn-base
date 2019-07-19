$(document).ready(
    function () {     
        var region_id = $('#PositionDepartment_DepartmentRegion_RegionId');        

        region_id.on('change', function (e) {
            if (region_id.val().length > 0) {
                update_department_by_region(region_id, 0);                
            } else {
                $('#PositionDepartmentId').empty();                
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
            $('#PositionDepartmentId').empty();
            $.each(new_options, function (key, value) {
                $('#PositionDepartmentId').append($('<option>', {
                    value: value.Value
                }).text(value.Text));                
            });    
        }
    });
}