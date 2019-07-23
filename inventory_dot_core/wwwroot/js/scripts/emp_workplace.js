$(document).ready(
    function () {
        var searchString = $('#whardSearchId');
        var regionId = $('#eRegionId');
        var officeId = $('#eOfficeId');


        searchString.on('input', function (e) {
            $('#RelheWhardId').empty();
            console.log($('#whardSearchId').val());

            if ((searchString.val()).length == 0)
                update_search(regionId.val(), officeId.val(), "null");
            else
                update_search(regionId.val(), officeId.val(), searchString.val());



            /*if (region_id.val().length > 0) {
                update_office_by_region(region_id, 0);
                update_position_by_region(region_id, 0);
            } else {
                $('#EmployeeOfficeId').empty();
                $('#EmployeePositionId').empty();
            }
            */
        })

    })



function update_search(region_id, office_id, searchString) {
    $.ajax({
        url: window.location.origin + '/api/whardSeachApi/' + region_id + '/' + office_id + '/' + searchString,
        data: {},
        success: function (response) {
            var new_options = response;
            //console.log(new_options);
            $('#RelheWhardId').empty();
            $.each(new_options, function (key, value) {
                $('#RelheWhardId').append($('<option>', {
                    value: value.Value
                }).text(value.Text));                
            });            
        }
    });
}
