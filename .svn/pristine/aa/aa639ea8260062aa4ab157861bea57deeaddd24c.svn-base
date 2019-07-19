$(document).ready(
    function () {     
        var region_id = $('#RoeOffice_OfficeHouses_HousesRegionId');
        var house_id = $('#RoeOffice_OfficeHousesId');

        region_id.on('change', function (e) {
            if (region_id.val().length > 0) {
                update_house_by_region(region_id);
                update_office_by_region(house_id);
                update_employee_by_region(region_id);
            } else {
                $('#RoeOffice_OfficeHousesId').empty();
                $('#RoeOfficeId').empty();
                $('#RoeEmployeeId').empty();
            }
        })

        house_id.on('change', function (e) {
            if (house_id.val().length > 0) {
                update_office_by_region(house_id);
            }
            else {
                $('#RoeOfficeId').empty();
            }
        })
        
    })

function update_house_by_region(region_id) {
    $.ajax({
        url: window.location.origin + '/api/housesApi/' + region_id.val(),
        data: {},
        success: function (response) {
            var new_options = response;
            $('#RoeOffice_OfficeHousesId').empty();
            $.each(new_options, function (key, value) {
                $('#RoeOffice_OfficeHousesId').append($('<option>', {
                    value: value.Value
                }).text(value.Text));                
            });    
        }
    });
}

function update_office_by_region(house_id) {
    $.ajax({
        url: window.location.origin + '/api/officesApi/GetJSONOfficesByHouse/' + house_id.val(),
        data: {},
        success: function (response) {
            var new_options = response;
            $('#RoeOfficeId').empty();
            $.each(new_options, function (key, value) {
                $('#RoeOfficeId').append($('<option>', {
                    value: value.Value
                }).text(value.Text));
            });
        }
    });
}

function update_employee_by_region(region_id) {
    $.ajax({
        url: window.location.origin + '/api/employeesApi/' + region_id.val(),
        data: {},
        success: function (response) {
            var new_options = response;
            $('#RoeEmployeeId').empty();
            $.each(new_options, function (key, value) {
                $('#RoeEmployeeId').append($('<option>', {
                    value: value.Value
                }).text(value.Text));
            });
        }
    });
}