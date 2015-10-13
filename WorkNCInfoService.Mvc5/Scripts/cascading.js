$(function () {
    $.ajax({
        type: 'GET',
        url: '/WorkZone/GetAllFactory',
        dataType: 'Json',
        success: function (data) {
            $.each(data, function (index, value) {
                $('#dropdownFactory').append('<option value="' + value.FactoryId + '">' + value.Name + '</option');
            });
        }
    });//ajax

    $('#dropdownFactory').change(function () {
        //$('#dropdownMachine').empty();
        $.ajax({
            type: 'POST',
            url: '/WorkZone/GetMachineByIdFactory',
            dataType: 'Json',
            data: { factoryId: $('#dropdownFactory').val() },
            success: function (data) {
                $('#dropdownMachine').append('<option value="">--Select machine--</option>');
                $.each(data, function (index, value) {
                    $('#dropdownMachine').append('<option value="' + value.id + '">' + value.Name + '</option>');
                });
            }

        });

    });//event change DropdownList factory
});