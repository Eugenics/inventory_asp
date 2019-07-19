$(document).ready(
    function () {        
        var region_id = $('#WhardRegionId');            


        region_id.on('change', function (e) {
            if (region_id.val().length > 0) {
                update_office_by_region(region_id, 0);
                update_mol_by_region(region_id, 0);
            } else {
                $('#WhardOfficeId').empty();
                $('#WhardMolEmployeeId').empty();
            }
        })
        
    })

function update_office_by_region(region_id) {
    $.ajax({
        url: window.location.origin + '/api/officesApi/' + region_id.val(),
        data: {},
        success: function (response) {
            var new_options = response;
            $('#WhardOfficeId').empty();
            $.each(new_options, function (key, value) {
                $('#WhardOfficeId').append($('<option>', {
                    value: value.Value
                }).text(value.Text));                
            });    
        }
    });
}

function update_mol_by_region(region_id) {
    $.ajax({
        url: window.location.origin + '/api/molApi/' + region_id.val(),
        data: {},
        success: function (response) {
            var new_options = response;
            $('#WhardMolEmployeeId').empty();
            $.each(new_options, function (key, value) {
                $('#WhardMolEmployeeId').append($('<option>', {
                    value: value.Value
                }).text(value.Text));
            });
        }
    });
}