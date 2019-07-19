$(document).ready(
    function () {     
        var region_id = $('#OfficeHouses_HousesRegion_RegionId');        

        region_id.on('change', function (e) {
            if (region_id.val().length > 0) {
                update_house_by_region(region_id, 0);                
            } else {
                $('#OfficeHousesId').empty();                
            }
        })
        
    })

function update_house_by_region(region_id) {
    $.ajax({
        url: window.location.origin + '/api/housesApi/' + region_id.val(),
        data: {},
        success: function (response) {
            var new_options = response;
            //console.log(new_options);
            $('#OfficeHousesId').empty();
            $.each(new_options, function (key, value) {
                $('#OfficeHousesId').append($('<option>', {
                    value: value.Value
                }).text(value.Text));                
            });    
        }
    });
}