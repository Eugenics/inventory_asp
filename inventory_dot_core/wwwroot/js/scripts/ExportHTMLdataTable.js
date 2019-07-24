$(document).ready(
    function () {
        $('#exportXLSXBtn').on('click', function (e) {
            exportData('xlsx');
        })


        $('#exportCSVBtn').on('click', function (e) {
            exportData('csv');
        })

    })


function exportData(type, fn, dl) {
	var elt = document.getElementById('dataTable');
	var wb = XLSX.utils.table_to_book(elt, {sheet:"Sheet JS"});
	return dl ?
        XLSX.write(wb, {bookType:type, bookSST:true, type: 'base64'}) :
        XLSX.writeFile(wb, fn || ('hardWare.' + (type || 'xlsx')));
}

    