$(function () {
    $.ajax({
        type: 'GET',
        url: '/WorkZone/GetAllFactory',
        dataType: 'Json',
        success: function (data) {
            $.each(data, function (index, value) {
                $('#FactoryId').append('<option value="' + value.FactoryId + '">' + value.Name + '</option');
            });
        }
    });//ajax

    $('#FactoryId').change(function () {
        $('#MachineId').empty();
        $.ajax({
            type: 'POST',
            url: '/WorkZone/GetMachineByIdFactory',
            dataType: 'Json',
            data: { factoryId: $('#FactoryId').val() },
            success: function (data) {
                //$('#dropdownMachine').html(' ');
                $('#MachineId').append('<option value="">--Select machine--</option>');
                $.each(data, function (index, value) {
                    $('#MachineId').append('<option value="' + value.MachineId + '">' + value.Name + '</option>');
                });
            }

        });

    });//event change DropdownList factory
});